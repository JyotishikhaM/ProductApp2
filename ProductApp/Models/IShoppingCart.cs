using System.IO.Pipelines;

namespace ProductApp.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Products product);
        int RemoveFromCart(Products product);
        List<ShoppingCartItem> GteShoppingCartItem();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
