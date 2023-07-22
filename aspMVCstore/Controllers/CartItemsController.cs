using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspMVCstore.Data;
using aspMVCstore.Models;

namespace aspMVCstore.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CartItem.Include(c => c.Cart).Include(c => c.Product);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID");
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductID");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartID,ProductID,Quantity")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                cartItem.CartID = Guid.NewGuid();
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", cartItem.CartID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductID", cartItem.ProductID);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CartItem == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", cartItem.CartID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductID", cartItem.ProductID);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CartID,ProductID,Quantity")] CartItem cartItem)
        {
            if (id != cartItem.CartID)
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
                    if (!CartItemExists(cartItem.CartID))
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
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", cartItem.CartID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductID", cartItem.ProductID);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CartItem == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartID == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CartItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CartItem'  is null.");
            }
            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItem.Remove(cartItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItems(List<CartItem> cartItems)
        {

            if (cartItems == null || _context.CartItem == null)
            {
                return NotFound();
            }


            foreach (var item in cartItems)
            {
                var cartItem = await _context.CartItem
                    .FirstOrDefaultAsync(c => c.CartID == item.CartID && c.ProductID == item.ProductID);
                var prod = await _context.Product.FirstOrDefaultAsync(p => p.ProductID == item.ProductID);
                cartItem.Quantity = item.Quantity;
                cartItem.TotalPrice = item.Quantity * prod.Price;
            }
            _context.SaveChanges();
            return RedirectToAction(actionName: "Details", controllerName: "Carts");


/*            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.C
            return Problem("Model state is not valid");*/
        }

        private bool CartItemExists(Guid id)
        {
          return (_context.CartItem?.Any(e => e.CartID == id)).GetValueOrDefault();
        }
    }
}
