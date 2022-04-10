using System.ComponentModel.DataAnnotations;

namespace VtM.Models
{
    public class Merit
    {
         
        public int Id { get; set; }
        public string MeritName { get; set; } = null!;
        public string MeritDescription { get; set; } = null!;
        [Range(1, 5)]
        public int MeritValue { get; set; }
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; } = null!;

    }
}
