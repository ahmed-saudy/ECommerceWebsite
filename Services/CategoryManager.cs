using ECommerceWebsite.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerceWebsite.Services
{
    public class CategoryManager
    {
        private readonly ECommerceProjectContext _context;

        public CategoryManager(ECommerceProjectContext context)
        {
            _context = context;
        }
        
      /// <summary>
      /// Get All Users 
      /// </summary>
      /// <returns></returns>
        public async Task<List<Category>> Index()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Get User Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Category> Details(int? id)
        {
            Category InvalidCategory = new Category();
            InvalidCategory.CategoryID = 0;
            if (id == null)
            {
                return InvalidCategory;
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return InvalidCategory;
            }

            return category;
        }


       
        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Category> Create(Category category)
        {
            _context.Add(category);


            await _context.SaveChangesAsync();
            return category;
        }

       /// <summary>
       /// Edit User
       /// </summary>
       /// <returns></returns>
       
        public async Task<Category> Edit(Category category)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();



            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return category;
        }

      /// <summary>
      /// Delete User
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public async Task DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            await _context.SaveChangesAsync();
          
        }

        public bool EntityExistsAsync(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }

        public Category NotFoundEntity()
        {
            Category category = new Category();
            category.CategoryID = -1;
            return category;
        }
    }
}
