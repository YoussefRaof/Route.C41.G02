using Route.C4.G02.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Route.C41.G02.PL.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max Length Of Name Is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length Of Name Is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Genderr { get; set; }
        public EmpType EmployeeType { get; set; }


      

        //[ForeignKey("DepartmentId")]
        public int? DepartmentId1 { get; set; } // Foregin Key Column

        [InverseProperty("Empolyees")]
        // Navigational Property => [ONE]
        public Department Department { get; set; }
    }
}
