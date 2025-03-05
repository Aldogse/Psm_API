using Model;
using Property_And_Supply_Management_API.ResponseDTO;

namespace Property_And_Supply_Management_API.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<string> GetDepartmentNameByIdAsync(int id);
        Task<List<Department>> GetDepartmentAsync();
        Task<Department> GetDepartmentByIdAsync(int id);     
        Task<List<DepartmentMaintenanceReportResponse>>GetDepartmentItemsUnderMaintenance();
        Task<PaginatedResponse<DepartmentsDetailsRequest>> PaginatedDepartmentRequest(int current_page,int page_size);
    }
}
