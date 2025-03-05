using Model;

namespace Property_And_Supply_Management_API.ResponseDTO
{
    public class DepartmentsDetailsRequest
    {
        public int Id { get; set; }
        public string department_name { get; set; }
        public string department_emal { get; set; }
        public List<ItemInformation> items { get; set; }
    }
}
