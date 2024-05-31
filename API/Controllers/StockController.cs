using API.Data;
using API.Dtos.Stock;
using API.Helpers;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepo;

    public StockController(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stocks = await _stockRepo.GetAllAsync(queryObject);
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stockDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepo.GetByIdAsync(id);
        if (stock is not null)
            return Ok(stock.ToStockDto());
        else
            return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var stockModel = stockDto.ToStockFromCreateDTO();
        await _stockRepo.CreateAsync(stockModel);

        return CreatedAtAction(
            nameof(GetById),
            new { id = stockModel.Id },
            stockModel.ToStockDto()
        );
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateStockRequestDto updateStock
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var stock = await _stockRepo.UpdateAsync(id, updateStock);
        if (stock is null)
            return NotFound();

        return Ok(stock.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _stockRepo.DeleteAsync(id);
        if (stock is not null)
            return NoContent();
        else
            return NotFound();
    }
}
