using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniPool.Model
{
    public class TripStatus : SmartEnum<TripStatus>
    {
        public static readonly TripStatus Registered = new TripStatus(nameof(Registered), 1);
        public static readonly TripStatus OnCourse = new TripStatus(nameof(OnCourse), 2);
        public static readonly TripStatus Finished = new TripStatus(nameof(Finished), 3);

        public TripStatus(string name, int value) : base(name, value)
        {
        }
    }
}
