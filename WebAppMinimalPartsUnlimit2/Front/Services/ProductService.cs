using WebAppMinimalPartsUnlimit2.Entities.Dtos;

namespace WebAppMinimalPartsUnlimit2.Front.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("products");
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"products/{id}");
        }

        public async Task CreateProductAsync(ProductCreateDto product)
        {
            await _httpClient.PostAsJsonAsync("products", product);
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            await _httpClient.PutAsJsonAsync($"products/{product.ProductId}", product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _httpClient.DeleteAsync($"products/{id}");
        }
    }
}
