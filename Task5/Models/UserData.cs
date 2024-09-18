namespace FakeUserGenerator.Models
{
    public class UserData
    {
        public int Number { get; set; }
        public Guid Identifier { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
