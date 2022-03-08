using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neetechs_MVC.Data;
using Neetechs_MVC.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Claims;

namespace Neetechs_MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ProfileController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProfileController/Details/5
        public async Task<IActionResult> Details(string? id)
        {      
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> AddImage([Bind("FormFile")] IFormFile FormFile)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var findProfile = await _context.Profiles.FindAsync(userId);
            //profile.FormFile = profile;
            if (FormFile != null)
            {
                byte[] bytes = null;
                var img = Image.FromStream(FormFile.OpenReadStream());
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
                        FormFile.CopyTo(ms); // copy to memory stream object
                        bytes = ms.ToArray();

                    }
                }
                findProfile.File = bytes;
                findProfile.FileName = FormFile.FileName;
            }
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(findProfile);
        }
        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> ProfileTask()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var findProfile = await _context.Profiles.FindAsync(userId);
            return Json(findProfile);
        }
        
    }
}
