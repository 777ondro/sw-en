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
    public partial class Form13 : Form
    {

        private System.Windows.Forms.ListView ListView1;
        private System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Button Button1;
        
        public Form13()
        {
            InitializeListView();
            InitializeComponent();

            

            
        }



// This method adds two columns to the ListView, setting the Text 
// and TextAlign, and Width properties of each ColumnHeader.  The 
// HeaderStyle property is set to NonClickable since the ColumnClick 
// event is not handled.  Finally the method adds ListViewItems and 
// SubItems to each column.
private void InitializeListView()
{
    this.ListView1 = new System.Windows.Forms.ListView();
    this.ListView1.BackColor = System.Drawing.SystemColors.Control;
    this.ListView1.Dock = System.Windows.Forms.DockStyle.Top;
    this.ListView1.Location = new System.Drawing.Point(0, 0);
    this.ListView1.Name = "ListView1";
    this.ListView1.Size = new System.Drawing.Size(292, 130);
    this.ListView1.TabIndex = 0;
    this.ListView1.View = System.Windows.Forms.View.Details;
    this.ListView1.MultiSelect = true;
    this.ListView1.HideSelection = false;
    this.ListView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    
    ColumnHeader columnHeader1 = new ColumnHeader();
    columnHeader1.Text = "Breakfast Item";
    columnHeader1.TextAlign = HorizontalAlignment.Left;
    columnHeader1.Width = 146;

     ColumnHeader columnHeader2 = new ColumnHeader();
    columnHeader2.Text = "Price Each";
    columnHeader2.TextAlign = HorizontalAlignment.Center;
    columnHeader2.Width = 142;
  
    this.ListView1.Columns.Add(columnHeader1);
    this.ListView1.Columns.Add(columnHeader2);

    this.TextBox1 = new System.Windows.Forms.TextBox();
    this.TextBox1.Location = new System.Drawing.Point(0, 140);
    this.TextBox1.Name = "TextBox1";
    this.TextBox1.Size = new System.Drawing.Size(100, 20);
    this.TextBox1.TabIndex = 0;

    this.Button1 = new System.Windows.Forms.Button();
    this.Button1.Location = new System.Drawing.Point(0, 200);
    this.Button1.Name = "Button1";
    this.Button1.Size = new System.Drawing.Size(75, 23);
    this.Button1.TabIndex = 0;
    this.Button1.Text = "Button1";
    this.Button1.UseVisualStyleBackColor = true;
    this.Button1.Click += new System.EventHandler(this.Button1_Click);
    






    string[] foodList = new string[]{"Juice", "Coffee", 
        "Cereal & Milk", "Fruit Plate", "Toast & Jelly", 
        "Bagel & Cream Cheese"};
    string[] foodPrice = new string[]{"1.09", "1.09", "2.19", 
        "2.49", "1.49", "1.49"};
    
    for(int count=0; count < foodList.Length; count++)
    {
        ListViewItem listItem = new ListViewItem(foodList[count]);
        listItem.SubItems.Add(foodPrice[count]);
        ListView1.Items.Add(listItem);
    }
    this.Controls.Add(ListView1);
    this.Controls.Add(TextBox1);
    this.Controls.Add(Button1);
}


// Uses the SelectedItems property to retrieve and tally the price 
// of the selected menu items.

private void Button1_Click(object sender, EventArgs e)
{
    ListView.SelectedListViewItemCollection breakfast = this.ListView1.SelectedItems;

    double price = 0.0;
    foreach (ListViewItem item in breakfast)
    {
        price += Double.Parse(item.SubItems[1].Text);
    }

    // Output the price to TextBox1.
    TextBox1.Text = price.ToString();
}


    }
}
