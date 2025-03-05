using Microsoft.EntityFrameworkCore;
using Model;
using PAS_API.Data;
using PAS_API.Interface;
using PAS_API.ResponseDTO;

namespace PAS_API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly PAS_API_DBContext _dbContext;
        public DepartmentRepository(PAS_API_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _dbContext.Departments.Where(x => x.Id == id).Include(i => i.Items).FirstOrDefaultAsync();
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _dbContext.Departments.Include(i => i.Items).ToListAsync();
        }

        public async Task<pagination_model<DepartmentDetailsResponse>> PaginatedDepartmentRequestAsync(int page_size, int current_page)
        {
            try
            {
                IQueryable<Department> departments = _dbContext.Departments.AsQueryable();
                int total_count = await departments.CountAsync();

                var paginated_query = departments.OrderBy(x => x.Id).Skip((current_page - 1) * page_size).Take(page_size);

                var response = await paginated_query.Select(department => new DepartmentDetailsResponse
                {
                    department_name = department.department_name,
                    departmental_email = department.departmental_email,
                    Items = department.Items.Select(item => new ItemDetailsResponse
                    {
                        item_id = item.Id,
                        item_name = item.item_name,
                        maintenance_date = item.maintenance_date.ToShortDateString(),
                        date_added = item.date_added.ToShortDateString(),
                        department_name = item.Department.department_name,
                        status = item.Status.ToString(),
                    }).ToList(),
                }).ToListAsync();

                return new pagination_model<DepartmentDetailsResponse>(page_size,current_page,total_count,response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
