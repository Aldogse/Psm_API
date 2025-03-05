namespace PAS_API
{
    public class pagination_model <T>
    {
        public pagination_model(int page_size, int current_page, int total_count, List<T> Page_items)
        {
            this.page_size = page_size;
            this.current_page = current_page;
            this.total_count = total_count;
            page_items = Page_items;
        }

        public int page_size { get; set; }
        public int current_page { get; set; }
        public int total_count { get; set; }
        public List<T> page_items { get; set; }
       
    }
}
