namespace SPC.Models
{
    public class SupplierOrder
    {
        public int IdSupplierOrder { get; set; }
        public int DrugId { get; set; }
        public string DrugCode { get; set; }
        public string DrugName { get; set; }
        public int SupplierId { get; set; }
        public int Qty { get; set; }
        public int Status { get; set; }
    }
}
