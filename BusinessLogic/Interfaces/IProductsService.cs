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
        void Edit(ProductDto product);
        void Delete(int id);
    }
}
