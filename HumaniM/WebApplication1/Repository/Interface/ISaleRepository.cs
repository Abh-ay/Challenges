using WebApplication1.Models;

namespace WebApplication1.Repository.Interface
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<Sale> AddSaleAsync(Sale sale);
        Task<bool> DeleteSaleAsync(int id);

    }
}