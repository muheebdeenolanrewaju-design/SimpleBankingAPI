namespace SimpleBankingApi.Repository.Interface;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string toEmail,string customerName,string accountNumber,decimal balance);
    
    Task SendDepositEmailAsync(string toEmail,string customerName,string accountNumber,decimal newBalance,decimal depositAmount, string reference);
    
    Task SendWithdrawalEmailAsync(string toEmail,string customerName,string accountNumber,decimal newBalance,decimal withdrawalAmount, string reference);
        
    Task SendTransferDebitEmailAsync(string toEmail,string customerName,string senderAccountNumber,string recieverAccountNumber,decimal newBalance,decimal transferAmount, string reference);
    
     Task SendTransferCreditEmailAsync(string toEmail,string customerName,string senderAccountNumber,string recieverAccountNumber,decimal newBalance,decimal transferAmount, string reference);

     Task SendDeactivationEmailAsync(string toEmail, string customerName, string accountNumber);
}