using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CENEX
{
    public partial class EN1993_1_1MessageBoxes : Form
    {
        #region Variables and property methods
        // Variables and property methods

        // Button - bool = true if all items are selected
        private bool selectedAllItems1;


        // selected items in checkedListBox1 true =  item is selected
        bool item0_check;

        public bool Item0_check
        {
            get { return item0_check; }
            set { item0_check = value; }
        }
        

        bool item1_check;

        public bool Item1_check
        {
            get { return item1_check; }
            set { item1_check = value; }
        }
        bool item2_check;

        public bool Item2_check
        {
            get { return item2_check; }
            set { item2_check = value; }
        }
        bool item3_check;

        public bool Item3_check
        {
            get { return item3_check; }
            set { item3_check = value; }
        }
        bool item4_check;

        public bool Item4_check
        {
            get { return item4_check; }
            set { item4_check = value; }
        }
        bool item5_check;

        public bool Item5_check
        {
            get { return item5_check; }
            set { item5_check = value; }
        }
        bool item6_check;

        public bool Item6_check
        {
            get { return item6_check; }
            set { item6_check = value; }
        }



        #endregion
            
        // Constructor
        public EN1993_1_1MessageBoxes()
        {
            InitializeComponent();
            if (checkedListBox1.GetItemChecked(0)) item0_check = true;
            if (checkedListBox1.GetItemChecked(1)) item1_check = true;
            if (checkedListBox1.GetItemChecked(2)) item2_check = true;
            if (checkedListBox1.GetItemChecked(3)) item3_check = true;
            if (checkedListBox1.GetItemChecked(4)) item4_check = true;
            if (checkedListBox1.GetItemChecked(5)) item5_check = true;
            if (checkedListBox1.GetItemChecked(6)) item6_check = true;

           // this.SystemMessageBoxShow ();
           
           
        }
        private void SystemMessageBoxShow ()
        {
            MessageBox.Show(
                           " Show Message 0: " + item0_check +
                           " Show Message 1: " + item1_check +
                           " Show Message 2: " + item2_check +
                           " Show Message 3: " + item3_check +
                           " Show Message 4: " + item4_check +
                           " Show Message 5: " + item5_check +
                           " Show Message 6: " + item6_check);
            


        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Class object

            EN1993_1_1MessageBoxes en = new EN1993_1_1MessageBoxes ();
            
            
           
            if (!selectedAllItems1)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
                selectedAllItems1 = true;
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                selectedAllItems1 = false;
            }
        }

        
       




    }
}
