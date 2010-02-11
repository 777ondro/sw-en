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
    public partial class Form2 : Form
    {
        bool close_Form2;

        public bool Close_Form2
        {
            get { return close_Form2; }
            set { close_Form2 = value; }
        }
       

        public Form2()
        {
            InitializeComponent();





        }
    public void method2a ()
        {
            MessageBox.Show("Method2a started. \n " + " Form 2 - password form is activated.");
        }

    public void Close_applicationForm2(bool close)
    {
        close_Form2 = checkBox2.Checked;
        if (checkBox2.Checked == true)
        {
            this.Close();

        }
    }
    private void button1_Click(object sender, EventArgs e)
    {

        if (checkBox2.Checked == true)
        {
       Form1 objektForm1 = new Form1();
            objektForm1.Close_applicationForm1(checkBox2.Checked);

            Close_applicationForm2(checkBox2.Checked);


        }
        else if (checkBox3.Checked == true)
        {
            if (checkBox1.Checked == true && textBox1.Text == "heslo")
            {
                Form3 objektForm3 = new Form3();
            }
            else

                MessageBox.Show(" Enter correct password and set it checked up to enter the private application, please.");
        }
        else
        {
            if (checkBox1.Checked == true && textBox1.Text == "heslo")
            {
                MessageBox.Show(" Select next step, please. Internal system array auxialiary calculation is running.");
                Form1 objektForm1 = new Form1();
                objektForm1.runClass2();
                Class3 objektClass3 = new Class3();
                objektClass3.arrays_specimen2();


            }
            else

                MessageBox.Show(" Enter correct password and set it checked up to enter / exit application, please.");
}
}
} 
}

