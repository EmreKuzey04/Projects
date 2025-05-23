using Microsoft.AspNetCore.Mvc;
using WEB.MVCUI.Areas.Admin.Models.Contexts;
using WEB.MVCUI.Areas.Admin.Models.Dtos;
using WEB.MVCUI.Areas.Admin.Models.Entities;

namespace WEB.MVCUI.Areas.Admin.Controllers
{
    [Area("admin")]
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
        public IActionResult Edit(int id)
        {
            using var ctx = new TradewndContext();
            var employee = ctx.Employees.SingleOrDefault(e => e.EmployeeID == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }


        [HttpPost]
        public IActionResult Edit(int EmployeeId, EmployeeUpdateDto dto)
        {
            using var ctx = new TradewndContext();
            var employee = ctx.Employees.SingleOrDefault(e => e.EmployeeID == EmployeeId);
            if (employee == null)
                return NotFound();

           
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.BirthDate = dto.BirthDate;
            employee.HireDate = dto.HireDate;
            employee.City = dto.City;
            employee.Country = dto.Country;
            employee.Phone = dto.Phone;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            using var ctx = new TradewndContext();
            var employee = ctx.Employees.SingleOrDefault(e => e.EmployeeID == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

 
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            using var ctx = new TradewndContext();
            var employee = ctx.Employees.SingleOrDefault(e => e.EmployeeID == id);
            if (employee == null)
                return NotFound();

            ctx.Employees.Remove(employee);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
