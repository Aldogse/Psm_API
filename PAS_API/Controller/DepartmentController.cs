using Microsoft.AspNetCore.Mvc;
using Model;
using PAS_API.Data;
using PAS_API.Interface;
using PAS_API.ResponseDTO;

namespace PAS_API.Controller
{
    [Route("Department/v1/")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly PAS_API_DBContext _dbContext;

        public DepartmentController(IDepartmentRepository departmentRepository, PAS_API_DBContext dbContext)
        {
            _departmentRepository = departmentRepository;
            _dbContext = dbContext;
        }

        [HttpGet("get-departments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
               List<Department>departments =  await _departmentRepository.GetDepartmentsAsync();
               List<DepartmentDetailsResponse> response = departments.Select(department => new DepartmentDetailsResponse
               {
                   department_name = department.department_name,
                   departmental_email = department.departmental_email,
                   Items = department.Items.Select(item => new ItemDetailsResponse
                   {
                       item_id = item.Id,
                       item_name = item.item_name,
                       department_name = item.Department.department_name,
                       status = item.Status.ToString(),
                       maintenance_date = item.maintenance_date.ToShortDateString(),
                       date_added = item.date_added.ToShortDateString(),
                   }).ToList(),
               }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpGet("get-paginated-department-response")]
        public async Task<IActionResult> PaginatedDepartmentRequest([FromQuery] int page_size, [FromQuery]int current_page)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _departmentRepository.PaginatedDepartmentRequestAsync(page_size, current_page);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error: {ex.Message}");
            }
        }
    }
}
