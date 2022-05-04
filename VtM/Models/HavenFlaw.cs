using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class HavenFlaw
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Range(1, 5)]
        public int Value { get; set; }
        public int HavenId { get; set; }
        public virtual Haven? Haven { get; set; }
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
