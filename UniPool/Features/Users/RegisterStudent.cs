using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniPool.Model;

namespace UniPool.Features.Users
{
    public static class RegisterStudent
    {
        public class Command : IRequest<int>
        {
            public string student_name { get; set; }
            public int student_id { get; set; }
            public string dependency { get; set; }
            public string email { get; set; }
            public string phone_number { get; set; }
            public int typeOfAccount { get; set; }
            public string password { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly UniPoolContext _db;
            public Handler(UniPoolContext db) => _db = db;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var student = new Student
                {
                    StudentId = request.student_id,
                    StudentName = request.student_name,
                    Dependency = request.dependency,
                    PhoneNumber = request.phone_number,
                    Email = request.email,
                    AccountType = AccountType.FromValue(request.typeOfAccount),
                    Password = request.password
                };

                if (_db.Students.Any(x => x.Email == student.Email))
                {
                    throw new Exception("El correo ya está en uso.");
                }

                _db.Students.Add(student);
                await _db.SaveChangesAsync();

                return student.StudentId;
            }
        }

    }
}
