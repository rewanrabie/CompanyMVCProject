using System.ComponentModel.DataAnnotations;

namespace MVC_3.ViewModels.Departments
{
    public class CreateDepartment
    {
        [Required(ErrorMessage = "Code is Required !")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "DateOfCreation is Required !")]
        public DateTime DateOfCreation { get; set; }
    }
}
