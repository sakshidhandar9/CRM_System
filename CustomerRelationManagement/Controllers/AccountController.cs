using CustomerRelationManagement.Models;
using CustomerRelationManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRelationManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ValidateUser(model.Username, model.Password, out string roleName))
                {
                    // Example: Store role in session
                    HttpContext.Session.SetString("Role", roleName);
                    HttpContext.Session.SetString("Username", model.Username);

                    return RedirectToAction("Create", "Customer");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }
    }
}