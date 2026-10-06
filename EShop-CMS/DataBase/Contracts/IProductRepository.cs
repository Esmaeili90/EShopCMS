using EShop_CMS.DataBase.Model;

namespace EShop_CMS.DataBase.Contracts
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        Product GetProductByGroup(ProductGroup group);
        int AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productid);
        void DeleteProduct(Product product);
        public List<Product> SearchProducts(string keyword);
        public List<Product> FindInSliders();
        public IEnumerable<Product> OrderProductsDec();
        public Product GetProductByIdNoTracking(int productId);
        public Product GetProductByIdWithGroup(int productId);
    }
}
