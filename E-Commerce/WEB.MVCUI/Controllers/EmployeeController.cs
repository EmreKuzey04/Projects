using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Models.Contexts;
using WEB.MVCUI.Models.Dtos;
using WEB.MVCUI.Models.Entities;

namespace WEB.MVCUI.Controllers
{
    public class EmployeeController : Controller
    {
        public ViewResult Index()
        {
            using var ctx = new TradewndContext();
            var employees =
                ctx.Employees.ToList();

            return View(employees);
        }

        public ViewResult Add()
        {
            return View();
        }

        public ViewResult Save(EmployeeAddDto dto)
        {
            var employee = new Employee();
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.BirthDate = dto.BirthDate;
            employee.HireDate = dto.HireDate;
            employee.City = dto.City;
            employee.Country = dto.Country;
            employee.Phone = dto.Phone;
            using var ctx = new TradewndContext();
            ctx.Employees.Add(employee);

            ctx.SaveChanges();
            return View();
        }
    }
}
