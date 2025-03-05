using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Property_And_Supply_Management_API.RequestDTO
{
    public class ItemAddRequest
    {
        public string item_name { get; set; }
        public int department_id { get; set; }
    }
}
