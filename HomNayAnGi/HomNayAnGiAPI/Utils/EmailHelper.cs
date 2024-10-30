using System.Net;
using System.Net.Mail;

namespace HomNayAnGiAPI.Utils;

public class EmailHelper
{
    static string SenderMail = "hieptvhe173252@gmail.com";
    static string SenderPassword = "xrub dxja ykrc nwuk";

    public static Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(SenderMail, SenderPassword)
        };
        var senderAddress = new MailAddress(SenderMail, "Hôm nay ăn gì");
        var mailMessage = new MailMessage
        {
            From = senderAddress,
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        return client.SendMailAsync(mailMessage);
    }
    
    public static void SendEmailMultiThread(string email, string subject, string body)
    {
        SendEmailAsync(email, subject, body).Wait();
    }
}