using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniPool.Model;

namespace UniPool.Features.Drivers
{
    public static class GetCurrentTrip
    {
        public class Query : IRequest<Result>
        {
            public int DriverId { get; set; }
        }

        public class Result
        {
            public int TripId { get; set; }
            public string Destination { get; set; }
            public int AvailableSeats { get; set; }
            public int MaxCapacity { get; set; }
            public string MeetingLocation { get; set; }
            public decimal Fare { get; set; }
            public List<Student> StudentsInTrip {get;set;}
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly UniPoolContext _db;

            public Handler(UniPoolContext db) => _db = db;

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var trip = await _db.Trips.Include(x => x.StudentsInTrip).ThenInclude(x => x.Student).SingleOrDefaultAsync(x => x.DriverId == request.DriverId && x.Status != TripStatus.Finished);

                return new Result
                {
                    TripId = trip.TripId,
                    Destination = trip.Destination,
                    AvailableSeats = trip.MaxCapacity,
                    MaxCapacity = trip.MaxCapacity,
                    MeetingLocation = trip.MeetingLocation,
                    Fare = trip.Fare,
                    StudentsInTrip = trip.StudentsInTrip.Select(x => x.Student).ToList()
                };
            }
        }
    }
}
