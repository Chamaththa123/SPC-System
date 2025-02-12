namespace SPC.Models
{
    public class Drug
    {
        public int IdDrug { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpiryDate { get; set; }
        public int Status { get; set; }
    }
}
