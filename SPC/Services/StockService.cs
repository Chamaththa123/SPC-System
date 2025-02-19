using SPC.DataAccess;
using SPC.Models;

namespace SPC.Services
{
    public class StockService
    {
        private readonly StockDAL _stockDAL;

        public StockService(StockDAL stockDAL)
        {
            _stockDAL = stockDAL;
        }

        public async Task<int> CreateStock(Stock stock)
        {
            return await _stockDAL.CreateStock(stock);
        }

        public async Task<List<Stock>> GetStockByBranch(int branchId)
        {
            return await _stockDAL.GetStockByBranch(branchId);
        }

        public async Task<Stock> GetStockById(int stockId)
        {
            return await _stockDAL.GetStockById(stockId);
        }

        public async Task<int> UpdateStockInStock(int stockId, int inStock)
        {
            return await _stockDAL.UpdateStockInStock(stockId, inStock);
        }
    }
}
