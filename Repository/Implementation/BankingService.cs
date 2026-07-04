using BankAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using SimpleBankingApi.Data;
using SimpleBankingApi.DTOs.Requests;
using SimpleBankingApi.DTOs.Responses;
using SimpleBankingApi.Model;
using SimpleBankingApi.Repository.Interface;
using SimpleBankingApi.Utilities;

namespace SimpleBankingApi.Repository.Implementation;

public class BankingService: IBankingService
{
     private readonly BankingDbContext _context;
        private readonly ILogger<BankingService> _logger;
        private readonly IEmailService _emailService;
    
        public BankingService(BankingDbContext context, ILogger<BankingService> logger, IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
        }
        
        
        public async Task<ApiResponse<AccountResponse>> CreateAccountAsync(CreateAccountRequest request)
        {
            try
            {
                var checkEmail = await _context.Accounts.AnyAsync(x => x.Email == request.Email);
                if (checkEmail)
                {
                    _logger.LogError($"Email already exists for {request.Email}");
                    return ApiResponse<AccountResponse>.FailureResponse("Email already exists");
                }
    
                var newAcct = new Account()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    AccountNumber = AccountNumberGenerator.GenerateAcct(),
                    Balance = 0.00m,
                    CreatedAt = DateTime.Now
                };
                
                _context.Accounts.Add(newAcct);
                await _context.SaveChangesAsync();
                
               _logger.LogInformation($"Successfully created account new account {newAcct.AccountNumber} for customer {request.FirstName} {request.LastName}");
               
             await  _emailService.SendWelcomeEmailAsync(newAcct.Email, $"{newAcct.FirstName} {newAcct.LastName}", newAcct.AccountNumber, newAcct.Balance);
               
               var response = new AccountResponse()
               {
                   AccountNumber = newAcct.AccountNumber,
                   CustomerName = $"{newAcct.FirstName} {newAcct.LastName}",
                   Balance = newAcct.Balance,
                   Email = newAcct.Email,
                   CreatedAt = newAcct.CreatedAt,
                   IsDeleted = false
               };
               
               return ApiResponse<AccountResponse>.SuccessResponse(response);
               
    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured during account creation for customer {request.FirstName} {request.LastName}");
                return ApiResponse<AccountResponse>.FailureResponse("Error occured during account creation");
            }
        }
    
        public async Task<ApiResponse<AccountResponse>> GetAccountAsync(string accountNumber)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            
                // Validation: Reject if not found or if soft-deleted
                if (account == null || account.IsDeleted)
                {
                    return ApiResponse<AccountResponse>.FailureResponse("Account not found or has been deactivated.");
                }

                var response = new AccountResponse()
                {
                    AccountNumber = account.AccountNumber,
                    CustomerName = $"{account.FirstName} {account.LastName}",
                    Balance = account.Balance,
                    Email = account.Email,
                    CreatedAt = account.CreatedAt
                };

                return ApiResponse<AccountResponse>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving account {accountNumber}");
                return ApiResponse<AccountResponse>.FailureResponse("Error retrieving account information");
            }
        }
    
        public async Task<ApiResponse<AccountResponse>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest request)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            
                // Validation: Business rule blocking actions on soft-deleted accounts
                if (account == null || account.IsDeleted)
                {
                    return ApiResponse<AccountResponse>.FailureResponse("Account not found or is deactivated.");
                }

                account.FirstName = request.FirstName ?? account.FirstName;
                account.LastName = request.LastName ?? account.LastName;

                await _context.SaveChangesAsync();

                var response = new AccountResponse()
                {
                    AccountNumber = account.AccountNumber,
                    CustomerName = $"{account.FirstName} {account.LastName}",
                    Balance = account.Balance,
                    Email = account.Email,
                    CreatedAt = account.CreatedAt
                };

                return ApiResponse<AccountResponse>.SuccessResponse(response, "Account updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating account {accountNumber}");
                return ApiResponse<AccountResponse>.FailureResponse("Error updating account details");
            }
        }
    
        public async Task<ApiResponse<bool>> DeleteAccountAsync(string accountNumber)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
                if (account == null || account.IsDeleted)
                {
                    return ApiResponse<bool>.FailureResponse("Account not found or already deactivated.");
                }

                // SOFT DELETE FLAG
                account.IsDeleted = true;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Account {accountNumber} has been soft-deleted.");

                // TRIGGER DEACTIVATION EMAIL
                // Note: If you haven't made a custom method for this yet, you can adapt your email interface 
                // or use a standard text body depending on your current IEmailService structure.
                await _emailService.SendDeactivationEmailAsync(account.Email, $"{account.FirstName} {account.LastName}", account.AccountNumber);

                return ApiResponse<bool>.SuccessResponse(true, "Account has been deactivated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deactivating account {accountNumber}");
                return ApiResponse<bool>.FailureResponse("Error occurred during account deactivation");
            }
        }
    
        public async Task<ApiResponse<IEnumerable<AccountResponse>>> GetAllAccountsAsync()
        {
            try
            {
                // Do not return soft-deleted accounts in regular listings
                var accounts = await _context.Accounts
                    .Where(x => !x.IsDeleted)
                    .Select(acct => new AccountResponse
                    {
                        AccountNumber = acct.AccountNumber,
                        CustomerName = $"{acct.FirstName} {acct.LastName}",
                        Balance = acct.Balance,
                        Email = acct.Email,
                        CreatedAt = acct.CreatedAt
                    }).ToListAsync();

                return ApiResponse<IEnumerable<AccountResponse>>.SuccessResponse(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all accounts");
                return ApiResponse<IEnumerable<AccountResponse>>.FailureResponse("Error fetching accounts");
            }
        }
        
        
        public async Task<ApiResponse<TransferSummary>> TransferFundsAsync(TransferRequest request)
            {
                try
                {
                    if (request.SenderAccountNumber == request.ReceiverAccountNumber)
                    {
                        _logger.LogError($"Transfer failed :Sender and receiver account cannot be the same");
                        return ApiResponse<TransferSummary>.FailureResponse("Sender and receiver account cannot be the same");
                    }
                    
                    var senderAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == request.SenderAccountNumber);
                    var receiverAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == request.ReceiverAccountNumber);
                    
                    if (senderAccount == null || receiverAccount == null)
                    {
                        _logger.LogError($"Transfer failed :Sender or receiver account not found");
                        return ApiResponse<TransferSummary>.FailureResponse("Sender or receiver account not found");
                    }
                    if (senderAccount.Balance < request.Amount)
                    {
                        _logger.LogError($"Transfer failed :Insufficient balance for sender account {request.SenderAccountNumber}");
                        return ApiResponse<TransferSummary>.FailureResponse("Insufficient balance for sender account");
                    }
                    
                    senderAccount.Balance -= request.Amount; // Debit from sender and remove the amount from sender balance
                    receiverAccount.Balance += request.Amount; // Credit to receiver and add the amount to receiver balance
        
                    var reference = TransactionRefGenerator.GenerateRef("TRF");
        
                    var debitTransaction = new Transaction()
                    {
                        AccountNumber = senderAccount.AccountNumber,
                        Amount = request.Amount,
                        Reference = $"D-{reference}",
                        // Description = $"Debit of {request.Amount} from account {senderAccount.AccountNumber}",
                        TransactionType = "Debit",
                        Naration = request.Naration,
                        CreatedAt = DateTime.UtcNow
                    };
                    
        
                    var creditTransaction = new Transaction()
                    {
                        AccountNumber = receiverAccount.AccountNumber,
                        Amount = request.Amount,
                        Reference = $"C-{reference}",
                        // Description = $"Credit of {request.Amount} to account {receiverAccount.AccountNumber}",
                        TransactionType = "Credit",
                        Naration = request.Naration,
                        CreatedAt = DateTime.UtcNow
                    };
                 
                    _context.Transactions.AddRange(debitTransaction, creditTransaction);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Successfully transferred {request.Amount} from account {senderAccount.AccountNumber} to account {receiverAccount.AccountNumber}");
                    
                    /// Email sending will be here
                    
                    //Send Debit Alert Email To sender
                    
                  await  _emailService.SendTransferDebitEmailAsync(senderAccount.Email,
                        $"{senderAccount.FirstName} {senderAccount.LastName}",
                        senderAccount.AccountNumber, receiverAccount.AccountNumber,
                        senderAccount.Balance, request.Amount, debitTransaction.Reference);
                    
                  //Send Creit Alert Email To reciever
                  await _emailService.SendTransferCreditEmailAsync(receiverAccount.Email,
                      $" {receiverAccount.FirstName} {receiverAccount.LastName}",
                      senderAccount.AccountNumber, receiverAccount.AccountNumber,
                      receiverAccount.Balance, request.Amount,creditTransaction.Reference);
                    // Designing the summary for the sender
                    var summary = new TransferSummary()
                    {
                        TransactionReference = debitTransaction.Reference,
                        SenderAccountNumber = senderAccount.AccountNumber,
                        ReceiverAccountNumber = receiverAccount.AccountNumber,
                        Amount = request.Amount,
                        SenderNewBalance = senderAccount.Balance,
                        CreatedAt = DateTime.UtcNow
                    };
                    return ApiResponse<TransferSummary>.SuccessResponse(summary, "Fund transfer processed successfully");
        
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        
            public async Task<ApiResponse<decimal>> DepositAsync(DepositRequest request)
            {
                try
                {
                   var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber);
                    if (account == null)
                    {
                        _logger.LogError($"Account not found for account number {request.AccountNumber}");
                        return ApiResponse<decimal>.FailureResponse("Account not found");
                    }
                    
                    account.Balance += request.Amount; 
                    
                    var reference = TransactionRefGenerator.GenerateRef("DEP");
                    var transaction = new Transaction()
                    {
                        AccountNumber = account.AccountNumber,
                        Amount = request.Amount,
                        Reference = reference,
                        // Description = $"Deposit of {request.Amount} to account {account.AccountNumber}",
                        TransactionType = "Deposit",
                        Naration = request.Naration,
                        CreatedAt = DateTime.Now
                    };
                    _context.Transactions.Add(transaction); 
                    await _context.SaveChangesAsync();
                    
                    _logger.LogInformation($"Successfully deposited {request.Amount} to account {account.AccountNumber}");
                    
                 //   Email sending will be here 
                await _emailService.SendDepositEmailAsync(account.Email, $"{account.FirstName} {account.LastName}", account.AccountNumber, account.Balance, request.Amount, reference);
                    
                    return ApiResponse<decimal>.SuccessResponse(account.Balance,"Deposit processed successfully");
                }
                catch (Exception ex)
                {
                   _logger.LogError(ex, $"Error occured during deposit for account {request.AccountNumber}");
                    return ApiResponse<decimal>.FailureResponse($"Error occured during deposit");
                }
            }
        
            public async Task<ApiResponse<decimal>> WithdrawAsync(WithdrawalRequest request)
            {
                try
                {
                    var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber);
                    if (account == null)
                    {
                        _logger.LogError($"Withdrawal failed :Account not found for account number {request.AccountNumber}");
                        return ApiResponse<decimal>.FailureResponse($"Account number {request.AccountNumber} does not exist");
                    }
                    
                    // Business rule 
                    if (account.Balance < request.Amount)
                    {
                        _logger.LogError($"Withdrawal failed :Insufficient balance for account number {request.AccountNumber}");
                        return ApiResponse<decimal>.FailureResponse($"Insufficient balance for account number {request.AccountNumber}");
                    }
                    
                    account.Balance -= request.Amount; 
                    
                    var reference = TransactionRefGenerator.GenerateRef("WDR");
                    var transaction = new Transaction()
                    {
                        AccountNumber = account.AccountNumber,
                        TransactionType = "Withdrawal",
                        Reference = reference,
                        Naration = "",
                        Amount = request.Amount,
                        // Description = $"Withdrawal of {request.Amount} from account {account.AccountNumber}",
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Transactions.Add(transaction); 
                    await _context.SaveChangesAsync();
                    
                    _logger.LogInformation($"Successfully withdrawn {request.Amount} from account {account.AccountNumber}");
                    
                    // Email sending will be here
                    await _emailService.SendWithdrawalEmailAsync(account.Email, $"{account.FirstName} {account.LastName}", account.AccountNumber, account.Balance, request.Amount, reference);
                    
                    return ApiResponse<decimal>.SuccessResponse(account.Balance,"Withdrawal processed successfully");
        
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occured during withdrawal for account {request.AccountNumber}");
                    return ApiResponse<decimal>.FailureResponse($"Error occured during withdrawal");
                }
            }
        
            public async Task<ApiResponse<BalanceResponse>> CheckBalanceAsync(string accountNumber)
            {
                try
                {
                    var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
                    if (account == null || account.IsDeleted)
                    {
                        return ApiResponse<BalanceResponse>.FailureResponse("Account not found or deactivated.");
                    }

                    var balanceResponse = new BalanceResponse
                    {
                        AccountNumber = account.AccountNumber,
                        CustomerName = $"{account.FirstName} {account.LastName}",
                        Balance = account.Balance
                    };

                    return ApiResponse<BalanceResponse>.SuccessResponse(balanceResponse);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error checking balance for account {accountNumber}");
                    return ApiResponse<BalanceResponse>.FailureResponse("Error fetching balance");
                }
            }
        
            public async Task<ApiResponse<IEnumerable<TransactionResponse>>> GetTransactionsHistoryAsync(string accountNumber)
            {
                throw new NotImplementedException();
            }
}