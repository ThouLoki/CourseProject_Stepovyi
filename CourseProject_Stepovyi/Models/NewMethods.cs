using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject_Stepovyi.Models
{
    static class NewMethods
    {
        public static List<DataPoint> LeastSquare(List<DataPoint> points)
        {

            double sum_xy = 0, sum_x = 0, sum_y = 0, sum_x2 = 0, max_x = points.Last().x, k, b;

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

            List<DataPoint> result = new List<DataPoint>();
            DataPoint p1 = new DataPoint();
            p1.x = 0;
            p1.y = b;
            result.Add(p1);
            DataPoint p2 = new DataPoint();
            p2.x = max_x;
            p2.y = max_x * k + b;
            result.Add(p2);

            return result;
        }

        public static List<DataPoint> Module(List<DataPoint> points)
        {

            int n = points.Count;
            double[,] table = new double[2 * n + 1, n + 3];
            double max_x = points[0].x;

            for (int i = 0; i < n; i++)
            {
                table[2 * n, i + 1] = 1;

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
            

            double[] result = new double[2];
            result = DualSimplex.Calculate(table);
            double k = result[0], b = result[1];

            List<DataPoint> ans = new List<DataPoint>();
            DataPoint p1 = new DataPoint();
            p1.x = 0;
            p1.y = b;
            ans.Add(p1);
            DataPoint p2 = new DataPoint();
            p2.x = max_x;
            p2.y = max_x * k + b;
            ans.Add(p2);

            return ans;
        }
    }
}
