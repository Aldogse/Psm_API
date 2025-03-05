using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model;
using Model.enums;

namespace Property_And_Supply_Management_API.RequestDTO
{
    public class ItemUpdateRequest
    {
        [Required]
        public string item_name { get; set; }

        [Required]
        public int department_id { get; set; }
        public DateTime date_added { get; set; }
        public DateTime maintenance_date { get; set; }
        public Status Status { get; set; }
    }
}
