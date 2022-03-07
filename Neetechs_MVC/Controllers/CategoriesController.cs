#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Neetechs_MVC.Data;
using Neetechs_MVC.Models;

namespace Neetechs_MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public async Task<IActionResult> CategoriesTask()
        {
            return PartialView("_Categories", await _context.Categories.ToListAsync());

        }
        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            List<SelectListItem> category = _context.Categories.Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id.ToString()
            }).ToList<SelectListItem>();
            //List<SelectListItem> brands = new List<SelectListItem>();
            ViewBag.SelectCategory = category;
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Name,FatherCategory,AddDate,Description,FormFile")] Category category)
        {
            List<SelectListItem> cat = _context.Categories.Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id.ToString()
            }).ToList<SelectListItem>();
            //List<SelectListItem> brands = new List<SelectListItem>();
            ViewBag.SelectCategory = cat;
            //service.Category = Categorie;
            ViewData["SelectCategory"] = category.FatherCategory;

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            category.UserId = userId;
            if (category.FormFile != null)
            {
                byte[] bytes = null;
                var img = Image.FromStream(category.FormFile.OpenReadStream());
                var height = img.Height;
                var width = img.Width;
                if (height > 200)
                {
                    var retio = 1;
                    if (height > width)
                    {
                        retio = height / width;
                    }
                    else
                    {
                        retio = width / height;
                    }
                    int newHeight = 200;
                    int newWidth = (int)(200 * retio);
                    string f = newWidth.GetType().Name;
                    Bitmap resizeImage = new Bitmap(img, newWidth, newHeight);
                    using var imageStream = new MemoryStream();
                    resizeImage.Save(imageStream, ImageFormat.Jpeg);
                    bytes = imageStream.ToArray();

                }
                else
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        category.FormFile.CopyTo(ms); // copy to memory stream object
                        bytes = ms.ToArray();

                    }
                }
                category.File = bytes;
                category.FileName = category.FormFile.FileName;
            }
     

   
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> cat = _context.Categories.Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id.ToString()
            }).ToList<SelectListItem>();
            //List<SelectListItem> brands = new List<SelectListItem>();
            ViewBag.SelectCategory = cat;
            //service.Category = Categorie;
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FatherCategory,AddDate,Description,FormFile")] Category category)
        {
            List<SelectListItem> cat = _context.Categories.Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id.ToString()
            }).ToList<SelectListItem>();
            //List<SelectListItem> brands = new List<SelectListItem>();
            ViewBag.SelectCategory = cat;
            //service.Category = Categorie;
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
