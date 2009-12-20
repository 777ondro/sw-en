using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Crownwood.Magic.Docking;
using Crownwood.Magic.Common;


namespace CENEX
{
    public partial class MainForm : Form
    {
        DatabaseConnection dat_conn;
        DockingManager _manager;
        Content c;
        public MainForm()
        {
            InitializeComponent();
            dat_conn = DatabaseConnection.getInstance();
            
            //EN1993_1_1Form i = new InfoSteelForm();
            //i.ShowDialog();

            //docking
            _manager = new DockingManager(this, VisualStyle.IDE);
            // Create Content which contains a RichTextBox
            c = _manager.Contents.Add(new TreeForm(), "tree menu");
            _manager.AddContentWithState(c, State.DockLeft);
   
        }
        

        private void treeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _manager.AddContentWithState(c, State.DockLeft);
        }

        private void steelInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EN1993_1_1Form inStForm = new EN1993_1_1Form();
            inStForm.ShowDialog();
        }
        private void TZBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TZBForm iTZB = new TZBForm();
            iTZB.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            EN1993_1_1Form i = new EN1993_1_1Form();
            i.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            EN1993_1_1Form i = new EN1993_1_1Form();
            i.ShowDialog();
        }
        private void crossSectionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CSOForm iCSOForm = new CSOForm();
            iCSOForm.ShowDialog();
        }

        private void en1999ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EN1999_1_1Form inAlForm = new EN1999_1_1Form();
            inAlForm.ShowDialog();
        }

        
        

        
    }
}
