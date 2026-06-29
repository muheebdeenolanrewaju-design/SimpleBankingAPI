namespace BankAPI.Utilities;

public static class MailUtils
{
    public static string GetEmailWrapper(string title, string content)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <title>{title}</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        @keyframes fadeInUp {{
            from {{
                opacity: 0;
                transform: translateY(30px);
            }}
            to {{
                opacity: 1;
                transform: translateY(0);
            }}
        }}
        
        @keyframes pulse {{
            0% {{ transform: scale(1); }}
            50% {{ transform: scale(1.05); }}
            100% {{ transform: scale(1); }}
        }}
        
        @keyframes shimmer {{
            0% {{ background-position: -1000px 0; }}
            100% {{ background-position: 1000px 0; }}
        }}
        
        @keyframes float {{
            0%, 100% {{ transform: translateY(0px); }}
            50% {{ transform: translateY(-10px); }}
        }}
        
        @keyframes glow {{
            0%, 100% {{ box-shadow: 0 0 20px rgba(254, 127, 45, 0.3); }}
            50% {{ box-shadow: 0 0 40px rgba(254, 127, 45, 0.6); }}
        }}
        
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            margin: 0;
            padding: 20px;
            background: linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f3460 100%);
            color: #2D3748;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            overflow-x: hidden;
        }}
        
        body::before {{
            content: '';
            position: fixed;
            top: -50%;
            left: -50%;
            width: 200%;
            height: 200%;
            background: radial-gradient(circle at 30% 50%, rgba(254, 127, 45, 0.05) 0%, transparent 50%),
                        radial-gradient(circle at 70% 80%, rgba(245, 203, 203, 0.05) 0%, transparent 50%);
            animation: float 20s ease-in-out infinite;
            pointer-events: none;
            z-index: 0;
        }}
        
        .container {{
            max-width: 600px;
            width: 100%;
            margin: 0 auto;
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(20px);
            border-radius: 32px;
            overflow: hidden;
            box-shadow: 0 30px 80px rgba(0, 0, 0, 0.3), 0 10px 30px rgba(0, 0, 0, 0.1);
            border: 1px solid rgba(255, 255, 255, 0.2);
            position: relative;
            z-index: 1;
            animation: fadeInUp 0.8s ease-out;
        }}
        
        .container::before {{
            content: '';
            position: absolute;
            top: -2px;
            left: -2px;
            right: -2px;
            bottom: -2px;
            background: linear-gradient(45deg, #FE7F2D, #F5CBCB, #FE7F2D, #F5CBCB);
            background-size: 400% 400%;
            border-radius: 34px;
            z-index: -1;
            animation: shimmer 6s linear infinite;
            opacity: 0.3;
        }}
        
        .header {{
            background: linear-gradient(135deg, #FE7F2D 0%, #e05a1a 40%, #c94a0e 100%);
            color: #ffffff;
            padding: 50px 40px 40px;
            text-align: center;
            position: relative;
            overflow: hidden;
        }}
        
        .header::before {{
            content: '';
            position: absolute;
            top: -40px;
            right: -20px;
            font-size: 200px;
            opacity: 0.08;
            transform: rotate(20deg);
            animation: float 8s ease-in-out infinite;
        }}
        
        .header::after {{
            content: '';
            position: absolute;
            bottom: -60px;
            left: -30px;
            font-size: 180px;
            opacity: 0.05;
            transform: rotate(-15deg);
            animation: float 10s ease-in-out infinite reverse;
        }}
        
        .header-content {{
            position: relative;
            z-index: 2;
        }}
        
        .header-icon {{
            font-size: 56px;
            margin-bottom: 10px;
            display: inline-block;
            animation: pulse 3s ease-in-out infinite;
            filter: drop-shadow(0 4px 12px rgba(0,0,0,0.2));
        }}
        
        .header h1 {{
            margin: 0;
            font-size: 32px;
            font-weight: 800;
            letter-spacing: -0.5px;
            text-shadow: 0 2px 20px rgba(0, 0, 0, 0.15);
            background: linear-gradient(135deg, #ffffff 0%, #ffe8d6 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            background-clip: text;
        }}
        
        .header-subtitle {{
            font-size: 16px;
            opacity: 0.95;
            margin-top: 8px;
            font-weight: 300;
            letter-spacing: 2px;
            text-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
            -webkit-text-fill-color: rgba(255,255,255,0.9);
        }}
        
        .header-decoration {{
            display: flex;
            justify-content: center;
            gap: 12px;
            margin-top: 12px;
        }}
        
        .header-decoration span {{
            display: inline-block;
            width: 8px;
            height: 8px;
            border-radius: 50%;
            background: rgba(255, 255, 255, 0.4);
            animation: pulse 2s ease-in-out infinite;
        }}
        
        .header-decoration span:nth-child(2) {{
            animation-delay: 0.3s;
        }}
        
        .header-decoration span:nth-child(3) {{
            animation-delay: 0.6s;
        }}
        
        .content {{
            padding: 45px 40px 35px;
            line-height: 1.7;
        }}
        
        .greeting-box {{
            background: linear-gradient(135deg, #FFF8F4 0%, #FDE8E0 100%);
            border-radius: 20px;
            padding: 28px 30px;
            margin-bottom: 35px;
            border-left: 5px solid #FE7F2D;
            position: relative;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(254, 127, 45, 0.08);
        }}
        
        .greeting-box::before {{
            content: '';
            position: absolute;
            top: -10px;
            right: 15px;
            font-size: 80px;
            color: rgba(254, 127, 45, 0.08);
            font-family: Georgia, serif;
        }}
        
        .greeting-box h2 {{
            color: #FE7F2D;
            font-size: 24px;
            margin-bottom: 8px;
            font-weight: 700;
        }}
        
        .greeting-box p {{
            color: #4A5568;
            font-size: 16px;
            margin: 0;
            line-height: 1.6;
        }}
        
        .account-card {{
            background: linear-gradient(135deg, #FE7F2D 0%, #d94a0e 100%);
            color: #ffffff;
            padding: 35px 30px;
            border-radius: 20px;
            text-align: center;
            margin-bottom: 40px;
            box-shadow: 0 15px 40px rgba(254, 127, 45, 0.35);
            position: relative;
            overflow: hidden;
            animation: glow 3s ease-in-out infinite;
        }}
        
        .account-card::before {{
            content: '';
            position: absolute;
            top: -50%;
            left: -50%;
            width: 200%;
            height: 200%;
            background: radial-gradient(circle at 30% 50%, rgba(255,255,255,0.1) 0%, transparent 60%);
            animation: float 12s ease-in-out infinite;
        }}
        
        .account-card::after {{
            content: '';
            position: absolute;
            bottom: 10px;
            right: 20px;
            font-size: 14px;
            opacity: 0.15;
            letter-spacing: 8px;
            animation: float 6s ease-in-out infinite reverse;
        }}
        
        .account-label {{
            font-size: 12px;
            text-transform: uppercase;
            letter-spacing: 3px;
            opacity: 0.9;
            font-weight: 600;
            display: block;
            margin-bottom: 10px;
            position: relative;
            z-index: 2;
        }}
        
        .account-number-wrapper {{
            position: relative;
            z-index: 2;
            padding: 15px 0;
        }}
        
        .account-number {{
            font-family: 'Courier New', monospace;
            font-size: 38px;
            letter-spacing: 8px;
            font-weight: 700;
            margin: 5px 0 10px;
            text-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
            background: rgba(255,255,255,0.1);
            padding: 8px 20px;
            border-radius: 12px;
            display: inline-block;
            backdrop-filter: blur(10px);
            border: 1px solid rgba(255,255,255,0.1);
        }}
        
        .account-balance {{
            font-size: 16px;
            opacity: 0.95;
            background: rgba(255, 255, 255, 0.2);
            padding: 8px 24px;
            border-radius: 30px;
            display: inline-block;
            position: relative;
            z-index: 2;
            backdrop-filter: blur(10px);
            border: 1px solid rgba(255,255,255,0.15);
            font-weight: 500;
        }}
        
        .section-title {{
            color: #2D3748;
            font-size: 18px;
            font-weight: 700;
            margin: 35px 0 20px;
            padding-bottom: 12px;
            border-bottom: 3px solid #F5CBCB;
            display: flex;
            align-items: center;
            gap: 12px;
            position: relative;
        }}
        
        .section-title::after {{
            content: '';
            position: absolute;
            bottom: -3px;
            left: 0;
            width: 60px;
            height: 3px;
            background: linear-gradient(90deg, #FE7F2D, transparent);
            border-radius: 3px;
        }}
        
        .section-title .emoji {{
            font-size: 22px;
        }}
        
        .details-table {{
            width: 100%;
            margin: 20px 0 30px;
            border-collapse: collapse;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.05);
        }}
        
        .details-table tr {{
            transition: all 0.3s ease;
        }}
        
        .details-table tr:hover {{
            background: linear-gradient(90deg, #FFF8F4, transparent);
            transform: scale(1.01);
        }}
        
        .details-table th, .details-table td {{
            padding: 16px 20px;
            text-align: left;
        }}
        
        .details-table th {{
            background: linear-gradient(135deg, #F5CBCB, #fde8e0);
            color: #4A3A2E;
            font-weight: 600;
            font-size: 14px;
            width: 40%;
            letter-spacing: 0.5px;
            border-right: 2px solid rgba(254, 127, 45, 0.1);
        }}
        
        .details-table td {{
            color: #2D3748;
            font-weight: 500;
            border-bottom: 1px solid #F5E8E0;
            background: white;
        }}
        
        .details-table tr:last-child td {{
            border-bottom: none;
        }}
        
        .status-badge {{
            background: linear-gradient(135deg, #48BB78, #38A169);
            color: white;
            padding: 6px 18px;
            border-radius: 30px;
            font-size: 13px;
            font-weight: 700;
            display: inline-block;
            letter-spacing: 0.5px;
            text-transform: uppercase;
            box-shadow: 0 4px 12px rgba(72, 187, 120, 0.3);
            animation: pulse 2s ease-in-out infinite;
        }}
        
        .steps-grid {{
            display: flex;
            flex-direction: column;
            gap: 18px;
            margin: 25px 0 15px;
        }}
        
        .step-item {{
            display: flex;
            align-items: flex-start;
            gap: 18px;
            padding: 18px 22px;
            background: linear-gradient(135deg, #FFFAF8, #FFF5F0);
            border-radius: 16px;
            transition: all 0.4s cubic-bezier(0.175, 0.885, 0.32, 1.275);
            border: 1px solid rgba(254, 127, 45, 0.08);
            position: relative;
            overflow: hidden;
        }}
        
        .step-item::before {{
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: linear-gradient(135deg, rgba(254, 127, 45, 0.03), transparent);
            opacity: 0;
            transition: opacity 0.3s ease;
        }}
        
        .step-item:hover {{
            transform: translateX(8px) scale(1.02);
            border-color: rgba(254, 127, 45, 0.3);
            box-shadow: 0 8px 30px rgba(254, 127, 45, 0.1);
        }}
        
        .step-item:hover::before {{
            opacity: 1;
        }}
        
        .step-number {{
            background: linear-gradient(135deg, #FE7F2D, #d94a0e);
            color: #ffffff;
            border-radius: 50%;
            width: 36px;
            height: 36px;
            min-width: 36px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: 700;
            font-size: 15px;
            box-shadow: 0 4px 15px rgba(254, 127, 45, 0.4);
            position: relative;
            z-index: 2;
            transition: transform 0.3s ease;
        }}
        
        .step-item:hover .step-number {{
            transform: scale(1.1) rotate(-5deg);
        }}
        
        .step-content {{
            flex: 1;
            position: relative;
            z-index: 2;
        }}
        
        .step-content strong {{
            color: #2D3748;
            font-size: 16px;
            display: block;
            margin-bottom: 4px;
        }}
        
        .step-content p {{
            margin: 0;
            color: #4A5568;
            font-size: 14px;
            line-height: 1.6;
        }}
        
        .pro-tip {{
            text-align: center;
            margin-top: 35px;
            padding: 20px 25px;
            background: linear-gradient(135deg, #FFF8F4, #FDE8E0);
            border-radius: 16px;
            border: 2px dashed #FE7F2D;
            position: relative;
            overflow: hidden;
            transition: all 0.3s ease;
        }}
        
        .pro-tip:hover {{
            transform: scale(1.02);
            box-shadow: 0 8px 30px rgba(254, 127, 45, 0.1);
        }}
        
        .pro-tip::before {{
            content: '';
            position: absolute;
            top: -15px;
            left: -10px;
            font-size: 60px;
            opacity: 0.08;
            transform: rotate(-15deg);
        }}
        
        .pro-tip span {{
            font-size: 15px;
            color: #4A5568;
            position: relative;
            z-index: 2;
        }}
        
        .pro-tip strong {{
            color: #FE7F2D;
        }}
        
        .footer {{
            background: linear-gradient(135deg, #1a1a2e, #16213e);
            color: rgba(255, 255, 255, 0.8);
            text-align: center;
            padding: 30px 35px;
            font-size: 13px;
            border-top: 4px solid #FE7F2D;
            position: relative;
            overflow: hidden;
        }}
        
        .footer::before {{
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: linear-gradient(135deg, rgba(254, 127, 45, 0.05), transparent);
            pointer-events: none;
        }}
        
        .footer .brand {{
            font-weight: 700;
            color: #FE7F2D;
            font-size: 17px;
            letter-spacing: 1px;
            position: relative;
            z-index: 2;
        }}
        
        .footer .brand .accent {{
            color: #F5CBCB;
        }}
        
        .footer .divider {{
            display: inline-block;
            margin: 0 10px;
            color: rgba(255,255,255,0.2);
        }}
        
        .footer .secure-badge {{
            display: inline-block;
            margin-top: 10px;
            font-size: 12px;
            color: rgba(255,255,255,0.5);
            background: rgba(255, 255, 255, 0.05);
            padding: 6px 18px;
            border-radius: 30px;
            border: 1px solid rgba(255,255,255,0.05);
            position: relative;
            z-index: 2;
            backdrop-filter: blur(10px);
        }}
        
        .footer .secure-badge::before {{
            content: '';
        }}
        
        .social-links {{
            display: flex;
            justify-content: center;
            gap: 15px;
            margin: 12px 0 8px;
            position: relative;
            z-index: 2;
        }}
        
        .social-links a {{
            color: rgba(255,255,255,0.4);
            text-decoration: none;
            font-size: 14px;
            transition: all 0.3s ease;
        }}
        
        .social-links a:hover {{
            color: #FE7F2D;
            transform: translateY(-2px);
        }}
        
        @media (max-width: 600px) {{
            body {{
                padding: 10px;
            }}
            
            .header {{
                padding: 35px 25px 30px;
            }}
            
            .header h1 {{
                font-size: 26px;
            }}
            
            .header-icon {{
                font-size: 44px;
            }}
            
            .content {{
                padding: 30px 20px 25px;
            }}
            
            .account-number {{
                font-size: 26px;
                letter-spacing: 5px;
                padding: 6px 14px;
            }}
            
            .account-card {{
                padding: 25px 20px;
            }}
            
            .details-table th, .details-table td {{
                padding: 12px 14px;
                font-size: 13px;
            }}
            
            .step-item {{
                padding: 14px 16px;
            }}
            
            .greeting-box {{
                padding: 20px;
            }}
            
            .footer {{
                padding: 25px 20px;
            }}
        }}
        
        @media (max-width: 400px) {{
            .account-number {{
                font-size: 20px;
                letter-spacing: 3px;
            }}
            
            .details-table th, .details-table td {{
                padding: 10px 12px;
                font-size: 12px;
            }}
            
            .header h1 {{
                font-size: 22px;
            }}
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='header-content'>
                <span class='header-icon'>B</span>
                <h1>{title}</h1>
                <div class='header-subtitle'>Simple | Secure | Smart Banking</div>
                <div class='header-decoration'>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            </div>
        </div>
        <div class='content'>
            {content}
        </div>
        <div class='footer'>
            <div class='brand'>Simple Banking <span class='accent'>API</span></div>
            <div class='social-links'>
                <a href='#'>Twitter</a>
                <a href='#'>LinkedIn</a>
                <a href='#'>GitHub</a>
            </div>
            <div style='margin-top: 6px; position: relative; z-index: 2;'>
                (c) {DateTime.UtcNow.Year} All rights reserved
                <span class='divider'>|</span>
                Built with love
            </div>
            <div class='secure-badge'>256-bit Encryption | SOC2 Compliant</div>
        </div>
    </div>
</body>
</html>";
    }

    public static string GetWelcomeEmailHtml(string customerName, string accountNumber, decimal balance)
    {
        var title = "Welcome to Simple Banking API!";
        var content = $@"
            <div class='greeting-box'>
                <h2>Welcome, {customerName}!</h2>
                <p>We're absolutely thrilled to welcome you to the future of banking. Your account is now active, secure, and ready for seamless transactions.</p>
            </div>
            
            <div class='account-card'>
                <span class='account-label'>Your Account Number</span>
                <div class='account-number-wrapper'>
                    <div class='account-number'>{accountNumber}</div>
                </div>
                <span class='account-balance'>Opening Balance: NGN {balance:N2}</span>
            </div>

            <div class='section-title'>
                <span class='emoji'>+</span>
                Account Details
            </div>
            <table class='details-table'>
                <tr>
                    <th>Account Holder</th>
                    <td><strong>{customerName}</strong></td>
                </tr>
                <tr>
                    <th>Account Status</th>
                    <td><span class='status-badge'>Active</span></td>
                </tr>
                <tr>
                    <th>Currency</th>
                    <td>Nigerian Naira (NGN)</td>
                </tr>
                <tr>
                    <th>Date Opened</th>
                    <td>{DateTime.UtcNow:dddd, MMMM d, yyyy} at {DateTime.UtcNow:h:mm tt} (UTC)</td>
                </tr>
            </table>

            <div class='section-title' style='margin-top: 40px;'>
                <span class='emoji'>></span>
                Quick Start Guide
            </div>
            <div class='steps-grid'>
                <div class='step-item'>
                    <div class='step-number'>1</div>
                    <div class='step-content'>
                        <strong>Fund Your Account</strong>
                        <p>Make your first deposit using our secure payment gateway and start your banking journey today.</p>
                    </div>
                </div>
                <div class='step-item'>
                    <div class='step-number'>2</div>
                    <div class='step-content'>
                        <strong>Transfer Instantly</strong>
                        <p>Send money to any account within our network with zero fees and instant processing.</p>
                    </div>
                </div>
                <div class='step-item'>
                    <div class='step-number'>3</div>
                    <div class='step-content'>
                        <strong>Track Transactions</strong>
                        <p>Monitor your spending with real-time transaction history and detailed analytics.</p>
                    </div>
                </div>
            </div>
            
            <div class='pro-tip'>
                <span><strong>Pro Tip:</strong> Download our mobile app for banking on the go! Available on iOS and Android.</span>
            </div>";

        return GetEmailWrapper(title, content);
    }
}