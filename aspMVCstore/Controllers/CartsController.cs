using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspMVCstore.Data;
using aspMVCstore.Models;

namespace aspMVCstore.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Cart != null ? 
                          View(await _context.Cart.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cart'  is null.");
        }

        // GET: Carts/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (_context.Cart == null)
            {
                return NotFound();
            }

            if(id == null)
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionCartID"))){
                    return NotFound();
                }
                else
                {
                    id = Guid.Parse(HttpContext.Session.GetString("SessionCartID"));
                }
            }
            ViewData["CartID"] = id;

            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }


        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: Carts/Edit/5


        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.CartID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Cart == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cart'  is null.");
            }
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                _context.Cart.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddItem(int ProductID)
        {
            if (ProductID == null || _context.Cart == null)
            {
                return NotFound();
            }

            if(string.IsNullOrEmpty(HttpContext.Session.GetString("SessionCartID")))
            {
                
                Cart shoppingCart = new Cart
                {
                    CartID = Guid.NewGuid()
                };
                _context.Add(shoppingCart);
                HttpContext.Session.SetString("SessionCartID", shoppingCart.CartID.ToString());
            }
             Guid cartID = Guid.Parse(HttpContext.Session.GetString("SessionCartID"));

            if(CartItemExists(cartID, ProductID))
            {
                CartItem item = await _context.CartItem
                    .Include(c => c.Product)
                    .FirstOrDefaultAsync(m => m.CartID == cartID && m.ProductID == ProductID);
                item.Quantity++;
                item.TotalPrice = item.Product.Price * item.Quantity;
                _context.Update(item);
            }
            else
            {
                CartItem item = new CartItem{ 
                    CartID = cartID,
                    ProductID = ProductID
                };
                var Prod = await _context.Product.FirstOrDefaultAsync(p => p.ProductID == ProductID);
                item.Quantity = 1;
                item.TotalPrice = Prod.Price;
                _context.Add(item);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(actionName: "Index", controllerName:"Products");
        }

        private bool CartExists(Guid id)
        {
          return (_context.Cart?.Any(e => e.CartID == id)).GetValueOrDefault();
        }

        private bool CartItemExists(Guid CartID, int ProductID)
        {
            return (_context.CartItem?.Any(e => e.CartID == CartID && e.ProductID == ProductID)).GetValueOrDefault();
        }
    }
}
