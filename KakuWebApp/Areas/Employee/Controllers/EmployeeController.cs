using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KakuWebApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using KakuWebApp.Areas.Employee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace KakuWebApp.Areas.Employee.Controllers
{

    [Area("Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            this._employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _employeeService.GetAllEmployee();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(Models.Employee employee, IFormFile profile)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (profile.Length > 0)
            {
                byte[] p1 = null;

                using (var fs1 = profile.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();

                    employee.profile = p1;
                }
            }

            await _employeeService.SaveEmployee(employee);

            return RedirectToAction(nameof(Index));
        }
    }
}