using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VtM.Enums;

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

        public Publicity Publicity { get; set; }

        public virtual ICollection<HavenMerit>? HavenMerits { get; set; } = new HashSet<HavenMerit>();
        public virtual ICollection<HavenFlaw>? HavenFlaw { get; set; } = new HashSet<HavenFlaw>();

        public virtual ICollection<HavenImage>? HavenImages { get; set; } = new HashSet<HavenImage>();

    }
}
