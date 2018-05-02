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
        [Range(1,15)]
        public int DotsCount { get; set; }
    }
}
