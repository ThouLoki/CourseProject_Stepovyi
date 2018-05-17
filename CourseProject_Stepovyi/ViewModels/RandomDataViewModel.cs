using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseProject_Stepovyi.ViewModels
{
    public class RandomDataViewModel
    {
        [Range(1, 50)]
        public int DotsCount { get; set; }
        [Required]
        [Range(-100, 100)]
        [StringLength(7, ErrorMessage = "please enter not more than 3 numbers after dot")]
        public string x_start_point { get; set; }
        [Required]
        [Range(-100, 100)]
        [StringLength(7, ErrorMessage = "please enter not more than 3 numbers after dot")]
        public string y_start_point { get; set; }
        [Required]
        [Range(0, 100)]
        [StringLength(17)]
        public string x_from { get; set; }
        [Required]
        [Range(0, 100)]
        [StringLength(7)]
        public string x_to { get; set; }
        [Required]
        [Range(0, 100)]
        [StringLength(7)]
        public string y_from { get; set; }
        [Required]
        [Range(0, 100)]
        [StringLength(7)]
        public string y_to { get; set; }
        [Required]
        [Range(0, 100)]
        [StringLength(7, ErrorMessage = "please enter not more than 3 numbers after dot")]
        public string from { get; set; }
        [Required]
        [Range(0, 100)]
        [StringLength(7,ErrorMessage ="please enter not more than 3 numbers after dot") ]
        public string to { get; set; }
        public string onlyint { get; set; }

    }
}
