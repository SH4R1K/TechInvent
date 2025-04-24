namespace TechInvent.DM.Models
{
    public class Disk : Component
    {
        public int IdComponent { get; set; }

        public string Model { get; set; }

        public int Size { get; set; }

        public virtual Component IdComponentNavigation { get; set; } = null!;
        public override bool Equals(object? obj)
        {
            if (obj is Disk other)
            {
                return Name == other.Name &&
                       Model == other.Model &&
                       Size == other.Size;
            }
            return false;
        }
    }
}
