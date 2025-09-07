namespace PrefectVotingApplication.Areas.Identity.Pages.Services
{
    public static class ConfirmationEmailTemplate
    {
        public static string BuildConfirmationEmail(string FirstName, string confirmationLink)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
              <meta charset='UTF-8'>
              <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f7;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 20px auto;
                    background: #ffffff;
                    border-radius: 8px;
                    box-shadow: 0 2px 6px rgba(0,0,0,0.1);
                    padding: 30px;
                }}
                h2 {{ color: #333333; }}
                p {{ color: #555555; line-height: 1.5; }}
                .button {{
                    display: inline-block;
                    margin-top: 20px;
                    padding: 12px 20px;
                    font-size: 16px;
                    color: white;
                    background-color: #0078D7;
                    text-decoration: none;
                    border-radius: 5px;
                }}
                .footer {{
                    margin-top: 30px;
                    font-size: 12px;
                    color: #888888;
                    text-align: center;
                }}
              </style>
            </head>
            <body>
              <div class='container'>
                <h2>Welcome to Prefect Voting!</h2>
                <p>Hi {FirstName},</p>
                <p>Thank you for registering with <strong>Prefect Voting Application</strong>. 
                Please confirm your email address by clicking the button below:</p>
    
                <a href='{confirmationLink}' class='button'>Confirm Email</a>

                <p>If the button doesn't work, copy and paste this link into your browser:</p>
                <p><a href='{confirmationLink}'>{confirmationLink}</a></p>

                <div class='footer'>
                  <p>This is an automated email. Please do not reply.</p>
                </div>
              </div>
            </body>
            </html>
            ";
        }
    }

}
