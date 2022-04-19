using VtM.Enums;

namespace VtM.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillName { get; set; }

        public SkillType SkillType { get; set; }

        public string? DescriptionLevel1 { get; set; }
        public string? DescriptionLevel2 { get; set; }
        public string? DescriptionLevel3 { get; set; }
        public string? DescriptionLevel4 { get; set; }
        public string? DescriptionLevel5 { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
    }
}
