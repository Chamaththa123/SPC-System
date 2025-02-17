using SPC.DataAccess;
using SPC.Models;

namespace SPC.Services
{
    public class SupplierOrderService
    {
        private readonly SupplierOrderDAL _supplierOrderDAL;

        public SupplierOrderService(SupplierOrderDAL supplierOrderDAL)
        {
            _supplierOrderDAL = supplierOrderDAL;
        }

        public async Task<int> AddSupplierOrder(SupplierOrder order) => await _supplierOrderDAL.AddSupplierOrder(order);
        public async Task<List<SupplierOrder>> GetAllSupplierOrders() => await _supplierOrderDAL.GetAllSupplierOrders();
        public async Task<SupplierOrder> GetSupplierOrderById(int id) => await _supplierOrderDAL.GetSupplierOrderById(id);
        public async Task<bool> UpdateSupplierOrderStatus(int id, int status) => await _supplierOrderDAL.UpdateSupplierOrderStatus(id, status);
        public List<SupplierOrder> GetSupplierOrdersBySupplierID(int supplierId)
        {
            return _supplierOrderDAL.GetSupplierOrdersBySupplierID(supplierId);
        }

    }
}
