using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Численные_методы
{
    public partial class тестовая : Form
    {
        public тестовая()
        {
            InitializeComponent();
        }

        private void основная1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            основная_1 основная_1 = new основная_1();
            основная_1.ShowDialog();
        }

        private void основная2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            основная_2 основная_2 = new основная_2();
            основная_2.ShowDialog();
        }
        //OLP
        /*if (k == 0)
            dataGridView1.Rows[k].Cells[5].Value = 0;
        else
        {
            double vi_1 = (Double)dataGridView1.Rows[k - 1].Cells[2].Value;//v(xn-1)
            double olp = vi_1 * Math.Exp(task.x - (task.x - h)) - task.U[0];//v(xn-1)exp{xn-(xn-1) - v(xn)}
            dataGridView1.Rows[k].Cells[5].Value = olp;//ОЛП

            double d = Math.Abs(olp);
            if (d > maxmodolp)
            {
                maxmodolp = d;
            }
            //Math.Max(Math.Abs(olp), d);
        }//OLP
          */
        double h;
        double b_h;
        //double b_h;
        double[] u0 = new double[1];
        int N_max;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                N_max = int.Parse(N_max_test_textBox12.Text);//N_max_base_1_textBox2.Text);
                u0[0] = double.Parse(u0_test_textBox1.Text);//u0_base_1_textBox11.Text);
                h = double.Parse(h_test_textBox2.Text);//h_base_1_textBox9.Text);
                //b_h = double.Parse(b_h_test_textBox11.Text);//b_base_1_textBox7.Text);
            }
            catch { MessageBox.Show("Некорректные данные"); }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            graf_chart1.Series.Clear();

            Series SeriesOfPoints_Selective = new Series("Численное");
            SeriesOfPoints_Selective.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective.Color = Color.Red;
            SeriesOfPoints_Selective.BorderWidth = 1;
            Series SeriesOfPoints_Selective_Tochnoe = new Series("Точное");
            SeriesOfPoints_Selective_Tochnoe.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective_Tochnoe.Color = Color.Green;            
            SeriesOfPoints_Selective_Tochnoe.BorderWidth = 1;

            dataGridView1.RowCount = N_max + 1;
            dataGridView1.ColumnCount = 8;

            dataGridView1.Columns[0].HeaderText = "i";
            dataGridView1.Columns[1].HeaderText = "xi";
            dataGridView1.Columns[2].HeaderText = "vi";
            dataGridView1.Columns[3].HeaderText = "v2i";
            dataGridView1.Columns[4].HeaderText = "vi-v2i";
            dataGridView1.Columns[5].HeaderText = "ОЛП";
            dataGridView1.Columns[6].HeaderText = "ui";
            dataGridView1.Columns[7].HeaderText = "|ui-vi|";

            dataGridView1.Columns[0].Width = 50;

            for (int i = 1; i < dataGridView1.ColumnCount; i++)
                dataGridView1.Columns[i].Width = 80;
            dataGridView1.Columns[0].Width = 50;
            for (int i = 2; i < 8; i++)
                dataGridView1.Columns[i].Width = 160;

            RightPart_test_task task = new RightPart_test_task();
            RightPart_test_task task1 = new RightPart_test_task();
            RightPart_test_task task_h_2 = new RightPart_test_task();
            task.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task1.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task_h_2.SetInit(0, u0);
            double maxuivi = Math.Abs(task.U[0] - u0[0] * Math.Exp(task.x));//Math.Abs(task.U[0] - Math.Exp(task.x)); 
            double x1 = task.x;
            double maxmodolp = 0;
            int s = 1;
            for (int k = 0; k < N_max; k++)
            {
                if (k == 0)
                {//НУ
                    dataGridView1.Rows[k].Cells[0].Value = 0;
                    dataGridView1.Rows[k].Cells[1].Value = task.x;//xn=x0
                    dataGridView1.Rows[k].Cells[2].Value = task.U[0];//vn=u0
                    dataGridView1.Rows[k].Cells[3].Value = task1.U[0];//v2n=u0
                    dataGridView1.Rows[k].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n=0
                    dataGridView1.Rows[k].Cells[5].Value = 0;
                    dataGridView1.Rows[k].Cells[6].Value = Math.Exp(task.x) * u0[0];//ui
                    dataGridView1.Rows[k].Cells[7].Value = Math.Abs(task.U[0] - Math.Exp(task.x) * u0[0]);//vi-ui
                    SeriesOfPoints_Selective.Points.AddXY(0, task.U[0]);
                    SeriesOfPoints_Selective_Tochnoe.Points.AddXY(0, Math.Exp(task.x) * u0[0]);//ui=Math.Exp(task.x) * u0[0];
                }              
                task_h_2.x = task.x;//xn
                task_h_2.U[0] = task.U[0];//vn

                task.NextStep(h);
                task1.NextStep(h/2.0);
                task1.x = task.x;//xn

                task_h_2.NextStep(h / 2.0);//из (xn,vn)-> (xn+1/2, vn+1/2)
                task_h_2.NextStep(h / 2.0);//из (xn+1/2, vn+1/2)-> (xn+1,vn+1=)
                double ui = Math.Exp(task.x) * u0[0];
                double S = 4 * Math.Abs((task_h_2.U[0] - task.U[0]) / 3);//2^2-1, 2-порядок метода//OLP
                double c1 = S;
                maxmodolp = Math.Max(c1, maxmodolp);//OLP
                double c = Math.Abs(task.U[0] - ui);//vi-ui
                if (c > maxuivi)
                {
                    maxuivi = c;
                    x1 = task.x;
                }//vi-ui
                dataGridView1.Rows[s].Cells[0].Value = s;
                dataGridView1.Rows[s].Cells[1].Value = task.x;//xn
                dataGridView1.Rows[s].Cells[2].Value = task.U[0];//vn
                dataGridView1.Rows[s].Cells[3].Value = task1.U[0];//v2n
                dataGridView1.Rows[s].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n
                dataGridView1.Rows[s].Cells[5].Value = S;
                dataGridView1.Rows[s].Cells[6].Value = ui;//ui
                dataGridView1.Rows[s].Cells[7].Value = Math.Abs(task.U[0] - ui);//vi-ui
                s++;
                SeriesOfPoints_Selective.Points.AddXY(task.x, task.U[0]);
                SeriesOfPoints_Selective_Tochnoe.Points.AddXY(task.x, ui);//ui=Math.Exp(task.x) * u0[0];
            }
            graf_chart1.Series.Add(SeriesOfPoints_Selective);
            graf_chart1.Series.Add(SeriesOfPoints_Selective_Tochnoe);
            graf_chart1.ChartAreas[0].AxisX.Maximum = task.x + 0.5; //Задаешь максимальные значения координат
            graf_chart1.ChartAreas[0].AxisY.Maximum = task.U[0]+0.5; //Задаешь максимальные значения координат
            graf_chart1.ChartAreas[0].AxisX.Minimum = 0; //Задаешь максимальные значения координат
            //chart1.ChartAreas[0].AxisX.Interval = 1; // и можешь интервалы настроить по своему усмотрению
            ///////////////////////////////////////////////////////
            n_test_textBox1.Text = (s-1).ToString();
            maxuivi_test_textBox4.Text = maxuivi.ToString();
            x_maxuivi_test_textBox9.Text = x1.ToString();
            maxmod_olp_test_textBox8.Text = maxmodolp.ToString();
            //bxn_test_textBox3.Text = (b_h - task.x).ToString();//double xn = task.x - h;
            ////////////////////////////////////////////////////////
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        double epsilon;
        double delta;

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                N_max = int.Parse(N_max_test_textBox12.Text);//N_max_base_1_textBox2.Text);
                u0[0] = double.Parse(u0_test_textBox1.Text);//u0_base_1_textBox11.Text);
                h = double.Parse(h_test_textBox2.Text);//h_base_1_textBox9.Text);
                b_h = double.Parse(b_h_test_textBox11.Text);//b_base_1_textBox7.Text);
                epsilon = double.Parse(epsilon_test_textBox1.Text);
                delta = double.Parse(delta_test_textBox1.Text);
            }
            catch { MessageBox.Show("Некорректные данные"); }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            graf_chart1.Series.Clear();

            Series SeriesOfPoints_Selective = new Series("Численное");
            SeriesOfPoints_Selective.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective.Color = Color.Red;
            SeriesOfPoints_Selective.BorderWidth = 1;
            Series SeriesOfPoints_Selective_Tochnoe = new Series("Точное");
            SeriesOfPoints_Selective_Tochnoe.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective_Tochnoe.Color = Color.Green;
            SeriesOfPoints_Selective_Tochnoe.BorderWidth = 1;

            dataGridView1.RowCount = N_max+1;
            dataGridView1.ColumnCount = 11;

            dataGridView1.Columns[0].HeaderText = "i";
            dataGridView1.Columns[1].HeaderText = "xi";
            dataGridView1.Columns[2].HeaderText = "vi";
            dataGridView1.Columns[3].HeaderText = "v2i";
            dataGridView1.Columns[4].HeaderText = "vi-v2i";
            dataGridView1.Columns[5].HeaderText = "ОЛП";
            dataGridView1.Columns[6].HeaderText = "hi";
            dataGridView1.Columns[7].HeaderText = "C1";
            dataGridView1.Columns[8].HeaderText = "C2";
            dataGridView1.Columns[9].HeaderText = "ui";
            dataGridView1.Columns[10].HeaderText = "|ui-vi|";

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[7].Width = 50;
            //dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[8].Width = 50;
            for (int i = 2; i < 6; i++)
                dataGridView1.Columns[i].Width = 160;
            dataGridView1.Columns[9].Width = 160;
            dataGridView1.Columns[10].Width = 160;

            RightPart_test_task task = new RightPart_test_task();
            RightPart_test_task task1 = new RightPart_test_task();
            RightPart_test_task task_h_2 = new RightPart_test_task();

            task.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task1.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task_h_2.SetInit(0,u0);
            double x1 = task.x;
            int s = 1, f = 0;
            double vn_1 = u0[0], vn2_1 = u0[0], xn_1 = 0;
            double[] u = new double[N_max+1];
            for (int k = 0; k < N_max; k++)
            {
                //dataGridView1.Rows.Add();
                int C1 = 0, C2 = 0;
                if (k==0)
                {//НУ
                    //dataGridView1.Rows.Add();//task.x=0, task.U[0]=u0
                    dataGridView1.Rows[k].Cells[0].Value = 0;
                    dataGridView1.Rows[k].Cells[1].Value = task.x;//xn=x0
                    dataGridView1.Rows[k].Cells[2].Value = task.U[0];//vn=u0
                    dataGridView1.Rows[k].Cells[3].Value = task1.U[0];//v2n=u0
                    dataGridView1.Rows[k].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n=0
                    //dataGridView1.Rows[k].Cells[5].Value = 0;//OLP
                    dataGridView1.Rows[k].Cells[6].Value = h;//hi
                    dataGridView1.Rows[k].Cells[7].Value = C1;//c1
                    dataGridView1.Rows[k].Cells[8].Value = C2;//c2
                    dataGridView1.Rows[k].Cells[9].Value = Math.Exp(task.x) * u0[0];//ui
                    dataGridView1.Rows[k].Cells[10].Value = Math.Abs(task.U[0] - Math.Exp(task.x) * u0[0]);//vi-ui
                    u[0] = task.U[0];//vn=u0
                    //SeriesOfPoints_Selective.Points.AddXY(0, task.U[0]);
                    //SeriesOfPoints_Selective_Tochnoe.Points.AddXY(0, Math.Exp(task.x) * u0[0]);//ui=Math.Exp(task.x) * u0[0];
                }
                double S1 = 0;
                while (!(task.x + h - b_h < delta))//h = proverka_h(task.x, h, b_h, delta);
                {
                    h = h / 2.0;
                    C1++;
                }//////////////////////////////////////////////
                if ((task.x < b_h + delta) && (task.x > b_h))
                    break;
                if (k != 0)
                {
                    double S = Math.Abs((task_h_2.U[0] - task.U[0]) / 3);//2^2-1, 2-порядок метода
                    S1 = S * 4;
                    if (S < epsilon / 8.0) //(k != 0))
                    {
                        h = 2 * h;
                        C2++;
                        while (!(task.x + h - b_h < delta))//h = proverka_h(task.x, h, b_h, delta);
                        {
                            h = h / 2.0;
                            C1++;
                        }
                    }
                    if ((S > epsilon) && (k != 0))
                    {
                        task.x = xn_1;//task.x - h;//xn-1
                        task.U[0] = vn_1;//(Double)dataGridView1.Rows[s-1].Cells[2].Value;
                        task1.x = xn_1;
                        task1.U[0] = vn2_1;//task.U[0];
                        s = f;
                        h = h / 2.0;
                        C1++;
                    }
                }
                xn_1 = task.x;
                vn_1 = task.U[0];
                vn2_1 = task1.U[0];
                f = s;
                task_h_2.x = task.x;//xn
                task_h_2.U[0] = task.U[0];//vn

                task.NextStep(h);
                task1.NextStep(h / 2.0);
                task1.x = task.x;//xn

                task_h_2.NextStep(h / 2.0);//из (xn,vn)-> (xn+1/2, vn+1/2)
                task_h_2.NextStep(h / 2.0);//из (xn+1/2, vn+1/2)-> (xn+1,vn+1=)

                double ui = Math.Exp(task.x) * u0[0];
                //if (k != 0)
                //if (dataGridView1.RowCount == s)
                  //  dataGridView1.Rows.Add();
                dataGridView1.Rows[s].Cells[0].Value = s;
                dataGridView1.Rows[s].Cells[1].Value = task.x;//xn
                dataGridView1.Rows[s].Cells[2].Value = task.U[0];//vn
                dataGridView1.Rows[s].Cells[3].Value = task1.U[0];//v2n
                dataGridView1.Rows[s].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n
                dataGridView1.Rows[s-1].Cells[5].Value = S1;
                dataGridView1.Rows[s].Cells[6].Value = h;//hi
                dataGridView1.Rows[s].Cells[9].Value = ui;//ui
                dataGridView1.Rows[s].Cells[10].Value = Math.Abs(task.U[0] - ui);//vi-ui
                dataGridView1.Rows[s].Cells[7].Value = C1;//c1
                dataGridView1.Rows[s].Cells[8].Value = C2;//c2
                u[s] = task.U[0];
                s++;
                if (task.x + 0.00000000000001 >= b_h + delta)
                    break;
                //}
                //SeriesOfPoints_Selective.Points.AddXY(task.x, task.U[0]);
                //SeriesOfPoints_Selective_Tochnoe.Points.AddXY(task.x, ui);//ui=Math.Exp(task.x) * u0[0];
            }//for
            
            ///////////////////////////////////////////////////////
            double Sn = 4 * Math.Abs((task_h_2.U[0] - task.U[0]) / 3);//2^2-1, 2-порядок метода
            dataGridView1.Rows[s - 1].Cells[5].Value = Sn;
            double maxmodolp = 0;
            double maxuivi = 0;//Math.Abs(task.U[0] - Math.Exp(task.x)); 
            double max_h = 0;
            double min_h = (Double)dataGridView1.Rows[0].Cells[6].Value;
            double x_minh = 0, x_maxh = 0;
            for (int i = 0; i < s ; i++)//n=s-1
            {
                double x = (Double)dataGridView1.Rows[i].Cells[1].Value;
                //double u = (Double)dataGridView1.Rows[i].Cells[2].Value;//vn
                double ui = Math.Exp(x) * u0[0];
                SeriesOfPoints_Selective.Points.AddXY(x, u[i]);
                SeriesOfPoints_Selective_Tochnoe.Points.AddXY(x, ui);//ui=Math.Exp(task.x) * u0[0];
                double c = Math.Abs((Double)dataGridView1.Rows[i].Cells[2].Value - u0[0] * Math.Exp((Double)dataGridView1.Rows[i].Cells[1].Value));//task.U[0] - Math.Exp(task.x));//vi-ui
                if (c > maxuivi)
                {
                    maxuivi = c;
                    x1 = (Double)dataGridView1.Rows[i].Cells[1].Value;
                }
                double c1 = (Double)dataGridView1.Rows[i].Cells[5].Value;
                maxmodolp = Math.Max(c1, maxmodolp);
                ///OLP
                /*if (i == 0)//OLP
                    dataGridView1.Rows[i].Cells[5].Value = 0;
                else
                {
                    double xi = (Double)dataGridView1.Rows[i].Cells[1].Value;//x
                    double xi_1 = (Double)dataGridView1.Rows[i - 1].Cells[1].Value;
                    double vi = (Double)dataGridView1.Rows[i].Cells[2].Value;//v
                    double vi_1 = (Double)dataGridView1.Rows[i - 1].Cells[2].Value;//vn-1
                    double olp = vi_1 * Math.Exp(xi - xi_1) - vi;
                    dataGridView1.Rows[i].Cells[5].Value = olp;//ОЛП

                    double d = Math.Abs(olp);
                    if (d > maxmodolp)
                    {
                        maxmodolp = d;
                    }
                }/////OLP*/
                ////h
                double c2 = (Double)dataGridView1.Rows[i].Cells[6].Value;//hi  
                if (c2 > max_h)
                {
                    max_h = c2;
                    x_maxh = (Double)dataGridView1.Rows[i].Cells[1].Value;//x
                }
                if (c2 < min_h)
                {
                    min_h = c2;
                    x_minh = (Double)dataGridView1.Rows[i].Cells[1].Value;//x
                }/////h
            }//for2  
            graf_chart1.Series.Add(SeriesOfPoints_Selective);
            graf_chart1.Series.Add(SeriesOfPoints_Selective_Tochnoe);
            graf_chart1.ChartAreas[0].AxisX.Maximum = task.x + 0.5; //Задаешь максимальные значения координат
            graf_chart1.ChartAreas[0].AxisY.Maximum = task.U[0] + 0.5; //Задаешь максимальные значения координат
            graf_chart1.ChartAreas[0].AxisX.Minimum = 0; //Задаешь максимальные значения координат
            //dataGridView1.RowCount = s+1;//обрезали лишние пустые строчки в таблице
            n_test_textBox1.Text = (s-1).ToString();
            maxuivi_test_textBox4.Text = maxuivi.ToString();
            x_maxuivi_test_textBox9.Text = x1.ToString();
            maxmod_olp_test_textBox8.Text = maxmodolp.ToString();
            bxn_test_textBox3.Text = (b_h - task.x).ToString();//double xn = task.x - h;
            max_h_test_textBox6.Text = max_h.ToString();
            min_h_test_textBox5.Text = min_h.ToString();
            x_minh_test_textBox10.Text = x_minh.ToString();
            x_maxh_test_textBox3.Text = x_maxh.ToString();
            ////////////////////////////////////////////////////////
        }    
    }
}
