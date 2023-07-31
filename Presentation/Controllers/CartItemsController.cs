using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain.Entities;
using Services.Abstractions;

namespace Presentation.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public CartItemsController(IServiceManager ServiceManager)
        {
            _serviceManager = ServiceManager;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var cartItems = await _serviceManager.CartItemService.GetAllAsync();
            return View(cartItems);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(Guid? CartID, int? ProductID)
        {
            if (ProductID == null || CartID == null)
            {
                return NotFound();
            }

            var cartItem = _serviceManager.CartItemService.GetByIdAsync((Guid)CartID, (int)ProductID);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? CartID, int? ProductID)
        {
            var cartItem = _serviceManager.CartItemService.GetByIdAsync((Guid)CartID, (int)ProductID);
            if (cartItem != null)
            {
                _serviceManager.CartItemService.DeleteAsync((Guid)CartID, (int)ProductID);
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItems(List<CartItem> cartItems)
        {

            if (cartItems == null)
            {
                return NotFound();
            }


            foreach (var item in cartItems)
            {
                var cartItem = await _serviceManager.CartItemService.GetByIdAsync(item.CartID, item.ProductID);
                var prod = await _serviceManager.ProductService.GetByIdAsync(item.ProductID);
                cartItem.Quantity = item.Quantity;
                cartItem.TotalPrice = item.Quantity * prod.Price;
                await _serviceManager.CartItemService.UpdateAsync(cartItem);
            }
            
            return RedirectToAction(actionName: "Details", controllerName: "Carts");


/*            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.C
            return Problem("Model state is not valid");*/
        }

/*        private bool CartItemExists(Guid id)
        {
          return (_context.CartItem?.Any(e => e.CartID == id)).GetValueOrDefault();
        }*/
    }
}
