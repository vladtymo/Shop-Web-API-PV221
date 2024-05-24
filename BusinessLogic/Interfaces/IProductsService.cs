using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<IEnumerable<ProductDto>> Get(IEnumerable<int> ids);
        Task<ProductDto?> Get(int id);
        IEnumerable<CategoryDto> GetAllCategories();
        void Create(CreateProductModel product);
        Task Edit(EditProductModel product);
        Task Delete(int id);

        Task CleanUpProductImages();
    }
}
