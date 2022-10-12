using System.ComponentModel.DataAnnotations;

namespace Bliss.Models
{
    public class Cruise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ActiveWaypoint { get; set; }
        public List<WayPoint> WayPoints { get; set; }
        public Cruise()
        {
            Name = "";
            WayPoints = new List<WayPoint>();
        }

    }
}
