using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class ThinBloodAlchemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string ActivationCost { get; set; }
        public string? DicePools { get; set; }
        public string System { get; set; }
        public string? Duration { get; set; }
        public string? AthanorCorporis { get; set; }
        public string? Calcinatio { get; set; }
        [Range(1,5)]
        public int AlchemyLevel { get; set; }
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
