using SPC.DataAccess;
using SPC.Models;

namespace SPC.Services
{
    public class DrugService
    {
        private readonly DrugDAL _drugDAL;

        public DrugService(DrugDAL drugDAL)
        {
            _drugDAL = drugDAL;
        }

        public async Task<int> AddDrug(Drug drug) => await _drugDAL.AddDrug(drug);

        public async Task<List<Drug>> GetAllDrugs() => await _drugDAL.GetAllDrugs();

        public async Task<Drug> GetDrugById(int id) => await _drugDAL.GetDrugById(id);

        public async Task<bool> UpdateDrug(Drug drug) => await _drugDAL.UpdateDrug(drug);
    }
}
