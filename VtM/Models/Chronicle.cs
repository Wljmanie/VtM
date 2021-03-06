using Microsoft.AspNetCore.Identity;

namespace VtM.Models
{
    public class Chronicle
    {
        

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string StoryTellerId { get; set; } = null!;

        public virtual VtMUser? StoryTeller { get; set; }
        public virtual ICollection<Character> Characters { get; set; } = new HashSet<Character>();
    }
}
