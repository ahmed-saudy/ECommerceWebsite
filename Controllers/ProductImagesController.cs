using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceWebsite.Models.DB;
using ECommerceWebsite.Services;

namespace ECommerceWebsite.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly ECommerceProjectContext _context;
        private readonly ProductImageManager _productImageManager;

        public ProductImagesController(ECommerceProjectContext context, ProductImageManager productImageManager)
        {
            _context = context;
            _productImageManager = productImageManager;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            return View(await _productImageManager.Index());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           

            return View(await _productImageManager.Details(id));
        }

       
        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ProductImage productImage= await _productImageManager.Details(id);


             ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "ProductID", productImage.ProductId);
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageName,ProductId,UploadDate")] ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productImageManager.Edit(productImage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_productImageManager.Details(productImage.Id).Id == -1)
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "ProductID", productImage.ProductId);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ProductImage productImage = await _productImageManager.Details(id);


            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          await _productImageManager.DeleteConfirmed(id);
          
          return RedirectToAction(nameof(Index));
        }

    }
}
