using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniPool.Model;

namespace UniPool.Features.Users
{
    public static class LogIn
    {
        public class Query : IRequest<Result> {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Result
        {
            public string student_name { get; set; }
            public int student_id { get; set; }
            public string dependency { get; set; }
            public string email { get; set; }
            public string phone_number { get; set; }
            public int typeOfAccount { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly UniPoolContext _db;
            public Handler(UniPoolContext db) => _db = db;

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _db.Students.SingleAsync(student => student.Email == request.Email && student.Password == request.Password);

                return new Result
                {
                    student_id = student.StudentId,
                    student_name = student.StudentName,
                    dependency = student.Dependency,
                    email = student.Email,
                    phone_number = student.PhoneNumber,
                    typeOfAccount = student.AccountType.Value
                };
            }
        }
    }
}
