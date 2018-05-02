using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseProject_Stepovyi.ViewModels
{
    public class RandomDataViewModel
    {
        [Required]
        [Range(1, 15)]
        public int DotsCount { get; set; }
        [Required]
        [Range(-100, 100)]
        public double x_start_point { get; set; }
        [Required]
        [Range(-100, 100)]
        public double y_start_point { get; set; }
        [Required]
        [Range(0, 100)]
        public double x_from { get; set; }
        [Required]
        [Range(0, 100)]
        public double x_to { get; set; }
        [Required]
        [Range(0, 100)]
        public double y_from { get; set; }
        [Required]
        [Range(0, 100)]
        public double y_to { get; set; }
    }
}
