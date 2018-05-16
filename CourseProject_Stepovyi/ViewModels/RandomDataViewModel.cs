using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseProject_Stepovyi.ViewModels
{
    public class RandomDataViewModel
    {
        [Range(1, 15)]
        public int DotsCount { get; set; }
        [Required]
        [Range(-100, 100)]
        public string x_start_point { get; set; }
        [Required]
        [Range(-100, 100)]
        public string y_start_point { get; set; }
        [Required]
        [Range(0, 100)]
        public string x_from { get; set; }
        [Required]
        [Range(0, 100)]
        public string x_to { get; set; }
        [Required]
        [Range(0, 100)]
        public string y_from { get; set; }
        [Required]
        [Range(0, 100)]
        public string y_to { get; set; }
        [Required]
        [Range(0, 100)]
        public string from { get; set; }
        [Required]
        [Range(0, 100)]
        public string to { get; set; }
        public string onlyint { get; set; }

    }
}
