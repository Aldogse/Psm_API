using System.Net;
using System.Net.Mail;
using Property_And_Supply_Management_API.Data;
using Property_And_Supply_Management_API.Interfaces;
using Property_And_Supply_Management_API.Repository;

namespace Property_And_Supply_Management_API.Services
{
    public class EmailSenderService : IIEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly EmailSenderInformation _emailSenderInformation;
        private readonly ItemRepository _itemRepository;

        public EmailSenderService(IConfiguration configuration, EmailSenderInformation emailSenderInformation,ItemRepository itemRepository)
        {
            _configuration = configuration;
            _emailSenderInformation = emailSenderInformation;
            _itemRepository = itemRepository;
        }
        public async Task SendMaintenanceNotificationEmailAsync()
        {
            try
            {
                var items = await _itemRepository.GetItemUnderMaintenanceAsync();
                //provide the information about the email sender
                var smtp = new SmtpClient()
                {
                    Port = _emailSenderInformation.PORT,
                    Host = _emailSenderInformation.HOST,
                    Credentials = new NetworkCredential(_emailSenderInformation.EMAIL,_emailSenderInformation.PASSWORD),
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };

                foreach(var item in items)
                {
                    var message = new MailMessage()
                    {
                        Body = $"Good day! Just want to inform that {item.item_name} is due for maintenance last {item.maintenance_date.ToShortDateString()}, please contact us at test@gmail.com to set the maintenance appointment",
                        IsBodyHtml = true,
                        From = new MailAddress(_emailSenderInformation.EMAIL),
                        Subject = "Maintenance Notification update"
                    };

                    message.To.Add(item.Department.departmental_email);
                    await smtp.SendMailAsync(message);
                }

            }
            catch (Exception ex)
            {
               throw new FileNotFoundException(ex.Message);
            }
        }
    }
}
