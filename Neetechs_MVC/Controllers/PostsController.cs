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
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }
  
        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {

            List<SelectListItem> categories = _context.Categories.Select(cat => new SelectListItem
            {
                Value = cat.Id.ToString(),
                Text = cat.Name
            }).ToList<SelectListItem>();

            ViewBag.SelectCategory = categories;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Description,FormFile")] Post post)
        {
            List<SelectListItem> categories = _context.Categories.Select(cat => new SelectListItem
            {
                Value = cat.Id.ToString(),
                Text = cat.Name
            }).ToList<SelectListItem>();

            ViewBag.SelectCategory = categories;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            post.UserId = userId;

            if (post.FormFile != null)
            {
                byte[] bytes = null;
                var img = Image.FromStream(post.FormFile.OpenReadStream());
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
                        post.FormFile.CopyTo(ms); // copy to memory stream object
                        bytes = ms.ToArray();

                    }
                }
                post.File = bytes;
                post.FileName = post.FormFile.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId,Categorie,Description,FileName,File")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
