using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using System;

namespace Route.C41.G02.PL.Controllers
{
    // Inheritance: DepartmentController Is A Controller
    // Compostion: DepartmentController Has A DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentsRepo;  //NULL
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentsRepo, IWebHostEnvironment env) //Ask CLR For Creation Of Object From Class Impelmenting "IDepartmentRepository"
        {
            _departmentsRepo = departmentsRepo;
            _env = env;
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
            if (ModelState.IsValid) // Server Side Validation
            {
                var count = _departmentsRepo.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var department = _departmentsRepo.Get(id.Value);

            if (department is null)
                return NotFound(); // 404

            return View(department);

        }

        [HttpGet]
        // /Department/Edit/10
        // /Department/Edit/
        public IActionResult Edit(int? id)
        {
            ///if(!id.HasValue)
            ///    return BadRequest();
            ///
            ///var department = _departmentsRepo.Get(id.Value);
            ///
            ///if (department is null)
            ///    return NotFound(); // 404
            ///
            ///return View(department);
            

            return Details(id, "Edit");

        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentsRepo.Update(department);
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                // 1. Log Exeception
                // 2.Show Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, Ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured During Updating Department");


                return View(department);
                
            }
        }



    }
}
