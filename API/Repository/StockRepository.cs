using API.Data;
using API.Dtos.Stock;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StockRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _dbContext.Stocks.AddAsync(stockModel);
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
                return null;

            _dbContext.Stocks.Remove(stockModel);
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _dbContext.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _dbContext.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockRequestDto)
        {
            var existingStock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
                return null;


            existingStock.Symbol = stockRequestDto.Symbol;
            existingStock.CompanyName = stockRequestDto.CompanyName;
            existingStock.Purchase = stockRequestDto.Purchase;
            existingStock.LastDividend = stockRequestDto.LastDividend;
            existingStock.Industry = stockRequestDto.Industry;
            existingStock.MarketCap = stockRequestDto.MarketCap;

            await _dbContext.SaveChangesAsync();
            return existingStock;

        }
    }
}