using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject_Stepovyi.Models
{
    public static class RandomHelper
    {
        public static double NextDouble(
        this Random random,
        double minValue,
        double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
