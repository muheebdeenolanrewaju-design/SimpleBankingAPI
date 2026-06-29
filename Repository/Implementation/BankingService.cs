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
                   CreatedAt = newAcct.CreatedAt
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
            throw new NotImplementedException();
        }
    
        public async Task<ApiResponse<AccountResponse>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest request)
        {
            throw new NotImplementedException();
        }
    
        public async Task<ApiResponse<bool>> DeleteAccountAsync(string accountNumber)
        {
            throw new NotImplementedException();
        }
    
        public async Task<ApiResponse<IEnumerable<AccountResponse>>> GetAllAccountsAsync()
        {
            throw new NotImplementedException();
        }
}