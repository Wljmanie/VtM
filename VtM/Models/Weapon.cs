using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //-- Image --//
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? FormFile { get; set; }

        [DisplayName("FileName")]
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }

        [DisplayName("File Extention")]
        public string? FileContentType { get; set; }

        public virtual Character? Character { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;

    }
}
