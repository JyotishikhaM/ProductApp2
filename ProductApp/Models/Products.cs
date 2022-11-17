using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }        
        public decimal Discount { get; set; }
        public decimal Totalprice { get; set; }
        public string? ImageUrl { get; set; }

    }
}
