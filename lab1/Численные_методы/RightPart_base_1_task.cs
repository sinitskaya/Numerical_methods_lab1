using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Численные_методы
{
    class RightPart_base_1_task: RK_4
    {
        private uint N = 1;
        //public double[] U0;
        public RightPart_base_1_task() { Init(N); }

        public override double[] F(double x, double[] U)
        {
            FU[0] = (Double)x /(1+x*x) * U[0]*U[0] + U[0] - U[0]*U[0]*U[0]*Math.Sin(10*x);
            return FU;
        }
    }
}
