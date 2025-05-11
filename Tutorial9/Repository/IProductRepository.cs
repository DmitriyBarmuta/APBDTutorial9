using Tutorial9.Model.Product;

namespace Tutorial9.Repository;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
}