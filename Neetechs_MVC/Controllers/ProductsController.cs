#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Neetechs.Model;
using Neetechs_MVC.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Neetechs_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
        // GET: MyProducts
        public async Task<IActionResult> SearchMyProducts(string search)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(search))
            {
                if (User.IsInRole(userId))
                {

                    return PartialView("_Search", await _context.Products.ToListAsync());
                }
                return PartialView("_Search", await _context.Products.Where(product =>
                    product.UserId.Contains(userId)
                    ).ToListAsync());
            }
            else
            {
                return PartialView("_Search", await _context.Products.Where(product =>
                    product.Brand.Contains(search)
                    || product.Date.ToString().Contains(search)
                    || product.UserId.Contains(userId)
                    || product.Name.Contains(search)
                    ).ToListAsync());

            }
        }
        // GET: Products
        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return PartialView("_Search", await _context.Products.ToListAsync());
            }
            else
            {
                return PartialView("_Search", await _context.Products.Where(product =>
                product.Brand.Contains(search)
                || product.Date.ToString().Contains(search)
                || product.Name.Contains(search)
                ).ToListAsync());

            }
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            List<SelectListItem> brands = new List<SelectListItem>();
            brands.Add(new SelectListItem("asus", "Asus"));
            brands.Add(new SelectListItem("asus", "Asus"));
            ViewBag.SelectBrand = brands;
            //ViewData["SelectBrand"] = brands;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Brand,Price,FileName,File")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId,Date,Brand,Price,FileName,File")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
