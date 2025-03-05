using Model;

namespace PAS_API.Interface
{
    public interface IEmailSenderRepository
    {
        Task ItemMaintenanceNotification();
    }
}
