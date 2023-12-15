using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        public DepartmentController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAll();
            var mappedDepartments= mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepartments);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<Department>(departmentViewModel);
                await unitOfWork.DepartmentRepository.Add(mappedDepartment);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await unitOfWork.DepartmentRepository.Get(id);

            if (department == null)
                return NotFound();

            var mappedDepartment=mapper.Map<DepartmentViewModel> (department);

            return View(mappedDepartment);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await unitOfWork.DepartmentRepository.Get(id);

            if (department == null)
                return NotFound();

            var mappedDepartment = mapper.Map<DepartmentViewModel>(department);


            return View(mappedDepartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<Department>(departmentViewModel);
                await unitOfWork.DepartmentRepository.Update(mappedDepartment);
                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await unitOfWork.DepartmentRepository.Get(id);
            if (department == null)
                return NotFound();

            await unitOfWork.DepartmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
