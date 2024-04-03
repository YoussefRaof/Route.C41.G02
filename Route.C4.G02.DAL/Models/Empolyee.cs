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
        

        
   
        public string Name { get; set; }

    
        public int? Age { get; set; }

      
        public string Address { get; set; }

      
        public decimal Salary { get; set; }

       
        public bool IsActive { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       

        public string PhoneNumber { get; set; }

     
        public DateTime HiringDate { get; set; }

        public Gender Genderr { get; set; }
        public EmpType EmployeeType { get; set; }


        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public string ImageName { get; set; }

        //[ForeignKey("DepartmentId")]
        public int? DepartmentId1 { get; set; } // Foregin Key Column

        [InverseProperty("Empolyees")]
        // Navigational Property => [ONE]
        public Department Department { get; set; }

    }
}
