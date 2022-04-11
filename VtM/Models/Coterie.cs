namespace VtM.Models
{
    public class Coterie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Chasse { get; set; }
        public int? Lien { get; set; }
        public int? Portillon { get; set; }
        public string? CoterieType { get; set; }



        public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
        public virtual ICollection<CoterieTenet> ChronicleTenets { get; set; } = new HashSet<CoterieTenet>();
    }
}
