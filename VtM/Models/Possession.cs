using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtM.Models
{
    public class Possession
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Amount { get; set; }
        public string? Description { get; set; }
        public int? CharacterId { get; set; }
        public bool CharacterWearsIt { get; set; }
        public string Location { get; set; } = null!;
        public int OrderId { get; set; }

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
