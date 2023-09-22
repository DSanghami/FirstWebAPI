namespace MyFirstWebAPI.Model
{
    public class EmpViewModel
    {
        public int EmpID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? City { get; set; } = string.Empty;
        public int? ReportsTo { get; set; }
    }
}
