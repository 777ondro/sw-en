using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form10 : Form
    {
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button btn1;
        public System.Windows.Forms.Button btn2;

        public Form10()
        {
            InitializeComponent();


            







            // Create and initialize a Button.
            /*
            Button buttonOK = new Button();
            Button btn1 = new Button();
            Button btn2 = new Button();
             */

            //this.buttonOK = new System.Windows.Forms.Button ();
            // this.btn1 = new System.Windows.Forms.Button ();
            // this.btn2 = new System.Windows.Forms.Button ();
            
        
            

            this.btn1 = new System.Windows.Forms.Button();
            this.btn1.BackColor = System.Drawing.SystemColors.Info;
            this.btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn1.ForeColor = System.Drawing.Color.Navy;
            this.btn1.Location = new System.Drawing.Point(12, 10);
            this.btn1.Name = "Button1";
            this.btn1.Size = new System.Drawing.Size(170, 58);
            this.btn1.TabIndex = 2;
            this.btn1.Text = "Button1";
            this.btn1.UseCompatibleTextRendering = true;
            this.btn1.UseVisualStyleBackColor = true;


            this.btn2 = new System.Windows.Forms.Button();
            this.btn2.BackColor = System.Drawing.SystemColors.Info;
            this.btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn2.ForeColor = System.Drawing.Color.Moccasin;
            this.btn2.Location = new System.Drawing.Point(12, 80);
            this.btn2.Name = "Button2";
            this.btn2.Size = new System.Drawing.Size(170, 58);
            this.btn2.TabIndex = 2;
            this.btn2.Text = "Button2";
            this.btn2.UseCompatibleTextRendering = true;
            this.btn2.UseVisualStyleBackColor = true;


            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonOK.BackColor = System.Drawing.SystemColors.Info;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonOK.ForeColor = System.Drawing.Color.Plum;
            this.buttonOK.Location = new System.Drawing.Point(12, 160);
            this.buttonOK.Name = "ButtonOK";
            this.buttonOK.Size = new System.Drawing.Size(170, 58);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "ButtonOK";
            this.buttonOK.UseCompatibleTextRendering = true;
            this.buttonOK.UseVisualStyleBackColor = true;

































            // Add the button to the form.
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btn2);




            // Set the button to return a value of OK when clicked.
            try
{
            buttonOK.DialogResult = DialogResult.OK;
}
catch
            {
                MessageBox.Show (" This is exception","EXCEPTION");
            }




            

            
        }

        void OnClick1(object sender, EventArgs e)
        {
            btn1.BackColor = Color.Orange;
           
        }

        void buttonOK_Click2(object sender, EventArgs e)
        {
            btn2.BackColor = Color.Pink;
        }

        void buttonOK_Click3(object sender, EventArgs e)
        {
            btn1.BackColor = Color.Green;
            btn2.BackColor = Color.LightBlue;
        } 

    }
    public class DemoTableLayoutPanel1 : TableLayoutPanel
    {
        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            base.OnCellPaint(e);

            Control c = this.GetControlFromPosition(e.Column, e.Row);

            if (c != null)
            {
                Graphics g = e.Graphics;

                g.DrawRectangle(
                    Pens.Red,
                    e.CellBounds.Location.X + 1,
                    e.CellBounds.Location.Y + 1,
                    e.CellBounds.Width - 2, e.CellBounds.Height - 2);

                g.FillRectangle(
                    Brushes.Blue,
                    e.CellBounds.Location.X + 1,
                    e.CellBounds.Location.Y + 1,
                    e.CellBounds.Width - 2,
                    e.CellBounds.Height - 2);
            };
        }

    }

    
   

    
        
    

}
