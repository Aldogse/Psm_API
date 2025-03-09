using System.Net;
using System.Net.Mail;
using PAS_API.Data;
using PAS_API.Interface;

namespace PAS_API.Services
{
    public class EmailSenderService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
 

        public EmailSenderService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope =  _serviceScopeFactory.CreateAsyncScope())
                {
                    var itemrepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();
                    var emailsenderInformation = scope.ServiceProvider.GetRequiredService<EmailSenderInformation>();

                    await ItemMaintenanceNotification(itemrepository,emailsenderInformation);

                    await Task.Delay(TimeSpan.FromHours(12),stoppingToken);
                }
            }
        }

        public async Task ItemMaintenanceNotification(IItemRepository _itemRepository,EmailSenderInformation? _emailSenderService)
        {
            try
            {
                var items_under_maintenance = await _itemRepository.GetItemsUnderMaintenanceAsync();

                if(items_under_maintenance != null || items_under_maintenance.Count == 0)
                {
                    return;
                }

                var smtpClient = new SmtpClient()
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_emailSenderService.email, _emailSenderService.password),
                    Host = _emailSenderService.host,
                    Port = _emailSenderService.port,
                    UseDefaultCredentials = false
                };

                foreach (var item in items_under_maintenance)
                {
                    var message = new MailMessage()
                    {
                        Subject = "Maintenance Notification",
                        Body = $"Good day! This is just to inform that {item.item_name} was up for maintenance last {item.maintenance_date}, email us at test@gmail.com to set up maintenance request.",
                        IsBodyHtml = true,
                        From = new MailAddress(_emailSenderService.email),
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
