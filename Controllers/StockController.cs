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
        public IActionResult GetALL()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto StockDto)
        {
            var stockModel = StockDto.ToStockFromCreateDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
          var stockmodel = _context.Stocks.FirstOrDefault(s => s.Id == id);
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
            _context.SaveChanges();
            return Ok(stockmodel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if(stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return NoContent();
        }
    }

}