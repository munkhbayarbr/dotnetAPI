using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetAPI.Dtos.Stock;
using dotnetAPI.interfaces;
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
        private readonly IStockRepository _stockRepository;
         public StockController(ApplicationDBContext context, IStockRepository stockRepository)
         {
            _stockRepository = stockRepository;
            _context = context;
         }
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            var stocks = await _stockRepository.GetAllAsync();
            var stockDto =  stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
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
            await _stockRepository.CreateAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
          var stockmodel = await _stockRepository.UpdateAsync(id, stockDto);
          if(stockmodel == null)
          {
              return NotFound();
          }
            return Ok(stockmodel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteAsync(id);
            if(stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}