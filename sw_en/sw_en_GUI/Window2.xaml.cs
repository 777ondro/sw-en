using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using CENEX;
using _3DTools;
using MATH;
using CRSC;
using BaseClasses;

namespace sw_en_GUI
{
  /// <summary>
  /// Interaction logic for Window2.xaml
  /// </summary>
  public partial class Window2 : Window
  {
    ///////////////////////////////////////////////////////////////
    // Create switch command for various sections, split code into separate objects / function of 3D drawing for each type
    /////////////////////////////////////////////////

    // Tutorial
    /// http://kindohm.com/technical/WPF3DTutorial.htm  ScreenSpaceLines3D
    /// stiahol som zdroje

    // Shape Type
    // Auxiliary identification of shape


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region zakomentovat
    /*
   
  private void load_0_00_01_TriangelsIndices()
  {
   const int secNum = 20;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   for (int i = 0; i < secNum - 1; i++)
   {
    if (i < secNum - 2)
    {
     M_TriangelsIndices.Add(i);
     M_TriangelsIndices.Add(secNum - 1);
     M_TriangelsIndices.Add(i + 1);
    }
    else // Last Element
    {
     M_TriangelsIndices.Add(i);
     M_TriangelsIndices.Add(secNum - 1);
     M_TriangelsIndices.Add(0);
    }
   }

   // Back Side
   for (int i = 0; i < secNum - 1; i++)
   {
    if (i < secNum - 2)
    {
     M_TriangelsIndices.Add(secNum + i);
     M_TriangelsIndices.Add(secNum + i + 1);
     M_TriangelsIndices.Add(secNum + secNum - 1);
    }
    else // Last Element
    {
     M_TriangelsIndices.Add(secNum + i);
     M_TriangelsIndices.Add(secNum);
     M_TriangelsIndices.Add(secNum + secNum - 1);
    }
   }

   // Shell Surface Arc
   for (int i = 0; i < secNum - 2; i++)
    AddRectangleIndices_CW_1234(M_TriangelsIndices, i, secNum + i, secNum + i + 1, i + 1);

   // Flat Sides - !!! Clock-wise points of arc generation
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, secNum - 1, 2 * secNum - 1, secNum);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, secNum - 1, secNum - 2, 2 * secNum - 2, 2 * secNum - 1);

  }

  private void load_0_02_03_TriangelIndices()
  {
   const int secNum = 73;  // Number of points in section (2D) includes centroid
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   for (int i = 0; i < secNum - 1; i++)
   {
    if (i < secNum - 2)
    {
     M_TriangelsIndices.Add(i);
     M_TriangelsIndices.Add(secNum - 1);
     M_TriangelsIndices.Add(i + 1);
    }
    else // Last Element
    {
     M_TriangelsIndices.Add(i);
     M_TriangelsIndices.Add(secNum - 1);
     M_TriangelsIndices.Add(0);
    }
   }

   // Back Side
   for (int i = 0; i < secNum - 1; i++)
   {
    if (i < secNum - 2)
    {
     M_TriangelsIndices.Add(secNum + i);
     M_TriangelsIndices.Add(secNum + i + 1);
     M_TriangelsIndices.Add(secNum + secNum - 1);
    }
    else // Last Element
    {
     M_TriangelsIndices.Add(secNum + i);
     M_TriangelsIndices.Add(secNum);
     M_TriangelsIndices.Add(secNum + secNum - 1);
    }
   }

   // Shell Surface OutSide
   for (int i = 0; i < secNum - 1; i++)
   {
    if (i < secNum - 2)
     AddRectangleIndices_CW_1234(M_TriangelsIndices, i, secNum + i, secNum + i + 1, i + 1);
    else
     AddRectangleIndices_CW_1234(M_TriangelsIndices, i, secNum + i, secNum, 0); // Last Element
   }
  }

  private void load_0_04_TriangelsIndices()
  {
   // const int secNum = 3;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   M_TriangelsIndices.Add(0);
   M_TriangelsIndices.Add(2);
   M_TriangelsIndices.Add(1);

   // Back Side 
   M_TriangelsIndices.Add(3);
   M_TriangelsIndices.Add(4);
   M_TriangelsIndices.Add(5);

   // Shell Surface
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 3, 4, 1);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 4, 5, 2);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 5, 3, 0);
  }

  private void load_0_20_TriangelIndices()
  {
   const int secNum = 37;  // Number of points in section (2D) 36+1 -centroid
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead

   for (int i = 0; i < secNum - 1; i++)
   {
    if (i < secNum - 2)
     AddRectangleIndices_CW_1234(M_TriangelsIndices, i, i + 1, i + secNum + 1, i + secNum);
    else
     AddRectangleIndices_CW_1234(M_TriangelsIndices, i, 0, i + 1, i + secNum); // Last Element
   }

   // Back Side
   for (int i = 0; i < secNum - 1; i++)
   {
    if (i < secNum - 2)
     AddRectangleIndices_CW_1234(M_TriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + secNum + 1, 2 * secNum + i + 1);
    else
     AddRectangleIndices_CW_1234(M_TriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + 1, 2 * secNum + 0); // Last Element
   }

   //// Shell Surface OutSide
   for (int i = 0; i < secNum - 2; i++)
   {
    AddRectangleIndices_CW_1234(M_TriangelsIndices, i, 2 * secNum + i, 2 * secNum + i + 1, i + 1);
   }
   // Shell Surface Inside
   for (int i = 0; i < secNum - 2; i++)
   {
    AddRectangleIndices_CW_1234(M_TriangelsIndices, secNum + i, secNum + i + 1, 2 * secNum + i + secNum + 1, 2 * secNum + i + secNum);
   }

   // Base
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, secNum, 3 * secNum, 2 * secNum);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, secNum - 2, 3 * secNum - 2, 4 * secNum - 2, secNum + (secNum - 2));
  }

  private void load_0_22_23_TriangelIndices()
  {
   // const int secNum = 2*36;  // Number of points in section (2D)
   const int secNum = 72; // Number of points in one circle of section (2D) /Number of points is equal to Number of cells par section

   load_0_26_28_TriangelIndices(0, secNum);
  }

  private void load_0_24_TriangelsIndices()
  {
   // const int secNum = 6;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 4, 3);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 4, 1, 2, 5);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 3, 5, 2);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 6, 8, 11, 9);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 10, 11, 8, 7);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 6, 9, 10, 7);

   // Shell Surface
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 6, 7, 1);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 7, 8, 2);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 8, 6, 0);

   AddRectangleIndices_CW_1234(M_TriangelsIndices, 3, 4, 10, 9);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 4, 5, 11, 10);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 4, 9, 11, 5);
  }

  private void load_0_25_TriangelIndices()
  {
   // const int secNum = 8;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 5, 4);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 2, 6, 5);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 7, 6, 2, 3);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 4, 7, 3);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 9, 8, 12, 13);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 9, 13, 14, 10);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 14, 15, 11, 10);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 8, 11, 15, 12);

   // Shell Surface
   // Outside
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 8, 9, 1);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 9, 10, 2);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 10, 11, 3);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 8, 0, 3, 11);
   // Inside
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 13, 5, 6, 14);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 7, 15, 14, 6);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 4, 12, 15, 7);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 12, 4, 5, 13);
  }

  private void load_0_26_28_TriangelIndices(int iAux, int secNum)
  {
   // iAux - number of auxiliary points in inside/outside collection of points
   // secNum - numer of real points in inside/outside collection of points
   // iAux + secNum - total number of points in inside/outside collection of section

   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead

   for (int i = 0; i < secNum; i++)
   {
    if (i < secNum - 1)
     AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + i, iAux + i + 1, iAux + i + (iAux + secNum) + 1, iAux + i + (iAux + secNum));
    else
     AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + i, iAux + 0, iAux + i + iAux + 1, iAux + i + (iAux + secNum)); // Last Element
   }

   // Back Side
   for (int i = 0; i < secNum; i++)
   {
    if (i < secNum - 1)
     AddRectangleIndices_CW_1234(M_TriangelsIndices, 2 * (iAux + secNum) + iAux + i, 2 * (iAux + secNum) + i + 2 * iAux + secNum, 2 * (iAux + secNum) + i + 2 * iAux + secNum + 1, 2 * (iAux + secNum) + iAux + i + 1);
    else
     AddRectangleIndices_CW_1234(M_TriangelsIndices, 2 * (iAux + secNum) + iAux + i, 2 * (iAux + secNum) + i + 2 * iAux + secNum, 2 * (iAux + secNum) + i + 2 * iAux + 1, 2 * (iAux + secNum) + iAux + 0); // Last Element
   }

   // Shell Surface OutSide
   for (int i = 0; i < secNum; i++)
   {
    if (i < secNum - 1)
     AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + i, 2 * (iAux + secNum) + iAux + i, 2 * (iAux + secNum) + iAux + i + 1, iAux + i + 1);
    else
     AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + i, 2 * (iAux + secNum) + iAux + i, 2 * (iAux + secNum) + iAux, iAux + 0); // Last Element
   }

   // Shell Surface Inside
   for (int i = 0; i < secNum; i++)
   {
    if (i < secNum - 1)
     AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + secNum + iAux + i, iAux + secNum + iAux + i + 1, 2 * (iAux + secNum) + i + 2 * iAux + secNum + 1, 2 * (iAux + secNum) + i + 2 * iAux + secNum);
    else
     AddRectangleIndices_CW_1234(M_TriangelsIndices, 2 * (iAux + secNum) + 2 * iAux + i + 1, 2 * (iAux + secNum) + i + 2 * iAux + secNum, iAux + secNum + iAux + i, 2 * iAux + secNum); // Last Element
   }
  }

  private void load_0_50_TriangelIndices()
  {
   // const int secNum = 12;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 2, 11);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 10, 3, 4, 9);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 8, 5, 6, 7);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 13, 12, 23, 14);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 15, 22, 21, 16);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 17, 20, 19, 18);

   // Shell Surface 
   DrawCaraLaterals(12, M_TriangelsIndices);
  }

  private void load_0_52_TriangelIndices()
  {
   // const int secNum = 8;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 2, 3);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 3, 4, 7);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 4, 5, 6, 7);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 9, 8, 11, 10);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 11, 8, 15, 12);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 13, 12, 15, 14);

   // Shell Surface 
   DrawCaraLaterals(8, M_TriangelsIndices);
  }

  private void load_0_54_TriangelIndices()
  {
   // const int secNum = 6;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 2, 5);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, 4, 5);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 7, 6, 11, 8);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 9, 8, 11, 10);

   // Shell Surface
   DrawCaraLaterals(6, M_TriangelsIndices);
  }

  private void load_0_56_TriangelIndices()
  {
   // const int secNum = 8;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 2, 7);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 6, 3, 4, 5);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 9, 8, 15, 10);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 11, 14, 13, 12);

   // Shell Surface
   DrawCaraLaterals(8, M_TriangelsIndices);
  }

  private void load_0_58_TriangelIndices()
  {
   // const int secNum = 8;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 6, 7);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 6, 1, 2, 5);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, 4, 5);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 9, 8, 15, 14);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 9, 14, 13, 10);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 11, 10, 13, 12);

   // Shell Surface
   DrawCaraLaterals(8, M_TriangelsIndices);
  }

  private void load_0_60_TriangelIndices(int secNum)
  {
   // const int secNum = 12;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 2, 11);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, 4, 5);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 5, 6, 7, 8);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 8, 9, 10, 11);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 5, 8, 11);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 23, 14, 13, 12);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 17, 16, 15, 14);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 20, 19, 18, 17);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 23, 22, 21, 20);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 23, 20, 17, 14);

   // Shell
   DrawCaraLaterals(12, M_TriangelsIndices);
  }

  private void load_0_61_TriangelIndices()
  {
   // const int secNum = 9;  // Number of points in section (2D)
   M_TriangelsIndices = new Int32Collection();

   // Front Side / Forehead
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, 7, 8);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 2, 3, 4);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 4, 5, 6, 7);
   // Front Side / Forehead
   M_TriangelsIndices.Add(1);
   M_TriangelsIndices.Add(7);
   M_TriangelsIndices.Add(4);

   // Back Side 
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 17, 16, 10, 9);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 13, 12, 11, 10);
   AddRectangleIndices_CW_1234(M_TriangelsIndices, 16, 15, 14, 13);
   // Back Side 
   M_TriangelsIndices.Add(10);
   M_TriangelsIndices.Add(13);
   M_TriangelsIndices.Add(16);

   // Shell
   DrawCaraLaterals(9, M_TriangelsIndices);
  }
  */
    #endregion



