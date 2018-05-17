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
        public static List<DataPoint> LeastSquare(List<DataPoint> points, out double err_x, out double k, out double b, out long ols_time)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            double sum_xy = 0, sum_x = 0, sum_y = 0, sum_x2 = 0, max_x = points.Last().x;
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
            watch.Stop();
            ols_time = watch.ElapsedMilliseconds;
            return result;
        }

        public static List<DataPoint> Module(List<DataPoint> points, out double k, out double b, out double err_m, out long module_time)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int n = points.Count;
            double[,] table = new double[2 * n + 1, n + 3];
            double max_x = points[0].x;

            for (int i = 0; i < n; i++)
            {
                table[2 * n, i + 1] = -1;

                table[i * 2, 0] = points[i].y * (-1);
                table[i * 2, i + 1] = -1;
                table[i * 2, n + 1] = points[i].x * (-1);
                table[i * 2, n + 2] = -1;

                table[i * 2 + 1, 0] = points[i].y * (1);
                table[i * 2 + 1, i + 1] = -1;
                table[i * 2 + 1, n + 1] = points[i].x;
                table[i * 2 + 1, n + 2] = 1;

                if (points[i].x > max_x) max_x = points[i].x;
            }
            n = table.GetLength(0);
            int m = table.GetLength(1);
            double[] result = new double[2];
            result = DualSimplex.Calculate(table);
            k = result[0]; b = result[1];
            err_m = 0;
            foreach (var a in points)
            {
                err_m += Math.Abs(a.y - (k * a.x + b));
            }
            List<DataPoint> ans = new List<DataPoint>();
            DataPoint p1 = new DataPoint();
            p1.x = 0;
            p1.y = b;
            ans.Add(p1);
            DataPoint p2 = new DataPoint();
            p2.x = max_x;
            p2.y = max_x * k + b;
            ans.Add(p2);
            watch.Stop();
            module_time = watch.ElapsedMilliseconds;
            return ans;
        }
    }
}
