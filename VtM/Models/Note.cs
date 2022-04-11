namespace VtM.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CharacterId { get; set; }

        public virtual Character Character { get; set; } = null!;
    }
}
