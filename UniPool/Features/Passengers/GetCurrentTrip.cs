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
    public static class GetCurrentTrip
    {
        public class Query : IRequest<Result>
        {
            public int StudentId { get; set; }
        }

        public class Result
        {
            public int TripId { get; set; }
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
                var student = await _db.Students.SingleOrDefaultAsync(x => x.StudentId == request.StudentId);
                if (student == null)
                {
                    throw new Exception("El estudiante no existe.");
                }

                var trip = await _db.Trips.Include(x => x.StudentsInTrip).ThenInclude(x => x.Student).SingleOrDefaultAsync(x => x.StudentsInTrip.Any(x => x.StudentId == student.StudentId));

                return new Result
                {
                    TripId = trip.TripId,
                    Destination = trip.Destination,
                    AvailableSeats = trip.MaxCapacity,
                    MaxCapacity = trip.MaxCapacity,
                    MeetingLocation = trip.MeetingLocation,
                    Fare = trip.Fare
                };
            }
        }
    }
}
