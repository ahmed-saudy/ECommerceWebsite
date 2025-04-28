using ECommerceWebsite.Models.DB;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Services
{
    public class ProductImageManager
    {
        private readonly ECommerceProjectContext _context;

        public ProductImageManager(ECommerceProjectContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<List<ProductImage>> Index()
        {
            var ECommerceProjectContext = _context.ProductImages.Include(p => p.Product);
            return await ECommerceProjectContext.ToListAsync();
        }

        // GET: ProductImages/Details/5
        public async Task<ProductImage> Details(int? id)
        {
            if (id == null)
            {
                return NotFoundProductImage();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFoundProductImage();
            }

            return productImage;
        }

       

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        public async Task<ProductImage> Create([Bind("Id,ImageName,ProductId,UploadDate")] ProductImage productImage)
        {
            _context.Add(productImage);
            await _context.SaveChangesAsync();

            return productImage;
        }

        // GET: ProductImages/Edit/5
        public async Task<ProductImage> Edit(int? id)
        {
            if (id == null)
            {
                return NotFoundProductImage();
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFoundProductImage();
            }
            return productImage;
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ProductImage> Edit( ProductImage productImage)
        {
            _context.Update(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        // POST: ProductImages/Delete/5
      
        public async Task DeleteConfirmed(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                GlobalMethods.DeleteOldImage(productImage.ImageName);
                _context.ProductImages.Remove(productImage);
            }

            await _context.SaveChangesAsync();
          
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.Id == id);
        }
        /// <summary>
        /// Not Found Entity
        /// </summary>
        /// <returns></returns>
        private ProductImage NotFoundProductImage()
        {
            ProductImage productImage=new ProductImage();
            productImage.ProductId = -1;
            return productImage;
        }
    }
}
