namespace ProductApp.Models
{
    public interface IProductRepository
    {
        IEnumerable<Products> AllProducts { get; }
        Products? GetProductById(int productId);
        void AddProduct(Products addproduct);
        void UpdateProduct(Products updateproduct);
        void DeleteProduct(int productId);
    }
}
