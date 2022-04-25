using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtM.Models
{
    public class DisciplinePower
    {
        public int Id { get; set; }
        public int? DisciplineId { get; set; }
        [ForeignKey("Amalgam")]
        public int? AmalgamId { get; set; }
        [Range(0, 5)]
        public int? AmalgramLevel { get; set; }
        [Range(1,5)]
        public int DisciplineLevel { get; set; }
        public int RouseCost { get; set; }
        public string? AdditionalCost { get; set; }
        public string DisciplinePowerName { get; set; } = null!;
        public string? DisciplinePowerDescription { get; set; }
        public string? Duration { get; set; }
        public string? System { get; set; }
        

        //Should grab those differently
        public string? RollDescription { get; set; }
        public string? CounterRollDescription { get; set; }

        public virtual Discipline? Discipline { get; set; }
        public virtual Discipline? Amalgam { get; set; }

        public int? BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
