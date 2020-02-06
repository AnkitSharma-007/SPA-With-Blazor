using System;
using System.Collections.Generic;
using BlazorSPA.Server.Interface;
using BlazorSPA.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorSPA.Server.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

        private readonly IEmployee objemployee;

        public EmployeeController(IEmployee _objemployee)
        {
            objemployee = _objemployee;
        }

        // To Fetch all employee records
        [HttpGet]
        [Route("Index")]
        public IEnumerable<Employee> Index()
        {
            return objemployee.GetAllEmployees();
        }

        // To Create a new employee record
        [HttpPost]
        [Route("Create")]
        public void Create([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
                objemployee.AddEmployee(employee);
        }

        // To fetch the details of a particular employee
        [HttpGet]
        [Route("Details/{id}")]
        public Employee Details(int id)
        {

            return objemployee.GetEmployeeData(id);
        }

        // Edit an existing employee records
        [HttpPut]
        [Route("Edit")]
        public void Edit([FromBody]Employee employee)
        {
            if (ModelState.IsValid)
                objemployee.UpdateEmployee(employee);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public void Delete(int id)
        {
            objemployee.DeleteEmployee(id);
        }
    }
}
