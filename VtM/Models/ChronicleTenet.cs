using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class ChronicleTenet
    {
        public int Id { get; set; }
        public int ChronicleId { get; set; }
        public string ChronicleTenetDescription { get; set; } = null!;
        //-- The Character that brought the Tenet in. --//
        [Display(Name = "Character that brought the Tenet in.")]
        public int? CharacterId { get; set; }

        public virtual Character? Character { get; set; }
        public virtual Chronicle Chronicle { get; set; } = null!;
    }
}
