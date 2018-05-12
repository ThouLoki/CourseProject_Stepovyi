using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject_Stepovyi.Models;

namespace CourseProject_Stepovyi.Models
{
    static class Methods
    {
        public static List<DataPoint> LeastSquare(List<DataPoint> points, out double err_x)
        {

            double sum_xy = 0, sum_x = 0, sum_y = 0, sum_x2 = 0, max_x = points.Last().x, k, b;
            err_x = 0;
            foreach(var a in points)
            {
                sum_xy += a.x * a.y;
                sum_y += a.y;
                sum_x += a.x;
                sum_x2 += a.x * a.x;
                if (a.x > max_x) max_x = a.x;
            }

            k = (points.Count * sum_xy - (sum_x * sum_y)) / (points.Count * sum_x2 - sum_x * sum_x);
            b = (sum_y - k * sum_x) / points.Count;
            foreach (var a in points)
            {
                err_x += Math.Pow((a.y - (k * a.x + b)),2);
            }
                List<DataPoint> result = new List<DataPoint>();
            DataPoint p1 = new DataPoint();
            p1.x = 0;
            p1.y = b;
            result.Add(p1);
            DataPoint p2 = new DataPoint();
            p2.x = max_x;
            p2.y = max_x * k + b;
            result.Add(p2);
            //Console.WriteLine("k = {0}, b = {1}", k, b);

            return result;
        }

        public static List<DataPoint> Module(List<DataPoint> points)
        {
            
            return null;
        }
    }
}
