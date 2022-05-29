using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
