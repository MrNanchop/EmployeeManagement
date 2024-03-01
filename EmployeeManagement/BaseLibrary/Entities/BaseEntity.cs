namespace BaseLibrary.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreateOn { get; set; }

    }
}
