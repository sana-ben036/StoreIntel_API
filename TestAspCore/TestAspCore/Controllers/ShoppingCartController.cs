using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Models;
using TestAspCore.Models.Repositories;

namespace TestAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IStoreRepository<Product> _storeRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IStoreRepository<Product> storeRepository, ShoppingCart shoppingCart)
        {
            _storeRepository = storeRepository;
            _shoppingCart = shoppingCart;
        }

        //[Authorize]
        [HttpGet]
        public IEnumerable<ShoppingCartItem> GetList()
        {
            var items =  _shoppingCart.GetShoppingCartItems();
           _shoppingCart.ShoppingCartItems = items;

            return items.Select(x => new ShoppingCartItem()
            {
                ShoppingCartItemId = x.ShoppingCartItemId,
                Amount = x.Amount,
            }); 
        }

        ////[Authorize]
        //public RedirectToActionResult AddToShoppingCart(int drinkId)
        //{
        //    var selectedDrink = _storeRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
        //    if (selectedDrink != null)
        //    {
        //        _shoppingCart.AddToCart(selectedDrink, 1);
        //    }
        //    return RedirectToAction("Index");
        //}

        //public RedirectToActionResult RemoveFromShoppingCart(Guid id)
        //{
        //    var selectedProduct = _storeRepository.Get().FirstOrDefault(p => p.DrinkId == drinkId);
        //    if (selectedDrink != null)
        //    {
        //        _shoppingCart.RemoveFromCart(selectedDrink);
        //    }
        //    return RedirectToAction("Index");
        //}

    }
}

