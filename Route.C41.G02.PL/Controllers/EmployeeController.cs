using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Hosting;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.PL.Helpers;
using Route.C41.G02.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
	[Authorize]
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

         public async Task <IActionResult> Index(string SearchInp)
        {
            TempData.Keep();
            // Binding Through View Dictionary : Transfer Extra Data From Action To Viee [One Way]

            // 1. ViewData
            //ViewData["Message"] = "Hello From Index";

            //ViewBag.Message = "Hello View Bag";
            var empolyees = Enumerable.Empty<Empolyee>();
            var employeeRepo = _uniitOfWork.Repository<Empolyee>() as EmployeeRepository;
            if (string.IsNullOrEmpty(SearchInp))
                empolyees = await employeeRepo.GetAllAsync();


            else
                empolyees = employeeRepo.SearchByName(SearchInp.ToLower());

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

        public async Task<IActionResult> Create(EmployeeViewModel empolyeeVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                empolyeeVM.ImageName = await DocumentSetting.UploadFile(empolyeeVM.Image, "images");


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


                _uniitOfWork.Repository<Empolyee>().Add(mappedEmp);
                var count = await _uniitOfWork.Complete();
                if (count > 0)
                {

                    //1. Update Project


                    //2. Delete Department
                    //_uniitOfWork.Repository<Department>().Delete()



                    return RedirectToAction("Index");
                }


                
            }
            return View(empolyeeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var empolyee = await _uniitOfWork.Repository<Empolyee>().GetAsync(id.Value);
            //var mappedEmp = _mapper.Map<Empolyee, EmployeeViewModel>(empolyee);

            if (viewname.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = empolyee.ImageName;

            if (empolyee is null)
                return NotFound(); // 404

            return View(viewname, _mapper.Map<Empolyee, EmployeeViewModel>(empolyee));

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll(); ;


            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel empolyeeVm)
        {
            if (id != empolyeeVm.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(empolyeeVm);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Empolyee>(empolyeeVm);
                _uniitOfWork.Repository<Empolyee>().Update(mappedEmp);
               await _uniitOfWork.Complete();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel empolyeeVm)
        {
            try
            {
                empolyeeVm.ImageName = TempData["ImageName"] as string;
                var mappedEmp = _mapper.Map<EmployeeViewModel, Empolyee>(empolyeeVm);


                _uniitOfWork.Repository<Empolyee>().Delete(mappedEmp);
                var count = await _uniitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSetting.DeleteFile(empolyeeVm.ImageName, "images");
                    return RedirectToAction("Index");

                }
                return View(empolyeeVm);
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
