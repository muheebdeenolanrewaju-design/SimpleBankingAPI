namespace SimpleBankingApi.Model;

public class Transaction
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; }
    public string TransactionType { get; set; } // Deposit , Withdrawal, TransferDebit , TransferCredit
    public decimal Amount { get; set; }
    public string Naration { get; set; }
    public string Reference  { get; set; }
    public DateTime CreatedAt { get; set; }
}