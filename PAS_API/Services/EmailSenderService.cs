using System.Net;
using System.Net.Mail;
using PAS_API.Interface;

namespace PAS_API.Services
{
    public class EmailSenderService : IEmailSenderRepository
    {
        private readonly IItemRepository _itemRepository;
        public EmailSenderInformation _emailSenderInformation;

        public EmailSenderService(IItemRepository itemRepository, EmailSenderInformation emailSenderInformation)
        {
            _itemRepository = itemRepository;
            _emailSenderInformation = emailSenderInformation;
        }
        public async Task ItemMaintenanceNotification()
        {
            try
            {
                var items_under_maintenance = await _itemRepository.GetItemsUnderMaintenanceAsync();

                var smtpClient = new SmtpClient()
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_emailSenderInformation.email, _emailSenderInformation.password),
                    Host = _emailSenderInformation.host,
                    Port = _emailSenderInformation.port,
                    UseDefaultCredentials = false
                };

                foreach (var item in items_under_maintenance)
                {
                    var message = new MailMessage()
                    {
                        Subject = "Maintenance Notification",
                        Body = $"Good day! This is just to inform that {item.item_name} was up for maintenance last {item.maintenance_date}, email us at test@gmail.com to set up maintenance request.",
                        IsBodyHtml = true,
                        From = new MailAddress(_emailSenderInformation.email),
                    };

                    message.To.Add(item.Department.departmental_email);
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
        }
    }
}
