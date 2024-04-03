using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C4.G02.DAL.Models
{
    public class Department : ModelBase
    {
        //public int Id { get; set; }
        
        public int Code { get; set; }

        public string Name { get; set; }

       
        public DateTime DateOfCreation { get; set; }

        // Navigational Propert [MANY]

        [InverseProperty("Department")]
        public ICollection<Empolyee> Empolyees { get; set; } = new HashSet<Empolyee>(); 
    }
}
