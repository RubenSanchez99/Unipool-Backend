using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniPool.Model;

namespace UniPool.Features.Drivers
{
    public static class GetDriverTrips
    {
        public class Query : IRequest<Result>
        {
            public int DriverId { get; set; }
        }

        public class Result
        {
            public List<Trip> Trips { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly UniPoolContext _db;
            public Handler(UniPoolContext db) => _db = db;

            public Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var trips = _db.Trips.Where(x => x.DriverId == request.DriverId).ToList();
                var result = new Result
                {
                    Trips = trips
                };
                return Task.FromResult(result);
            }
        }
    }
}
