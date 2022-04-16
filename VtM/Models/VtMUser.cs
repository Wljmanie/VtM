using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtM.Models
{
    public class VtMUser : IdentityUser
    {
        public string NickName { get; set; } = null!;

        //-- Image --//
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? FormFile { get; set; }

        [DisplayName("FileName")]
        public string? FileName { get; set; }
        public string? FileData { get; set; }

        [DisplayName("File Extention")]
        public string? FileContentType { get; set; }

        public virtual ICollection<Character> Characters { get; set; } = new HashSet<Character>();


    }
}
