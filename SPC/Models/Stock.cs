namespace SPC.Models
{
    public class Stock
    {
        public int IdStock { get; set; }
        public int DrugIdDrug { get; set; }
        public int BranchId { get; set; }
        public int InStock { get; set; }
        public string ExpireDate { get; set; }
    }
}
