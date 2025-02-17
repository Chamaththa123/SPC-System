namespace SPC.Models
{
    public class TenderSubmission
    {
        public int IdTenderSubmission { get; set; }
        public int TenderIdTender { get; set; }
        public int SupplierIdSupplier { get; set; }
        public string Description { get; set; }
        public string UnitPrice { get; set; }
        public int Status { get; set; }
        public string Date { get; set; }
    }
}
