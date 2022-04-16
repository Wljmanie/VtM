using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VtM.Models
{
    public class Background
    {
        public int Id { get; set; }
        [MinLength(2), MaxLength(100)]
        public string BackgroundName { get; set; } = null!;
        public string BackgroundDescription { get; set; } = null!;
        [Range(1, 5)]
        public int BackgroundValue { get; set; }
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; } = null!;

        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
