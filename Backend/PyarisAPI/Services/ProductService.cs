using PyarisAPI.Models;

namespace PyarisAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByGroupAsync(string group);
        Task<IEnumerable<Product>> GetProductsBySubGroupAsync(string subGroup);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }

    public class ProductService : IProductService
    {
        private readonly PyarisAPI.Data.PyarisDbContext _context;

        public ProductService(PyarisAPI.Data.PyarisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _context.Products.Where(p => p.Active).OrderByDescending(p => p.Id).ToList();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByGroupAsync(string group)
        {
            return _context.Products.Where(p => p.Group == group && p.Active).ToList();
        }

        public async Task<IEnumerable<Product>> GetProductsBySubGroupAsync(string subGroup)
        {
            return _context.Products.Where(p => p.SubGroup == subGroup && p.Active).ToList();
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
