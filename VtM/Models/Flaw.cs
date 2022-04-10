using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class Flaw
    {
        public int Id { get; set; }
        public string FlawName  { get; set; } = null!;
        public string FlawDescription { get; set; } = null!;
        [Range(1,5)]
        public int FlawValue { get; set; }
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; } = null!;
    }
}
