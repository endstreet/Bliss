using Bliss.Models;

namespace Bliss.Services
{
    public class DbService
    {
        BlissContext db;
        public DbService()
        {
            db = new BlissContext();
        }
        //Cruises
        public Cruise AddNewCruise(Cruise cruise)
        {
            if (db.Cruises.Where(c => c.Name == cruise.Name).Any())
            {
                return GetCruise(cruise.Name);
            }
            db.Add(cruise);
            db.SaveChanges();
            return GetCruise(cruise.Name);
        }

        public Cruise GetCruise(string cruiseName)
        {
            return db.Cruises.Where(c => c.Name == cruiseName).First();
        }

        public Cruise UpdateCruise(Cruise cruise)
        {
            Cruise? c = db.Cruises.Find(cruise.Id);
            c.Name = cruise.Name;
            c.ActiveWaypoint = cruise.ActiveWaypoint;
            db.SaveChanges();
            return GetCruise(cruise.Name);
        }

        public void DeleteCruise(Cruise cruise)
        {
            db.Cruises.Remove(cruise);
            db.SaveChanges();
        }

        public List<Cruise> GetCruises(int cruiseId)
        {
            return db.Cruises.ToList();
        }

        public List<WayPoint> GetCruiseWayPoints(int cruiseId)
        {
            return db.WayPoints.Where(x => x.CruiseId == cruiseId).ToList();
        }

        public WayPoint AddNewWayPoint(WayPoint wayPoint)
        {
            //Connect Inserted waypoint to its neighbour.
            if (db.WayPoints.Where(c => c.CruiseId == wayPoint.CruiseId && c.Sequence >= wayPoint.Sequence).Any())
            {
                WayPoint wa = db.WayPoints.Where(c => c.CruiseId == wayPoint.CruiseId && c.Sequence >= wayPoint.Sequence).First();
                wa.StartLat = wayPoint.EndLat;
                wa.StartLng = wayPoint.EndLng;
                db.SaveChanges();
            }
            //Shuffle for inserts
            foreach (WayPoint w in db.WayPoints.Where(c => c.CruiseId == wayPoint.CruiseId && c.Sequence >= wayPoint.Sequence))
            {
                w.Sequence++;
            }
            if (db.WayPoints.Where(c => c.StartLat == wayPoint.StartLat).Any())
            {
                return GetWayPoint(wayPoint.StartLat);
            }
            db.Add(wayPoint);
            db.SaveChanges();
            return GetWayPoint(wayPoint.StartLat);
        }

        public WayPoint GetWayPoint(double startLat)
        {
            return db.WayPoints.Where(c => c.StartLat == startLat).First();
        }

        public WayPoint UpdateWayPoint(WayPoint wayPoint)
        {
            WayPoint? c = db.WayPoints.Find(wayPoint.Id);
            c.StartLat = wayPoint.StartLat;
            c.StartLng = wayPoint.StartLng;
            c.EndLat = wayPoint.EndLat;
            c.EndLng = wayPoint.EndLng;
            c.Arrival = wayPoint.Arrival;
            c.CruiseId = wayPoint.CruiseId;
            c.Departure = wayPoint.Departure;
            c.Sequence = wayPoint.Sequence;
            db.SaveChanges();
            return GetWayPoint(wayPoint.StartLat);
        }

        public void DeleteWayPoint(WayPoint wayPoint)
        {
            //Roll up the remaining sequences.
            foreach (WayPoint w in db.WayPoints.Where(c => c.CruiseId == wayPoint.CruiseId && c.Sequence >= wayPoint.Sequence))
            {
                w.Sequence--;
            }
            db.WayPoints.Remove(wayPoint);
            db.SaveChanges();
        }
    }
}
