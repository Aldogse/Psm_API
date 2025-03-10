using Microsoft.AspNetCore.Components;
using Model.Contracts;
using PSM_Frontend.Web.Interface;

namespace PSM_Frontend.Web.BaseClasses
{
    public class DepartmentBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        public IEnumerable<DepartmentDTO> Departments { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetDepartments();
        }
    }
}
