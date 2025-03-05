using Model.enums;

namespace PAS_API.RequestDTO
{
    public class UpdateItemRequest
    {
        public string item_name { get; set; }
        public DateTime maintenance_date { get; set; }
        public Status Status { get; set; }
        public int department_id { get; set; }
        public DateTime last_modified_date { get; set; }
    }
}
