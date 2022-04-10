namespace VtM.Models
{
    public class TouchstoneConviction
    {
        public int Id { get; set; }
        public string Touchstone { get; set; } = null!;
        public string Conviction { get; set; } = null!;

        public int CharacterId { get; set; }

        public virtual Character Character { get; set; } = null!;
    }
}
