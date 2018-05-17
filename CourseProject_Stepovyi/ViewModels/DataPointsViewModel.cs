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
        [StringLength(7, ErrorMessage = "please enter not more than 3 numbers after dot")]
        public string x { get; set; }
        [Required]
        [Range(-100, 100)]
        [StringLength(7, ErrorMessage = "please enter not more than 3 numbers after dot")]
        public string y { get; set; }
        public int ID { get; set; }
    }
}
