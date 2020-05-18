using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UniPool.Model
{
    public class Student
    {
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public string Dependency { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AccountType AccountType { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
