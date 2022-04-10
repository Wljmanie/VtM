namespace VtM.Models
{
    public class SkillSpecialization
    {
        public int Id { get; set; }
        
        public string Specialization { get; set; } = null!;

        public int CharacterSkillId { get; set; }

        public virtual CharacterSkill CharacterSkill { get; set; } = null!;

    }
}
