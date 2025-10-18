using Employee_Management.Data;
using Employee_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmpDbContext _context;

        public AccountController(EmpDbContext context)
        {
            _context = context;
        }

        #region Registration Page
        public IActionResult Register()
        {
            return View();
        }
        #endregion

        #region Registration User
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    IsAgree = model.IsAgree
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }
        #endregion

        #region Login Page
        public IActionResult Login()
        {
            return View();
        }

        #endregion


        #region Logout Page
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        #region Login User
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginUser = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (loginUser != null)
                {
                    
                    if (loginUser.Email == "manishraj@gmail.com")
                    {
                        return RedirectToAction("Index", "AdminDashboard");
                    }
                    else
                    {
                        var empDetails = _context.Employees
                                                 .FirstOrDefault(e => e.UserId == loginUser.UserId);

                        if (empDetails == null)
                        {

                            return RedirectToAction("AddEmployee", "Employee", new { userId = loginUser.UserId });

                        }
                        else
                        {
                           
                            return RedirectToAction("EmployeeDashboard", "Employee");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View(model);
        }


        #endregion
        #endregion
    }
}
