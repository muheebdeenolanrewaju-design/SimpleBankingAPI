namespace SimpleBankingApi.DTOs.Requests;

public class WithdrawalRequest
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
}