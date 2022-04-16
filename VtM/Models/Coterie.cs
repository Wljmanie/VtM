﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VtM.Models
{
    public class Coterie
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Chasse { get; set; }
        public int? Lien { get; set; }
        public int? Portillon { get; set; }
        public string? CoterieType { get; set; }

        //-- Image --//
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile? FormFile { get; set; }

        [DisplayName("FileName")]
        public string? FileName { get; set; }
        public string? FileData { get; set; }

        [DisplayName("File Extention")]
        public string? FileContentType { get; set; }



        public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
        public virtual ICollection<CoterieTenet> CoterieTenets { get; set; } = new HashSet<CoterieTenet>();
    }
}