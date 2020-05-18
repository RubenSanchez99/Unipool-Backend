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
    public static class FinishTrip
    {
        public class Command : IRequest<Trip>
        {
            public int TripId { get; set; }
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

                trip.StudentsInTrip = new List<StudentTrip>();
                trip.Status = TripStatus.Finished;
                await _db.SaveChangesAsync();

                return trip;
            }
        }
    }
}
