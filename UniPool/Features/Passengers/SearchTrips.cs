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
    public static class SearchTrips
    {
        public class Query : IRequest<Result>
        {
            public string SearchQuery { get; set; }
        }

        public class Result
        {
            public List<Trip> Trips { get; set; }
            
            public class Trip
            {
                public int TripId { get; set; }
                public string Destination { get; set; }
                public int AvailableSeats { get; set; }
                public DateTime DepartureTime { get; set; }
                public decimal Fare { get; set; }
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly UniPoolContext _db;

            public Handler(UniPoolContext db) => _db = db;

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _db.Trips
                    .Where(t => t.SearchVector.Matches(request.SearchQuery) && t.Status == TripStatus.Registered)
                    .Select(t => new Result.Trip
                    {
                        TripId = t.TripId,
                        Destination = t.Destination,
                        AvailableSeats = t.MaxCapacity,
                        DepartureTime = t.DepartureTime,
                        Fare = t.Fare
                    }).ToListAsync();

                return new Result
                {
                    Trips = result
                };
            }
        }
    }
}
