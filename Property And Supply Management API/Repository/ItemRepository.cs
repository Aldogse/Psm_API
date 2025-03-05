using Microsoft.EntityFrameworkCore;
using Model;
using Property_And_Supply_Management_API.Data;
using Property_And_Supply_Management_API.Interfaces;
using Property_And_Supply_Management_API.ResponseDTO;

namespace Property_And_Supply_Management_API.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly PSMdbContext _pSMdbContext;
        public ItemRepository(PSMdbContext pSMdbContext)
        {
            _pSMdbContext = pSMdbContext;
        }
        public async Task<List<Item>> GetAllItemAsync()
        {
            return await _pSMdbContext.Items.Include(d => d.Department).AsNoTracking().ToListAsync();
        }

        public async Task<string> GetDepartmentEmailByItemIdAsync(int id)
        {
            var item = await _pSMdbContext.Items.Where(i => i.Id == id).Include(d => d.Department).FirstOrDefaultAsync();

            if(item.Department == null)
            {
                throw new Exception("Department id cannot be found");
            }
            return  item.Department.departmental_email;
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _pSMdbContext.Items.Include(d => d.Department).Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<int?>> GetItemOwnersAsync()
        {
            var items_under_maintenance = await GetItemUnderMaintenanceAsync();
            List<int?> item_owners = new List<int?>();
            foreach(var item in items_under_maintenance)
            {
                item_owners.Add(item.department_id);
            }

            return  item_owners.ToList();
        }

        public async Task<List<Item>> GetItemUnderMaintenanceAsync()
        {
            return await _pSMdbContext.Items.Where(i => i.maintenance_date <= DateTime.Today).Include(d => d.Department).ToListAsync();
        }

        public async Task<PaginatedResponse<ItemDetailsResponse>> paginated_item_responseAsync(int current_page, int page_size)
        {
            IQueryable<Item> items = _pSMdbContext.Items.AsQueryable();
            //count the number of items in the db
            int total_count = await items.CountAsync();

            var pagination_query = items.OrderBy(i => i.Id).Skip((current_page - 1) * page_size).Take(page_size);

            var response = await pagination_query.Select(item => new ItemDetailsResponse
            {
                item_name = item.item_name,
                department = item.Department.department_name,
                date_added = item.date_added.ToShortDateString(),
                maintenance_date = item.maintenance_date.ToShortDateString(),
                Status = item.Status.ToString(),
            }).ToListAsync();

            return new PaginatedResponse<ItemDetailsResponse>(current_page,total_count,response);
        }
    }
}
