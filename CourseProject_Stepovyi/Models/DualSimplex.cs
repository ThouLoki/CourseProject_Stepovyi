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
        static int[] basis;
        static int n;
        static int m;
        static int k;
        public static double[] Calculate(double[,] source)
        {
            n = source.GetLength(0);
            m = source.GetLength(1);
            k = m - 3;
            a = new double[n, m + n - 1];
            basis = new int[m + n - 1];
            for (int i = m; i < m + n - 1; i++)
                basis[i] = 1;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    a[i, j] = source[i, j];
            for (int i = 0; i < n; i++)
                for (int j = m; j < m + n - 1; j++)
                    if (j - m == i) a[i, j] = 1;
                    else a[i, j] = 0;
            n = a.GetLength(0);
            m = a.GetLength(1);

            int ix = MainRow(), jx;
            int ib = -1, ik = -1;
            while (ix != -1)
            {
                jx = MainCol(ix);
                if (jx == k + 1) ik = ix;
                if (jx == k + 2) ib = ix;
                Jordan(ix, jx);
                basis[ix] = 0;
                basis[jx] = 1;
                ix = MainRow();
            }

            double[] ans = new double[2];
            if (ik != -1) ans[0] = a[ik, 0];
            if (ib != -1) ans[1] = a[ib, 0];
            return ans;
        }

        private static int MainCol(int i)
        {
            int xj = -1;
            for (int j = 1; j < m; j++)
                if (a[i, j] < 0 && basis[j] == 0)
                {
                    if (xj == -1) xj = j;
                    else
                            if ((a[n - 1, j] / a[i, j]) <= (a[n - 1, xj] / a[i, xj])) xj = j;
                }

            return xj;
        }
        private static int MainRow()
        {
            int ix = -1;
            for (int i = 0; i < n - 1; i++)
                if (a[i, 0] < 0)
                {
                    if (ix == -1) ix = i;
                    else
                            if (a[i, 0] < a[ix, 0]) ix = i;
                }
            return ix;
        }

        private static void Jordan(int ix, int jx)
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
