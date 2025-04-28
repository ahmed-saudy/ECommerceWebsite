using Microsoft.AspNetCore.Mvc;
using ECommerceWebsite.Models;
using ECommerceWebsite.VM;

namespace ECommerceWebsite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp()
        {
            return RedirectToAction("Users/Create");
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save the user in DB
                return RedirectToAction("SignIn");
            }
            return View(model);
        }

        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Check user in DB
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}