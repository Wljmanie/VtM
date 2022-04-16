using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtM.Models
{
    public class Haven
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Description { get; set; }
        [Range(0,3)]
        public int? HavenRating { get; set; }

        public virtual ICollection<HavenMerit>? HavenMerits { get; set; }
        public virtual ICollection<HavenFlaw>? HavenFlaw { get; set; }

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
