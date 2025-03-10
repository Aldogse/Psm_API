using System.Net.Http.Json;
using Model.Contracts;
using PSM_Frontend.Web.Interface;

namespace PSM_Frontend.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            var departments = await _httpClient.GetFromJsonAsync<IEnumerable<DepartmentDTO>>("Department/v1/get-departments");
            return departments;
        }
    }
}
