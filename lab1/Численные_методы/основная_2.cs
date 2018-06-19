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
    public partial class основная_2 : Form
    {
        public основная_2()
        {
            InitializeComponent();
            //pictureBox1.Image = Image.FromFile("3.png");
        }

        private void тестоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            тестовая тестовая = new тестовая();
            тестовая.ShowDialog();
        }

        private void основная1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            основная_1 основная_1 = new основная_1();
            основная_1.ShowDialog();
        }

        //double x0 = 0;
        double h;
        //double a_h;
        double b_h;
        double a;
        double b;      
        double[] u0 = new double[2];
        //double delta;
        int N_max;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                N_max = int.Parse(N_max_base_2_textBox4.Text);//N_max_test_textBox12.Text);//N_max_base_1_textBox2.Text);
                u0[0] = double.Parse(u0_base_2_textBox9.Text);//u0_test_textBox1.Text);//u0_base_1_textBox11.Text);
                u0[1] = double.Parse(u0p_base_2_textBox7.Text);
                h = double.Parse(h_base_2_textBox6.Text);//h_test_textBox2.Text);//h_base_1_textBox9.Text);
                //b_h = double.Parse(b_h_test_textBox11.Text);//b_base_1_textBox7.Text);
                a = double.Parse(a_base_2_textBox5.Text);
                b = double.Parse(b_base_2_textBox2.Text);
            }
            catch { MessageBox.Show("Некорректные данные"); }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            graf_chart1.Series.Clear();
            graf_chart2.Series.Clear();

            Series SeriesOfPoints_Selective = new Series("u(x)");
            SeriesOfPoints_Selective.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective.Color = Color.Red;
            SeriesOfPoints_Selective.BorderWidth = 1;
            Series SeriesOfPoints_Selective_Tochnoe = new Series("u'(x)");
            SeriesOfPoints_Selective_Tochnoe.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective_Tochnoe.Color = Color.Green;            
            SeriesOfPoints_Selective_Tochnoe.BorderWidth = 1;

            Series SeriesOfPoints_Selective_FP = new Series("u'(u)");
            SeriesOfPoints_Selective_FP.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective_FP.Color = Color.Blue;
            SeriesOfPoints_Selective_FP.BorderWidth = 1;

            dataGridView1.RowCount = N_max + 1;
            dataGridView1.ColumnCount = 6;

            dataGridView1.Columns[0].HeaderText = "i";
            dataGridView1.Columns[1].HeaderText = "xi";
            dataGridView1.Columns[2].HeaderText = "vi";
            dataGridView1.Columns[3].HeaderText = "v2i";
            dataGridView1.Columns[4].HeaderText = "vi-v2i";
            dataGridView1.Columns[5].HeaderText = "ОЛП";

            dataGridView1.Columns[0].Width = 50;

            for (int i = 2; i < 6; i++)
                dataGridView1.Columns[i].Width = 160;
            //dataGridView1.Columns[5].Width = 160;

            RightPart_base_2_task task = new RightPart_base_2_task(a,b);
            RightPart_base_2_task task1 = new RightPart_base_2_task(a,b);
            RightPart_base_2_task task_h_2 = new RightPart_base_2_task(a,b);
            task.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task1.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task_h_2.SetInit(0, u0);
            double x1 = task.x;
            double maxmodolp = 0;
            int s = 1;
            double[] UP = new double[N_max + 1];
            double c_u = task.U[0], max_u = c_u, min_u = max_u;
            for (int k = 0; k < N_max; k++)
            {
                if (k == 0)
                {//НУ
                    dataGridView1.Rows[k].Cells[0].Value = 0;
                    dataGridView1.Rows[k].Cells[1].Value = task.x;//xn=x0
                    dataGridView1.Rows[k].Cells[2].Value = task.U[0];//vn=u0
                    UP[0] = task.U[1];
                    dataGridView1.Rows[k].Cells[3].Value = task1.U[0];//v2n=u0
                    dataGridView1.Rows[k].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n=0
                    dataGridView1.Rows[k].Cells[5].Value = 0;
                    SeriesOfPoints_Selective.Points.AddXY(0, task.U[0]);
                    SeriesOfPoints_Selective_Tochnoe.Points.AddXY(0, task.U[1]);//ui=Math.Exp(task.x) * u0[0];
                    SeriesOfPoints_Selective_FP.Points.AddXY(task.U[0], task.U[1]);//ui=Math.Exp(task.x) * u0[0];
                }              
                task_h_2.x = task.x;//xn
                task_h_2.U[0] = task.U[0];//vn
                task_h_2.U[1] = task.U[1];//vn

                task.NextStep(h);
                task1.NextStep(h/2.0);
                task1.x = task.x;//xn

                task_h_2.NextStep(h / 2.0);//из (xn,vn)-> (xn+1/2, vn+1/2)
                task_h_2.NextStep(h / 2.0);//из (xn+1/2, vn+1/2)-> (xn+1,vn+1=)
                //double ui = Math.Exp(task.x) * u0[0];
                double [] S = new double [2]; double Sn;
                S[0] = 4 * Math.Abs((task_h_2.U[0] - task.U[0]) / 3);//2^2-1, 2-порядок метода//OLP
                S[1] = 4 * Math.Abs((task_h_2.U[1] - task.U[1]) / 3);
                Sn = Math.Max(S[0], S[1]);
                double c1 = Sn;
                maxmodolp = Math.Max(c1, maxmodolp);//OLP
                dataGridView1.Rows[s].Cells[0].Value = s;
                dataGridView1.Rows[s].Cells[1].Value = task.x;//xn
                dataGridView1.Rows[s].Cells[2].Value = task.U[0];//vn
                UP[s] = task.U[1];
                dataGridView1.Rows[s].Cells[3].Value = task1.U[0];//v2n
                dataGridView1.Rows[s].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n
                dataGridView1.Rows[s].Cells[5].Value = Sn;
                s++;
                c_u = task.U[0];
                max_u = Math.Max(c_u, max_u);
                min_u = Math.Min(c_u, min_u);

                SeriesOfPoints_Selective.Points.AddXY(task.x, task.U[0]);
                SeriesOfPoints_Selective_Tochnoe.Points.AddXY(task.x, task.U[1]);//ui=Math.Exp(task.x) * u0[0];
                SeriesOfPoints_Selective_FP.Points.AddXY(task.U[0], task.U[1]);//ui=Math.Exp(task.x) * u0[0];
            }
            double c3 = UP[0], max_up = UP[0], min_up = max_up;
            for (int i = 0; i < s; i++)
            {
                c3 = UP[i];
                max_up = Math.Max(c3, max_up);
                min_up = Math.Min(c3, min_up);
            }
            graf_chart1.Series.Add(SeriesOfPoints_Selective);
            graf_chart1.Series.Add(SeriesOfPoints_Selective_Tochnoe);
            graf_chart2.Series.Add(SeriesOfPoints_Selective_FP);
            graf_chart1.ChartAreas[0].AxisX.Maximum = task.x + h; //Задаешь максимальные значения координат
            if ((task.U[0] > 0) && (task.U[1] > 0) || (task.U[0]== 0) && (task.U[1] == 0))
                graf_chart1.ChartAreas[0].AxisY.Maximum = Math.Max(task.U[0] + 0.5, task.U[1] + 0.5); //Задаешь максимальные значения координат
            if ((task.U[0] < 0) && (task.U[1] < 0))
                graf_chart1.ChartAreas[0].AxisY.Maximum = Math.Min(task.U[0] - 0.5, task.U[1] - 0.5); //Задаешь максимальные значения координат
            graf_chart2.ChartAreas[0].AxisY.Maximum = max_up + 0.5; //Задаешь максимальные значения координат
            graf_chart2.ChartAreas[0].AxisY.Minimum = min_up - 0.5; //Задаешь максимальные значения координат
            graf_chart2.ChartAreas[0].AxisX.Maximum = max_u + 0.5; //Задаешь максимальные значения координат
            graf_chart2.ChartAreas[0].AxisX.Minimum = min_u - 0.5; //Задаешь максимальные значения координат

            ///////////////////////////////////////////////////////
            n_base_2_textBox17.Text = (s-1).ToString();
            maxOLP_base_2_textBox15.Text = maxmodolp.ToString();
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
                N_max = int.Parse(N_max_base_2_textBox4.Text);//N_max_test_textBox12.Text);//N_max_base_1_textBox2.Text);
                u0[0] = double.Parse(u0_base_2_textBox9.Text);//u0_test_textBox1.Text);//u0_base_1_textBox11.Text);
                u0[1] = double.Parse(u0p_base_2_textBox7.Text);
                h = double.Parse(h_base_2_textBox6.Text);//h_test_textBox2.Text);//h_base_1_textBox9.Text);
                b_h = double.Parse(b_h_base_2_textBox1.Text);//b_h_test_textBox11.Text);//b_base_1_textBox7.Text);
                epsilon = double.Parse(epsilon_base2_textBox1.Text);//epsilon_test_textBox1.Text);
                delta = double.Parse(delta_base_2_textBox8.Text);//delta_test_textBox1.Text);
                a = double.Parse(a_base_2_textBox5.Text);
                b = double.Parse(b_base_2_textBox2.Text);
            }
            catch { MessageBox.Show("Некорректные данные"); }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            graf_chart1.Series.Clear();
            graf_chart2.Series.Clear();

            Series SeriesOfPoints_Selective = new Series("u(x)");
            SeriesOfPoints_Selective.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective.Color = Color.Red;
            SeriesOfPoints_Selective.BorderWidth = 1;
            Series SeriesOfPoints_Selective_Tochnoe = new Series("u'(x)");
            SeriesOfPoints_Selective_Tochnoe.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective_Tochnoe.Color = Color.Green;
            SeriesOfPoints_Selective_Tochnoe.BorderWidth = 1;
            Series SeriesOfPoints_Selective_FP = new Series("u'(u)");
            SeriesOfPoints_Selective_FP.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Selective_FP.Color = Color.Blue;
            SeriesOfPoints_Selective_FP.BorderWidth = 1;

            dataGridView1.RowCount = N_max+1;
            //dataGridView1.ColumnCount = 9;
            dataGridView1.ColumnCount = 10;

            dataGridView1.Columns[0].HeaderText = "i";
            dataGridView1.Columns[1].HeaderText = "xi";
            dataGridView1.Columns[2].HeaderText = "vi";
            dataGridView1.Columns[3].HeaderText = "v2i";
            dataGridView1.Columns[4].HeaderText = "vi-v2i";
            dataGridView1.Columns[5].HeaderText = "ОЛП";
            dataGridView1.Columns[6].HeaderText = "hi";
            dataGridView1.Columns[7].HeaderText = "C1";
            dataGridView1.Columns[8].HeaderText = "C2";

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[8].Width = 50;
            for (int i = 1; i < 7; i++)
                dataGridView1.Columns[i].Width = 160;
            dataGridView1.Columns[9].Width = 160;
           // dataGridView1.Columns[10].Width = 160;

            RightPart_base_2_task task = new RightPart_base_2_task(a,b);
            RightPart_base_2_task task1 = new RightPart_base_2_task(a,b);
            RightPart_base_2_task task_h_2 = new RightPart_base_2_task(a,b);

            task.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task1.SetInit(0, u0);//SetInit(double x0, double[] U0, double a_, double b_)
            task_h_2.SetInit(0,u0);
            double x1 = task.x;
            int s = 1, f = 0;
            double vn_1 = u0[0], vn2_1 = u0[0], xn_1 = 0, vnp_1 = u0[1], vnp2_1 = u0[1];
            double[] UP = new double [N_max+1];
            for (int k = 0; k < N_max; k++)
            {
                //dataGridView1.Rows.Add();
                int C1 = 0, C2 = 0;
                if (k==0)
                {//НУ
                    dataGridView1.Rows[k].Cells[0].Value = 0;
                    dataGridView1.Rows[k].Cells[1].Value = task.x;//xn=x0
                    dataGridView1.Rows[k].Cells[2].Value = task.U[0];//vn=u0
                    dataGridView1.Rows[k].Cells[3].Value = task1.U[0];//v2n=u0
                    dataGridView1.Rows[k].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n=0
                    dataGridView1.Rows[k].Cells[6].Value = h;//hi
                    dataGridView1.Rows[k].Cells[7].Value = C1;//c1
                    dataGridView1.Rows[k].Cells[8].Value = C2;//c2
                    UP[0] = task.U[1];
                    dataGridView1.Rows[k].Cells[9].Value = task.U[1];//c2
                }
                double S1 = 0;
                while (!(task.x + h - b_h < delta))//h = proverka_h(task.x, h, b_h, delta);
                {
                    h = h / 2.0;
                    C1++;
                }//////////////////////////////////////////////
                if ((task.x < b_h + delta) && (task.x>b_h))
                    break;
                if (k != 0)
                {
                    double[] S = new double[2]; double Sn;
                    S[0] =  Math.Abs((task_h_2.U[0] - task.U[0]) / 3);//2^2-1, 2-порядок метода//LP
                    S[1] =  Math.Abs((task_h_2.U[1] - task.U[1]) / 3);//LP
                    S1 = 4* Math.Max(S[0], S[1]);//OLP
                    Sn = Math.Max(S[0], S[1]);//LP
                    //double S2 = Sn *4;//:4
                    if (Sn < epsilon / 8.0) //(k != 0))
                    {
                        h = 2 * h;
                        C2++;
                        while (!(task.x + h - b_h < delta))//h = proverka_h(task.x, h, b_h, delta);
                        {
                            h = h / 2.0;
                            C1++;
                        }
                    }
                    if (Sn > epsilon) //)&& (k != 0)
                    {
                        task.x = xn_1;//task.x - h;//xn-1
                        task.U[0] = vn_1;//(Double)dataGridView1.Rows[s-1].Cells[2].Value;
                        task.U[1] = vnp_1;///7798687t87t
                                          ///
                        task1.x = xn_1;
                        task1.U[0] = vn2_1;//task.U[0];
                        task1.U[1] = vnp_1;///7798687t87t
                        s = f;
                        h = h / 2.0;
                        C1++;
                    }
                }
                xn_1 = task.x;
                vn_1 = task.U[0];
                vnp_1 = task.U[1];

                vn2_1 = task1.U[0];
                vnp2_1 = task1.U[1];
                f = s;
                task_h_2.x = task.x;//xn
                task_h_2.U[0] = task.U[0];//vn
                task_h_2.U[1] = task.U[1];//vn

                task.NextStep(h);
                task1.NextStep(h / 2.0);
                task1.x = task.x;//xn

                task_h_2.NextStep(h / 2.0);//из (xn,vn)-> (xn+1/2, vn+1/2)
                task_h_2.NextStep(h / 2.0);//из (xn+1/2, vn+1/2)-> (xn+1,vn+1=)

                dataGridView1.Rows[s].Cells[0].Value = s;
                dataGridView1.Rows[s].Cells[1].Value = task.x;//xn
                dataGridView1.Rows[s].Cells[2].Value = task.U[0];//vn
                UP[s] = task.U[1];
                dataGridView1.Rows[s].Cells[3].Value = task1.U[0];//v2n
                dataGridView1.Rows[s].Cells[4].Value = task.U[0] - task1.U[0];//vn-v2n
                dataGridView1.Rows[s-1].Cells[5].Value = S1;
                dataGridView1.Rows[s].Cells[6].Value = h;//hi
                dataGridView1.Rows[s].Cells[7].Value = C1;//c1
                dataGridView1.Rows[s].Cells[8].Value = C2;//c2
                dataGridView1.Rows[s].Cells[9].Value = task.U[1];//c2
                s++;
                if (task.x  + 0.00000000000001>= b_h + delta)
                    break;
            }//for
            
            ///////////////////////////////////////////////////////
            double[] S2 = new double[2]; double Sn2;
            S2[0] = 4* Math.Abs((task_h_2.U[0] - task.U[0]) / 3);//2^2-1, 2-порядок метода//OLP
            S2[1] = 4* Math.Abs((task_h_2.U[1] - task.U[1]) / 3);//OLP
            Sn2 = Math.Max(S2[0], S2[1]);//OLP
            //double S21 = Sn2;
            dataGridView1.Rows[s - 1].Cells[5].Value = Sn2;//последняя OLP
            ///////////////////////////////////////////////
            double maxmodolp = 0;
            double max_h = 0;
            double min_h = (Double)dataGridView1.Rows[0].Cells[6].Value;
            double x_minh = 0, x_maxh = 0, c4 = (Double)dataGridView1.Rows[0].Cells[2].Value, max_u = (Double)dataGridView1.Rows[0].Cells[2].Value, min_u=max_u;
            double[] UP1 = new double [s];
            for (int i = 0; i < s ; i++)//n=s-1
            {
                UP1[i] = UP[i];
                double x = (Double)dataGridView1.Rows[i].Cells[1].Value;
                double u = (Double)dataGridView1.Rows[i].Cells[2].Value;//vn
                SeriesOfPoints_Selective.Points.AddXY(x, u);
                SeriesOfPoints_Selective_Tochnoe.Points.AddXY(x, UP[i]);//ui=Math.Exp(task.x) * u0[0];
                SeriesOfPoints_Selective_FP.Points.AddXY(u, UP[i]);//ui=Math.Exp(task.x) * u0[0];
                double c1 = (Double)dataGridView1.Rows[i].Cells[5].Value;
                maxmodolp = Math.Max(c1, maxmodolp);//OLP
                c4 = (Double)dataGridView1.Rows[i].Cells[2].Value;
                max_u = Math.Max(c4, max_u);
                min_u = Math.Min(c4, min_u);
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
            double c3 = UP1[0], max_up = UP1[0], min_up=max_up;
            for (int i = 0; i < s; i++ )
            {
                c3 = UP1[i];
                max_up = Math.Max(c3, max_up);
                min_up = Math.Min(c3, min_up);
            }
            graf_chart1.Series.Add(SeriesOfPoints_Selective);
            graf_chart1.Series.Add(SeriesOfPoints_Selective_Tochnoe);
            graf_chart2.Series.Add(SeriesOfPoints_Selective_FP);

            graf_chart1.ChartAreas[0].AxisX.Maximum = task.x + 0.5; //Задаешь максимальные значения координат
            //graf_chart1.ChartAreas[0].AxisY.Maximum = task.U[0] + 0.5; //Задаешь максимальные значения координат
            graf_chart1.ChartAreas[0].AxisX.Minimum = 0; //Задаешь максимальные значения координат
            if ((task.U[0] > 0) && (task.U[1] > 0) || (task.U[0] == 0) && (task.U[1] == 0))
                graf_chart1.ChartAreas[0].AxisY.Maximum = Math.Max(task.U[0] + 0.5, task.U[1] + 0.5); //Задаешь максимальные значения координат
            if ((task.U[0] < 0) && (task.U[1] < 0))
                graf_chart1.ChartAreas[0].AxisY.Maximum = Math.Min(task.U[0] - 0.5, task.U[1] - 0.5); //Задаешь максимальные значения координат

            graf_chart2.ChartAreas[0].AxisY.Maximum = max_up + 0.5; //Задаешь максимальные значения координат
            graf_chart2.ChartAreas[0].AxisY.Minimum = min_up - 0.5; //Задаешь максимальные значения координат
            graf_chart2.ChartAreas[0].AxisX.Maximum = max_u + 0.5; //Задаешь максимальные значения координат
            graf_chart2.ChartAreas[0].AxisX.Minimum = min_u - 0.5; //Задаешь максимальные значения координат

            n_base_2_textBox17.Text = (s-1).ToString();//n_test_textBox1.Text = (s-1).ToString();
            maxOLP_base_2_textBox15.Text = maxmodolp.ToString();//maxmod_olp_test_textBox8.Text = 
            bxn_base_2_textBox16.Text = (b_h - task.x).ToString();//bxn_test_textBox3.Text = (b_h - task.x).ToString();//double xn = task.x - h;
            max_hi_base_2_textBox11.Text = max_h.ToString();
            min_hi_base_2_textBox1.Text = min_h.ToString();
            x_minhi_base_2_textBox12.Text = x_minh.ToString();
            x_maxhi_base_2_textBox13.Text = x_maxh.ToString();
            ////////////////////////////////////////////////////////
        }       
    }       
}
