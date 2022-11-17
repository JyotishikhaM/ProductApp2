using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace ProductApp.Models
{
    public class ShoppingCart
    {
        public readonly ProductDbContext _productDbContext;

        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(ProductDbContext productDbContext)
        {
            _productDbContext=productDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            ProductDbContext context = services.GetService<ProductDbContext>() ?? throw new Exception("Error initializing");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Products product)
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

        public List<ShoppingCartItem> GteShoppingCartItem()
        {
            return ShoppingCartItems ??= _productDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product).ToList();
        }
        public decimal GetShoppingCartTotal()
        {
          //var total= _productDbContext.Products.Price 
            var subtotal = _productDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).
                Select(c => (c.Product.Price-c.Product.Discount) * c.Amount/100).Sum();
            return subtotal;
        }
    }
}