    #region zakomentovat

    //private void load_3_02_TriangelIndices(short sShape, int iAux, int iRadiusSegment)
    //{
    //    // const int secNum = iAux + iRadiusPoints * 4;  // Number of points in section (2D)
    //    int iRadiusPoints = iRadiusSegment + 1;

    //    M_TriangelsIndices = new Int32Collection();

    //    if (sShape == 0)
    //    {
    //        // Front Side / Forehead
    //        // Points order 1,2,3,4

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, iAux + 1 + iRadiusSegment, iAux + 2 + iRadiusSegment);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, iAux + 2 + iRadiusSegment, 2, 6);

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, iAux + 2 + 4 * iRadiusPoints - 1, 6);

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 3, iAux + 3 * iRadiusPoints, 5, iAux + 1 + 4 * iRadiusPoints);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + 3 * iRadiusPoints, iAux + 3 * iRadiusPoints + 1, 4, 5);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(1, iAux + 1, iRadiusSegment, M_TriangelsIndices, false);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(2, iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(3, iAux + 1 + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(4, iAux + 1 + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);

    //        // Back Side
    //        // Points order 1,4,3,2

    //        int iPointNumbersOffset = iAux + 2 + 4 * iRadiusPoints; // Number of nodes per section - Nodes offset

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 2 + iRadiusSegment, iPointNumbersOffset + iAux + 1 + iRadiusSegment, iPointNumbersOffset + 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 6, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 + iRadiusSegment);

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 2, iPointNumbersOffset + 6, iPointNumbersOffset + iAux + 2 + 4 * iRadiusPoints - 1, iPointNumbersOffset + 3);

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1 + 4 * iRadiusPoints, iPointNumbersOffset + 5, iPointNumbersOffset + iAux + 3 * iRadiusPoints);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + iAux + 3 * iRadiusPoints, iPointNumbersOffset + 5, iPointNumbersOffset + 4, iPointNumbersOffset + iAux + 3 * iRadiusPoints + 1);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 1, iRadiusSegment, M_TriangelsIndices, true);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1 + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 4, iPointNumbersOffset + iAux + 1 + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);

    //        // Shell
    //        DrawCaraLaterals(iAux, 2 + 4 * iRadiusPoints, M_TriangelsIndices);
    //    }
    //    else if (sShape == 1)
    //    {
    //        // Not implemented
    //    }
    //    else if (sShape == 2)
    //    {
    //        // Front Side / Forehead
    //        // Points order 1,2,3,4

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 2, 3, 6);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 3, 4, 5, 6);

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, iAux + 7 + 2 * iRadiusPoints, 2);

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, iAux + 2 * iRadiusPoints + 3, iAux + 2 * iRadiusPoints + 6, iAux + 2 * iRadiusPoints + 7);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + 2 * iRadiusPoints + 3, iAux + 2 * iRadiusPoints + 4, iAux + 2 * iRadiusPoints + 5, iAux + 2 * iRadiusPoints + 6);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(0, iAux + 4, iRadiusSegment, M_TriangelsIndices, false);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(1, iAux + 4 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);

    //        // Back Side
    //        // Points order 1,4,3,2

    //        int iPointNumbersOffset = iAux + 8 + 2 * iRadiusPoints; // Number of nodes per section - Nodes offset

    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 2, iPointNumbersOffset + 3, iPointNumbersOffset + 6);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 3, iPointNumbersOffset + 4, iPointNumbersOffset + 5, iPointNumbersOffset + 6);

    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 7 + 2 * iRadiusPoints, iPointNumbersOffset + 2);

    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 3, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 6, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 7);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 3, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 4, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 5, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 6);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 4, iRadiusSegment, M_TriangelsIndices, true);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 4 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);

    //        // Shell
    //        DrawCaraLaterals(iAux, 8 + 2 * iRadiusPoints, M_TriangelsIndices);
    //    }
    //    else // Exception
    //    {

    //    }
    //}

    //private void load_3_03_04_TriangelIndices(int iAux, int iRadiusSegment)
    //{
    //    // const int secNum = iAux + iRadiusPoints * 3 + 1;  // Number of points in section (2D)
    //    int iRadiusPoints = iRadiusSegment + 1;

    //    M_TriangelsIndices = new Int32Collection();

    //    // Front Side / Forehead
    //    // Points order 1,2,3,4

    //    AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, iAux + iRadiusSegment, 1, iAux + 3 * iRadiusPoints);
    //    AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, iAux + 2 * iRadiusPoints, 2, iAux + 3 * iRadiusPoints);

    //    // Arc sectors
    //    // 1st SolidCircleSector
    //    AddSolidCircleSectorIndices(0, iAux, iRadiusSegment, M_TriangelsIndices, false);
    //    // 2nd SolidCircleSector
    //    AddSolidCircleSectorIndices(1, iAux + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //    // 3rd SolidCircleSector
    //    AddSolidCircleSectorIndices(2, iAux + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);

    //    // Back Side
    //    // Points order 1,4,3,2

    //    int iPointNumbersOffset = iAux + 1 + 3 * iRadiusPoints; // Number of nodes per section - Nodes offset

    //    AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 3 * iRadiusPoints, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + iRadiusSegment);
    //    AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 3 * iRadiusPoints, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 * iRadiusPoints);

    //    // Arc sectors
    //    // 1st SolidCircleSector
    //    AddSolidCircleSectorIndices(iPointNumbersOffset + 0, iPointNumbersOffset + iAux, iRadiusSegment, M_TriangelsIndices, true);
    //    // 2nd SolidCircleSector
    //    AddSolidCircleSectorIndices(iPointNumbersOffset + 1, iPointNumbersOffset + iAux + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //    // 3rd SolidCircleSector
    //    AddSolidCircleSectorIndices(iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);

    //    // Shell
    //    DrawCaraLaterals(iAux, 1 + 3 * iRadiusPoints, M_TriangelsIndices);
    //}

    //private void load_3_07_TriangelIndices(short sShape, int iAux, int iRadiusSegment)
    //{
    //    int secNum = 4 * (iRadiusSegment + 1); // Number of points to draw in one section inside or outside surface

    //    if (sShape == 0 || sShape == 1 || sShape == 2)
    //        load_0_26_28_TriangelIndices(iAux, secNum);
    //    else if (sShape == 3)
    //    {
    //        // const int secNum = iAux + iRadiusPoints * 4;  // Number of points in section (2D)
    //        int iRadiusPoints = iRadiusSegment + 1;

    //        M_TriangelsIndices = new Int32Collection();

    //        // Front Side / Forehead
    //        // Points order 1,2,3,4

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, iAux + iRadiusSegment, iAux + 1 + iRadiusSegment, 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, iAux + 2 * iRadiusSegment + 1, iAux + 2 * iRadiusSegment + 2, 2);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 3, 2, iAux + 3 * iRadiusSegment + 2, iAux + 3 * iRadiusSegment + 3);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux, 0, 3, iAux + 4 * iRadiusSegment + 3);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(0, iAux, iRadiusSegment, M_TriangelsIndices, false);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(1, iAux + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(2, iAux + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(3, iAux + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);

    //        // Back Side
    //        // Points order 1,4,3,2

    //        int iPointNumbersOffset = iAux + 4 * iRadiusPoints; // Number of nodes per section - Nodes offset

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 1 + iRadiusSegment, iPointNumbersOffset + iAux + iRadiusSegment);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 * iRadiusSegment + 2, iPointNumbersOffset + iAux + 2 * iRadiusSegment + 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 3 * iRadiusSegment + 3, iPointNumbersOffset + iAux + 3 * iRadiusSegment + 2, iPointNumbersOffset + 2);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + iAux, iPointNumbersOffset + iAux + 4 * iRadiusSegment + 3, iPointNumbersOffset + 3, iPointNumbersOffset + 0);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 0, iPointNumbersOffset + iAux, iRadiusSegment, M_TriangelsIndices, true);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 1, iPointNumbersOffset + iAux + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);

    //        // Shell - outside
    //        DrawCaraLaterals(iAux, 4 * iRadiusPoints, M_TriangelsIndices);
    //        // Shell - inside
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, iPointNumbersOffset + 0, iPointNumbersOffset + 3, 3);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, iPointNumbersOffset + 1, iPointNumbersOffset + 0, 0);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, iPointNumbersOffset + 2, iPointNumbersOffset + 1, 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 3, iPointNumbersOffset + 3, iPointNumbersOffset + 2, 2);
    //    }
    //    else if (sShape == 4)
    //    {

    //    }
    //    else
    //        load_0_25_TriangelIndices();
    //}

    //private void load_3_08_TriangelIndices(short sShape, int iAux, int iRadiusSegment)
    //{
    //    int iRadiusPoints = iRadiusSegment + 1;

    //    M_TriangelsIndices = new Int32Collection();

    //    if (sShape == 0) // 0 - Five radii, tapered flanges, optional tapered web (5+2 auxiliary points)
    //    {
    //        // Front Side / Forehead
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, iAux + 5 * iRadiusPoints - 1, iAux + 5 * iRadiusPoints);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 2, iAux + 2 + iRadiusSegment, iAux + 5 * iRadiusPoints - 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, iAux + 1 + iRadiusSegment, iAux + 2 + iRadiusSegment);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 6, 4, iAux + 2 * iRadiusPoints, iAux + 4 * iRadiusPoints);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + 4 * iRadiusPoints, iAux + 2 * iRadiusPoints, iAux + 2 * iRadiusPoints + 1, iAux + 4 * iRadiusPoints - 1);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(3, iAux + 1, iRadiusSegment, M_TriangelsIndices, false);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(4, iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(5, iAux + 1 + 2 * iRadiusPoints, 2 * iRadiusSegment, M_TriangelsIndices, false);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(6, iAux + 4 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //        // 5th SolidCircleSector
    //        AddSolidCircleSectorIndices(0, iAux + 5 * iRadiusPoints, iRadiusSegment - 1, M_TriangelsIndices, false); // Segments number = iRadiusSegment-1

    //        // Last Triangle 
    //        M_TriangelsIndices.Add(0); // 1st Point
    //        M_TriangelsIndices.Add(iAux); // 1st Point of Radii (1st after auxiliary)
    //        M_TriangelsIndices.Add(iAux + 6 * iRadiusPoints - 2); // Last Point


    //        // Back Side 

    //        int iPointNumbersOffset = iAux + 6 * iRadiusPoints - 1; // Number of nodes per section - Nodes offset

    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 5 * iRadiusPoints, iPointNumbersOffset + iAux + 5 * iRadiusPoints - 1, iPointNumbersOffset + 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 5 * iRadiusPoints - 1, iPointNumbersOffset + iAux + 2 + iRadiusSegment, iPointNumbersOffset + 2);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 + iRadiusSegment, iPointNumbersOffset + iAux + 1 + iRadiusSegment, iPointNumbersOffset + 3);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + 6, iPointNumbersOffset + iAux + 4 * iRadiusPoints, iPointNumbersOffset + iAux + 2 * iRadiusPoints, iPointNumbersOffset + 4);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iPointNumbersOffset + iAux + 4 * iRadiusPoints, iPointNumbersOffset + iAux + 4 * iRadiusPoints - 1, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 1, iPointNumbersOffset + iAux + 2 * iRadiusPoints);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1, iRadiusSegment, M_TriangelsIndices, true);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 4, iPointNumbersOffset + iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 5, iPointNumbersOffset + iAux + 1 + 2 * iRadiusPoints, 2 * iRadiusSegment, M_TriangelsIndices, true);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 6, iPointNumbersOffset + iAux + 4 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //        // 5th SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 5 * iRadiusPoints, iRadiusSegment - 1, M_TriangelsIndices, true); // Segments number = iRadiusSegment-1

    //        // Last Triangle 
    //        M_TriangelsIndices.Add(iPointNumbersOffset + 0); // 1st Point
    //        M_TriangelsIndices.Add(iPointNumbersOffset + iAux + 6 * iRadiusPoints - 2); // Last Point
    //        M_TriangelsIndices.Add(iPointNumbersOffset + iAux); // 1st Point of Radii (1st after auxiliary)


    //        // Shell
    //        DrawCaraLaterals(iAux, 6 * iRadiusPoints - 1, M_TriangelsIndices);
    //    }
    //    else if (sShape == 1) // 1 - Four radii, tapered or parallel flanges, optional tapered web (4+2 auxiliary points)
    //    {
    //        // Front Side / Forehead
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, iAux + 3 * iRadiusPoints + 2, iAux + 3 * iRadiusPoints + 3);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 2, iAux + 1 * iRadiusPoints + 1, iAux + 3 * iRadiusPoints + 2);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, iAux + 1 * iRadiusPoints, iAux + 1 * iRadiusPoints + 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 5, 4, iAux + 2 * iRadiusPoints, iAux + 2 * iRadiusPoints + 3);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + 2 * iRadiusPoints + 3, iAux + 2 * iRadiusPoints, iAux + 2 * iRadiusPoints + 1, iAux + 2 * iRadiusPoints + 2);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(3, iAux + 1, iRadiusSegment, M_TriangelsIndices, false);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(4, iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(5, iAux + 2 * iRadiusPoints + 3, iRadiusSegment, M_TriangelsIndices, false);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(0, iAux + 3 * iRadiusPoints + 3, iRadiusSegment - 1, M_TriangelsIndices, false); // Segments number = iRadiusSegment-1

    //        // Last Triangle 
    //        M_TriangelsIndices.Add(0); // 1st Point
    //        M_TriangelsIndices.Add(iAux); // 1st Point of Radii (1st after auxiliary)
    //        M_TriangelsIndices.Add(iAux + 4 * iRadiusPoints + 1); // Last Point

    //        // Back Side 

    //        int iPointNumbersOffset = iAux + 4 * iRadiusPoints + 2; // Number of nodes per section - Nodes offset

    //        // Changed orientation of triangles
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 3 * iRadiusPoints + 2, iPointNumbersOffset + iAux + 3 * iRadiusPoints + 3);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 1 * iRadiusPoints + 1, iPointNumbersOffset + iAux + 3 * iRadiusPoints + 2);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 2, iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1 * iRadiusPoints, iPointNumbersOffset + iAux + 1 * iRadiusPoints + 1);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 5, iPointNumbersOffset + 4, iPointNumbersOffset + iAux + 2 * iRadiusPoints, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 3);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 3, iPointNumbersOffset + iAux + 2 * iRadiusPoints, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 1, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 2);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1, iRadiusSegment, M_TriangelsIndices, true);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 4, iPointNumbersOffset + iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
    //        // 3rd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 5, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 3, iRadiusSegment, M_TriangelsIndices, true);
    //        // 4th SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 3 * iRadiusPoints + 3, iRadiusSegment - 1, M_TriangelsIndices, true); // Segments number = iRadiusSegment-1

    //        // Last Triangle 
    //        M_TriangelsIndices.Add(iPointNumbersOffset + 0); // 1st Point
    //        M_TriangelsIndices.Add(iPointNumbersOffset + iAux + 4 * iRadiusPoints + 1); // Last Point
    //        M_TriangelsIndices.Add(iPointNumbersOffset + iAux); // 1st Point of Radii (1st after auxiliary)

    //        // Shell
    //        DrawCaraLaterals(iAux, 4 * iRadiusPoints + 2, M_TriangelsIndices);
    //    }
    //    else if (sShape == 2) // 2 - Two radii at flanges tips, tapered or parallel flanges, optional tapered web (4 auxiliary points)
    //    {
    //        // Front Side / Forehead
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, iAux + iRadiusPoints + 4, iAux + iRadiusPoints + 5);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 1, 2, iAux + iRadiusPoints + 1, iAux + iRadiusPoints + 4);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, iAux + iRadiusPoints + 0, iAux + iRadiusPoints + 1);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + iRadiusPoints + 4, iAux + iRadiusPoints + 1, iAux + iRadiusPoints + 2, iAux + iRadiusPoints + 3);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(3, iAux + 1, iRadiusSegment, M_TriangelsIndices, false);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(0, iAux + iRadiusPoints + 5, iRadiusSegment - 1, M_TriangelsIndices, false); // Segments number = iRadiusSegment-1

    //        // Last Triangle 
    //        M_TriangelsIndices.Add(0); // 1st Point
    //        M_TriangelsIndices.Add(iAux); // 1st Point of Radii (1st after auxiliary)
    //        M_TriangelsIndices.Add(iAux + 2 * iRadiusPoints + 3); // Last Point

    //        // Back Side 

    //        int iPointNumbersOffset = iAux + 2 * iRadiusPoints + 4; // Number of nodes per section - Nodes offset

    //        // Changed orientation of triangles
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + iRadiusPoints + 4, iPointNumbersOffset + iAux + iRadiusPoints + 5);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + iRadiusPoints + 1, iPointNumbersOffset + iAux + iRadiusPoints + 4);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 2, iPointNumbersOffset + 3, iPointNumbersOffset + iAux + iRadiusPoints + 0, iPointNumbersOffset + iAux + iRadiusPoints + 1);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + iAux + iRadiusPoints + 4, iPointNumbersOffset + iAux + iRadiusPoints + 1, iPointNumbersOffset + iAux + iRadiusPoints + 2, iPointNumbersOffset + iAux + iRadiusPoints + 3);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1, iRadiusSegment, M_TriangelsIndices, true);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 0, iPointNumbersOffset + iAux + iRadiusPoints + 5, iRadiusSegment - 1, M_TriangelsIndices, true); // Segments number = iRadiusSegment-1

    //        // Last Triangle 
    //        M_TriangelsIndices.Add(iPointNumbersOffset + 0); // 1st Point
    //        M_TriangelsIndices.Add(iPointNumbersOffset + iAux + 2 * iRadiusPoints + 3); // Last Point
    //        M_TriangelsIndices.Add(iPointNumbersOffset + iAux); // 1st Point of Radii (1st after auxiliary)

    //        // Shell
    //        DrawCaraLaterals(iAux, 2 * iRadiusPoints + 4, M_TriangelsIndices);
    //    }
    //    else if (sShape == 3) // 3 - Two radii at flanges roots, tapered or parallel flanges, optional tapered web (2 auxiliary points)
    //    {
    //        // Front Side / Forehead
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 2, 3, iAux + 2 * iRadiusPoints + 6, iAux + 2 * iRadiusPoints + 7);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 3, 4, iAux + iRadiusPoints, iAux + 2 * iRadiusPoints + 6);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 4, 5, 6, 7);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, 0, 1, iAux + iRadiusPoints + 4, iAux + 2 * iRadiusPoints + 2);
    //        AddRectangleIndices_CW_1234(M_TriangelsIndices, iAux + 2 * iRadiusPoints + 2, iAux + iRadiusPoints + 4, iAux + iRadiusPoints + 5, iAux + iRadiusPoints + 6);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(1, iAux + 5, iRadiusSegment, M_TriangelsIndices, false);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(0, iAux + 2 * iRadiusPoints + 2, iRadiusSegment, M_TriangelsIndices, false);

    //        // Back Side 

    //        int iPointNumbersOffset = iAux + 2 * iRadiusPoints + 8; // Number of nodes per section - Nodes offset

    //        // Changed orientation of triangles
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 2, iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 6, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 7);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 3, iPointNumbersOffset + 4, iPointNumbersOffset + iAux + iRadiusPoints, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 6);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 4, iPointNumbersOffset + 5, iPointNumbersOffset + 6, iPointNumbersOffset + 7);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + iRadiusPoints + 4, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 2);
    //        AddRectangleIndices_CCW_1234(M_TriangelsIndices, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 2, iPointNumbersOffset + iAux + iRadiusPoints + 4, iPointNumbersOffset + iAux + iRadiusPoints + 5, iPointNumbersOffset + iAux + iRadiusPoints + 6);

    //        // Arc sectors
    //        // 1st SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 5, iRadiusSegment, M_TriangelsIndices, true);
    //        // 2nd SolidCircleSector
    //        AddSolidCircleSectorIndices(iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 2 * iRadiusPoints + 2, iRadiusSegment, M_TriangelsIndices, true);

    //        // Shell
    //        DrawCaraLaterals(iAux, 2 * iRadiusPoints + 8, M_TriangelsIndices);
    //    }
    //    else //Exception
    //    { }
    //}
    #endregion

    /// <summary>
    /// ///////////////////////////////////////////////////////
    /// MAIN CONSTRUCTOR
    /// ///////////////////////////////////////////////////////
    /// </summary>
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    public Window2()
    {
        
      InitializeComponent();
      
      // Temp
      // Half Circle Bar
      // Quater Circle Bar
      // load_0_00_01_TriangelsIndices();
      // Round or Ellipse Bar
      // load_0_02_03_TriangelIndices();
      // Triangular Prism 
      // load_0_04_TriangelsIndices();
      // Flat Bar
      // load_0_05_TriangelIndices();
      // Half Circle
      // load_0_20_TriangelIndices();
      // TUBE / PIPE Circle or Ellipse Shape
      // load_0_22_23_TriangelIndices();
      // Triangular Prism with Opening
      // load_0_24_TriangelsIndices();
      // HL-section / Rectanglular Hollow Cross-section
      // load_0_25_TriangelIndices();
      // Polygonal Hollow Section
      // load_0_26_28_TriangelIndices(0,test1.objCrScHollow.INoPoints);
      // I - section
      // load_0_50_TriangelIndices();
      // U-section
      // load_0_52_TriangelIndices();
      // L-section / Angle section
      // load_0_54_TriangelIndices();
      // T-section / T section
      // load_0_56_TriangelIndices();
      // Z-section / Z section
      // load_0_58_TriangelIndices();
      // Cruciform Bar
      // load_0_60_TriangelIndices(test1.objCrScSolid.ITotNoPoints);
      // Y-section / Y section
      // load_0_61_TriangelIndices();

      // Rolled I doubly symmetric profile, Tapered or parallel flanges
      // CCrSc obj_CrSc = new CCrSc_3_00(0, 8, 200, 90, 11.3f, 7.5f, 7.5f, 4.5f, 159.1f);
      // load_3_00_TriangelIndices(0, 12,8); // Shape ID, number of auxiliary points , number of segments of arc
      // load_3_00_TriangelIndices(1,8, 8); // Shape ID, number of auxiliary points , number of segments of arc
      // load_3_00_TriangelIndices(2, 4, 8); // Shape ID, number of auxiliary points , number of segments of arc

      // Rolled I monosymmetric profile, Tapered or parallel flanges
      // load_3_00_TriangelIndices(0, 12, 8); // Shape ID, number of auxiliary points , number of segments of arc
      // load_3_00_TriangelIndices(1,8,4); // Shape ID, number of auxiliary points , number of segments of arc
      // load_3_00_TriangelIndices(2, 4, 4); // Shape ID, number of auxiliary points , number of segments of arc

      // Rolled U profile, Tapered or parallel flanges, channel section
      // load_3_02_TriangelIndices(0,6, 8); // Shape ID, number of auxiliary points , number of segments of arc
      // load_3_02_TriangelIndices(2,2, 8); // Shape ID,number of auxiliary points , number of segments of arc
      // Rolled L profile, angle section
      // load_3_03_04_TriangelIndices(3, 8); // Number of auxiliary points, number of segments of arc
      // Rectanglular Hollow Cross-section
      // load_3_07_TriangelIndices(0, 4, 4); // Shape ID, number of auxiliary points per section, number of segments of one arc
      // load_3_07_TriangelIndices(2, 4, 4); // Shape ID, number of auxiliary points per section, number of segments of one arc
      // load_3_07_TriangelIndices(3, 4, 4); // Shape ID, number of auxiliary points per section, number of segments of one arc (iAux = 4)
      // load_3_07_TriangelIndices(5, 0, 0); // Shape ID, number of auxiliary points per section, number of segments of one arc
      // Rolled T profile, Tapered flanges
      // load_3_08_TriangelIndices(1,6,4); // Shape ID, number of auxiliary points, number of segments of arc
      // load_3_08_TriangelIndices(2,4,4); // Shape ID, number of auxiliary points, number of segments of arc
      // load_3_08_TriangelIndices(3, 2, 4); // Shape ID, number of auxiliary points, number of segments of arc

      //MeshGeometry3D mesh = new MeshGeometry3D();

      //Point3D p0 = new Point3D(-1, -1, -1);
      //Point3D p1 = new Point3D(1, -1, -1);
      //Point3D p2 = new Point3D(1, -1, 1);
      //Point3D p3 = new Point3D(-1, -1, 1);
      //Point3D p4 = new Point3D(-1, 1, -1);
      //Point3D p5 = new Point3D(1, 1, -1);
      //Point3D p6 = new Point3D(1, 1, 1);
      //Point3D p7 = new Point3D(-1, 1, 1);

      //mesh = 
      //GeometryModel3D geoModel3D = new GeometryModel3D();
      //geoModel3D.Geometry = mesh;


      Model3DGroup gr = new Model3DGroup();
      //gr.Children.Add(new AmbientLight());

      GeometryModel3D model = new GeometryModel3D();
      MeshGeometry3D mesh = new MeshGeometry3D();
      mesh.Positions = new Point3DCollection();

      //ScreenSpaceLines3D line = new ScreenSpaceLines3D();
      //line.Color = Color.FromRgb(0,255,0);
      //line.Points.Add(mesh.Positions[0]);
      //line.Points.Add(mesh.Positions[1]);

      //Viewport3D view = new Viewport3D();
      //view.Children.Add(line);

      //gr.Children.Add(new AmbientLight());
      model.Geometry = mesh;
      SolidColorBrush br = new SolidColorBrush(Color.FromRgb(255, 0, 0));
      model.Material = new DiffuseMaterial(br);
      gr.Children.Add(model);
      _trackport.Model = (Model3D)gr; //CreateRectangle(p3, p2, p6, p7, Brushes.Red);

      _trackport.Trackball.TranslateScale = 1000;   //step for moving object (panning)

      _trackport.SetupScene();
    }






    public Window2(CModel cmodel)
    {
      InitializeComponent();

      Model3DGroup gr = new Model3DGroup();
      //gr.Children.Add(new AmbientLight());
      SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255, 255, 0));

      EGCS eGCS = EGCS.eGCSLeftHanded;
      //EGCS eGCS = EGCS.eGCSRightHanded;

      // Check that real model exists and create model geometry
      if (cmodel != null)
      {
          // Model Group of Members
          // Prepare member model
          for (int i = 0; i < cmodel.m_arrMembers.Length; i++) // !!! BUG pocet prvkov sa nacitava z xls aj z prazdnych riadkov pokial su nejako formatovane / nie default
          {
              // Start Node of Member
              Point3D mpA = new Point3D(cmodel.m_arrMembers[i].NodeStart.FCoord_X, cmodel.m_arrMembers[i].NodeStart.FCoord_Y, cmodel.m_arrMembers[i].NodeStart.FCoord_Z);
              // End node of Member
              Point3D mpB = new Point3D(cmodel.m_arrMembers[i].NodeEnd.FCoord_X, cmodel.m_arrMembers[i].NodeEnd.FCoord_Y, cmodel.m_arrMembers[i].NodeEnd.FCoord_Z);

              System.Console.Write("\n" + "Member ID:" +(i+1).ToString() +"\n"); // Write Member ID in console window
              System.Console.Write("Start Node ID:" + cmodel.m_arrMembers[i].NodeStart.INode_ID.ToString() + "\n"); // Write Start Node ID and coordinates in console window
              System.Console.Write( mpA.X.ToString() + "\t"+ mpA.Y.ToString() +"\t"+ mpA.Z.ToString()+ "\n");
              System.Console.Write("End Node ID:" + cmodel.m_arrMembers[i].NodeEnd.INode_ID.ToString() + "\n");     // Write   End Node ID and coordinates in console window
              System.Console.Write(mpB.X.ToString() + "\t" + mpB.Y.ToString() + "\t" + mpB.Z.ToString() + "\n\n");

              // Create Member model
              GeometryModel3D membermodel = getGeometryModel3D(eGCS, brush, cmodel.m_arrMembers[i].CrSc, mpA, mpB);

              // Add current member model to the model group
              gr.Children.Add(membermodel);
          }
      }



      Point3D cameraPosition = new Point3D(0, 0, 200);

      //SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
      //GeometryModel3D model = getGeometryModel3D(brush, obj_CrSc, new Point3D(10, 10, 10), new Point3D(500, 300, 200));
      //gr.Children.Add(model);

      ////Point3D cameraPosition = ((MeshGeometry3D)model.Geometry).Positions[0];
      ////cameraPosition.Z -= 1000;

      //brush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
      //model = getGeometryModel3D(brush, obj_CrSc, new Point3D(110, 110, 10), new Point3D(600, 400, 200));
      //gr.Children.Add(model);

      //IMPORTANT: this is the best way to do it, but we can't use it because of trackball
      //because camera is set by trackball Taransform this.Camera.Transform = _trackball.Transform;
      //and headlite too:  this.Headlight.Transform = _trackball.Transform;

      _trackport.PerspectiveCamera.Position = cameraPosition;
      //_trackport.PerspectiveCamera.LookDirection = new Vector3D(cameraPosition.X, cameraPosition.Y, cameraPosition.Z - 100);

      _trackport.Model = (Model3D)gr; //CreateRectangle(p3, p2, p6, p7, Brushes.Red);
      _trackport.SetupScene();

    }

    private GeometryModel3D getGeometryModel3D(EGCS eGCS, SolidColorBrush brush, CCrSc obj_CrSc, Point3D mpA, Point3D mpB)
    {
      GeometryModel3D model = new GeometryModel3D();

      MeshGeometry3D mesh = getMeshGeometry3DFromCrSc(eGCS, obj_CrSc, mpA, mpB); // Mesh one member

      model.Geometry = mesh;

      model.Material = new DiffuseMaterial(brush);

      return model;
    }


    private MeshGeometry3D getMeshGeometry3DFromCrSc(EGCS eGCS, CCrSc obj_CrSc, Point3D mpA, Point3D mpB)
    {
      MeshGeometry3D mesh = new MeshGeometry3D();
      mesh.Positions = new Point3DCollection();

      // Main Nodes of Member
      Point3D m_pA = mpA;
      Point3D m_pB = mpB;

      // Priemet do osi GCS - rozdiel suradnic v GCS
      double m_dDelta_X = m_pB.X - m_pA.X;
      double m_dDelta_Y = m_pB.Y - m_pA.Y;
      double m_dDelta_Z = m_pB.Z - m_pA.Z;

      // Realna dlzka prvku // Length of member - straigth segment of member
      // Prečo je záporná ???
      // double m_dLength = -Math.Sqrt(Math.Pow(m_dDelta_X, 2) + Math.Pow(m_dDelta_Y, 2) + Math.Pow(m_dDelta_Z, 2));
         double m_dLength = Math.Sqrt(Math.Pow(m_dDelta_X, 2) + Math.Pow(m_dDelta_Y, 2) + Math.Pow(m_dDelta_Z, 2));

      // Number of Points per section
      short iNoCrScPoints2D;
      // Points 2D Coordinate Array
      if (obj_CrSc.IsShapeSolid) // Solid I,U,Z,HL,L, ..............
      {
        iNoCrScPoints2D = obj_CrSc.ITotNoPoints; // Depends on Section Type 

        // Solid section
        float[,] res = obj_CrSc.CrScPointsOut; // I,U,Z,HL,L, ..............

        // Fill Mesh Positions for Start and End Section of Element - Defines Edge Points of Element

        // I,U,Z,HL, L, ....
        if (res != null) // Check that data are available
        {
          for (int j = 0; j < iNoCrScPoints2D; j++)
          {
              // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0, res[j, 0], res[j, 1]));
          }
          for (int j = 0; j < iNoCrScPoints2D; j++)
          {
              // X - end, Y, Z
              mesh.Positions.Add(new Point3D(m_dLength, res[j, 0], res[j, 1]));
          }
        }
        else
        {
          // Exception
        }
      }
      else
      {
        // Tubes , Polygonal Hollow Sections
        iNoCrScPoints2D = (short)(2 * obj_CrSc.INoPointsOut); // Twice number of one surface
        //iNoCrScPoints2D = (short)(obj_CrSc.INoPointsOut + obj_CrSc.INoPointsIn);
        float[,] res1 = obj_CrSc.CrScPointsOut; // TU
        float[,] res2 = obj_CrSc.CrScPointsIn; // TU

        // Tube, regular hollow sections
        // TU

        // Start
        if (res1 != null) // Check that data are available
        {
          // OutSide Radius Points
          for (int j = 0; j < obj_CrSc.INoPointsOut; j++)
          {
            // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0, res1[j, 0], res1[j, 1]));
          }
        }
        else
        {
          // Exception
        }

        if (res2 != null) // Check that data are available
        {
          // Inside Radius Points
          for (int j = 0; j < obj_CrSc.INoPointsIn; j++)
          {
            // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0,res2[j, 0], res2[j, 1]));
          }
        }
        else
        {
          // Exception
        }

        // End
        if (res1 != null) // Check that data are available
        {
          // OutSide Radius Points
          for (int j = 0; j < obj_CrSc.INoPointsOut; j++)
          {
            // X - end, Y, Z
            mesh.Positions.Add(new Point3D(m_dLength, res1[j, 0], res1[j, 1]));
          }
        }
        else
        {
          // Exception
        }

        if (res2 != null) // Check that data are available
        {
          // Inside Radius Points
          for (int j = 0; j < obj_CrSc.INoPointsIn; j++)
          {
           // X - end, Y, Z
           mesh.Positions.Add(new Point3D(m_dLength,res2[j, 0], res2[j, 1]));
          }
        }
        else
        {
          // Exception
        }
      }

      // Dislay data in the output window

      string sOutput = "Before transformation \n\n"; // create temporary string

      for (int i = 0; i < 2 * iNoCrScPoints2D; i++) // for all mesh positions (start and end of member, number of edge points of whole member = 2 * number in one section)
      {
          Point3D p3D = mesh.Positions[i]; // Get mesh element/item (returns Point3D)

          sOutput += "Node ID: " + i.ToString();
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.X.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Y.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Z.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns

          sOutput += "\n"; // New row
      }

      System.Console.Write(sOutput); // Write in console window


      // Transform coordinates
      TransformMember_LCStoGCS(eGCS, m_pA, m_pB, m_dDelta_X, m_dDelta_Y, m_dDelta_Z, mesh.Positions);

      // Mesh Triangles - various cross-sections shapes defined
      mesh.TriangleIndices = obj_CrSc.TriangleIndices;


     // Dislay data in the output window

      sOutput = null;
      sOutput = "After transformation \n\n"; // create temporary string

      for (int i = 0; i < 2*iNoCrScPoints2D; i++) // for all mesh positions (start and end of member, number of edge points of whole member = 2 * number in one section)
      {
          Point3D p3D = mesh.Positions[i]; // Get mesh element/item (returns Point3D)

          sOutput += "Node ID: " + i.ToString();
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.X.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Y.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Z.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns

          sOutput += "\n"; // New row
      }

      System.Console.Write(sOutput); // Write in console window

      // Change mesh triangle indices
      // Change orientation of normals

      //if (eGCS == EGCS.eGCSLeftHanded)
      //{
          int iSecond = 1;
          int iThird = 2;

          int iTIcount = mesh.TriangleIndices.Count;
          for (int i = 0; i < iTIcount / 3; i++)
          {
              int iTI_2 = mesh.TriangleIndices[iSecond];
              int iTI_3 = mesh.TriangleIndices[iThird];

              mesh.TriangleIndices[iThird] = iTI_2;
              mesh.TriangleIndices[iSecond] = iTI_3;

              iSecond += 3;
              iThird += 3;
          }
      //}


      return mesh;
    }


    public GeometryModel3D CreateRectangle(
       Point3D point1, Point3D point2,
       Point3D point3, Point3D point4,
       Brush brush)
    {
      MeshGeometry3D mesh = new MeshGeometry3D();
      mesh.Positions.Add(point1);
      mesh.Positions.Add(point2);
      mesh.Positions.Add(point3);
      mesh.Positions.Add(point4);

      mesh.TriangleIndices.Add(0);
      mesh.TriangleIndices.Add(1);
      mesh.TriangleIndices.Add(2);

      mesh.TriangleIndices.Add(0);
      mesh.TriangleIndices.Add(2);
      mesh.TriangleIndices.Add(3);

      return new GeometryModel3D(mesh,
       new DiffuseMaterial(brush));
    }

    public Point3DCollection TransformMember_LCStoGCS(EGCS eGCS, Point3D pA, Point3D pB, double dDeltaX, double dDeltaY, double dDeltaZ, Point3DCollection pointsCollection)
    {
      // Returns transformed coordinates of member nodes


      ////////////////////////////////////////////////////////////////////////////////////////////////
      // TRANSFORMACE SOUŘADNÉHO SYSTÉMU
      ////////////////////////////////////////////////////////////////////////////////////////////////

      // 13.1.2012

      // Right handed system


      /* 
      1. Vygenerovat suradnice v LCS pre realnu dlzku
      2. Pootocit okolo GCS osi X,Y,Z
      3. Posunut cely prut tak aby zaciatocny bod bol v GCS (dostaneme suradnice uzlov prierezu v realnom GCS)
      4. Vygenerovat plochy povrchu prvku
      5. Zobrazit prvok

       Rovnice transformace souřadnic v prostoru se dá vyjádřit maticovou rovnicí:
       a + T.x = r kde T je prostorová transformační matice
             
   Transformační matice T je složena z jednotlivých rovinných transformačních matic, přitom záleží na pořadí
   jednotlivých rotací:
         
   T = Tyz.Txz.Txy
   1.rotace: Txy je rovinná transformační matice při natočení kolem osy Z o úhel Chi.
   2.rotace: Txz je rovinná transformační matice při natočení kolem osy Y o úhel Beta.
   3.rotace: Tyz je rovinná transformační matice při natočení kolem osy X o úhel Alpha.
        
   */



      // FUNKCIE z FEM 3D

      // Ziskat dlzky priemetov pruta do jednotlivych osi
      // GetGCSLengh(float fCoordStart, float fCoordEnd, float fGCsCoord);

      // Ziskat uhly pootocenia priemetov pruta do rovin GCS okolo osi GCS
      // public float[,] fTransMatrix(float x_ba, float y_ba, float z_ba, float l, float angle, float[] fCoord_CA);

      //commented 8.4.2012
      //CVector VMemberStartPointCoordinGCS_a = new CVector(3); // a1,a2,a3
      //CVector VPointCoordinLCS_x = new CVector(3); // x,y,z
      //CVector VPointCoordinGCS_r = new CVector(3); // r,s,t


      //CMatrix fTxy = new CMatrix(3);
      //CMatrix fTxz = new CMatrix(3);
      //CMatrix fTyz = new CMatrix(3);

      //    fTxy.m_fArrMembers = new float[3, 3]
      //{
      //    {(float)Math.Cos(fChi), - (float)Math.Sin(fChi), 0.0f},
      //    {(float)Math.Sin(fChi),   (float)Math.Cos(fChi), 0.0f},
      //    {0f, 0f, 1f}
      //};

      //    fTxz.m_fArrMembers = new float[3, 3]
      //{
      //    {(float)Math.Cos(fBeta),0f,  - (float)Math.Sin(fBeta)},
      //    {0f, 1f, 0f},
      //    {(float)Math.Sin(fChi), 0f,    (float)Math.Cos(fBeta)}
      //};

      //    fTyz.m_fArrMembers = new float[3, 3]
      //{
      //   {1f, 0f, 0f},
      //   {0f, (float)Math.Cos(fAlpha),  - (float)Math.Sin(fAlpha)},
      //   {0f, (float)Math.Sin(fChi),      (float)Math.Cos(fBeta)}
      //};
      //    // Výsledná rovnice transformace souřadnic v prostoru je:
      //    CMatrix fT = new CMatrix(3);

      //    fT.m_fArrMembers = new float[3, 3]
      //{
      //   {(float)(Math.Cos(fBeta) * Math.Cos(fChi)), -(float)(Math.Cos(fBeta) * Math.Sin(fChi)), -(float)Math.Sin(fChi)},
      //   {(float)(Math.Cos(fAlpha) * Math.Sin(fChi) - Math.Sin(fAlpha) * Math.Sin(fBeta) * Math.Cos(fChi)), (float)(Math.Cos(fAlpha) * Math.Cos(fChi) + Math.Sin(fAlpha) * Math.Sin(fBeta) * Math.Sin(fChi)), -(float)(Math.Sin(fAlpha)*Math.Cos(fBeta))},
      //   {(float)(Math.Cos(fAlpha) * Math.Sin(fBeta) * Math.Cos(fChi) + Math.Sin(fAlpha) * Math.Sin(fChi)), (float)(Math.Sin(fAlpha) * Math.Cos(fChi) - Math.Cos(fAlpha) * Math.Sin(fBeta) * Math.Sin(fChi)), (float)(Math.Cos(fAlpha) * Math.Cos(fBeta))}
      //};

      //// r = a + T.x
      //VPointCoordinGCS_r = VectorF.fGetSum(VMemberStartPointCoordinGCS_a,VectorF.fMultiplyMatrVectr(fT, VPointCoordinLCS_x));

      // Angles
      double dAlphaX = 0, dBetaY = 0, dGammaZ = 0;

      // Priemet do rovin GCS - dlzka priemetu do roviny
      double dLength_XY = 0,
             dLength_YZ = 0,
             dLength_XZ = 0;

      if (!MathF.d_equal(dDeltaX, 0.0) || !MathF.d_equal(dDeltaY, 0.0))
        dLength_XY = Math.Sqrt(Math.Pow(dDeltaX, 2) + Math.Pow(dDeltaY, 2));

      if (!MathF.d_equal(dDeltaY, 0.0) || !MathF.d_equal(dDeltaZ, 0.0))
        dLength_YZ = Math.Sqrt(Math.Pow(dDeltaY, 2) + Math.Pow(dDeltaZ, 2));

      if (!MathF.d_equal(dDeltaX, 0.0) || !MathF.d_equal(dDeltaZ, 0.0))
        dLength_XZ = Math.Sqrt(Math.Pow(dDeltaX, 2) + Math.Pow(dDeltaZ, 2));

      // Temporary console output
      System.Console.Write("\n" + "Lengths - projection of element into global coordinate system:\n");
      System.Console.Write("Length - global X-axis:\t" + dDeltaX.ToString("0.000") + "\n"); // Write length in X-axis
      System.Console.Write("Length - global Y-axis:\t" + dDeltaY.ToString("0.000") + "\n"); // Write length in Y-axis
      System.Console.Write("Length - global Z-axis:\t" + dDeltaZ.ToString("0.000") + "\n\n"); // Write length in Z-axis

      // Uhly pootocenia LCS okolo osi GCS
      // !!!!!!!!!!!!!!!!!!! Nefunguju spravne 19.2.2013, je nutne zohladnit kvadrant -> znamienka a posun o PI/2

      /*
      if (!MathF.d_equal(dDeltaY, 0.0))
          dAlphaX = Math.Atan(dDeltaZ / dDeltaY); // radians
      else if (!MathF.d_equal(dLength_YZ, 0.0))
          dAlphaX = Math.Acos(dDeltaY / dLength_YZ);
          //dAlphaX = Math.Asin(dDeltaZ / dLength_YZ);

      if (!MathF.d_equal(dDeltaX, 0.0))
      {
          dBetaY = Math.Atan(dDeltaZ / dDeltaX); // radians
          dGammaZ = Math.Atan(dDeltaY / dDeltaX); // radians  (* Math.PI / 180.0)
      }
      else
      {
          if (!MathF.d_equal(dLength_XZ, 0.0))
              dBetaY = Math.Acos(dDeltaX / dLength_XZ);
              //dBetaY = Math.Asin(dDeltaZ / dLength_XZ);

          if (!MathF.d_equal(dLength_XY, 0.0))
            dGammaZ = Math.Acos(dDeltaY / dLength_XY);
             //dGammaZ = Math.Asin(dDeltaX / dLength_XY);
      }
      */

      dAlphaX = Geom2D.GetAlpha2D_CW(dDeltaY, dDeltaZ);
      dBetaY = Geom2D.GetAlpha2D_CW_2(dDeltaX, dDeltaZ); // !!! Pre pootocenie okolo Y su pouyite ine kvadranty !!!
      dGammaZ = Geom2D.GetAlpha2D_CW(dDeltaX, dDeltaY);

      // Temporary console output
      System.Console.Write("\n" + "Rotation angles:\n");
      System.Console.Write("Rotation about global X-axis:\t" + dAlphaX.ToString("0.000") + "rad\t " + (dAlphaX * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about X-axis
      System.Console.Write("Rotation about global Y-axis:\t" +  dBetaY.ToString("0.000") + "rad\t " + ( dBetaY * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about Y-axis
      System.Console.Write("Rotation about global Z-axis:\t" + dGammaZ.ToString("0.000") + "rad\t " + (dGammaZ * 180.0f / MathF.fPI).ToString("0.0") + "deg \n\n"); // Write rotation about Z-axis

      for (int i = 0; i < pointsCollection.Count; i++)
      {
          pointsCollection[i] = RotatePoint(eGCS, pA, pointsCollection[i], dAlphaX, dBetaY, dGammaZ, dDeltaX, dDeltaY, dDeltaZ);
      }

      return pointsCollection;
    }


    protected Point3D RotatePoint(EGCS eGCS, Point3D pA, Point3D p, double alphaX, double betaY, double gamaZ, double dDeltaX, double dDeltaY, double dDeltaZ)
    {
      Point3D p3Drotated = new Point3D();

      /* Commented 25.5.2013
      // Left Handed

      p3Drotated.X = pA.X + p.X;
      p3Drotated.Y = pA.Y + (p.Y * Math.Cos(alphaX) - p.Z * Math.Sin(alphaX));
      p3Drotated.Z = pA.Z + (p.Y * Math.Sin(alphaX) + p.Z * Math.Cos(alphaX));

      p3Drotated.X = pA.X + (p.X * Math.Cos(betaY) + p.Z * Math.Sin(betaY));
      p3Drotated.Y = pA.Y + p.Y;
      p3Drotated.Z = pA.Z + (-p.X * Math.Sin(betaY) + p.Z * Math.Cos(betaY));

      p3Drotated.X = pA.X + (p.X * Math.Cos(gamaZ) - p.Y * Math.Sin(gamaZ));
      p3Drotated.Y = pA.Y + (p.X * Math.Sin(gamaZ) + p.Y * Math.Cos(gamaZ));
      p3Drotated.Z = pA.Z + p.Z;
      */

      // Left Handed
      // * Where (alphaX) represents the rotation about the X axis, (betaY) represents the rotation about the Y axis, and (gamaZ) represents the rotation about the Z axis
      /*
      X Rotation *

      1     0                0                  0
      0     cos (alphaX)    -sin (alphaX)       0
      0     sin (alphaX)     cos (alphaX)       0
      0     0                0                  1
      */

      /*
      Y Rotation *

      cos (betaY)    0     sin (betaY)    0
      0              1     0              0
      -sin (betaY)   0     cos (betaY)    0
      0              0     0              1
      */

      /*
      Z Rotation *

      cos (gamaZ)     -sin (gamaZ)     0      0
      sin (gamaZ)     cos (gamaZ)      0      0
      0                 0              1      0
      0                 0              0      1
      */

      ////////////////////////////////////////////////////////////////////////////////////////////
      // Right Handed

      /*
      X Rotation *

      1           0             0               0
      0     cos (alphaX)     sin (alphaX)       0
      0     -sin (alphaX)    cos (alphaX)       0
      0           0             0               1
      */

      /*
      Y Rotation *

      cos (betaY)    0     -sin (betaY)   0
      0              1     0              0
      sin (betaY)    0     cos (betaY)    0
      0              0     0              1
      */

      /*
      Z Rotation *

      cos (gamaZ)     sin (gamaZ)      0      0
      -sin (gamaZ)    cos (gamaZ)      0      0
      0                 0              1      0
      0                 0              0      1
      */

      /*
      Point3D pTemp1 = new Point3D();
      Point3D pTemp2 = new Point3D();

      // In case that member is parallel to global axis should be rotated only once

      if (eGCS == EGCS.eGCSLeftHanded)
      {
          // Left handed

          pTemp1.X = p.X;
          pTemp1.Y = (Math.Cos(alphaX) * p.Y - Math.Sin(alphaX) * p.Z);
          pTemp1.Z = (Math.Sin(alphaX) * p.Y + Math.Cos(alphaX) * p.Z);

          pTemp2.X = Math.Cos(betaY) * pTemp1.X + Math.Sin(betaY) * pTemp1.Z;
          pTemp2.Y = pTemp1.Y;
          pTemp2.Z = -Math.Sin(betaY) * pTemp1.Y + Math.Cos(betaY) * pTemp1.Z;

          p3Drotated.X = pA.X + Math.Cos(gamaZ) * pTemp2.X - Math.Sin(gamaZ) * pTemp2.Y;
          p3Drotated.Y = pA.Y + Math.Sin(gamaZ) * pTemp2.X + Math.Cos(gamaZ) * pTemp2.Y;
          p3Drotated.Z = pA.Z + pTemp2.Z;
      }
      else
      {
          // Right handed

          pTemp1.X = p.X;
          pTemp1.Y = (Math.Cos(alphaX) * p.Y + Math.Sin(alphaX) * p.Z);
          pTemp1.Z = (-Math.Sin(alphaX) * p.Y + Math.Cos(alphaX) * p.Z);

          pTemp2.X = Math.Cos(betaY) * pTemp1.X - Math.Sin(betaY) * pTemp1.Z;
          pTemp2.Y = pTemp1.Y;
          pTemp2.Z = Math.Sin(betaY) * pTemp1.Y + Math.Cos(betaY) * pTemp1.Z;

          p3Drotated.X = pA.X + (Math.Cos(gamaZ) * pTemp2.X + Math.Sin(gamaZ) * pTemp2.Y);
          p3Drotated.Y = pA.Y + (-Math.Sin(gamaZ) * pTemp2.X + Math.Cos(gamaZ) * pTemp2.Y);
          p3Drotated.Z = pA.Z + pTemp2.Z;
      }
      */

      // Cumulative 3D rotation and translation
      // http://inside.mines.edu/~gmurray/ArbitraryAxisRotation/

      // Rotate around x, y, z


      if (dDeltaX < 0 && MathF.d_equal(dDeltaY, 0.0) && MathF.d_equal(dDeltaZ, 0.0))      // Parallel to X-axis with negative orientation
          betaY = 0; // Do not rotate about Y-axis
      else if (MathF.d_equal(dDeltaX, 0.0) && dDeltaY < 0 && MathF.d_equal(dDeltaZ, 0.0)) // Parallel to Y-axis with negative orientation
          alphaX = 0; // Do not rotate about X-axis
      else if (MathF.d_equal(dDeltaX, 0.0) && MathF.d_equal(dDeltaY, 0.0) && dDeltaZ < 0) // Parallel to Z-axis with negative orientation
          betaY = 0; // Do not rotate about Y-axis
      else
      {
          // No action - General position of member in space 
      }

      p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + (Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY) - Math.Cos(alphaX) * Math.Sin(gamaZ)) * p.Y + (Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY) + Math.Sin(alphaX) * Math.Sin(gamaZ)) * p.Z);
      p3Drotated.Y = pA.Y + ((Math.Cos(betaY) * Math.Sin(gamaZ)) * p.X + (Math.Cos(alphaX) * Math.Cos(gamaZ) + Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ)) * p.Y + (-Math.Cos(gamaZ) * Math.Sin(alphaX) + Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ)) * p.Z);
      p3Drotated.Z = pA.Z + ((-Math.Sin(betaY)) * p.X + (Math.Cos(betaY) * Math.Sin(alphaX)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);


      // Rotate around z, y, x
      /*
      p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + (-Math.Cos(betaY) * Math.Sin(gamaZ)) * p.Y + (Math.Sin(betaY)) * p.Z);
      p3Drotated.Y = pA.Y + ((Math.Cos(alphaX) * Math.Sin(gamaZ) + Math.Sin(alphaX)*Math.Sin(betaY) * Math.Cos(gamaZ)) * p.X + (Math.Cos(alphaX) * Math.Cos(gamaZ) - Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ)) * p.Y + (-Math.Sin(alphaX) * Math.Cos(betaY)) * p.Z);
      p3Drotated.Z = pA.Z + ((Math.Sin(alphaX) * Math.Sin(gamaZ) - Math.Cos(alphaX) * Math.Sin(betaY) * Math.Cos(gamaZ)) * p.X + (Math.Sin(alphaX) * Math.Cos(gamaZ) + Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin (gamaZ)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);
      */


      return p3Drotated;
    }




  }
}
