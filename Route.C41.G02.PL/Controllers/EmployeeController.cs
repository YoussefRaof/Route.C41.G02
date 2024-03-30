using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Hosting;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUniitOfWork _uniitOfWork;
        //private readonly IEmployeeRepository _employeesRepo;  //NULL
        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IMapper mapper,
            IUniitOfWork uniitOfWork,
            //IEmployeeRepository employeesRepo,
            /*IDepartmentRepository departmentRepo, */
            IWebHostEnvironment env) 
            //Ask CLR For Creation Of Object From Class Impelmenting "IDepartmentRepository"
        {
            _mapper = mapper;
            _uniitOfWork = uniitOfWork;
            //_employeesRepo = employeesRepo;
            //_departmentRepo = departmentRepo;
            _env = env;
        }

        public IActionResult Index(string SearchInp)
        {
            TempData.Keep();
            // Binding Through View Dictionary : Transfer Extra Data From Action To Viee [One Way]

            // 1. ViewData
            //ViewData["Message"] = "Hello From Index";

            //ViewBag.Message = "Hello View Bag";
            var empolyees = Enumerable.Empty<Empolyee>();
            if (string.IsNullOrEmpty(SearchInp))
                empolyees = _uniitOfWork.EmployeeRepository.GetAll();


            else
                empolyees = _uniitOfWork.EmployeeRepository.SearchByName(SearchInp.ToLower());

            var MappedEmps = _mapper.Map<IEnumerable<Empolyee>, IEnumerable<EmployeeViewModel>>(empolyees);

            return View(MappedEmps);


        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll(); ;
            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeViewModel empolyeeVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {

                // Manual Mapping

                /// var mappedEmp = new Empolyee()
                /// {
                ///     Name = empolyeeVM.Name,
                ///     Age = empolyeeVM.Age,
                ///     Address = empolyeeVM.Address,
                ///     Salary = empolyeeVM.Salary,
                ///     Email = empolyeeVM.Email,
                ///     PhoneNumber = empolyeeVM.PhoneNumber,
                ///     IsActive = empolyeeVM.IsActive,
                ///     HiringDate =  empolyeeVM.HiringDate,
                ///
                ///
                /// };
                /// 







                var mappedEmp = _mapper.Map<EmployeeViewModel, Empolyee>(empolyeeVM);


                 _uniitOfWork.EmployeeRepository.Add(mappedEmp);
                var count = _uniitOfWork.Complete();
                if (count > 0)
                {

                    //1. Update Project


                    //2. Delete Department
                    //_uniitOfWork.DepartmentRepository.Delete()

                    

                    return RedirectToAction("Index");
                }



            }
            return View(empolyeeVM);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var empolyee = _uniitOfWork.EmployeeRepository.Get(id.Value);
            var mappedEmp = _mapper.Map<Empolyee, EmployeeViewModel>(empolyee);

            if (empolyee is null)
                return NotFound(); // 404

            return View(mappedEmp);

        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll(); ;


            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel empolyeeVm)
        {
            if (id != empolyeeVm.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(empolyeeVm);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Empolyee>(empolyeeVm);
                _uniitOfWork.EmployeeRepository.Update(mappedEmp);
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
                    ModelState.AddModelError(string.Empty, "Error Occured During Updating Employee");


                return View(empolyeeVm);

            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel empolyeeVm)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Empolyee>(empolyeeVm);


                _uniitOfWork.EmployeeRepository.Delete(mappedEmp);
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


                return View(empolyeeVm);

            }
        }
    }
}
