using WebApiModel;

namespace WebEmployees
{
    public interface ISuppliers
    {
        Task<Supplier?> GetAsync(int supplierId);
        Task<Supplier?> UpdateAsync(int supplierId, Supplier supplier);
        Task<bool?> DeleteAsync(int supplierId);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier?> CreateAsync(Supplier supplier);
    }
}