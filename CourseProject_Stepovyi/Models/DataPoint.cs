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
        //[Range(-1000,1000)]\
        //[DataType(DataType.Currency)]
        public double x { get; set; }
        [DataMember(Name = "y")]
        //[DataType(DataType.Currency)]
        //[Range(-1000, 1000)]
        public double y { get; set; }
        public int ID { get; set; }
    }
}
