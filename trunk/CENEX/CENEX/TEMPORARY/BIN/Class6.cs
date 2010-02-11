﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
// The basic customer Order class.

public class Order : System.Object
{
   private string ordID = "";

   public Order(string orderid)
   {
      this.ordID = orderid;
   }

   public string OrderID
   {      
      get{return this.ordID;}
      set{this.ordID = value;}
   }

} // End Order class

/*
// Create a new ArrayList to hold the Customer objects.
public Arraylist customerArray = new ArrayList(); 

private void FillMyTreeView()
{
// Add customers to the ArrayList of Customer objects.
for(int x=0; x<1000; x++)
{
  customerArray.Add(new Customer("Customer" + x.ToString()));
}

// Add orders to each Customer object in the ArrayList.
foreach (Customer customer1 in customerArray)
{
  for(int y=0; y<15; y++)
  {
     customer1.CustomerOrders.Add(new Order("Order" + y.ToString()));    
  }
}

// Display a wait cursor while the TreeNodes are being created.
Cursor.Current = new Cursor("MyWait.cur");

// Suppress repainting the TreeView until all the objects have been created.
treeView1.BeginUpdate();

// Clear the TreeView each time the method is called.
treeView1.Nodes.Clear();

// Add a root TreeNode for each Customer object in the ArrayList.
foreach(Customer customer2 in customerArray)
{
  treeView1.Nodes.Add(new TreeNode(customer2.CustomerName));

  // Add a child treenode for each Order object in the current Customer object.
  foreach(Order order1 in customer2.CustomerOrders)
  {
     treeView1.Nodes[customerArray.IndexOf(customer2)].Nodes.Add(
       new TreeNode(customer2.CustomerName + "." + order1.OrderID));
  }
}

// Reset the cursor to the default for all controls.
Cursor.Current = Cursors.Default;

// Begin repainting the TreeView.
treeView1.EndUpdate();
}


}*/
}
