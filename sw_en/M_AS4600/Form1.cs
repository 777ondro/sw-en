using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MATERIAL;
using CRSC;

namespace M_AS4600
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            CSO cso = new CSO();
            cso.A_g = 3232;
            cso.I_y = 11200;
            cso.I_z = 55406;
            cso.I_yz = 1458;
            cso.I_t = 5887878;
            cso.I_w = 5277778;

            cso.b = 0.09f;
            cso.t_min = 0.002;
            cso.t_max = 0.002;

            cso.m_Mat.m_fE = 2.1e+8f;
            cso.m_Mat.m_fG = 8.1e+7f;
            cso.m_Mat.m_fNu = 0.297f;


            CCalcul calculate = new CCalcul(cso);
        }
    }
}
