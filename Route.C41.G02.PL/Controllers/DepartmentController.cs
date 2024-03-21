using Microsoft.AspNetCore.Mvc;
using Route.C4.G02.DAL.Models;
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
            var departments = _departmentsRepo.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) // Server Side Validation
            {
                var count = _departmentsRepo.Add(department);
                if(count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(department);
        }


    }
}
