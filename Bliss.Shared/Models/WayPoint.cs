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
    public class WayPoint
    {
        [Key]
        public int Id { get; set; }
        public int CruiseId { get; set; }
        public int Sequence { get; set; }
        public double StartLat { get; set; }
        public double StartLng { get; set; }
        public double EndLat { get; set; }
        public double EndLng { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Arrival { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }

        public WayPoint(int sequence)
        {

        }

        [NotMapped]
        public PointLatLng Start
        {
            get { return new PointLatLng(StartLat, StartLng); }
        }
        [NotMapped]
        public PointLatLng End
        {
            get { return new PointLatLng(EndLat, EndLng); }
        }
    }
}
