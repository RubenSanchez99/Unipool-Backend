using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniPool.Model;

namespace UniPool.Features.Drivers
{
    public class RegisterTrip
    {
        public class Command : IRequest<int>
        {
            public int DriverId { get; set; }
            public string Destination { get; set; }
            public int MaxCapacity { get; set; }
            public string MeetingLocation { get; set; }
            public DateTime DepartureTime { get; set; }
            public decimal Fare { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly UniPoolContext _db;

            public Handler(UniPoolContext db) => _db = db;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var trip = new Trip
                {
                    DriverId = request.DriverId,
                    Destination = request.Destination,
                    MaxCapacity = request.MaxCapacity,
                    MeetingLocation = request.MeetingLocation,
                    DepartureTime = request.DepartureTime,
                    Fare = request.Fare
                };

                await _db.Trips.AddAsync(trip);
                await _db.SaveChangesAsync();

                return trip.TripId;
            }
        }
    }
}
