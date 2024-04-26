using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
	[Authorize]
	// Inheritance: DepartmentController Is A Controller
	// Compostion: DepartmentController Has A DepartmentRepository
	public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUniitOfWork _uniitOfWork;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper,IUniitOfWork uniitOfWork, IWebHostEnvironment env) //Ask CLR For Creation Of Object From Class Impelmenting "IDepartmentRepository"
        {
            _mapper = mapper;
            _uniitOfWork = uniitOfWork;
          
            _env = env;
        }

        // BaseUrl : Depatment/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _uniitOfWork.Repository<Department>().GetAllAsync();
            var MappedDeps = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDeps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(DepartmentViewModel departmentVm)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var MappedDep = _mapper.Map<DepartmentViewModel,Department>(departmentVm);
                 _uniitOfWork.Repository<Department>().Add(MappedDep);
                var count = await _uniitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(departmentVm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var department = await _uniitOfWork.Repository<Department>().GetAsync(id.Value);
            var MappedDep = _mapper.Map<Department, DepartmentViewModel>(department);


            if (department is null)
                return NotFound(); // 404

            return View(MappedDep);

        }

        [HttpGet]
        // /Department/Edit/10
        // /Department/Edit/
        public async Task<IActionResult> Edit(int? id)
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


            return await Details(id, "Edit");

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

                _uniitOfWork.Repository<Department>().Update(MappedEmp);
                _uniitOfWork.Complete();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVm)
        {
            try
            {
                var Mapped = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _uniitOfWork.Repository<Department>().Delete(Mapped);
                _uniitOfWork.Complete();
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
