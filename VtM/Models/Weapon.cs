using VtM.Enums;

namespace VtM.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DamageType DamageType { get; set; }
        public int DamageModifier { get; set; }
        public int? Ammo { get; set; }
        public int? CharacterId { get; set; }

        public virtual Character? Character { get; set; }

    }
}
