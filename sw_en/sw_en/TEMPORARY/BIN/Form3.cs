﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            ShowDialog ();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            MessageBox.Show(" THIS IS PRIVATE APPLICATION FORM \n HELLO WORLD ");

        }
    }
}
