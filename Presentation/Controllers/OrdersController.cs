using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain.Entities;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;

        public OrdersController(IServiceManager serviceManager,
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
        {
            _serviceManager = serviceManager;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Admin"))
            {
                return View(_serviceManager.OrderService.GetAllAsync());
            }

            return View(_serviceManager.OrderService.GetAllAsync(_userManager.GetUserAsync(User).Result.Id));
                          
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _serviceManager.OrderService.GetByIdAsync((int)id);
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
            if (id == null)
            {
                return NotFound();
            }

            var order = await _serviceManager.OrderService.GetByIdAsync((int)id);
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
                    await _serviceManager.OrderService.UpdateAsync(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
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
            if (id == null)
            {
                return NotFound();
            }

            var order = await _serviceManager.OrderService.GetByIdAsync((int)id);
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
            var order = await _serviceManager.OrderService.GetByIdAsync((id));
            if (order != null)
            {
                order.Status = OrderStatus.Cancelled;
                _serviceManager.OrderService.UpdateAsync(order);
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateOrder(Guid CartID)
        {   

            var user = _userManager.GetUserAsync(User);

            Order newOrder = new Order
            {
                OwnerID = _userManager.GetUserId(User),
                Status = OrderStatus.Processing,
                Address = user.Result.Address
            };

            await _serviceManager.OrderService.CreateAsync(newOrder);

            var cart = await _serviceManager.CartService.GetByIdAsync(CartID);

            foreach (var item in cart.CartItems)
            {
                OrderItem newOrderItem = new OrderItem
                {
                    OrderID = newOrder.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity
                };

                await _serviceManager.OrderItemService.CreateAsync(newOrderItem);
            }

            HttpContext.Session.Remove("SessionCartID");

            return RedirectToAction(nameof(Details), new {id = newOrder.OrderID});
        }

/*        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }*/
    }
}
