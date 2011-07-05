using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WindowsFormsApplication1
{
    // The basic Customer class.
public class Customer : System.Object
{
   private string custName = "";
   protected ArrayList custOrders = new ArrayList();

   public Customer(string customername)
   {
      this.custName = customername;
   }

   public string CustomerName
   {      
      get{return this.custName;}
      set{this.custName = value;}
   }

   public ArrayList CustomerOrders 
   {
      get{return this.custOrders;}
   }

} // End Customer class 

}

