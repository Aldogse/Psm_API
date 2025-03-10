using Model.Contracts;

namespace PSM_Frontend.Web.Interface
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetDepartments();
    }
}
