using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Property_And_Supply_Management_API.Data;
using Property_And_Supply_Management_API.Interfaces;
//using Property_And_Supply_Management_API.Pagination;
using Property_And_Supply_Management_API.ResponseDTO;

namespace Property_And_Supply_Management_API.Controllers
{
    [Route("Department/v1/")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly PSMdbContext _psmsDbContext;

        public DepartmentController(IDepartmentRepository departmentRepository, PSMdbContext pSMdbContext)
        {
            _departmentRepository = departmentRepository;
            _psmsDbContext = pSMdbContext;

        }

        [HttpGet("get-all-departments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
              //Create a list for fetch all the data from department table
              List<Department> departments = await _departmentRepository.GetDepartmentAsync();

                if(departments == null || departments.Count == 0)
                {
                    return NotFound("No departments showing on the database");
                }

                //create a new list of response that will be transmitted to the client
                //our approach is to transform all departments data to a department details request 
                List<DepartmentsDetailsRequest>department_details = departments.Select(department => new DepartmentsDetailsRequest
                {
                    Id = department.Id,
                    department_name = department.department_name,
                    department_emal = department.departmental_email,
                    // in item we will use select again to transform each item details to the one that we want them to see 
                    items = department.Items.Select(item => new ItemInformation
                    {
                        id = item.Id,
                        item_name = item.item_name,
                        date_added = item.date_added.ToShortDateString(),
                        maintenance_date = item.maintenance_date.ToShortDateString(),
                    }).ToList()
                }).ToList();
                //return the value on the client
                return Ok(department_details);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }           
        }
        [HttpGet("paginated-department-request")]
        public async Task<IActionResult> paginated_request([FromQuery]int current_page, [FromQuery]int page_size)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var paginated_response = await _departmentRepository.PaginatedDepartmentRequest(current_page, page_size);
                return Ok(paginated_response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
