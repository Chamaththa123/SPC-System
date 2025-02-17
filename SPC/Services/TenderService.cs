using SPC.DataAccess;
using SPC.Models;

namespace SPC.Services
{
    public class TenderService
    {
        private readonly TenderDAL _tenderDAL;

        public TenderService(TenderDAL tenderDAL)
        {
            _tenderDAL = tenderDAL;
        }

        public Task<int> AddTender(Tender tender) => _tenderDAL.AddTender(tender);

        public async Task<List<dynamic>> GetAllTenders()
        {
            return await _tenderDAL.GetAllTenders();
        }


        public async Task<dynamic> GetTenderById(int id) => await _tenderDAL.GetTenderById(id);

        public Task<int> UpdateTender(Tender tender) => _tenderDAL.UpdateTender(tender);

        public Task<int> DeleteTender(int id) => _tenderDAL.DeleteTender(id);

        public Task<int> ToggleTenderStatus(int id) => _tenderDAL.ToggleTenderStatus(id);
    }
}
