namespace PAS_API.ResponseDTO
{
    public class ItemDetailsResponse
    {
        public int item_id {  get; set; }
        public string item_name { get; set; }
        public string maintenance_date { get; set; }
        public string date_added { get; set; }
        public string department_name { get; set; }
        public string status { get; set; }
    }
}
