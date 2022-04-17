using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtM.Models
{
    public class Clan
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Bane { get; set; } = null!;
        public string Compulsion { get; set; } = null!;
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }

        //-- Image --//
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? FormFile { get; set; }

        [DisplayName("FileName")]
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }

        [DisplayName("File Extention")]
        public string? FileContentType { get; set; }

    }
}
