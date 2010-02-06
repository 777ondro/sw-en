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
using CENEX.MODEL.PAINT;
using CENEX.DATABASE.MAT;
using CENEX.DATABASE.CRSC;



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

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_specimens a = new Form1_specimens();
            a.ShowDialog();
        }
        private void eC1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void eC2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EN1992_1_1Form obj_EN1992_1_1Form = new EN1992_1_1Form();
            obj_EN1992_1_1Form.ShowDialog();
        }
        private void eC3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EN1993_1_1Form obj_EN1993_1_1Form = new EN1993_1_1Form ();
            obj_EN1993_1_1Form.ShowDialog();
        }
        private void eC4ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eC5ToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void eC6ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eC7ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eC8ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void eC9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EN1999_1_1Form obj_EN1999_1_1Form = new EN1999_1_1Form();
            obj_EN1999_1_1Form.ShowDialog();
        }

        private void paintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaintForm obj_PaintForm = new PaintForm();
            obj_PaintForm.ShowDialog();
        }

        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data_MatForm obj_Data_MatForm = new Data_MatForm();
            obj_Data_MatForm.ShowDialog();
        }

        private void crosssectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data_CSForm obj_Data_CSForm = new Data_CSForm();
            obj_Data_CSForm.ShowDialog();
        }

        

        
        

        
    }
}
