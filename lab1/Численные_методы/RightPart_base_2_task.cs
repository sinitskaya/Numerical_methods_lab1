using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Численные_методы
{
    class RightPart_base_2_task: RK_4
    {
        private uint N = 2;
        //public double[] U0;
        public double a,b;
        public RightPart_base_2_task(double a_, double b_)
        {
            Init(N);
            a = a_;
            b = b_;
        }

        public override double[] F(double x, double[] U)
        {
            FU[0] = U[1];
            FU[1] = -a*a*Math.Sin(U[0]) -b*Math.Sin(x) ;
            return FU;
        }
    }
}
