using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class Flaw
    {
        public int Id { get; set; }
        public string Name  { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Range(1,5)]
        public int Value { get; set; }
        public int? CharacterId { get; set; }
        public virtual Character? Character { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
