using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<Sale> AddSaleAsync(Sale sale);
        Task<bool> DeleteSaleAsync(int id);
    }
}
