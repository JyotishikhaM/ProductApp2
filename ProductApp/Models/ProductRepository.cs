namespace ProductApp.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public IEnumerable<Products> AllProducts
        {
            get 
            { 
                return _productDbContext.Products; 
            }
        }
        public Products? GetProductById(int productId)
        {
            return _productDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public void AddProduct(Products addproduct)
        {
            _productDbContext.Products.Add(addproduct);
            _productDbContext.SaveChanges();
        }
        public void UpdateProduct(Products updateproduct)
        {
            _productDbContext.Update(updateproduct);
            _productDbContext.SaveChanges();
        }
        public void DeleteProduct(int productId)
        {
            Products objProdut = _productDbContext.Products.Find(productId);
            _productDbContext.Products.Remove(objProdut);
            _productDbContext.SaveChanges();
        }      

    }
}
