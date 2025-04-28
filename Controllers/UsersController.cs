using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceWebsite.Models.DB;
using ECommerceWebsite.Services;
using Microsoft.AspNetCore.Identity;
using ECommerceWebsite.VM;

namespace ECommerceWebsite.Controllers
{
    public class UsersController : Controller
    {

        //IUserManager<User> _userManager;
        UserManager _userManager;
        public UsersController(UserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<User> users = await _userManager.Index();
            List<UserVM> usersVM = new List<UserVM>();

            foreach (var user in users) 
            {
                UserVM userVM = new UserVM();
                userVM.UserID = user.Id;
                userVM.UserName = user.UserName;
                userVM.FirstName = user.FirstName;
                userVM.LastName = user.LastName;
                userVM.Email = user.Email;
                userVM.PhoneNumber = user.PhoneNumber;
                userVM.CreatedAt = user.CreatedAt;
                userVM.Image = user.ImageName;
                usersVM.Add(userVM);
            }

            return View(usersVM);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Details(id);

            if (user == null)
            {
                return NotFound();
            }
            UserVM userVM = new UserVM();
            userVM.UserID = user.Id;
            userVM.UserName = user.UserName;
            userVM.FirstName = user.FirstName;
            userVM.LastName = user.LastName;
            userVM.Email = user.Email;
            userVM.PhoneNumber = user.PhoneNumber;
            userVM.Image = user.ImageName;
            return View(userVM);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM userVM, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                User user=new User();
                //user.Id = userVM.UserID!.Value  ;
                user.UserName=userVM.UserName  ;
                user.FirstName=userVM.FirstName  ;
                user.LastName=userVM.LastName  ;
                user.Email = userVM.Email ;
                user.PhoneNumber =userVM.PhoneNumber ;
                user.CreatedAt = DateTime.Now ;
                user.ImageName = userVM.Image;

                await _userManager.Create(user,ImageFile);
                
                return RedirectToAction(nameof(Index));
            }
            return View(userVM);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Details(id);
            if (user == null)
            {
                return NotFound();
            }
            UserVM userVM = new UserVM();
            userVM.UserID = user.Id;
            userVM.UserName = user.UserName;
            userVM.FirstName = user.FirstName;
            userVM.LastName = user.LastName;
            userVM.Email = user.Email;
            userVM.PhoneNumber = user.PhoneNumber;
            userVM.CreatedAt = user.CreatedAt ;
            userVM.Image = user.ImageName;

            return View(userVM);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserVM userVM, IFormFile? ImageFile)
        {
            if (id != userVM.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User();
                    user.Id = userVM.UserID!.Value;
                    user.UserName = userVM.UserName;
                    user.FirstName = userVM.FirstName;
                    user.LastName = userVM.LastName;
                    user.Email = userVM.Email;
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.CreatedAt = userVM.CreatedAt;
                    user.ImageName = userVM.Image;
                    await _userManager.Edit(user, ImageFile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_userManager.Details(userVM.UserID).Id==-1)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userVM);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Details(id);
            if (user == null)
            {
                return NotFound();
            }
            UserVM userVM = new UserVM();
            userVM.UserID = user.Id;
            userVM.UserName = user.UserName;
            userVM.FirstName = user.FirstName;
            userVM.LastName = user.LastName;
            userVM.Email = user.Email;
            userVM.PhoneNumber = user.PhoneNumber;
            userVM.CreatedAt = user.CreatedAt;
            userVM.Image = user.ImageName;
            return View(userVM);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.Details(id);
            if (user != null)
            {
                await _userManager.DeleteConfirmed(id);
            }
          
            return RedirectToAction(nameof(Index));
        }

      
    }
}
