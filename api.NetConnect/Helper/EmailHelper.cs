using api.NetConnect.data.Entity;
using api.NetConnect.DataControllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace api.NetConnect.Helper
{
    public class EmailHelper
    {
        public static void SendResetMail(User user)
        {
            var fromAddress = new MailAddress(Properties.Settings.Default.Email_FromAdress, Properties.Settings.Default.Email_FromName);
            var toAddress = new MailAddress(user.Email, $"{user.FirstName} {user.LastName}");
            string subject = "Password vergessen";
            string body = LoadTemplate("template_passwordReset.html", new Dictionary<string, string>() {
                {"baseurl", Properties.Settings.Default.BaseAbosulteUrl},
                {"resetcode", user.PasswordReset},
                {"infolink", "news"}
            });

            SendMail(fromAddress, toAddress, subject, body, true);
        }

        private static String LoadTemplate(String FileName, Dictionary<String, String> Replace)
        {
            var serverBasePath = HttpContext.Current.Server.MapPath("~");
            var filePath = Path.Combine(serverBasePath, "Resources");
            var body = File.ReadAllText(filePath + "\\" + FileName);

            foreach(KeyValuePair<String, String> kvp in Replace)
                body = body.Replace($"%{kvp.Key}%", kvp.Value);

            return body;
        }

        private static void SendMail(MailAddress FromAddress, MailAddress ToAddress, String Subject, String Body, Boolean IsBodyHtml = false)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Properties.Settings.Default.Email_FromAdress, Properties.Settings.Default.Email_FromPassword)
            };
            using (var message = new MailMessage(FromAddress, ToAddress)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = IsBodyHtml
            })
            {
                smtp.Send(message);
            }
        }
    }
}