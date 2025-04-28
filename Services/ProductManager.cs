using ECommerceWebsite.Models.DB;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerceWebsite.Services
{
    public class ProductManager /*: IProductManager<Product>*/
    {
        private readonly ECommerceProjectContext _context;
        private readonly ProductImageManager _productImageManager;
     

        public ProductManager(ECommerceProjectContext context,ProductImageManager productImageManager)
        {
            _context = context;
            _productImageManager= productImageManager;
           
        }

        public async Task<Product> Create(Product entity, IFormFile? MainImage)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();

            if ( MainImage!=null)
            {

                //Save the main Image
                entity.ImageName =  GlobalMethods.ReturnNewGUIDNamewithFileExtension(MainImage);
                string ImagePath = ConstantSettings.MainSavingPathCSharp + entity.ImageName;
                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                    await MainImage.CopyToAsync(stream);
                }

                int maxId = _context.Products.Max(p => p.ProductID);

                var product = _context.Products.Where(x => x.ProductID == maxId).ToList().First(); ;
                product.ImageName=entity.ImageName;

                await _context.SaveChangesAsync();

            }



            return entity;
        }

        public async Task DeleteConfirmed(int id)
        {
           Product? product= await _context.Products.FirstOrDefaultAsync<Product>(x => x.ProductID == id);

            if (product != null) {

                foreach (var item in _context.ProductImages.Where(x => x.ProductId == id))
                {
                    _context.Remove(item);
                    GlobalMethods.DeleteOldImage(item.ImageName);
                }
                _context.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> Details(int? id)
        {
            if (id == null)
            {
                return NotFoundEntity();
            }

            var product = await _context.Products
                .Include(p => p.Category).Include(p=>p.ProductImages)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFoundEntity();
            }
            return product;
        }

        public  async Task<Product> Edit(Product entity)
        {

             _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Product> Edit(Product entity, IFormFile? MainImage, IFormFile[]? imageFiles)
        {

            var product= _context.Products.Where(x=>x.ProductID==entity.ProductID).FirstOrDefault();
            product.Category=entity.Category;
            product.Price=entity.Price;
            product.StockQuantity=entity.StockQuantity;
            product.Name=entity.Name;
            product.ShortDescription = entity.ShortDescription;
            product.LongDescription = entity.LongDescription;
            product.CategoryID=entity.CategoryID;
            product.InSection=entity.InSection;
           
           
           var images=  _productImageManager.Index().Result.Where(x => x.ProductId == product!.ProductID).ToList();

            //delete the old image & copy the main image
            GlobalMethods.DeleteOldImage(product!.ImageName);
            if (MainImage == null)
            {
                product!.ImageName = "";
            }
            else
            {
                product.ImageName = GlobalMethods.ReturnNewGUIDNamewithFileExtension(MainImage);
                string ImagePath = ConstantSettings.MainSavingPathCSharp + product.ImageName;
                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                    await MainImage.CopyToAsync(stream);
                }
            }


            //copy the gallary images
            foreach (var image in images) {

                await _productImageManager.DeleteConfirmed(image.Id);
                GlobalMethods.DeleteOldImage(image.ImageName);
            }

            foreach (var item in imageFiles)
            {
                ProductImage newImage = new ProductImage();
                newImage.ProductId = entity.ProductID;
                newImage.ImageName= GlobalMethods.ReturnNewGUIDNamewithFileExtension(item);
                string ImagePath1 = ConstantSettings.MainSavingPathCSharp + newImage.ImageName;
                using (var stream = new FileStream(ImagePath1, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                await _productImageManager.Create(newImage);


            }
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            if (id == null)
            {
                return false ;
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            //ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", product.CategoryID);

            return true;
        }
       
        public async Task<List<Product>> Index()
        {
            var ECommerceProjectContext = _context.Products.Include(p => p.Category);
            return await ECommerceProjectContext.ToListAsync();
        }

        public Product NotFoundEntity()
        {
            Product NotFoundProduct = new Product();
            NotFoundProduct.ProductID = -1;
            return NotFoundProduct;
        }

    }

}
