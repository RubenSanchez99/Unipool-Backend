using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniPool.Model;

namespace UniPool.Features.Passengers
{
    public class GetTripInfo
    {
        public class Query : IRequest<Result>
        {
            public int TripId { get; set; }
        }

        public class Result
        {
            public int TripId { get; set; }
            public string DriverName { get; set; }
            public string DriverDependency { get; set; }
            public string Destination { get; set; }
            public int AvailableSeats { get; set; }
            public int MaxCapacity { get; set; }
            public string MeetingLocation { get; set; }
            public decimal Fare { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly UniPoolContext _db;

            public Handler(UniPoolContext db) => _db = db;

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = _db.Trips.Include(x => x.Driver).Single(x => x.TripId == request.TripId);
                return new Result
                {
                    TripId = result.TripId,
                    DriverName = result.Driver.StudentName,
                    DriverDependency = result.Driver.Dependency,
                    Destination = result.Destination,
                    AvailableSeats = result.MaxCapacity,
                    MaxCapacity = result.MaxCapacity,
                    MeetingLocation = result.MeetingLocation,
                    Fare = result.Fare
                };
            }
        }
    }
}
