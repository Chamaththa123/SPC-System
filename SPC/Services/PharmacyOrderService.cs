using SPC.DataAccess;
using SPC.Models;

namespace SPC.Services
{
    public class PharmacyOrderService
    {
        private readonly PharmacyOrderDAL _pharmacyOrderDAL;

        public PharmacyOrderService(PharmacyOrderDAL pharmacyOrderDAL)
        {
            _pharmacyOrderDAL = pharmacyOrderDAL;
        }

        public async Task<int> AddPharmacyOrder(PharmacyOrder order) => await _pharmacyOrderDAL.AddPharmacyOrder(order);
        public async Task<List<PharmacyOrder>> GetAllPharmacyOrders() => await _pharmacyOrderDAL.GetAllPharmacyOrders();
        public async Task<PharmacyOrder> GetPharmacyOrderById(int id) => await _pharmacyOrderDAL.GetPharmacyOrderById(id);
        public async Task<bool> UpdatePharmacyOrderStatus(int id, int status) => await _pharmacyOrderDAL.UpdatePharmacyOrderStatus(id, status);
        public async Task<List<PharmacyOrder>> GetPharmacyOrdersByBranchID(int branchId) => await _pharmacyOrderDAL.GetPharmacyOrdersByBranchID(branchId);
    }
}
