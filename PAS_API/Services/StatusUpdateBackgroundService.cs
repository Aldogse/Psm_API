using Model;
using PAS_API.Data;
using PAS_API.Interface;

namespace PAS_API.Services
{
    public class StatusUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public StatusUpdateBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var item_repo = scope.ServiceProvider.GetService<IItemRepository>();
                        var dbContext = scope.ServiceProvider.GetRequiredService<PAS_API_DBContext>();
                        await ItemUnderMaintenanceStatusUpdate(dbContext,item_repo);
                        await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task ItemUnderMaintenanceStatusUpdate(PAS_API_DBContext dbContext,IItemRepository? _itemRepository)
        {
            try
            {
                List<Item>? item_under_maintenance = await _itemRepository.GetItemsUnderMaintenanceAsync();

                if(!item_under_maintenance.Any())
                {
                    return;
                }

                foreach (var item in item_under_maintenance)
                {
                    item.Status = Model.enums.Status.UnderMaintenance;
                    dbContext.Update(item);
                }
                await dbContext.SaveChangesAsync();
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
