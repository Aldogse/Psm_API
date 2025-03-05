using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.enums;
using Property_And_Supply_Management_API.Data;
using Property_And_Supply_Management_API.Interfaces;
using Property_And_Supply_Management_API.RequestDTO;
using Property_And_Supply_Management_API.ResponseDTO;

namespace Property_And_Supply_Management_API.Controllers
{
    [ApiController]
    [Route("Item/v1/")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly PSMdbContext _pSMdbContext;
        private readonly IDepartmentRepository _departmentRepository;

        public ItemController(IItemRepository itemRepository, PSMdbContext pSMdbContext, IDepartmentRepository departmentRepository)
        {
            _itemRepository = itemRepository;
            _pSMdbContext = pSMdbContext;
            _departmentRepository = departmentRepository;
        }

        [HttpGet("get-all-items")]
        public async Task<IActionResult> Get_items() 
        {
            try
            {
                List<Item>items = await _itemRepository.GetAllItemAsync();
                List<ItemDetailsResponse> item_details = new List<ItemDetailsResponse>();

                if(items == null || items.Count == 0)
                {
                    return StatusCode(200, "No item stored");
                }

                foreach(var item in items)
                {
                    var responses = new ItemDetailsResponse
                    {
                        item_name = item.item_name,
                        department = item.Department.department_name,
                        date_added = item.date_added.ToShortDateString(),
                        maintenance_date = item.date_added.ToShortDateString(),
                        Status = item.Status.ToString(),
                    };
                    item_details.Add(responses);
                }
                return Ok(item_details);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error : {ex.Message}");
            }
        }

        [HttpGet("get-item-by-item_id/{id}")]
        public async Task <IActionResult> Get_Item_byId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Item item = await _itemRepository.GetItemByIdAsync(id);

                if(item == null)
                {
                    return NotFound("Item cannot be found");
                }

                var response = new ItemDetailsResponse
                {
                    item_name = item.item_name,
                    department = item.Department.department_name,
                    date_added = item.date_added.ToShortDateString(),
                    maintenance_date = item.maintenance_date.ToShortDateString(),
                    Status = item.Status.ToString(),
                };
                return Ok(response);    
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal Server Error {ex.Message}");
            }
        }
        [HttpPost("add-item")]
        public async Task <IActionResult> add_item([FromBody] ItemAddRequest addRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Item add_item = new Item
                {
                    item_name = addRequest.item_name,
                    department_id = addRequest.department_id,
                    maintenance_date = DateTime.Now.AddDays(45),
                    last_modified_date  = DateTime.Now,
                    date_added = DateTime.Now,
                    Status = Model.enums.Status.Available
                };
                _pSMdbContext.Items.Add(add_item);
                await _pSMdbContext.SaveChangesAsync();

                return Ok("Item Added");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal Server Error {ex.Message}");
            }
        }
        [HttpDelete("delete-item/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Item item = await _pSMdbContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if(item == null)
            {
                return NotFound();
            }

            _pSMdbContext.Remove(item);
            await _pSMdbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("update-item/{id}")]
        public async Task <IActionResult> update_itemAsync(int id, [FromBody] ItemUpdateRequest itemUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Item item_to_update = await _itemRepository.GetItemByIdAsync(id);

                if(item_to_update == null )
                {
                    return NotFound("Item can not be found");
                }

                item_to_update.item_name = itemUpdateRequest.item_name;
                item_to_update.department_id = itemUpdateRequest.department_id;
                item_to_update.Status = itemUpdateRequest.Status;
                item_to_update.date_added = itemUpdateRequest.date_added;

                 _pSMdbContext.Update(item_to_update);
                 await _pSMdbContext.SaveChangesAsync();


                var response = new ItemDetailsResponse
                {
                    item_name = itemUpdateRequest.item_name,
                    department = await _departmentRepository.GetDepartmentNameByIdAsync(itemUpdateRequest.department_id),
                    date_added = itemUpdateRequest.date_added.ToShortDateString(),
                    maintenance_date = itemUpdateRequest.maintenance_date.ToShortDateString(),
                    Status = itemUpdateRequest.Status.ToString(),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpGet("paginated-item-response")]
        public async Task<IActionResult> paginated_item_response([FromQuery] int page_size, [FromQuery] int current_page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _itemRepository.paginated_item_responseAsync(page_size, current_page);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("item-owner-under-maintenance")]
        public async Task<IActionResult> GetOwnerItemsUnderMaintenance()
        {
            try
            {
                var departments_id = await _itemRepository.GetItemOwnersAsync(); 

                if(departments_id == null || departments_id.Count == 0)
                {
                    return StatusCode(200,"No departments posses any item under maintenance");
                }

                return Ok(departments_id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"internal server response{ex.Message}");
            }
        }
    }
}
