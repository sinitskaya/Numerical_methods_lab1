using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Численные_методы
{
    class RightPart_test_task: RK_4//правая часть тестовой задачи
    {
        private uint N = 1;
        public RightPart_test_task() { Init(N); }           

        public override double[] F(double x, double[] U)
        {
            FU[0] = U[0];
            return FU;
        }
    }
}
