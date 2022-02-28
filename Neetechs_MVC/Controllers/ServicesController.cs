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
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            List<SelectListItem> category = _context.Categories.Select(cat => new SelectListItem(){
                                      Text = cat.Name,
                                      Value = cat.Id.ToString()
                                  }).ToList<SelectListItem>();
            //List<SelectListItem> brands = new List<SelectListItem>();
            ViewBag.SelectCategory = category;
            return View();
         }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Category,Price,Description,Location,FormFile")] Service service)
        {
            
            List<SelectListItem> category = _context.Categories.Select(cat => new SelectListItem()
            {
                Text = cat.Name,
                Value = cat.Id.ToString()
            }).ToList<SelectListItem>();
            //List<SelectListItem> brands = new List<SelectListItem>();
            ViewBag.SelectCategory = category;
            //service.Category = Categorie;
            ViewData["SelectCategory"] = service.Category;

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            service.UserId = userId;

            if (service.FormFile != null)
            {
                byte[] bytes = null;
                var img = Image.FromStream(service.FormFile.OpenReadStream());
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
                        service.FormFile.CopyTo(ms); // copy to memory stream object
                        bytes = ms.ToArray();

                    }
                }
                service.File = bytes;
                service.FileName = service.FormFile.FileName;
            }
           // service = service;
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }


        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId,Date,Categorie,Price,Description,Location,FileName,File")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
