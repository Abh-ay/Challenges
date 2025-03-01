using WebApplication1.Models;
using WebApplication1.Repository.Interface;

namespace WebApplication1.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _saleRepository.GetAllSalesAsync();
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _saleRepository.GetSaleByIdAsync(id);
        }

        public async Task<Sale> AddSaleAsync(Sale sale)
        {
            return await _saleRepository.AddSaleAsync(
                new Sale
                {
                    Name = sale.Name,
                    Description = sale.Description,
                    Price = sale.Price,
                    StartedOn = sale.StartedOn,
                    EndedOn = sale.EndedOn,
                    AddedOn = sale.AddedOn,
                    EditedOn = sale.EditedOn,
                    RefLocationId = sale.RefLocationId
                }
                );
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            return await _saleRepository.DeleteSaleAsync(id);
        }
    }

}
