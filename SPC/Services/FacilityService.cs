using SPC.DataAccess;
using SPC.Models;

namespace SPC.Services
{
    public class FacilityService
    {
        private readonly FacilityDAL _facilityDAL;

        public FacilityService(FacilityDAL facilityDAL)
        {
            _facilityDAL = facilityDAL;
        }


        public Task<List<Facility>> GetAllFacility() => _facilityDAL.GetAllFacility();
        public Task<Facility> GetFacilityById(int id) => _facilityDAL.GetFacilityById(id);
        public Task AddFacility(Facility facility) => _facilityDAL.AddFacility(facility);
        public Task UpdateFacility(Facility facility) => _facilityDAL.UpdateFacility(facility);
    }
}
