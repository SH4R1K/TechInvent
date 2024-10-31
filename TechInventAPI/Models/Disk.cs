namespace TechInventAPI.Models
{
    public class Disk : Component
    {
        public int IdComponent { get; set; }

        public string Model { get; set; }
        
        public int Size { get; set; }

        public virtual Component IdComponentNavigation { get; set; } = null!;
    }
}
