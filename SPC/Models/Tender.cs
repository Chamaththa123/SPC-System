namespace SPC.Models
{
    public class Tender
    {
        public int IdTender { get; set; }
        public int DrugId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Date { get; set; }
    }
}
