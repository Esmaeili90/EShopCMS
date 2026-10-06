using EShop_CMS.DataBase.Context;
using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using Microsoft.EntityFrameworkCore;

namespace EShop_CMS.DataBase.Repository
{
    public class ProductRepository : IProductRepository
    {
        DefaultContext _context;
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }

        public void DeleteProduct(int productid)
        {
            var product = GetProductById(productid);
            DeleteProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            var result = _context.Products.ToList();
            return result;
        }

        public Product GetProductByGroup(ProductGroup group)
        {
            return _context.Products.FirstOrDefault(p => p.group == group);
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public List<Product> SearchProducts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllProducts();
            }

            return _context.Products
                .Where(p => p.Title.Contains(keyword))
                .ToList();
        }

        public List<Product> FindInSliders()
        {
            return _context.Products.Where(p => p.InSlider).ToList();
        }

        

        IEnumerable<Product> IProductRepository.OrderProductsDec()
        {
            var result = _context.Products.Where(p => p.IsPresent).OrderByDescending(p => p.DataCreated)
                 .Take(10).ToList();
            return result;
        }
        public Product GetProductByIdNoTracking(int productId)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == productId);
        }
        public Product GetProductByIdWithGroup(int productId)
        {
            return _context.Products
                .Include(p => p.group)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == productId);
        }
    }
}
