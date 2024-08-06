using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetAPI.Dtos.Stock;
using dotnetAPI.interfaces;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace dotnetAPI.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
             return await _context.Stocks.Include(s => s.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
           return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Stock?> GetByIdAsync(int id, UpdateStockRequestDto stockDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
           var exists = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
           if(exists == null)
           {
               return null;
           }
            exists.Symbol = stockDto.Symbol;
            exists.CompanyName = stockDto.CompanyName;
            exists.Purchase = stockDto.Purchase;
            exists.LastDiv = stockDto.LastDiv;
            exists.Industry = stockDto.Industry;
            exists.MarketCap = stockDto.MarketCap;
            
            await  _context.SaveChangesAsync();
            
            return exists;
        }
    }
}
/*
   private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(s => s.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
           var exists = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == s.Id);
           if(exists == null)
           {
               return null;
           }
            exists.Symbol = stockDto.Symbol;
            exists.CompanyName = stockDto.CompanyName;
            exists.Purchase = stockDto.Purchase;
            exists.LastDiv = stockDto.LastDiv;
            exists.Industry = stockDto.Industry;
            exists.MarketCap = stockDto.MarketCap;
            
            await  _context.SaveChangesAsync();
            
            return exists;
        }
*/