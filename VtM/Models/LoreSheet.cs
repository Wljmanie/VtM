namespace VtM.Models
{
    public class LoreSheet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
