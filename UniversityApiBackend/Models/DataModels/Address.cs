namespace UniversityApiBackend.Models.DataModels
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
     public string? Comunity { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
