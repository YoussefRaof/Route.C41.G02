using Route.C4.G02.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Route.C41.G02.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Required Ya Ofa !!")]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        // Navigational Propert [MANY]

        [InverseProperty("Department")]
        public ICollection<Empolyee> Empolyees { get; set; } = new HashSet<Empolyee>();
    }
}
