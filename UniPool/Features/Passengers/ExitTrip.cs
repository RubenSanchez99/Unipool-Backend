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
    public static class ExitTrip
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

                if (trip.StudentsInTrip == null)
                {
                    throw new Exception("No hay estudiantes en este viaje.");
                }

                var student = await _db.Students.SingleOrDefaultAsync(x => x.StudentId == request.StudentId);
                if (student == null)
                {
                    throw new Exception("El estudiante no existe.");
                }

                trip.StudentsInTrip.RemoveAll(x => x.StudentId == student.StudentId);
                await _db.SaveChangesAsync();

                return trip;
            }
        }
    }
}
