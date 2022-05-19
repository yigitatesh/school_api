namespace SchoolApi.Models
{
    public class Student 
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => string.Format("{0} {1}", FirstName, LastName);
        public int Grade { get; set; } = 0;
        public string? PhoneNumber { get; set; }
    }
}