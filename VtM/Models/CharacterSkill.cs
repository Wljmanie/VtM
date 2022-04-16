using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class CharacterSkill
    {
        public int Id { get; set; }
        public int SkillId { get; set; }

        [Range(1,5)]
        public int? SkillLevel { get; set; }

        public int? CharacterId { get; set; }

        public virtual Skill? Skill{ get; set; }
        public virtual Character? Character{ get; set; }
        public virtual ICollection<SkillSpecialization> Specializations { get; set; } = new HashSet<SkillSpecialization>();
    }
}
