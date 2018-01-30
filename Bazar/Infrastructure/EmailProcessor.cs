using System;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

using Bazar.Models.Entities;
using Bazar.Models.Repository;
using Bazar.Models.ViewModels;

namespace Bazar.Infrastructure
{
    public class EmailSettings
    {
        public string MailFromAdress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MailToAdress { get; set; }
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
        public bool UseSsl { get; set; }

        public EmailSettings(IOptions<AppSettings> appSettings)
        {
            MailFromAdress = appSettings.Value.MailFromAdress;
            Username = appSettings.Value.Username;
            Password = appSettings.Value.Password;
            MailToAdress = appSettings.Value.MailToAdress;
            ServerName = appSettings.Value.ServerName;
            ServerPort = appSettings.Value.ServerPort;
            UseSsl = appSettings.Value.SSL;
        }
    }

    public class EmailProcessor
    {
        private EmailSettings emailSettings;
        private readonly IViewRenderService _viewRenderService;

        public EmailProcessor(EmailSettings settings, IViewRenderService viewRenderService)
        {
            emailSettings = settings;
            _viewRenderService = viewRenderService;
        }

        public async Task ProcessorOrder(Order order)
        {
            string body = await _viewRenderService.RenderToStringAsync("Email/Order", order);

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", emailSettings.MailFromAdress));
            emailMessage.To.Add(new MailboxAddress("", emailSettings.MailToAdress));
            emailMessage.Subject = "Заказ в Краща Їжа";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body.ToString()
            };

            using (var client = new SmtpClient())
            {
                client.Connect(emailSettings.ServerName, emailSettings.ServerPort, emailSettings.UseSsl);
                client.Authenticate(emailSettings.Username, emailSettings.Password);

                try
                {
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
