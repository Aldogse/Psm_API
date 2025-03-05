using Model;
using Property_And_Supply_Management_API.ResponseDTO;

namespace Property_And_Supply_Management_API.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItemAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<List<Item>> GetItemUnderMaintenanceAsync();
        Task<PaginatedResponse<ItemDetailsResponse>> paginated_item_responseAsync(int current_page,int page_size);
        Task<string> GetDepartmentEmailByItemIdAsync(int id);
        Task<List<int?>>GetItemOwnersAsync();

    }
}
