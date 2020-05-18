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
    public static class RegisterTrip
    {
        public class Command : IRequest<Trip>
        {
            public int DriverId { get; set; }
            public string Destination { get; set; }
            public string CoordinatesLatitude { get; set; }
            public string CoordinatesLongitude { get; set; }
            public int MaxCapacity { get; set; }
            public string MeetingLocation { get; set; }
            public string DepartureTime { get; set; }
            public decimal Fare { get; set; }
        }

        public class Handler : IRequestHandler<Command, Trip>
        {
            private readonly UniPoolContext _db;

            public Handler(UniPoolContext db) => _db = db;

            public async Task<Trip> Handle(Command request, CancellationToken cancellationToken)
            {
                var driver = await _db.Students.SingleAsync(x => x.StudentId == request.DriverId);

                if (driver.AccountType != AccountType.Driver)
                {
                    throw new Exception("El usuario no está registrado como chofer.");
                }

                var departureTimeSplit = request.DepartureTime.Split(":");
                var departureHour = int.Parse(departureTimeSplit[0]);
                var departureMinute = int.Parse(departureTimeSplit[1]);

                var trip = new Trip
                {
                    DriverId = request.DriverId,
                    Destination = request.Destination,
                    CoordinatesLatitude = request.CoordinatesLatitude,
                    CoordinatesLongitude = request.CoordinatesLongitude,
                    MaxCapacity = request.MaxCapacity,
                    MeetingLocation = request.MeetingLocation,
                    DepartureTime = DateTime.Today.AddHours(departureHour).AddMinutes(departureMinute),
                    Fare = request.Fare,
                    Status = TripStatus.Registered
                };

                if (_db.Trips.Any(x => x.DriverId == request.DriverId && x.Status != TripStatus.Finished))
                {
                    throw new Exception("No se pude tener más de un viaje activo a la vez.");
                }

                await _db.Trips.AddAsync(trip);
                await _db.SaveChangesAsync();

                return trip;
            }
        }
    }
}
