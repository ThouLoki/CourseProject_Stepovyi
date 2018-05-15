using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject_Stepovyi.Models
{
    static class DualSimplex
    {
        static double[,] a;
        static int n;
        static int m;
        static int k;
        public static double[] Calculate(double[,] source)
        {
            n = source.GetLength(0);
            m = source.GetLength(1);
            k = m - 3;
            a = new double[n , m + n - 1];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    a[i, j] = source[i, j];
            for (int i = 0; i < n ; i++)
                for (int j = m; j < m + n - 1 ; j++)
                    if (j - m == i) a[i, j] = 1;
                    else a[i, j] = 0;
            n = a.GetLength(0);
            m = a.GetLength(1);

            int ib = MainRow();
            int jb = k + 2;
            Jordan(ib, jb);

            int ik = MainRow();
            int jk = k + 1;
            Jordan(ik, jk);

            double[] ans = new double[2];
            ans[0] = a[ik, 0];
            ans[1] = a[ib, 0];
            return ans;
        }
        
        public static int MainRow()
        {
            int ix = 0;
            for (int i = 1; i < n - 1; i++)
                if ((a[i, 0] < 0) && (a[i, 0] * (-1) > a[ix, 0])) ix = i;
            return ix;
        }

        public static void Jordan(int ix, int jx)
        {
            double[,] d = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    d[i, j] = a[i, j] - (a[i, jx] * a[ix, j]) / a[ix, jx];
                }
            for (int i = 0; i < n; i++)
                d[i, jx] = 0;
            d[ix, jx] = 1;
            for (int j = 0; j < m; j++)
                d[ix, j] = a[ix, j] / a[ix, jx];
            a = d;
        }

    }
}
