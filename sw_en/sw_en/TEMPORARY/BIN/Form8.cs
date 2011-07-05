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
    public partial class Form8 : Form
    {
    private TabControl tabControl1;
    private TabPage tabPage1;

    
        public Form8()
        {
            InitializeComponent();
            InitializeComponent1 ();
            InstantiateMyCheckBox();
            InitializeMyTabs();
           
            

            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();

            // Gets the controls collection for tabControl1.
            // Adds the tabPage1 to this collection.
            this.tabControl1.TabPages.Add(tabPage1);

            this.tabControl1.Location = new Point(25, 25);
            this.tabControl1.Size = new Size(250, 250);

            this.ClientSize = new Size(300, 300);
            this.Controls.Add(tabControl1);

            TextBox1Password_char();


        }

        private void TextBox1Password_char()
        {
            textBox1.PasswordChar = '*';
            textBox1.Refresh();
        }

        private void InitializeComponent1()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Multiline = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.textBox1);
            this.Text = "TextBox Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        [STAThread]
        static void Main3()
        {
            Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form8());
        }

        public void InstantiateMyCheckBox()
        {
            // Create and initialize a CheckBox.   
            CheckBox checkBox11 = new CheckBox();

            // Make the check box control appear as a toggle button.
            checkBox11.Appearance = Appearance.Button;

            // Turn off the update of the display on the click of the control.
            checkBox11.AutoCheck = false;

            // Add the check box control to the form.
            Controls.Add(checkBox11);

        }

        // Declares tabPage1 as a TabPage type.

        private System.Windows.Forms.TabPage tabPage11;
        private void InitializeMyTabs()
        {
            this.tabControl1 = new TabControl();

            // Invokes the TabPage() constructor to create the tabPage1.
            this.tabPage11 = new System.Windows.Forms.TabPage();

            this.tabControl1.Controls.AddRange(new Control[] { this.tabPage11 });
            this.tabControl1.Location = new Point(305, 25);
            this.tabControl1.Size = new Size(250, 250);
            this.ClientSize = new Size(300, 300);
            this.Controls.AddRange(new Control[] {
            this.tabControl1});
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
{
            fontDialog1.ShowColor = true;

            fontDialog1.Font = textBox1.Font;
            fontDialog1.Color = textBox1.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
            }
        }

// get the colors from the text box and assign them to the colordialog's

// custom colors using an array of int

// The Text box contains 16 lines of colors as integer

int[] MyColors = new int[16];

for (int i = 0; i < 16; i++)
{

    MyColors[i] = int.Parse(textBox1.Lines[i]);

}

colorDialog1.CustomColors = MyColors;

// Sets the initial color select to the current text color,

// so that if the user cancels out, the original color is restored.

// "myColor" is an application setting variable that I added.

// System.Drawing.Color.Navy - colors
colorDialog1.Color = CENEX.Properties.Settings.Default.myColor;

//

if (colorDialog1.ShowDialog() == DialogResult.OK)
{

    CENEX.Properties.Settings.Default.myColor = colorDialog1.Color;

    // now save the custom colors to the text box:

    textBox1.Clear();

    int[] colors = (int[])colorDialog1.CustomColors.Clone();

    string[] colorsTxt = new string[16];

    for (int i = 0; i < 16; i++)
    {

        colorsTxt[i] = colors[i].ToString();

    }

    textBox1.Lines = colorsTxt;

}
        }

    }
}
