using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using aspMVCstore.Data;
using aspMVCstore.Models;

namespace aspMVCstore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;

        public OrdersController(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if(_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }

 

            if (User.IsInRole("Admin"))
            {
                return View(await _context.Order.ToListAsync());
            }

            return View(await _context.Order
                        .Where(o => o.OwnerID == _userManager.GetUserAsync(User).Result.Id)
                        .ToListAsync());
                          
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            if(_userManager.GetUserAsync(User).Result.Id == order.OwnerID || User.IsInRole("Admin"))
            {
                return View(order);
            }

            return Forbid();
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OwnerID,TotalPrice,Address,Status")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                System.Console.WriteLine(message);
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                order.Status = OrderStatus.Cancelled;
                _context.Order.Update(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateOrder(Guid CartID)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }

            var user = _userManager.GetUserAsync(User);

            Order newOrder = new Order
            {
                OwnerID = _userManager.GetUserId(User),
                Status = OrderStatus.Processing,
                Address = user.Result.Address
            };

            _context.Add(newOrder);
            await _context.SaveChangesAsync();
            if (_context.Cart == null)
            {
                return NotFound();
            }
            var cart = await _context.Cart.Include(c => c.CartItems).FirstOrDefaultAsync(m => m.CartID == CartID);

            foreach (var item in cart.CartItems)
            {
                OrderItem newOrderItem = new OrderItem
                {
                    OrderID = newOrder.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity
                };

                _context.Add(newOrderItem);
            }
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("SessionCartID");

            return RedirectToAction(nameof(Details), new {id = newOrder.OrderID});
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
