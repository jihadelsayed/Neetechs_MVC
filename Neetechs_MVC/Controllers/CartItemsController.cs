#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Neetechs.Model;
using Neetechs_MVC.Data;
using Neetechs_MVC.Models;
using Newtonsoft.Json;

namespace Neetechs_MVC.Controllers
{

    public class CartItemsController : Controller

    {
        public List<CartItem> cartItems { get; set; }
        public double Total { get; set; }


        private readonly ApplicationDbContext _context;


        public CartItemsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // remove from session and quntity in the addtocart and calculated price and pay
        // GET: CartItems
        public IActionResult Index()
        {
            var cartSession = HttpContext.Session.GetString("CartSession");
            //check if there is a cartItem 
            if (cartSession == null)
            {
                cartItems = new List<CartItem>();
            }
            else
            {
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("CartSession"));
            }
            //return PartialView("_CartItems", cartItems.ToList());
            return View(cartItems.ToList());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var products = await _context.Products.ToListAsync();

            return PartialView("Index", products);
        }
        private int Exists(List<CartItem> cartItem, int? id)
        {
            for (var i = 0; i < cartItem.Count; i++)
            {
                if (cartItem[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity")] CartItem cartItem)
        {
            if (id != cartItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.Id))
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
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var cartItem = await _context.CartItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItem.FindAsync(id);
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.Id == id);
        }
        public IActionResult CartItemsTask()
        {
            var cartSession = HttpContext.Session.GetString("CartSession");
            //check if there is a cartItem 
            if (cartSession == null)
            {
                cartItems = new List<CartItem>();
            }
            else
            {
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("CartSession"));
            }
            return PartialView("_CartItems", cartItems.ToList());

        }
        public IActionResult AddtoCart(int id)
        {
            var cartSession = HttpContext.Session.GetString("CartSession");
            //check if there is a cartItem 
            if (cartSession == null)
            {
                cartItems = new List<CartItem>();
            }
            else
            {
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("CartSession"));
            }
            CartItem cartItem = cartItems.Where(p => p.Product.Id == id).FirstOrDefault();
            if (cartItem == null)
            {
                cartItem = new CartItem();
                Product product = _context.Products.Where(p => p.Id == id).FirstOrDefault();
                cartItem.Product = product;
                cartItem.Quantity = 1;
                cartItems.Add(cartItem);

            }
            else
            {
                cartItem.Quantity = cartItem.Quantity + 1;


            }

            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(cartItems));

            return Json(JsonConvert.SerializeObject(cartItems));
         
        }
        public IActionResult DeleteFromCart(int id)
        {
            cartItems = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("CartSession"));
            CartItem cartItem = cartItems.Where(p => p.Id == id).FirstOrDefault();
            cartItems.Remove(cartItem);
            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(cartItems));
            return Json(JsonConvert.SerializeObject(cartItems));
        }
        public IActionResult getCartSession(string id)
        {
            return Json(HttpContext.Session.GetString("CartSession"));
        }
    }
}
