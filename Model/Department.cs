using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class Department
    {
        [Key]
        public int Id { get; set; } 
        public string department_name { get; set; }
        public string departmental_email { get; set; }
        public ICollection<Item> Items { get; set; }
        
    }
}
