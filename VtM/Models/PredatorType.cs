namespace VtM.Models
{
    public class PredatorType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string HuntingRole { get; set; } = null!;
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
