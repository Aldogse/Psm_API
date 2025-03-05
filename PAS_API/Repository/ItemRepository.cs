using Microsoft.EntityFrameworkCore;
using Model;
using PAS_API.Data;
using PAS_API.Interface;
using PAS_API.ResponseDTO;

namespace PAS_API.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly PAS_API_DBContext _dbcontext;
        public ItemRepository(PAS_API_DBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _dbcontext.Items.Include(d => d.Department).ToListAsync();           
        }

        public async Task<Item> GetItemByIdAsync(int item_id)
        {
            return await _dbcontext.Items.Where(item => item.Id == item_id).Include(d => d.Department).FirstOrDefaultAsync();
        }

        public async Task<List<Item>> GetItemsUnderMaintenanceAsync()
        {
            return await _dbcontext.Items.Where(item => item.maintenance_date <= DateTime.Today).ToListAsync();
        }

        public async Task<pagination_model<ItemDetailsResponse>> paginated_response(int page_size, int current_page)
        {
            IQueryable<Item> items = _dbcontext.Items.AsQueryable();

            int total_count = await items.CountAsync();

            var pagination_query = items.OrderBy(item => item.Id).Skip((current_page - 1) * page_size).Take(page_size);

            var paginated_response = await pagination_query.Select(item => new ItemDetailsResponse
            {
                item_name = item.item_name,
                department_name = item.Department.department_name,
                maintenance_date = item.maintenance_date.ToShortDateString(),
                status = item.Status.ToString(),
                date_added = item.date_added.ToShortDateString()
            }).ToListAsync();

            return new pagination_model<ItemDetailsResponse>(page_size,current_page,total_count,paginated_response);
        }
    }
}
