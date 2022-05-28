using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bliss.Shared
{
    internal class WayPoint
    {
        [Key]
        public int Id { get; set; }
        public int CruiseId { get; set; }
        public int Sequence { get; set; }
        public PointLatLng StartPoint { get; set; }
        public PointLatLng EndPoint { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Arrival { get; set; }

        [NotMapped]
        public bool IsSelected { get;set; } 

        public WayPoint(int sequence)
        {

        }

    }
}
