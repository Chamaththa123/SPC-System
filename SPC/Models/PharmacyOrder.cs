namespace SPC.Models
{
    public class PharmacyOrder
    {
        public int IdPharmacyOrder { get; set; }
        public int StockId { get; set; }
        public int DrugId { get; set; }
        public string DrugCode { get; set; }
        public string DrugName { get; set; }
        public int BranchId { get; set; }
        public int Qty { get; set; }
        public int Status { get; set; }
    }
}
