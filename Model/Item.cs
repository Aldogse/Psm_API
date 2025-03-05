using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.enums;

namespace Model
{
    public class Item 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string item_name { get; set; }

        [ForeignKey("Department")]
        [Required]
        public int? department_id { get; set; }
        public Department? Department { get; set; }
        public DateTime date_added { get; set; }
        public DateTime last_modified_date { get; set; }
        public DateTime maintenance_date { get; set; }
        public Status Status { get; set; }  


    }
}
