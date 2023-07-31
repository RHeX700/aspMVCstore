using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain.Entities;
using Services;
using Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Presentation.Controllers
{
    public class CartsController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public CartsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: Carts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(_serviceManager.CartService.GetAllAsync());
        }

        // GET: Carts/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {


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

            var cart = await _serviceManager.CartService.GetByIdAsync((Guid)id);
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
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _serviceManager.CartService.GetByIdAsync((Guid)id);
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
            var cart = await _serviceManager.CartService.GetByIdAsync(id);
            if (cart != null)
            {
                _serviceManager.CartService.DeleteAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddItem(int ProductID)
        {
            if (ProductID == null)
            {
                return NotFound();
            }

            if(string.IsNullOrEmpty(HttpContext.Session.GetString("SessionCartID")))
            {
                
                Cart shoppingCart = new Cart
                {
                    CartID = Guid.NewGuid()
                };
                await _serviceManager.CartService.CreateAsync(shoppingCart);
                HttpContext.Session.SetString("SessionCartID", shoppingCart.CartID.ToString());
            }
             Guid cartID = Guid.Parse(HttpContext.Session.GetString("SessionCartID"));

            if(_serviceManager.CartItemService.CartItemExists(cartID, ProductID))
            {
                CartItem item = await _serviceManager.CartItemService.GetByIdAsync(cartID, ProductID);
                item.Quantity++;
                item.TotalPrice = item.Product.Price * item.Quantity;
                await _serviceManager.CartItemService.UpdateAsync(item);
            }
            else
            {
                CartItem item = new CartItem{ 
                    CartID = cartID,
                    ProductID = ProductID
                };
                var Prod = await _serviceManager.ProductService.GetByIdAsync(ProductID);
                item.Quantity = 1;
                item.TotalPrice = Prod.Price;
                await _serviceManager.CartItemService.CreateAsync(item);
            }

            return RedirectToAction(actionName: "Index", controllerName:"Products");
        }

/*        private bool CartExists(Guid id)
        {
          return (_context.Cart?.Any(e => e.CartID == id)).GetValueOrDefault();
        }*/

/*        private bool CartItemExists(Guid CartID, int ProductID)
        {
            return (_context.CartItem?.Any(e => e.CartID == CartID && e.ProductID == ProductID)).GetValueOrDefault();
        }*/
    }
}
