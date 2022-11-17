using ProductApp.Models;

namespace ProductApp.ViewModel
{
    public class ProductsViewModel
    {
        public IEnumerable<Products> Products { get; }
        public IShoppingCart ShoppingCart { get; }
        public decimal ShoppingCartTotal { get; }

        public ProductsViewModel(IShoppingCart shoppingCart,decimal shoppingCartTotal)
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
        }

    }
}
