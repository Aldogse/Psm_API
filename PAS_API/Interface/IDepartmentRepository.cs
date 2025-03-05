using Model;
using PAS_API.ResponseDTO;

namespace PAS_API.Interface
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<List<Department>> GetDepartmentsAsync();
        Task<pagination_model<DepartmentDetailsResponse>> PaginatedDepartmentRequestAsync(int page_size,int current_page);
    }
}
