namespace VtM.Models
{
    public class DisciplineLevel
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int CharacterId { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual Character Character { get; set; }
    }
}
