using System.ComponentModel.DataAnnotations;

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
    }
}
