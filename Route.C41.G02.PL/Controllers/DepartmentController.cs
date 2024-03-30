using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace Route.C41.G02.PL.Controllers
{
    // Inheritance: DepartmentController Is A Controller
    // Compostion: DepartmentController Has A DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentsRepo;  //NULL
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper,IDepartmentRepository departmentsRepo, IWebHostEnvironment env) //Ask CLR For Creation Of Object From Class Impelmenting "IDepartmentRepository"
        {
            _mapper = mapper;
            _departmentsRepo = departmentsRepo;
            _env = env;
        }

        // BaseUrl : Depatment/Index
        public IActionResult Index()
        {
            var departments = _departmentsRepo.GetAll();
            var MappedDeps = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDeps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(DepartmentViewModel departmentVm)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var MappedDep = _mapper.Map<DepartmentViewModel,Department>(departmentVm);
                var count = _departmentsRepo.Add(MappedDep);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(departmentVm);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var department = _departmentsRepo.Get(id.Value);
            var MappedDep = _mapper.Map<Department, DepartmentViewModel>(department);


            if (department is null)
                return NotFound(); // 404

            return View(MappedDep);

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
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVm)
        {
            if (id != departmentVm.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(departmentVm);

            try
            {
                var MappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVm);

                _departmentsRepo.Update(MappedEmp);
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

                
                return View(departmentVm);

            }
        }

        //: /Department/Delete/10
        //: /Department/Delete/
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVm)
        {
            try
            {
                var Mapped = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _departmentsRepo.Delete(Mapped);    
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


                return View(departmentVm);

            }
        }


    }
}
