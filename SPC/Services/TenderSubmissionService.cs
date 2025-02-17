using SPC.DataAccess;
using SPC.Models;

namespace SPC.Services
{
    public class TenderSubmissionService
    {
        private readonly TenderSubmissionDAL _tenderSubmissionDAL;

        public TenderSubmissionService(TenderSubmissionDAL tenderSubmissionDAL)
        {
            _tenderSubmissionDAL = tenderSubmissionDAL;
        }

        public async Task<int> AddTenderSubmission(TenderSubmission submission) => await _tenderSubmissionDAL.AddTenderSubmission(submission);
        public async Task<List<TenderSubmission>> GetAllTenderSubmissions() => await _tenderSubmissionDAL.GetAllTenderSubmissions();
        public async Task<bool> UpdateTenderSubmission(TenderSubmission submission) => await _tenderSubmissionDAL.UpdateTenderSubmission(submission);
        public async Task<bool> DeleteTenderSubmission(int id) => await _tenderSubmissionDAL.DeleteTenderSubmission(id);
        public async Task<bool> UpdateTenderSubmissionStatus(int id, int status) => await _tenderSubmissionDAL.UpdateTenderSubmissionStatus(id, status);
        public async Task<List<TenderSubmission>> GetTenderSubmissionsBySupplierId(int supplierId)
    => await _tenderSubmissionDAL.GetTenderSubmissionsBySupplierId(supplierId);

        public async Task<List<TenderSubmission>> GetTenderSubmissionsByTenderId(int tenderId)
            => await _tenderSubmissionDAL.GetTenderSubmissionsByTenderId(tenderId);

        public async Task<List<int>> GetActiveTenderSubmissionsByDrugId(int drugId)
        {
            return await _tenderSubmissionDAL.GetActiveTenderSubmissionsByDrugId(drugId);
        }

    }
}
