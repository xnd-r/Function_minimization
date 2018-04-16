using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function_minimization
{
    public struct _point
    {
        public double x, y;

        public _point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    abstract class global
    {
        public double a, b, c, d, x1, x2, eps, res_eps, method_param;
        public int step_amo, res_step_amo;
        public List<double> X = new List<double>();
        public double xf, zf;

        public List<double> GetPoints()
        {
            return X;
        }

        public _point GetRes()
        {
            return new _point(xf, zf);
        }

        public double GetFinalEps()
        {
            return res_eps;
        }

        public int res_step_amount()
        {
            return res_step_amo;
        }

        public double F_i(double x)
        {
            return a * Math.Sin(b * x) + c * Math.Cos(d * x);
        }

        abstract public double R(double xi_1, double xi);
        abstract public double GetNewX(double xt_1, double xt);

        public _point FindMin()
        {
            _init_x();
            SetRes();
            int num = 0;
            double xt_1 = x1, xt = x2, xnew = 0;
            while (xt - xt_1 > eps && num < step_amo)
            {
                num++;
                xt_1 = GetXt().x;
                xt = GetXt().y;
                xnew = GetNewX(xt_1, xt);
                X.Add(xnew);
                X.Sort();
                SetRes(xnew);
            }
            res_eps = xt - xt_1;
            res_step_amo = num;
            return GetRes();
        }

        public void SetRes()
        {
            if (F_i(x1) > F_i(x2))
            {
                xf = x2;
                zf = F_i(x2);
            }
            else
            {
                xf = x1;
                zf = F_i(x1);
            }
        }

        public void SetRes(double xnew)
        {
            if (zf > F_i(xnew))
            {
                zf = F_i(xnew);
                xf = xnew;
            }
        }

        public void _init_x()
        {
            X.Add(x1);
            X.Add(x2);
            X.Sort();
        }

        public _point GetXt()
        {
            double maxR = -100000d;
            double xt_1 = x1, xt = x2;
            for (int i = 1; i < X.Count; i++)
                if (R(X[i - 1], X[i]) > maxR)
                {
                    maxR = R(X[i - 1], X[i]);
                    xt_1 = X[i - 1];
                    xt = X[i];
                }
            return new _point(xt_1, xt);
        }

        public double GetM()
        {
            double M = -100000d;
            for (int i = 1; i < X.Count; i++)
                M = Math.Max(M, Math.Abs((F_i(X[i]) - F_i(X[i - 1])) / (X[i] - X[i - 1])));
            return M > 0 ? method_param * M : 1;
        }

    }
}
