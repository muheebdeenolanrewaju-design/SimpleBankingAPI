using System;
 
 public class EmailTemplateGenerator
 {
     private static string GetEmailWrapper(string title, string content)
     {
         return $@"
         <!DOCTYPE html>
         <html>
         <head>
             <meta charset='UTF-8'>
             <meta name='viewport' content='width=device-width, initial-scale=1.0'>
             <title>{title}</title>
             <style>
                 body {{
                     font-family: Arial, Helvetica, sans-serif;
                     line-height: 1.6;
                     color: #333333;
                     max-width: 600px;
                     margin: 0 auto;
                     padding: 0;
                     background: #f5f5f5;
                 }}
                 .container {{
                     background: #ffffff;
                     border-radius: 8px;
                     margin: 20px;
                     padding: 0;
                     box-shadow: 0 2px 8px rgba(0,0,0,0.06);
                 }}
                 .header {{
                     background: #FF7873;
                     padding: 30px 30px 20px;
                     border-radius: 8px 8px 0 0;
                 }}
                 .logo {{
                     font-size: 24px;
                     font-weight: 700;
                     color: #ffffff;
                     letter-spacing: 1px;
                 }}
                 .logo span {{
                     font-weight: 300;
                 }}
                 .header-line {{
                     width: 40px;
                     height: 3px;
                     background: #ffffff;
                     margin-top: 8px;
                     opacity: 0.5;
                 }}
                 .body-content {{
                     padding: 30px 30px 20px;
                 }}
                 .badge {{
                     display: inline-block;
                     background: #FF7873;
                     color: #ffffff;
                     font-size: 11px;
                     font-weight: 600;
                     padding: 4px 14px;
                     border-radius: 3px;
                     text-transform: uppercase;
                     letter-spacing: 0.5px;
                 }}
                 .badge.debit {{
                     background: #e64a3a;
                 }}
                 h2 {{
                     font-size: 22px;
                     font-weight: 400;
                     color: #333333;
                     margin: 12px 0 6px;
                 }}
                 .greeting {{
                     color: #666666;
                     font-size: 14px;
                     margin-bottom: 20px;
                 }}
                 .amount-box {{
                     background: #fff8f7;
                     border-left: 4px solid #FF7873;
                     padding: 16px 20px;
                     margin: 16px 0;
                 }}
                 .amount {{
                     font-size: 28px;
                     font-weight: 600;
                     color: #FF7873;
                 }}
                 .amount.negative {{
                     color: #e64a3a;
                 }}
                 .amount-label {{
                     font-size: 12px;
                     color: #999999;
                     margin-top: 2px;
                 }}
                 .details {{
                     margin: 20px 0 10px;
                 }}
                 .detail-row {{
                     display: flex;
                     padding: 8px 0;
                     border-bottom: 1px solid #f0f0f0;
                 }}
                 .detail-row:last-child {{
                     border-bottom: none;
                 }}
                 .detail-label {{
                     flex: 0 0 110px;
                     font-size: 13px;
                     color: #888888;
                 }}
                 .detail-value {{
                     flex: 1;
                     font-size: 14px;
                     color: #333333;
                     font-weight: 500;
                 }}
                 .detail-value.balance {{
                     color: #FF7873;
                     font-weight: 600;
                 }}
                 .detail-value.balance-debit {{
                     color: #e64a3a;
                     font-weight: 600;
                 }}
                 .detail-value.timestamp {{
                     color: #999999;
                     font-weight: 400;
                 }}
                 .footer {{
                     padding: 20px 30px;
                     border-top: 1px solid #f0f0f0;
                     text-align: center;
                 }}
                 .footer p {{
                     margin: 3px 0;
                     font-size: 12px;
                     color: #999999;
                 }}
                 .footer .highlight {{
                     color: #FF7873;
                 }}
                 .divider {{
                     width: 30px;
                     height: 2px;
                     background: #FF7873;
                     margin: 0 auto 8px;
                     opacity: 0.3;
                 }}
                 .security-badge {{
                     display: inline-block;
                     font-size: 10px;
                     color: #aaaaaa;
                     letter-spacing: 0.5px;
                 }}
                 .security-badge span {{
                     margin: 0 6px;
                 }}
             </style>
         </head>
         <body>
             <div class='container'>
                 <div class='header'>
                     <div class='logo'>Your<span>Bank</span></div>
                     <div class='header-line'></div>
                 </div>
                 <div class='body-content'>
                     {content}
                 </div>
                 <div class='footer'>
                     <div class='divider'></div>
                     <p>This is an automated notification</p>
                     <p class='security-badge'>
                         <span>•</span> Secure <span>•</span> Reliable <span>•</span> Trusted
                     </p>
                     <p style='margin-top: 8px; font-size: 11px; color: #bbbbbb;'>&copy; 2026 Your Bank. All rights reserved.</p>
                 </div>
             </div>
         </body>
         </html>";
     }
 
     // ADD THIS NEW METHOD - WELCOME EMAIL
     public static string GetWelcomeEmailHtml(string customerName, string accountNumber, decimal balance)
     {
         var title = "Welcome to International African Bank API!";
         var content = $@"
                 <span class='badge'>Welcome</span>
                 <h2>Welcome to International African Bank!</h2>
                 <p class='greeting'>Dear {customerName},</p>
                 <p style='color: #555555; font-size: 14px; line-height: 1.8;'>
                     Thank you for choosing International African Bank. We are excited to have you on board!
                 </p>
                 <p style='color: #555555; font-size: 14px; line-height: 1.8;'>
                     Your account has been successfully created and is ready to use.
                 </p>
                 <div class='amount-box'>
                     <div style='font-size: 14px; color: #666666;'>Account Balance</div>
                     <div class='amount'>₦{balance:N2}</div>
                     <div class='amount-label'>Account Number: {accountNumber}</div>
                 </div>
                 <div class='details'>
                     <div class='detail-row'>
                         <div class='detail-label'>Account Number</div>
                         <div class='detail-value'>{accountNumber}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Account Holder</div>
                         <div class='detail-value'>{customerName}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Opening Balance</div>
                         <div class='detail-value balance'>₦{balance:N2}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Date Opened</div>
                         <div class='detail-value timestamp'>{DateTime.UtcNow:dd MMM yyyy HH:mm} UTC</div>
                     </div>
                 </div>
                 <div style='margin-top: 20px; padding: 16px; background: #f8f9fa; border-radius: 6px;'>
                     <p style='font-size: 13px; color: #666666; margin: 0;'>
                         <strong>Getting Started:</strong><br>
                         • You can deposit funds into your account<br>
                         • Transfer money to other accounts<br>
                         • Check your balance anytime<br>
                         • View your transaction history
                     </p>
                 </div>";
 
         return GetEmailWrapper(title, content);
     }
 
     // Your existing methods
     public static string GetDepositAlertEmailHtml(string customerName, string accountNumber, decimal depositAmount, decimal newBalance, string reference)
     {
         var title = "Transaction Alert: Credit [Deposit]";
         var content = $@"
                 <span class='badge'>Deposit</span>
                 <h2>You've Received a Deposit</h2>
                 <p class='greeting'>Dear {customerName},</p>
                 <div class='amount-box'>
                     <div class='amount'>+ ₦{depositAmount:N2}</div>
                     <div class='amount-label'>Your account has been credited</div>
                 </div>
                 <div class='details'>
                     <div class='detail-row'>
                         <div class='detail-label'>Account Number</div>
                         <div class='detail-value'>{accountNumber}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Transaction Type</div>
                         <div class='detail-value'>Deposit (Credit)</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Reference</div>
                         <div class='detail-value'>{reference}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Available Balance</div>
                         <div class='detail-value balance'>₦{newBalance:N2}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Timestamp</div>
                         <div class='detail-value timestamp'>{DateTime.UtcNow:f} (UTC)</div>
                     </div>
                 </div>";
 
         return GetEmailWrapper(title, content);
     }
 
     public static string GetWithdrawalAlertEmailHtml(string customerName, string accountNumber, decimal withdrawalAmount, decimal newBalance, string reference)
     {
         var title = "Transaction Alert: Debit [Withdrawal]";
         var content = $@"
                 <span class='badge debit'>Withdrawal</span>
                 <h2>Funds Withdrawn</h2>
                 <p class='greeting'>Dear {customerName},</p>
                 <div class='amount-box'>
                     <div class='amount negative'>- ₦{withdrawalAmount:N2}</div>
                     <div class='amount-label'>Funds have been debited</div>
                 </div>
                 <div class='details'>
                     <div class='detail-row'>
                         <div class='detail-label'>Account Number</div>
                         <div class='detail-value'>{accountNumber}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Transaction Type</div>
                         <div class='detail-value'>Withdrawal (Debit)</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Reference</div>
                         <div class='detail-value'>{reference}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Available Balance</div>
                         <div class='detail-value balance-debit'>₦{newBalance:N2}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Timestamp</div>
                         <div class='detail-value timestamp'>{DateTime.UtcNow:f} (UTC)</div>
                     </div>
                 </div>";
 
         return GetEmailWrapper(title, content);
     }
 
     public static string GetTransferDebitAlertEmailHtml(string customerName, string senderAccountNumber, string receiverAccountNumber, decimal transferAmount, decimal newBalance, string reference)
     {
         var title = "Transaction Alert: Debit [Transfer]";
         var content = $@"
                 <span class='badge debit'>Transfer</span>
                 <h2>Funds Transferred Out</h2>
                 <p class='greeting'>Dear {customerName},</p>
                 <div class='amount-box'>
                     <div class='amount negative'>- ₦{transferAmount:N2}</div>
                     <div class='amount-label'>Transfer completed</div>
                 </div>
                 <div class='details'>
                     <div class='detail-row'>
                         <div class='detail-label'>Sender Account</div>
                         <div class='detail-value'>{senderAccountNumber}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Receiver Account</div>
                         <div class='detail-value'>{receiverAccountNumber}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Transaction Type</div>
                         <div class='detail-value'>Transfer (Debit)</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Reference</div>
                         <div class='detail-value'>{reference}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Available Balance</div>
                         <div class='detail-value balance-debit'>₦{newBalance:N2}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Timestamp</div>
                         <div class='detail-value timestamp'>{DateTime.UtcNow:f} (UTC)</div>
                     </div>
                 </div>";
 
         return GetEmailWrapper(title, content);
     }
 
     public static string GetTransferCreditAlertEmailHtml(string customerName, string senderAccountNumber, string receiverAccountNumber, decimal transferAmount, decimal newBalance, string reference)
     {
         var title = "Transaction Alert: Credit [Transfer]";
         var content = $@"
                 <span class='badge'>Transfer</span>
                 <h2>Funds Transfer Received</h2>
                 <p class='greeting'>Dear {customerName},</p>
                 <div class='amount-box'>
                     <div class='amount'>+ ₦{transferAmount:N2}</div>
                     <div class='amount-label'>Inward transfer received</div>
                 </div>
                 <div class='details'>
                     <div class='detail-row'>
                         <div class='detail-label'>Sender Account</div>
                         <div class='detail-value'>{senderAccountNumber}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Receiver Account</div>
                         <div class='detail-value'>{receiverAccountNumber}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Transaction Type</div>
                         <div class='detail-value'>Transfer (Credit)</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Reference</div>
                         <div class='detail-value'>{reference}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Available Balance</div>
                         <div class='detail-value balance'>₦{newBalance:N2}</div>
                     </div>
                     <div class='detail-row'>
                         <div class='detail-label'>Timestamp</div>
                         <div class='detail-value timestamp'>{DateTime.UtcNow:f} (UTC)</div>
                     </div>
                 </div>";
 
         return GetEmailWrapper(title, content);
     }
     
     public static string GetDeactivationEmailHtml(string customerName, string accountNumber)
{
    var title = "Account Status Alert: Deactivation";
    var content = $@"
            <span class='badge debit'>Deactivated</span>
            <h2>Account Deactivation Notice</h2>
            <p class='greeting'>Dear {customerName},</p>
            <p style='color: #555555; font-size: 14px; line-height: 1.8;'>
                This is to inform you that your bank account has been successfully deactivated and closed.
            </p>
            <div class='amount-box' style='background: #fffafa; border-left: 4px solid #e64a3a;'>
                <div style='font-size: 14px; color: #666666;'>Account Status</div>
                <div class='amount' style='color: #e64a3a;'>INACTIVE</div>
                <div class='amount-label'>Account Number: {accountNumber}</div>
            </div>
            <div class='details'>
                <div class='detail-row'>
                    <div class='detail-label'>Account Number</div>
                    <div class='detail-value'>{accountNumber}</div>
                </div>
                <div class='detail-row'>
                    <div class='detail-label'>Account Holder</div>
                    <div class='detail-value'>{customerName}</div>
                </div>
                <div class='detail-row'>
                    <div class='detail-label'>Status Change</div>
                    <div class='detail-value balance-debit'>Closed / Suspended</div>
                </div>
                <div class='detail-row'>
                    <div class='detail-label'>Timestamp</div>
                    <div class='detail-value timestamp'>{DateTime.UtcNow:dd MMM yyyy HH:mm} UTC</div>
                </div>
            </div>
            <div style='margin-top: 20px; padding: 16px; background: #f8f9fa; border-radius: 6px;'>
                <p style='font-size: 13px; color: #666666; margin: 0;'>
                    <strong>Important Security Notice:</strong><br>
                    • This account can no longer participate in active banking operations.<br>
                    • All automated incoming deposits or transfer receipts will automatically fail.<br>
                    • If this deactivation request was unauthorized, please reach out to your account manager immediately.
                </p>
            </div>";

    return GetEmailWrapper(title, content);
}
 }