using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseProject_Stepovyi.Models
{
    public class CourseProject_StepovyiContext : DbContext
    {
        public CourseProject_StepovyiContext (DbContextOptions<CourseProject_StepovyiContext> options)
            : base(options)
        {
        }

        public DbSet<CourseProject_Stepovyi.Models.DataPoint> DataPoint { get; set; }
    }
}
