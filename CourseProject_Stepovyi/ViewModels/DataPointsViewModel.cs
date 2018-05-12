using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject_Stepovyi.ViewModels
{
    public class DataPointsViewModel
    {
        [Required]
        [Range(-100,100)]
        public string x { get; set; }
        [Required]
        [Range(-100, 100)]
        public string y { get; set; }
        public int ID { get; set; }
    }
}
