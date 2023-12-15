using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using PL.Helpers;
using PL.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #region Index
        public async Task<IActionResult> Index(string SearchValue = "")
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = await unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = await unitOfWork.EmployeeRepository.Search(SearchValue);
            }
            var mappedEmployee = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmployee);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Images");
                var mappedEmployee = mapper.Map<Employee>(employeeViewModel);
                await unitOfWork.EmployeeRepository.Add(mappedEmployee);
                return RedirectToAction("Index");
            }
            return View(employeeViewModel);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await unitOfWork.EmployeeRepository.Get(id);
            var dapartmentName = await unitOfWork.EmployeeRepository.GetDepartmentByEmployee(id);
            var mappedEmployee = mapper.Map<EmployeeViewModel>(employee);


            if (employee == null)
                return NotFound();

            return View(mappedEmployee);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await unitOfWork.EmployeeRepository.Get(id);

            var mappedEmployee = mapper.Map<EmployeeViewModel>(employee);

            if (employee == null)
                return NotFound();

            return View(mappedEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeViewModel employeeViewModel)
        {

            if (ModelState.IsValid)
            {
                employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Images");

                var mappedEmployee = mapper.Map<Employee>(employeeViewModel);

                await unitOfWork.EmployeeRepository.Update(mappedEmployee);
                return RedirectToAction("Index");
            }
            return View(employeeViewModel);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
                return NotFound();
            DocumentSettings.DeleteFile("Images", employee.ImageUrl);
            await unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
