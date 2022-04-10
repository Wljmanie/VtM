namespace VtM.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteName { get; set; } = null!;
        public string NoteDescription { get; set; } = null!;
        public int CharacterId { get; set; }

        public virtual Character Character { get; set; } = null!;
    }
}
