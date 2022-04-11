using Microsoft.AspNetCore.Identity;

namespace VtM.Models
{
    public class Chronicle
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string StoryTellerId { get; set; } = null!;

        public virtual IdentityUser StoryTeller { get; set; } = null!;
        public virtual ICollection<IdentityUser> Players { get; set; } = new HashSet<IdentityUser>();
    }
}
