namespace VtM.Models
{
    public class PredatorType
    {
        public int Id { get; set; }
        public string PredatorName { get; set; } = null!;
        public string PredatorDescription { get; set; } = null!;

        public string HuntingRole { get; set; } = null!;
    }
}
