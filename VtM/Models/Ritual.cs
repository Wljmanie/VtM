using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class Ritual
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Range(1,5)]
        public int RitualLevel { get; set; }
        public string? Ingredients { get; set; }
        public string? Process { get; set; }
        public string? System { get; set; }
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
