namespace Property_And_Supply_Management_API.ResponseDTO
{
    public class PaginatedResponse <T>
    {
        public PaginatedResponse(int current_page,int total_count,List<T> page_items)
        {

            Current_Page = current_page;
            Total_Count = total_count;
            Page_Items = page_items;
        }

        public int Current_Page { get; }
        public int Total_Count { get; }
        public List<T> Page_Items { get; }
    }
}
 