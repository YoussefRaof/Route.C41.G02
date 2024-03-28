using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.C4.G02.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,

       [EnumMember(Value = "Female")]
        Female = 2
    }

    public enum EmpType
    {
        [EnumMember(Value = "FullTime")]
        FullTime = 1,

        [EnumMember(Value = "PartTime")]
        PartTime = 2
    }
    public class Empolyee:ModelBase
    {
        

        [Required]
        [MaxLength(50, ErrorMessage = "Min Length Of Name Is 5 Chars")]
        [MinLength(5, ErrorMessage = "Max Length Of Name Is 50 Chars")]
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


        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        //[ForeignKey("DepartmentId")]
        public int? DepartmentId1 { get; set; } // Foregin Key Column

        [InverseProperty("Empolyees")]
        // Navigational Property => [ONE]
        public Department Department { get; set; }

    }
}
