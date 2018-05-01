using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace CourseProject_Stepovyi.Models
{
    public class DataPoint
    {
        [DataMember(Name = "x")]
        public double x { get; set; }
        [DataMember(Name = "y")]
        public double y { get; set; }
        public int ID { get; set; }
    }
}
