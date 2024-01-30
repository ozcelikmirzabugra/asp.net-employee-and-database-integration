using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employees.Data;
using employees.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace employees.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var employees = _dbContext.Employees.ToList();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee); // Return the view with validation errors if the model state is not valid
        }
        public IActionResult Edit(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _dbContext.Update(employee);
                _dbContext.SaveChanges();
            }
            return View(employee);
        }
        public IActionResult Delete(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id, Employee employee)
        {
            if(employee == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }
    }
}

