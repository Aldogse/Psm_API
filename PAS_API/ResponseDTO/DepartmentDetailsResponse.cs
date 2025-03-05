using Model;

namespace PAS_API.ResponseDTO
{
    public class DepartmentDetailsResponse
    {
        public string department_name { get; set; }
        public string departmental_email { get; set; }
        public List<ItemDetailsResponse> Items { get; set; }
    }
}
