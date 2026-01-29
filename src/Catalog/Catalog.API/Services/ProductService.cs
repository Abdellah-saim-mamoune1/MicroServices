using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMongoClient client, IOptions<MongoDbSettings> options)
        {
            var database = client.GetDatabase(options.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await _products.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ProductDto product)
        {
            Product p = new Product
            {
                Category = product.Category,
                Description = product.Description,
                Price = product.Price,
                Name = product.Name,
                ImageFile = product.ImageFile,
                Summary = product.Summary
            };
            await _products.InsertOneAsync(p);
        }

        public async Task UpdateAsync(string id, Product product) =>
            await _products.ReplaceOneAsync(x => x.Id == id, product);

        public async Task DeleteAsync(string id) =>
            await _products.DeleteOneAsync(x => x.Id == id);
    }
}
