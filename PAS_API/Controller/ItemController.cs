using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using PAS_API.Data;
using PAS_API.Interface;
using PAS_API.RequestDTO;
using PAS_API.ResponseDTO;

namespace PAS_API.Controller
{
    [Route("Item/v1/")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly PAS_API_DBContext _dbContext;
        public ItemController(IItemRepository itemRepository, PAS_API_DBContext dbcontext)
        {
            _itemRepository = itemRepository;
            _dbContext = dbcontext;
        }

        [HttpGet("get-all-items")]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                List<Item> items = await _itemRepository.GetAllItemsAsync();
                if(items.Count == 0 || items == null)
                {
                    return Ok("No item stored");
                }

                List<ItemDetailsResponse> response = items.Select(item => new ItemDetailsResponse
                {
                    item_id = item.Id,
                    item_name = item.item_name,
                    maintenance_date = item.maintenance_date.ToShortDateString(),
                    date_added = item.date_added.ToShortDateString(),
                    department_name = item.Department.department_name,
                    status = item.Status.ToString(),
                }).ToList();

                return Ok(response);  
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server Error : {ex.Message}");
            }
        }

        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var item = await _itemRepository.GetItemByIdAsync(id);

                if(item == null) 
                {
                    return NotFound();
                }

                var response = new ItemDetailsResponse()
                {
                    item_id = item.Id,
                    item_name= item.item_name,
                    department_name = item.Department.department_name,
                    status = item.Status.ToString(),
                    maintenance_date = item.maintenance_date.ToShortDateString(),
                    date_added = item.date_added.ToShortDateString(),
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("paginated-response")]
        public async Task<IActionResult> paginated_item_request([FromQuery]int page_size, [FromQuery]int current_page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var paginated_response = await _itemRepository.paginated_response(page_size,current_page);
                return Ok(paginated_response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }



        //CUD OPERATIONS
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemAsync([FromBody]AddItemRequest addItemRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Item new_item = new Item()
                {
                    item_name = addItemRequest.item_name,
                    department_id = addItemRequest.department,
                    maintenance_date = DateTime.Now.AddDays(45),
                    date_added = DateTime.Now,
                    last_modified_date = DateTime.Now,
                };
                await _dbContext.AddAsync(new_item);
                await _dbContext.SaveChangesAsync();

                return Ok($"{new_item.item_name} added successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error:{ex.Message}");
            }
        }

        [HttpDelete("delete-item/{id}")]
        public async Task <IActionResult> DeleteItem(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var item_to_delete = await _dbContext.Items.Where(x => x.Id == id).FirstOrDefaultAsync();

                 if(item_to_delete == null)
                 {
                    return NotFound();
                 }
                 _dbContext.Remove(item_to_delete);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error: {ex.Message}");
            }
        }

       [HttpPut("update-item")]
       public async Task <IActionResult> UpdateItem([FromQuery]int id, [FromBody] UpdateItemRequest updateItemRequest)
       {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Item item_to_update = await _itemRepository.GetItemByIdAsync(id);

                if(item_to_update == null)
                {
                    return NotFound();
                }

                item_to_update.item_name = updateItemRequest.item_name;
                item_to_update.department_id = updateItemRequest.department_id;
                item_to_update.maintenance_date = updateItemRequest.maintenance_date;
                item_to_update.Status = updateItemRequest.Status;
                item_to_update.last_modified_date = DateTime.UtcNow;

                 _dbContext.Update(item_to_update);
                await _dbContext.SaveChangesAsync();
                return Ok("Item updated Successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error : {ex.Message}");
            }
       }
        
    }
}
