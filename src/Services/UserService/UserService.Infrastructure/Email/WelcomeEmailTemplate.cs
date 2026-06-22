namespace UserService.Infrastructure.Email;

public static class WelcomeEmailTemplate
{
    public static string Build(string firstName)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
</head>
<body style=""margin:0;padding:0;background-color:#f4f6f8;font-family:Arial,Helvetica,sans-serif;"">
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f4f6f8;padding:32px 0;"">
        <tr>
            <td align=""center"">
                <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);"">
                    <tr>
                        <td style=""background-color:#4f46e5;padding:24px 32px;"">
                            <h1 style=""margin:0;color:#ffffff;font-size:24px;"">Fundoo Notes</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style=""padding:32px;"">
                            <h2 style=""margin:0 0 16px;color:#111827;font-size:20px;"">Welcome, {firstName}!</h2>
                            <p style=""margin:0 0 16px;color:#374151;font-size:15px;line-height:1.6;"">
                                Your account has been created successfully. You can now sign in and start creating notes.
                            </p>
                            <p style=""margin:0 0 24px;color:#374151;font-size:15px;line-height:1.6;"">
                                Organize your ideas, pin important notes, and stay productive every day.
                            </p>
                            <p style=""margin:0;color:#6b7280;font-size:14px;line-height:1.6;"">
                                Regards,<br />
                                <strong>Fundoo Notes Team</strong>
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
    }
}
