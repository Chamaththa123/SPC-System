namespace SPC.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Contact { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public int BranchId { get; set; }
    }
}
