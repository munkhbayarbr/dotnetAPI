using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Dtos.Stock
{
    public class StockDto
    {
         public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    
        public decimal Purchase { get; set; }
    
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }

        internal static Models.Stock ToStockFromCreateDto()
        {
            throw new NotImplementedException();
        }
    }
}