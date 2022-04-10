using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class BloodPotency
    {
        public int Id { get; set; }
        [Range(0,10)]
        public int BloodPotencyValue { get; set; }
        [Range(1,6)]
        public int BloodSurge { get; set; }
        [Range(1,5)]
        public int DamageMendedPerRouse { get; set; }
        [Range(0,5)]
        public int DisciplinePowerBonues { get; set; }
        [Range(0,6)]
        public int BaneSeverity { get; set; }

        public string FeedingPenalty { get; set; } = null!;

    }
}
