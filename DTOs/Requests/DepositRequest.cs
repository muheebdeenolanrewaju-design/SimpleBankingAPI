namespace SimpleBankingApi.DTOs.Requests;

public class DepositRequest
{
    public string AccountNumber { get; set; }
    public string Naration { get; set; }
    public decimal Amount { get; set; }
}