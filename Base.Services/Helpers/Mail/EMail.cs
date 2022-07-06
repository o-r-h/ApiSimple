using Base.Domain.Entities.Customs;
using Base.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Base.Services.Helpers.Mail
{
    public class EMail
    {
        public static ApplicationConfiguration.ConfigurationEMail configuration = ApplicationConfiguration.Current.EMail;

        public static MailMessage MailMessage(Message message)
        {
            return new MailMessage
            {
                From = !string.IsNullOrWhiteSpace(message.From) ?
                    new MailAddress(message.From.Trim(), (message.FromName ?? "").Trim()) :
                    new MailAddress(configuration.Account, configuration.AccountName),
                IsBodyHtml = message.IsHTML,
                Priority = message.Priority == Message.HIGH_PRIORITY ? MailPriority.High :
                           message.Priority == Message.LOW_PRIORITY ? MailPriority.Low : MailPriority.Normal
            };
        }

        public static SmtpClient SmtpClient()
        {
            return new SmtpClient
            {
                Host = configuration.Host,
                Port = configuration.Port > 0 ? configuration.Port : 25,
                EnableSsl = configuration.UseSSL,
                Credentials = new System.Net.NetworkCredential(
                    configuration.Account, configuration.Password)
            };
        }

        public static List<string> SeparateAddresses(string emailAddresses, List<string> excludeEMails = null)
        {
            excludeEMails = excludeEMails ?? new List<string>();
            emailAddresses = (emailAddresses ?? "").Replace(",", ";");
            return emailAddresses.Split(';')
                .Where(x => !string.IsNullOrWhiteSpace(x) && !excludeEMails.Exists(y => y.EqualsIgnoreAll(x)))
                .Select(x => x.ToLower())
                .Distinct()
                .ToList();
        }

        public static string ConvertToHTML(string s)
        {
            return HttpUtility.HtmlEncode(s).Replace(Environment.NewLine, "<br>");
        }

        public static string Send(Message message)
        {
            var email = MailMessage(message);
            var smtp = SmtpClient();
            string error = "";

            var destinatarios = SeparateAddresses(message.To);
            destinatarios.ForEach(x => email.To.Add(x));
            SeparateAddresses(message.CC, destinatarios).ForEach(x => email.CC.Add(x));

            email.Subject = message.Subject;
            email.Body = message.Body;

            if (message.Attachments != null)
            {
                foreach (var attachment in message.Attachments)
                {
                    try
                    {
                        var stream = new MemoryStream(attachment.Content);
                        email.Attachments.Add(new System.Net.Mail.Attachment(stream, attachment.Name));
                    }
                    catch (Exception e)
                    {
                        error += $"attachment: {attachment.Name} - {e.Message}{Environment.NewLine}";
                    }
                }
            }

            if (message.Resources != null)
            {
                var avHtml = AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html);
                foreach (var resource in message.Resources)
                {
                    if (resource.Content != null)
                    {
                        avHtml.LinkedResources.Add(new LinkedResource(new MemoryStream(resource.Content), resource.MediaType) { ContentId = resource.Identifier });
                    }
                    else
                    {
                        avHtml.LinkedResources.Add(new LinkedResource(resource.Path, resource.MediaType) { ContentId = resource.Identifier });
                    }
                }
                email.AlternateViews.Add(avHtml);
            }

            try
            {
                smtp.Send(email);
            }
            catch (Exception e)
            {
                error += $"To: {message.To} - Subject: {message.Subject} - {e.Message}{Environment.NewLine}";
            }
            finally
            {
                email.Dispose();
            }

            return error;
        }

        public static void Throw(Message message)
        {
            new Thread(() => Send(message)).Start();
        }

        public static void SendWithTemplate(Message message)
        {
            var template = System.IO.File.ReadAllText("assets/webtemplates/general.html");
            message.Body = template.Replace("[[MESSAGE]]", message.Body);

            message.Resources = message.Resources ?? new List<Resource>();

            message.Resources.Add(new Resource { Identifier = "logoImage", Path = "assets/images/simple-logo.png" });

            message.Subject = $"{message.Subject}";

            Send(message);
        }

        public static void ThrowWithTemplate(Message message)
        {
            new Thread(() => SendWithTemplate(message)).Start();
        }
    }
}
