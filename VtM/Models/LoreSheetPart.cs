using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class LoreSheetPart
    {
        public int Id { get; set; }
        public int LoreSheetId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Range(1,5)]
        public int Level { get; set; }

        public virtual LoreSheet? LoreSheet { get; set; }
    }
}
