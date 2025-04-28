using ECommerceWebsite.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerceWebsite.Services
{
    public class UserManager/*: IUserManager<User>*/
    {
        private readonly ECommerceProjectContext _context;

        public UserManager(ECommerceProjectContext context)
        {
            _context = context;
        }
        
      /// <summary>
      /// Get All Users 
      /// </summary>
      /// <returns></returns>
        public async Task<List<User>> Index()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Get User Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> Details(int? id)
        {
            User InvalidUser= new User();
            InvalidUser.Id = 0;
            if (id == null)
            {
                return InvalidUser;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return InvalidUser;
            }

            return user;
        }


       
        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Create(User user, IFormFile? ImageFile)
        {
            _context.Add(user);

            if (ImageFile != null)
            {
                user.ImageName = GlobalMethods.ReturnNewGUIDNamewithFileExtension(ImageFile);
                string ImagePath = ConstantSettings.MainSavingPathCSharp + user.ImageName;
                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
            }


            await _context.SaveChangesAsync();
            return user;
        }

       /// <summary>
       /// Edit User
       /// </summary>
       /// <returns></returns>
       
        public async Task<User> Edit( User user,IFormFile? ImageFile)
        {
            try
            {
                //============================Save Image=====================
                try
                {
                    if (ImageFile != null)
                    {
                        //===========================delete the old image============================
                        GlobalMethods.DeleteOldImage(user.ImageName);
                        //======================================================
                        user.ImageName = GlobalMethods.ReturnNewGUIDNamewithFileExtension(ImageFile);
                        string ImagePath = ConstantSettings.MainSavingPathCSharp + user.ImageName;
                        //Copy the image to the data base
                        using (var stream = new FileStream(ImagePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                //================================================



                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return user;
        }

      /// <summary>
      /// Delete User
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public async Task DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                GlobalMethods.DeleteOldImage(user.ImageName);
                _context.Users.Remove(user);
             
            }

            await _context.SaveChangesAsync();
          
        }

        public bool EntityExistsAsync(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public User NotFoundEntity()
        {
            User user = new User();
            user.Id = -1;
            return user;
        }

    }
}
