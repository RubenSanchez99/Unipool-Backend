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
    public static class JoinTrip
    {
        public class Command : IRequest<Trip>
        {
            public int TripId { get; set; }
            public int StudentId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Trip>
        {
            private readonly UniPoolContext _db;

            public Handler(UniPoolContext db) => _db = db;

            public async Task<Trip> Handle(Command request, CancellationToken cancellationToken)
            {
                var trip = await _db.Trips.Include(x => x.StudentsInTrip).SingleOrDefaultAsync(x => x.TripId == request.TripId);
                if (trip == null)
                {
                    throw new Exception("El viaje no existe.");
                }

                if (trip.Status != TripStatus.Registered)
                {
                    throw new Exception("El viaje no está disponible.");
                }

                if (trip.StudentsInTrip == null)
                {
                    trip.StudentsInTrip = new List<StudentTrip>();
                }

                var student = await _db.Students.SingleOrDefaultAsync(x => x.StudentId == request.StudentId);
                if (student == null)
                {
                    throw new Exception("El estudiante no existe.");
                }

                if (trip.StudentsInTrip.Count + 1 > trip.MaxCapacity)
                {
                    throw new Exception("El viaje ya está en su capacidad máxima.");
                }

                if (_db.Trips.Any(x => x.StudentsInTrip.Any(x => x.StudentId == student.StudentId)))
                {
                    throw new Exception("Ya estás en un viaje activo.");
                }

                trip.StudentsInTrip.Add(new StudentTrip() { StudentId = student.StudentId, TripId = trip.TripId });
                await _db.SaveChangesAsync();

                return trip;
            }
        }
    }
}
