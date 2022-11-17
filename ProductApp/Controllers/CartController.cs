using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using ProductApp.ViewModel;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace ProductApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRpository;
        private readonly IShoppingCart _shoppingCart;
        private readonly ProductDbContext _productDbContext;
        private readonly IHttpContextAccessor _contextAccessor;


        public CartController(IProductRepository productRpository, IShoppingCart shoppingCart,ProductDbContext productDbContext,IHttpContextAccessor httpContextAccessor)
        {
            _productRpository = productRpository;
            _shoppingCart = shoppingCart;
            _productDbContext = productDbContext;
            _contextAccessor = httpContextAccessor;
        }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public string? ShoppingCartId { get; set; }
        private int IsExist(Products produtc)
        {
            List<ShoppingCartItem> cart = new List<ShoppingCartItem>();
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            for(int i = 0; i < cart.Count; i++)            
                if (cart[i].Product.ProductId.Equals(produtc.ProductId))
                    return i;
                return -1;
        }

        public void Buy(Products product)
        {
            var shoppingCartItem = _productDbContext.ShoppingCartItems.SingleOrDefault(
                   s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _productDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _productDbContext.SaveChanges();

        }

        public int RemoveFromCart(Products product)
        {
            var shoppingCartItem = _productDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _productDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _productDbContext.SaveChanges();
            return localAmount;
        }

        //public IActionResult Index()
        //{
        //    var items = _shoppingCart.GteShoppingCartItem();
        //    _shoppingCart.ShoppingCartItems = items;

        //    var shoppingCartViewModel = new ProductsViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

        //    return View(shoppingCartViewModel);
        //}
    }
}
