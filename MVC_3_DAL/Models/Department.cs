using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3_DAL.Models
{
    public class Department : BaseEntity
    {
        public int Id { get; set; } //0
        [Required (ErrorMessage ="Code Is Required!")]
        public string Code { get; set; } //null

        [Required(ErrorMessage = "Name Is Required!")]
        public string Name { get; set; } //null 
   
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        // public ICollection<Employee>? Employees { get; set; }
        public List<Employee> Employees { get; set; } //
    }
}
