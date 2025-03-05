using Model;
using PAS_API.ResponseDTO;

namespace PAS_API.Interface
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int item_id);
        Task<List<Item>> GetItemsUnderMaintenanceAsync();
        Task <pagination_model<ItemDetailsResponse>>paginated_response(int page_size,int current_page);
    }
}
