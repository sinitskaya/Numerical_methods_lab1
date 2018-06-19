using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Численные_методы
{
    public abstract class RK_4
    {
        //u-неизвестная функция
        //double a_h, b_h;
        //public int N;//тогда N не надо передавать тут в функциях исправить!
        //public double h;
        /// Текущее время
        public double x;
        /// Искомое решение Y[0] - само решение, Y[i] - i-тая производная решения
        public double[] U;
        /// Внутренние переменные 
        protected double[] FU;
        /// Конструктор
        /// <param name="N">размерность системы</param>
        public void RungeKutta(uint N)
        {
            Init(N);
        }
        /// Конструктор
        public void RungeKutta() { }
        /// Выделение памяти под рабочие массивы
        /// <param name="N">Размерность массивов</param>
        public void Init(uint N)
        {
            U = new double[N];
            FU = new double[N];         
        }
        /// Установка начальных условий
        /// <param name="t0">Начальное время</param>
        /// <param name="Y0">Начальное условие</param>
        public void SetInit(double x0, double[] U0)
        {
            //a_h = a_h_;
            //b_h = b_h_;
            x = x0;
            if (U == null)
                Init((uint)U0.Length);
            for (int i = 0; i < U.Length; i++)
                U[i] = U0[i];
        }
        /// Расчет правых частей системы
        /// <param name="t">текущее время</param>
        /// <param name="Y">вектор решения</param>
        /// <returns>правая часть</returns>
        abstract public double[] F(double x, double[] U);
        /// Следующий шаг метода Рунге-Кутта
        /// <param name="dt">текущий шаг по времени (может быть переменным)</param>
        /// 
        public void Proverka_h()
        { }

        public void NextStep(double h)
        {

            if (h < 0) return;//включить сюда проверка h

            double[] U1 = new double[U.Length];
            for (int i=0; i< U.Length; i++)
                U1[i] = F(x, U)[i];//без [i] U1 станет=U2

            double[] v1 = new double[U.Length];//вектор v
            double[] r1 = new double[U.Length];
            for (int i = 0; i < v1.Length; i++)
            {
                v1[i] = U[i];
                r1[i] = v1[i] + h * U1[i];
            }

            double[] U2 = new double[U.Length];
            for (int i = 0; i < U.Length; i++)
                U2[i] =  F(x + h, r1)[i];

            // рассчитать решение на новом шаге
            for (int i = 0; i < U.Length; i++)
                U[i] = U[i] + h / 2.0 * (U1[i] + U2[i]);

            // рассчитать текущее время
            x = x + h;
        }
    }
}