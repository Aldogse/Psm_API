using Microsoft.EntityFrameworkCore;
using Model;
using Property_And_Supply_Management_API.Data;
using Property_And_Supply_Management_API.Interfaces;
using Property_And_Supply_Management_API.ResponseDTO;

namespace Property_And_Supply_Management_API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly PSMdbContext _dbContext;
        public DepartmentRepository(PSMdbContext pSMdbContext)
        {
            _dbContext = pSMdbContext;
        }

        public async Task<List<Department>> GetDepartmentAsync()
        {
           return await _dbContext.Departments.Include(i => i.Items).AsNoTracking().ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return  await _dbContext.Departments.Include(I => I.Items).Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<DepartmentMaintenanceReportResponse>> GetDepartmentItemsUnderMaintenance()
        {

            var departments_information = await _dbContext.Departments.Select(department => new DepartmentMaintenanceReportResponse
            {
                department_name = department.department_name,
                items_in_possesion = department.Items.Select(item => new ItemDetailsResponse
                {
                    item_name = item.item_name,
                    maintenance_date = item.maintenance_date.ToShortDateString(),                   
                }).ToList(),
            }).ToListAsync();

             return departments_information.ToList();
        }

        public async Task<string> GetDepartmentNameByIdAsync(int id)
        {
            var department = await _dbContext.Departments.Where(i => i.Id == id).FirstOrDefaultAsync();
            return  department.department_name;
        }

        public async Task<PaginatedResponse<DepartmentsDetailsRequest>> PaginatedDepartmentRequest(int current_page, int page_size)
        {
            IQueryable<Department> departments = _dbContext.Departments.AsQueryable();
            int count = await departments.CountAsync();

            var paginated_query = _dbContext.Departments.OrderBy(i => i.Id).Skip((current_page - 1) * page_size).Take(page_size);

            var response = paginated_query.Select(department => new DepartmentsDetailsRequest
            {
                Id = department.Id,
                department_name = department.department_name,
                items = department.Items.Select(item => new ItemInformation
                {
                    id = item.Id,
                    item_name = item.item_name,
                    date_added = item.date_added.ToShortDateString(),
                    maintenance_date = item.maintenance_date.ToShortDateString(),
                }).ToList(),
            }).ToList();

            return new PaginatedResponse<DepartmentsDetailsRequest>(current_page,count,response);
        }
    }
}
