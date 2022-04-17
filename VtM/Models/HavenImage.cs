using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtM.Models
{
    public class HavenImage
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int HavenId { get; set; }
        public virtual Haven Haven { get; set; } = null!;

        //-- Image --//
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? FormFile { get; set; }

        [DisplayName("FileName")]
        public string? FileName { get; set; }
        public string? FileData { get; set; }

        [DisplayName("File Extention")]
        public string? FileContentType { get; set; }
    }
}
