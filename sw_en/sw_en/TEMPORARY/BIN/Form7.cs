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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }


        // Groupbox
        private void InitializeMyGroupBox()
        {
            // Create and initialize a GroupBox and a Button control.
            GroupBox groupBox11 = new GroupBox();
            Button button11 = new Button();
            button11.Location = new Point(1000, 420);

            // Set the FlatStyle of the GroupBox.
            groupBox11.FlatStyle = FlatStyle.Flat;

            // Add the Button to the GroupBox.
            groupBox11.Controls.Add(button11);

            // Add the GroupBox to the Form.
            Controls.Add(groupBox11);

            // Create and initialize a GroupBox and a Button control.
            GroupBox groupBox21 = new GroupBox();
            Button button21 = new Button();
            button21.Location = new Point(1000, 320);
            groupBox21.Location = new Point(1000, 320);

            // Set the FlatStyle of the GroupBox.
            groupBox21.FlatStyle = FlatStyle.Standard;

            // Add the Button to the GroupBox.
            groupBox21.Controls.Add(button21);

            // Add the GroupBox to the Form.
            Controls.Add(groupBox21);
        }
        private GroupBox groupBox11;
        private RadioButton radioButton21;
        private RadioButton radioButton11;
        // Radiobuttons
        public void InitializeRadioButtons()
        {
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.radioButton21 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            // adding members into the groupbox

            this.groupBox11.Controls.Add(this.radioButton11);
            this.groupBox11.Controls.Add(this.radioButton21);
            this.groupBox11.Location = new System.Drawing.Point(1000, 200);
            this.groupBox11.Size = new System.Drawing.Size(200, 100);
            this.groupBox11.Text = "Radio Buttons";

            this.radioButton21.Location = new System.Drawing.Point(1000, 250);
            this.radioButton21.Size = new System.Drawing.Size(67, 17);
            this.radioButton21.Text = "Choice 2";

            this.radioButton11.Location = new System.Drawing.Point(1000, 350);
            this.radioButton11.Name = "radioButton1";
            this.radioButton11.Size = new System.Drawing.Size(67, 17);
            this.radioButton11.Text = "Choice 1";

            this.ClientSize = new System.Drawing.Size(1000, 266);
            this.Controls.Add(this.groupBox11);
        }
        private TabControl tabControl11;
    }
}
