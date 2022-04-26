using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class CoterieTenet
    {
        public int Id { get; set; }
        public int CoterieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = null!;
        //-- The Character that brought the Tenet in. --//
        [Display(Name = "Character that brought the Tenet in.")]
        public int? CharacterId { get; set; }

        public virtual Character? Character { get; set; }
        public virtual Coterie? Coterie { get; set; }
    }
}
