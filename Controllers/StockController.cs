using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetAPI.Dtos.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Dtos.Stock;
using MyApi.Mappers;

namespace MyApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
         public StockController(ApplicationDBContext context)
         {
            _context = context;
         }
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stockDto =  stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto StockDto)
        {
            var stockModel = StockDto.ToStockFromCreateDto();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
          var stockmodel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
          if(stockmodel == null)
          {
              return NotFound();
          }
            stockmodel.Symbol = stockDto.Symbol;
            stockmodel.CompanyName = stockDto.CompanyName;
            stockmodel.Purchase = stockDto.Purchase;
            stockmodel.LastDiv = stockDto.LastDiv;
            stockmodel.Industry = stockDto.Industry;
            stockmodel.MarketCap = stockDto.MarketCap;
            await  _context.SaveChangesAsync();
            return Ok(stockmodel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}