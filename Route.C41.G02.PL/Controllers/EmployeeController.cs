using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using System;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeesRepo;  //NULL
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeesRepo, IWebHostEnvironment env) //Ask CLR For Creation Of Object From Class Impelmenting "IDepartmentRepository"
        {
            _employeesRepo = employeesRepo;
            _env = env;
        }

        public IActionResult Index()
        {
            TempData.Keep();
            // Binding Through View Dictionary : Transfer Extra Data From Action To Viee [One Way]

            // 1. ViewData
            ViewData["Message"] = "Hello From Index";

            ViewBag.Message = "Hello View Bag";

            var departments = _employeesRepo.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Empolyee empolyee)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var count = _employeesRepo.Add(empolyee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Successfully";
                   
                }
                else
                {
                    TempData["Message"] = "Error, Employee Is Not Created :(";
                    
                }
                return RedirectToAction("Index");


            }
            return View(empolyee);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var empolyee = _employeesRepo.Get(id.Value);

            if (empolyee is null)
                return NotFound(); // 404

            return View(empolyee);

        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {



            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Empolyee empolyee)
        {
            if (id != empolyee.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(empolyee);

            try
            {
                _employeesRepo.Update(empolyee);
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                // 1. Log Exeception
                // 2.Show Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, Ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured During Updating Employee");


                return View(empolyee);

            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public IActionResult Delete(Empolyee empolyee)
        {
            try
            {
                _employeesRepo.Delete(empolyee);
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                // 1. Log Exeception
                // 2.Show Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, Ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured During Deleting Department");


                return View(empolyee);

            }
        }
    }
}
