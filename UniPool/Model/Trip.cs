using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace UniPool.Model
{
    public class Trip
    {
        public int TripId { get; set; }
        public int DriverId { get; set; }
        public Student Driver { get; set; }
        public string Destination { get; set; }
        public string CoordinatesLatitude { get; set; }
        public string CoordinatesLongitude { get; set; }
        public int MaxCapacity { get; set; }
        public string MeetingLocation { get; set; }
        public decimal Fare { get; set; }
        public DateTime DepartureTime { get; set; }
        public TripStatus Status { get; set; }
        [JsonIgnore]
        public NpgsqlTsVector SearchVector { get; set; }

        public List<StudentTrip> StudentsInTrip { get; set; }
    }

    public class StudentTrip
    {
        public int TripId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
