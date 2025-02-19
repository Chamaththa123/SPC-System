namespace SPC.Models
{
    public class Stock
    {
        public int IdStock { get; set; }
        public int DrugIdDrug { get; set; }
        public string DrugCode { get; set; }  
        public string DrugName { get; set; } 
        public int BranchId { get; set; }
        public int InStock { get; set; }
        public string ExpireDate { get; set; }
    }
}
