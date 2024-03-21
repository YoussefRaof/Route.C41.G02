using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;

namespace Route.C41.G02.PL.Controllers
{
    // Inheritance: DepartmentController Is A Controller
    // Compostion: DepartmentController Has A DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentsRepo;  //NULL



        public DepartmentController(IDepartmentRepository departmentsRepo) //Ask CLR For Creation Of Object From Class Impelmenting "IDepartmentRepository"
        {
            _departmentsRepo = departmentsRepo;
            
        }

        // BaseUrl : Depatment/Index
        public IActionResult Index()
        {
            return View();
        }

    }
}
