using SimpleBankingApi.DTOs.Requests;
using SimpleBankingApi.DTOs.Responses;

namespace SimpleBankingApi.Repository.Interface;

public interface IBankingService
{
    // Crud Operations for Account 
    
    Task<ApiResponse<AccountResponse>> CreateAccountAsync(CreateAccountRequest request);
   
    Task<ApiResponse<AccountResponse>> GetAccountAsync(string accountNumber);
   
    Task<ApiResponse<AccountResponse>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest request);
   
    Task<ApiResponse<bool>> DeleteAccountAsync(string accountNumber);
   
    Task<ApiResponse<IEnumerable<AccountResponse>>> GetAllAccountsAsync();
    
    
    
      // Bank Operations for Account 
      Task<ApiResponse<TransferSummary>> TransferFundsAsync(TransferRequest request);
      
      Task<ApiResponse<decimal>> DepositAsync(DepositRequest request);
      
      Task<ApiResponse<decimal>> WithdrawAsync(WithdrawalRequest request);
      
      Task<ApiResponse<BalanceResponse>> CheckBalanceAsync(string accountNumber);
      
      Task<ApiResponse<IEnumerable<TransactionResponse>>> GetTransactionsHistoryAsync(string accountNumber);


}