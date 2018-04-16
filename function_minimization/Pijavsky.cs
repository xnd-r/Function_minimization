using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function_minimization
{
    class Pijavsky : global
    {
        public Pijavsky(double r, double a, double b, double c, double d, double x1, double x2, int n_st, double eps)
        {
            this.method_param = r;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.x1 = x1;
            this.x2 = x2;
            this.step_amo = n_st;
            this.eps = eps;
        }

        public override double R(double xi_1, double xi)
        {
            return 0.5 * GetM() * (xi - xi_1) - (F_i(xi) + F_i(xi_1)) / 2;
        }

        public override double GetNewX(double xt_1, double xt)
        {
            return 0.5 * (xt + xt_1) - (F_i(xt) - F_i(xt_1)) / (2 * GetM());
        }
    }
}
