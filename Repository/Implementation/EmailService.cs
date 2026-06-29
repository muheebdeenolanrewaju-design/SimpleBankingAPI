
using BankAPI.Utilities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SimpleBankingApi.Model;
using SimpleBankingApi.Repository.Interface;
using SimpleBankingApi.Utilities;

namespace SimpleBankingApi.Repository.Implementation;

public class EmailService:IEmailService
{
     private readonly SmtpMail _smtpMail;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<SmtpMail> smtpMail, ILogger<EmailService> logger)
    {
        _logger = logger;
        _smtpMail = smtpMail.Value;
    }

    private async Task SendMimeMessageAsync(MimeMessage message)
    {
        
        
        _logger.LogInformation($"Sending Email to {message.To} (Subject: {message.Subject})" );

        using var client = new SmtpClient();

        try
        {
            _logger.LogInformation($"Connecting to Smtp Server {_smtpMail.Server}:{_smtpMail.Port}");

            await client.ConnectAsync(_smtpMail.Server, _smtpMail.Port, false);

            _logger.LogInformation("Connected to Smtp Server");
            await client.AuthenticateAsync(_smtpMail.Username, _smtpMail.Password);


            _logger.LogInformation("Authenticated to Smtp Server");
            await client.SendAsync(message);
            _logger.LogInformation($"Email Sent to {message.To} successfully");
            
            await client.DisconnectAsync(true); // Disconnect and release all resources

        }
        catch (Exception ex)
        {
          _logger.LogError(ex, $"Error occured while sending email to {message.To}");
        }

    }

    private MimeMessage CreateBaseMessage(string toEmail, string subject)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_smtpMail.SenderName, _smtpMail.SenderEmail));
        message.To.Add(new MailboxAddress(toEmail, toEmail));
        message.Subject = subject;
        return message;
    }
    
    
    
    public async Task SendWelcomeEmailAsync(string toEmail, string customerName, string accountNumber, decimal balance)
    {
        var subject = "Welcome to Simple Baanking API!";
        
        var htmlbody = MailUtils.GetWelcomeEmailHtml(customerName, accountNumber, balance);
        
        var message = CreateBaseMessage(toEmail, subject);
        message.Body = new TextPart(TextFormat.Html) { Text = htmlbody };
        
        await SendMimeMessageAsync(message);
        
        
        
    }
}