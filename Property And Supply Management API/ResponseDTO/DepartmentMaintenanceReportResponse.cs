using Model;

namespace Property_And_Supply_Management_API.ResponseDTO
{
    public class DepartmentMaintenanceReportResponse
    {
        public string department_name {  get; set; }
        public List<ItemDetailsResponse> items_in_possesion { get; set; }
    }
}
