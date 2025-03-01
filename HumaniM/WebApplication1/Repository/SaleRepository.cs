using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repository.Interface;

namespace WebApplication1.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SaleDbContext _dbContext;

        public SaleRepository(SaleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            try
            {

                return await _dbContext.Sale.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _dbContext.Sale.FindAsync(id);
        }

        public async Task<Sale> AddSaleAsync(Sale sale)
        {
           _dbContext.Sale.Add(
               new Sale { 
                   Name = sale.Name, 
                   Description = sale.Description, 
                   Price = sale.Price, 
                   AddedOn = sale.AddedOn, 
                   EditedOn = sale.EditedOn, 
                   EndedOn = sale.EndedOn, 
                   StartedOn = sale.StartedOn, 
                   RefLocationId = sale.RefLocationId 
               });
            await _dbContext.SaveChangesAsync();
            return sale;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var s = await _dbContext.Sale.FindAsync(id);
            if (s != null) {
                _dbContext.Sale.Remove(s);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
