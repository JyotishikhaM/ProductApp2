using System.IO.Pipelines;

namespace ProductApp.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Products Product { get; set; } = default!;
        public int Amount { get; set; }
        public int Quantity { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
