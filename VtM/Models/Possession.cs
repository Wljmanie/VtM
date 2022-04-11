namespace VtM.Models
{
    public class Possession
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Amount { get; set; }
        public string? Description { get; set; }
        public int CharacterId { get; set; }
        public bool CharacterWearsIt { get; set; }
        public string Location { get; set; } = null!;

        public virtual Character Character { get; set; } = null!;
    }
}
