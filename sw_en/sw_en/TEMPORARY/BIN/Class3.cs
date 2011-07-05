using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Class3
    {




public void method3 ()
{
    Class1 objektClass1 = new Class1();
    objektClass1.method1();
}


public void arrays_specimen ()
{
///
/// http://msdn.microsoft.com/en-us/library/aa288453%28VS.71%29.aspx
/// 

    int[,] array_int = new int[3, 2] { { 11, 12 }, { 21, 22 }, { 31, 32 } };
    double[,] array_double = new double[6, 2] { { 11.5, 12.54 }, { 21.54897, -22.65 }, { 31, 32 }, { 41, 42 }, { -51.4753, 52 }, { 61, 62.64 } };
    string[,] array_string = new string[2, 2] { { "Excel", "Word" }, { "Subsystem", "Oversystem" } };

    int[,] numbers = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
    string[,] siblings = { { "Mike", "Amy" }, { "Mary", "Albert" } };
       
    // Jagged Array (Array-of-Arrays)
    int[][] numbers1 = new int[2][] { new int[] { 2, 3, 4 }, new int[] { 5, 6, 7, 8, 9 } };
    int[][] numbers2 = new int[][] { new int[] { 2, 3, 4 }, new int[] { 5, 6, 7, 8, 9 } };
    int[][] numbers3 = { new int[] { 2, 3, 4 }, new int[] { 5, 6, 7, 8, 9 } };

    double array_value1 = array_double[2, 1];
    double array_value2 = array_double [3, 0];
    int array_value3 = numbers2 [0][1];
    int array_value4 = numbers2[1][2];


    Console.WriteLine(" Array value is" + array_value1);
    MessageBox.Show(

        "Suma",  // Peto pokus

        "Array value 1 is "
        + array_value1.ToString()+ "\n"
        + "Array value 2 is "
        + array_value2.ToString() + "\n"
        + "Array value 3 is "
        + array_value3.ToString() + "\n"
        + "Array value 4 is "
        + array_value4.ToString() + "\n"
        + "\n"
        + "Sum of all arrays is "
        + (array_value1 + array_value2 + array_value3 + array_value4).ToString()
    );


}
        public void arrays_specimen2 ()
{
int[,] array2_int = new int[3, 2] { { 14, 2 }, { 2, 4 }, { 5, 65 } };

// Table cells values (array positions values)

int array_value1 = array2_int[0, 0];
int array_value2 = array2_int[0, 1];
int array_value3 = array2_int[1, 0];
int array_value4 = array2_int[1, 1];
int array_value5 = array2_int[2, 0];
int array_value6 = array2_int[2, 1];






        MessageBox.Show(

            

        "Array row 1 is "
        + array_value1.ToString()+ "" +  array_value2.ToString()+ "\n"
        + "Array value 2 is "
        + array_value3.ToString()+ "" + array_value4.ToString() + "\n"
        + "Array value 3 is "
        + array_value5.ToString()+ "" + array_value6.ToString() + "\n"
        + "\n"
        + "Sum of all array cells is "
        + (array_value1 + array_value2 + array_value3 + array_value4+ array_value5+ array_value6).ToString()
        
       
, "HEADER OF MESSAGE BOX" 
        );


}




}
}




