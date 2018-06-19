using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Численные_методы
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void основная2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            основная_2 основная_2 = new основная_2();
            основная_2.ShowDialog();
        }

    }
}
