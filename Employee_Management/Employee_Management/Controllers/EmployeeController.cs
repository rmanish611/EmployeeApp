using Employee_Management.Data;
using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Employee_Management.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmpDbContext _context;

        public EmployeeController(EmpDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public IActionResult AddEmployee(int userId)
        //{
        //    var viewModel = new EmployeeDTO
        //    {
        //        UserId = userId,
        //        Countries = _context.Countries.ToList() ?? new List<Country>(),
        //        States = _context.States.ToList() ?? new List<State>(),
        //        Cities = _context.Cities.ToList() ?? new List<City>(),
        //        Departments = _context.Departments.ToList() ?? new List<Department>(),
        //        Hobbies = _context.Hobbies.ToList() ?? new List<Hobby>()
        //    };

        //    return View(viewModel);
        //}

        [HttpGet]
        public IActionResult AddEmployee(int? userId, int? employeeId)
        {
            EmployeeDTO viewModel;

            if (employeeId.HasValue)
            {
                // Edit mode
                var emp = _context.Employees
                                  .Include(e => e.EmployeeHobbies)
                                  .FirstOrDefault(e => e.EmployeeId == employeeId.Value);
                if (emp == null) return NotFound();

                viewModel = new EmployeeDTO
                {
                    EmployeeId = emp.EmployeeId,
                    UserId = emp.UserId,
                    Name = emp.Name,
                    Age = emp.Age,
                    Gender = emp.Gender,
                    Salary = emp.Salary,
                    Contact = emp.Contact,
                    CountryId = emp.CountryId,
                    StateId = emp.StateId,
                    CityId = emp.CityId,
                    DepartmentId = emp.DepartmentId,
                    SelectedHobbies = emp.EmployeeHobbies.Select(h => h.HobbyId).ToList(),
                    Countries = _context.Countries.ToList(),
                    States = _context.States.ToList(),
                    Cities = _context.Cities.ToList(),
                    Departments = _context.Departments.ToList(),
                    Hobbies = _context.Hobbies.ToList()
                };
            }
            else
            {
                // Add mode
                viewModel = new EmployeeDTO
                {
                    UserId = userId ?? 0,
                    Countries = _context.Countries.ToList(),
                    States = _context.States.ToList(),
                    Cities = _context.Cities.ToList(),
                    Departments = _context.Departments.ToList(),
                    Hobbies = _context.Hobbies.ToList()
                };
            }

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AddEmployee(EmployeeDTO model, IFormFile ImageFile, IFormFile ResumeFile)
        {
            EmployeeDetails employee;

            if (model.EmployeeId > 0)
            {
                // Edit
                employee = _context.Employees.Include(e => e.EmployeeHobbies).FirstOrDefault(e => e.EmployeeId == model.EmployeeId);
                if (employee == null) return NotFound();
            }
            else
            {
                employee = new EmployeeDetails();
                _context.Employees.Add(employee);
            }

            // Common fields
            employee.Name = model.Name;
            employee.Age = model.Age;
            employee.Gender = model.Gender;
            employee.Salary = model.Salary;
            employee.Contact = model.Contact;
            employee.CountryId = model.CountryId;
            employee.StateId = model.StateId;
            employee.CityId = model.CityId == 0 ? 1 : model.CityId;
            employee.DepartmentId = model.DepartmentId;
            employee.UserId = model.UserId;
            employee.IsAgree = model.IsAgree;

            // Image save
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imgPath = Path.Combine("wwwroot/images/employees", ImageFile.FileName);
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
                employee.ImagePath = "/images/employees/" + ImageFile.FileName;
            }

            // Resume save
            if (ResumeFile != null && ResumeFile.Length > 0)
            {
                var resPath = Path.Combine("wwwroot/resumes", ResumeFile.FileName);
                using (var stream = new FileStream(resPath, FileMode.Create))
                {
                    ResumeFile.CopyTo(stream);
                }
                employee.ResumePath = "/resumes/" + ResumeFile.FileName;
            }

            // Hobbies
            //var existingHobbies = employee.EmployeeHobbies.ToList();
            //_context.EmployeeHobbies.RemoveRange(existingHobbies);
            if (model.SelectedHobbies != null)
            {
                foreach (var h in model.SelectedHobbies)
                {
                    _context.EmployeeHobbies.Add(new EmployeeHobby { Employee = employee, HobbyId = h });
                }
            }

            _context.SaveChanges();
            return RedirectToAction("EmployeeDashboard");
        }


        public JsonResult GetStates(int countryId)
        {
            var states = _context.States
                          .Where(s => s.CountryId == countryId)
                          .Select(s => new { s.StateId, s.StateName })
                          .ToList();
            return Json(states);
        }

        public JsonResult GetCities(int stateId)
        {
            var cities = _context.Cities
                          .Where(c => c.StateId == stateId)
                          .Select(c => new { c.CityId, c.CityName })
                          .ToList();
            return Json(cities);
        }
        public IActionResult EmployeeDashboard()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Country)
                .Include(e => e.State)
                .Include(e => e.City)
                .Include(e => e.EmployeeHobbies)
                    .ThenInclude(h => h.Hobby)
                .ToList();

            return View(employees);
        }

        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return RedirectToAction("AddEmployee", "Employee", new { userId = id });
        }






    }
}