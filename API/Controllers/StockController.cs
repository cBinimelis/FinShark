using API.Data;
using API.Dtos.Stock;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public StockController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _dbContext.Stocks.ToListAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _dbContext.Stocks.FindAsync(id);
            if (stock is not null)
                return Ok(stock.ToStockDto());
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _dbContext.Stocks.AddAsync(stockModel);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStock)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock is not null)
            {
                stock.Symbol = updateStock.Symbol;
                stock.CompanyName = updateStock.CompanyName;
                stock.Purchase = updateStock.Purchase;
                stock.LastDividend = updateStock.LastDividend;
                stock.Industry = updateStock.Industry;
                stock.MarketCap = updateStock.MarketCap;

                await _dbContext.SaveChangesAsync();
                return Ok(stock.ToStockDto());
            }
            else
                return NotFound();

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock is not null)
            {
                _dbContext.Remove(stock);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            else
                return NotFound();
        }
    }
}