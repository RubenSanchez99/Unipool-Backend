using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniPool.Model
{
    public class Trip
    {
        public int TripId { get; set; }
        public int DriverId { get; set; }
        public Student Driver { get; set; }
        public string Destination { get; set; }
        public int MaxCapacity { get; set; }
        public string MeetingLocation { get; set; }
        public decimal Fare { get; set; }
        public DateTime DepartureTime { get; set; }

        public NpgsqlTsVector SearchVector { get; set; }
    }
}
