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
using System.Configuration;
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
      private bool bDebugging;

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
    public Window2(bool bDebugging)
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

      GeometryModel3D memberModel3D = new GeometryModel3D();
      MeshGeometry3D mesh = new MeshGeometry3D();
      mesh.Positions = new Point3DCollection();

      //ScreenSpaceLines3D line = new ScreenSpaceLines3D();
      //line.Color = Color.FromRgb(0,255,0);
      //line.Points.Add(mesh.Positions[0]);
      //line.Points.Add(mesh.Positions[1]);

      //Viewport3D view = new Viewport3D();
      //view.Children.Add(line);

      //gr.Children.Add(new AmbientLight());
      memberModel3D.Geometry = mesh;
      SolidColorBrush br = new SolidColorBrush(Color.FromRgb(255, 0, 0));
      memberModel3D.Material = new DiffuseMaterial(br);

      gr.Children.Add(memberModel3D); // Add member to model group

      _trackport.Model = (Model3D)gr; //CreateRectangle(p3, p2, p6, p7, Brushes.Red);

      _trackport.Trackball.TranslateScale = 1000;   //step for moving object (panning)

      _trackport.SetupScene();
    }

    public Window2(CModel cmodel, bool bDebugging)
    {
      InitializeComponent();

      if (cmodel != null)
      {
          Model3DGroup gr = new Model3DGroup();
          //gr.Children.Add(new AmbientLight());

          // Default color
          SolidColorBrush brushDefault = new SolidColorBrush(Color.FromRgb(255, 0, 0));

          EGCS eGCS = EGCS.eGCSLeftHanded;
          //EGCS eGCS = EGCS.eGCSRightHanded;

          // Global coordinate system - axis
          ScreenSpaceLines3D sAxisX_3D = new ScreenSpaceLines3D();
          ScreenSpaceLines3D sAxisY_3D = new ScreenSpaceLines3D();
          ScreenSpaceLines3D sAxisZ_3D = new ScreenSpaceLines3D();
          Point3D pGCS_centre = new Point3D(0,0,0);
          Point3D pAxisX = new Point3D(1, 0, 0);
          Point3D pAxisY = new Point3D(0, 1, 0);
          Point3D pAxisZ = new Point3D(0, 0, 1);

          sAxisX_3D.Points.Add(pGCS_centre);
          sAxisX_3D.Points.Add(pAxisX);
          sAxisX_3D.Color = Colors.Red;
          sAxisX_3D.Thickness = 2;

          sAxisY_3D.Points.Add(pGCS_centre);
          sAxisY_3D.Points.Add(pAxisY);
          sAxisY_3D.Color = Colors.Green;
          sAxisY_3D.Thickness = 2;

          sAxisZ_3D.Points.Add(pGCS_centre);
          sAxisZ_3D.Points.Add(pAxisZ);
          sAxisZ_3D.Color = Colors.Blue;
          sAxisZ_3D.Thickness = 2;

          //I made ViewPort public property to Access ViewPort object inside TrackPort3D
          //to ViewPort add 3 children (3 axis)
          _trackport.ViewPort.Children.Add(sAxisX_3D);
          _trackport.ViewPort.Children.Add(sAxisY_3D);
          _trackport.ViewPort.Children.Add(sAxisZ_3D);
          
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

                  // Angle of rotation about local x-axis
                  cmodel.m_arrMembers[i].DTheta_x = 0; // Temporary

                  if (bDebugging)
                  {
                      System.Console.Write("\n" + "Member ID:" + (i + 1).ToString() + "\n"); // Write Member ID in console window
                      System.Console.Write("Start Node ID:" + cmodel.m_arrMembers[i].NodeStart.ID.ToString() + "\n"); // Write Start Node ID and coordinates in console window
                      System.Console.Write(mpA.X.ToString() + "\t" + mpA.Y.ToString() + "\t" + mpA.Z.ToString() + "\n");
                      System.Console.Write("End Node ID:" + cmodel.m_arrMembers[i].NodeEnd.ID.ToString() + "\n");     // Write   End Node ID and coordinates in console window
                      System.Console.Write(mpB.X.ToString() + "\t" + mpB.Y.ToString() + "\t" + mpB.Z.ToString() + "\n\n");
                  }

                  if(cmodel.m_arrMembers[i].CrScStart.CrScPointsOut != null) // CCrSc is is abstract without geometrical properties (dimensions), only centroid line could be displayed
                  {
                      // Member material color
                      byte R = (byte)(i / 2 == 0 ? 255 : 252);
                      byte G = (byte)(i / 2 == 0 ? 234 : 241);
                      byte B = (byte)(i / 2 == 0 ? 233 : 230);

                      SolidColorBrush br = new SolidColorBrush(Color.FromRgb(R,G,B)); // Material color

                      if (i <= 10)
                          br.Color = Colors.White;
                      else if (i <= 20)
                          br.Color = Colors.Red;
                      else if (i <= 30)
                          br.Color = Colors.LightGreen;
                      else if (i <= 40)
                          br.Color = Colors.White;
                      else if (i <= 50)
                          br.Color = Colors.Red;
                      else if (i <= 60)
                          br.Color = Colors.Green;
                      else if (i <= 70)
                          br.Color = Colors.LightSalmon;
                      else if (i <= 80)
                          br.Color = Colors.Red;
                      else if (i <= 90)
                          br.Color = Colors.Green;
                      else if (i <= 100)
                          br.Color = Colors.Red;
                      else if (i <= 110)
                          br.Color = Colors.GreenYellow;
                      else if (i <= 12)
                          br.Color = Colors.White;
                      else if (i <= 130)
                          br.Color = Colors.LightBlue;
                      else if (i <= 140)
                          br.Color = Colors.Green;
                      else if (i <= 150)
                          br.Color = Colors.Orange;
                      else if (i <= 160)
                          br.Color = Colors.Red;
                      else if (i <= 170)
                          br.Color = Colors.LightCyan;
                      else if (i <= 180)
                          br.Color = Colors.White;
                      else if (i <= 190)
                          br.Color = Colors.Yellow;
                      else if (i <= 200)
                          br.Color = Colors.LightCyan;
                      else
                          br.Color = Colors.Gold;

                      br.Opacity = 0.6; // Doesnt work :-/

                      // Create Member model
                      GeometryModel3D membermodel = getMemberGeometryModel3D(eGCS, br, cmodel.m_arrMembers[i].CrScStart, cmodel.m_arrMembers[i].CrScEnd, mpA, mpB, cmodel.m_arrMembers[i].DTheta_x);

                      // Add current member model to the model group
                      gr.Children.Add(membermodel);
                  }
              }
          }

          // Get model centre
          float fTempMax_X = float.MinValue;
          float fTempMin_X = float.MaxValue;
          float fTempMax_Y = float.MinValue;
          float fTempMin_Y = float.MaxValue;
          float fTempMax_Z = float.MinValue;
          float fTempMin_Z = float.MaxValue;

          for (int i = 0; i < cmodel.m_arrNodes.Length; i++)
          {
              // Maximum X - coordinate
              if (cmodel.m_arrNodes[i].FCoord_X > fTempMax_X)
                  fTempMax_X = cmodel.m_arrNodes[i].FCoord_X;

              // Minimum X - coordinate
              if (cmodel.m_arrNodes[i].FCoord_X < fTempMin_X)
                  fTempMin_X = cmodel.m_arrNodes[i].FCoord_X;

              // Maximum Y - coordinate
              if (cmodel.m_arrNodes[i].FCoord_Y > fTempMax_Y)
                  fTempMax_Y = cmodel.m_arrNodes[i].FCoord_Y;

              // Minimum Y - coordinate
              if (cmodel.m_arrNodes[i].FCoord_Y < fTempMin_Y)
                  fTempMin_Y = cmodel.m_arrNodes[i].FCoord_Y;

              // Maximum Z - coordinate
              if (cmodel.m_arrNodes[i].FCoord_Z > fTempMax_Z)
                  fTempMax_Z = cmodel.m_arrNodes[i].FCoord_Z;

              // Minimum Z - coordinate
              if (cmodel.m_arrNodes[i].FCoord_Z < fTempMin_Z)
                  fTempMin_Z = cmodel.m_arrNodes[i].FCoord_Z;
          }

          float fModel_Length_X = fTempMax_X - fTempMin_X;
          float fModel_Length_Y = fTempMax_Y - fTempMin_Y;
          float fModel_Length_Z = fTempMax_Z - fTempMin_Z;

          Point3D pModelGeomCentre = new Point3D(fModel_Length_X / 2.0f, fModel_Length_Y / 2.0f, fModel_Length_Z / 2.0f);

          Point3D cameraPosition = new Point3D(pModelGeomCentre.X, pModelGeomCentre.Y + 500, pModelGeomCentre.Z + 100);

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

          _trackport.PerspectiveCamera.LookDirection = new Vector3D(0, -1, -0.2);

          _trackport.Model = (Model3D)gr; //CreateRectangle(p3, p2, p6, p7, Brushes.Red);
          _trackport.SetupScene();
      }
    }

    private GeometryModel3D getMemberGeometryModel3D(EGCS eGCS, SolidColorBrush brush, CCrSc obj_CrScA, CCrSc obj_CrScB, Point3D mpA, Point3D mpB, double dTheta_x)
    {
      GeometryModel3D model = new GeometryModel3D();

      MeshGeometry3D mesh = getMeshMemberGeometry3DFromCrSc(eGCS, obj_CrScA, obj_CrScB, mpA, mpB, dTheta_x); // Mesh one member

      model.Geometry = mesh;

      model.Material = new DiffuseMaterial(brush);  // Set MemberModel Material

      return model;
    }

    private MeshGeometry3D getMeshMemberGeometry3DFromCrSc(EGCS eGCS, CCrSc obj_CrScA, CCrSc obj_CrScB, Point3D mpA, Point3D mpB, double dTheta_x)
    {
      MeshGeometry3D mesh = new MeshGeometry3D();
      mesh.Positions = new Point3DCollection();

      // Main Nodes of Member
      Point3D m_pA = mpA;
      Point3D m_pB = mpB;

      // Angle of rotation about local x-axis
      double m_dTheta_x = dTheta_x;

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
      if (obj_CrScA.IsShapeSolid) // Solid I,U,Z,HL,L, ..............
      {
          iNoCrScPoints2D = obj_CrScA.ITotNoPoints; // Depends on Section Type
        // Fill Mesh Positions for Start and End Section of Element - Defines Edge Points of Element

        if (obj_CrScA.CrScPointsOut != null) // Check that data are available
        {
          for (int j = 0; j < iNoCrScPoints2D; j++)
          {
              // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1]));
          }
          for (int j = 0; j < iNoCrScPoints2D; j++)
          {
              // X - end, Y, Z
              if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1])); // Constant size member
              else
                mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsOut[j, 0], obj_CrScB.CrScPointsOut[j, 1])); // Tapered member
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
        iNoCrScPoints2D = (short)(2 * obj_CrScA.INoPointsOut); // Twice number of one surface

        // Tube, regular hollow sections
        // TU

        // Start
        if (obj_CrScA.CrScPointsOut != null) // Check that data are available
        {
          // OutSide Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsOut; j++)
          {
            // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1]));
          }
        }
        else
        {
          // Exception
        }

        if (obj_CrScA.CrScPointsIn != null) // Check that data are available
        {
          // Inside Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsIn; j++)
          {
            // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0,obj_CrScA.CrScPointsIn[j, 0], obj_CrScA.CrScPointsIn[j, 1]));
          }
        }
        else
        {
          // Exception
        }

        // End
        if (obj_CrScA.CrScPointsOut != null) // Check that data are available
        {
          // OutSide Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsOut; j++)
          {
              // X - end, Y, Z
              if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1])); // Constant size member
              else
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsOut[j, 0], obj_CrScB.CrScPointsOut[j, 1])); // Tapered member
          }
        }
        else
        {
          // Exception
        }

        if (obj_CrScA.CrScPointsIn != null) // Check that data are available
        {
          // Inside Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsIn; j++)
          {
              // X - end, Y, Z
              if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsIn[j, 0], obj_CrScA.CrScPointsIn[j, 1])); // Constant size member
              else
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsIn[j, 0], obj_CrScB.CrScPointsIn[j, 1])); // Tapered member
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

        if(bDebugging)
            System.Console.Write(sOutput); // Write in console window


      // Transform coordinates
      TransformMember_LCStoGCS(eGCS, m_pA, m_pB, m_dDelta_X, m_dDelta_Y, m_dDelta_Z, m_dTheta_x, mesh.Positions);

      // Mesh Triangles - various cross-sections shapes defined
      mesh.TriangleIndices = obj_CrScA.TriangleIndices;


     // Dislay data in the output window

      sOutput = null;
      sOutput = "After transformation \n\n"; // create temporary string

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

        if(bDebugging)
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

    public GeometryModel3D CreateRectangle(Point3D point1, Point3D point2, Point3D point3, Point3D point4, Brush brush)
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

    public Point3DCollection TransformMember_LCStoGCS(EGCS eGCS, Point3D pA, Point3D pB, double dDeltaX, double dDeltaY, double dDeltaZ, double dTheta_x, Point3DCollection pointsCollection)
    {
      // Returns transformed coordinates of member nodes

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

      if (bDebugging)
      {
          // Temporary console output
          System.Console.Write("\n" + "Lengths - projection of element into global coordinate system:\n");
          System.Console.Write("Length - global X-axis:\t" + dDeltaX.ToString("0.000") + "\n"); // Write length in X-axis
          System.Console.Write("Length - global Y-axis:\t" + dDeltaY.ToString("0.000") + "\n"); // Write length in Y-axis
          System.Console.Write("Length - global Z-axis:\t" + dDeltaZ.ToString("0.000") + "\n\n"); // Write length in Z-axis
      }
      // Uhly pootocenia LCS okolo osi GCS
      // Angles
      dAlphaX = Geom2D.GetAlpha2D_CW(dDeltaY, dDeltaZ);
      dBetaY = Geom2D.GetAlpha2D_CW_2(dDeltaX, dDeltaZ); // !!! Pre pootocenie okolo Y su pouzite ine kvadranty !!!
      dGammaZ = Geom2D.GetAlpha2D_CW(dDeltaX, dDeltaY);

      // Auxialiary angles for members graphics
      double dBetaY_aux = Geom2D.GetAlpha2D_CW_3(dDeltaX, dDeltaZ, Math.Sqrt(Math.Pow(dLength_XY,2) + Math.Pow(dDeltaZ,2)));
      double dGammaZ_aux = dGammaZ;
        if(Math.PI / 2 < dBetaY && dBetaY < 1.5*Math.PI)
        { 
          if(dGammaZ < Math.PI)
            dGammaZ_aux = dGammaZ + Math.PI;
          else
            dGammaZ_aux = dGammaZ - Math.PI;
        }

        if (bDebugging)
        {
            // Temporary console output
            System.Console.Write("\n" + "Rotation angles:\n");
            System.Console.Write("Rotation about global X-axis:\t" + dAlphaX.ToString("0.000") + "rad\t " + (dAlphaX * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about X-axis
            System.Console.Write("Rotation about global Y-axis:\t" + dBetaY.ToString("0.000") + "rad\t " + (dBetaY * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about Y-axis
            System.Console.Write("Rotation about global Z-axis:\t" + dGammaZ.ToString("0.000") + "rad\t " + (dGammaZ * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about Z-axis
            System.Console.Write("\n" + "Auxiliary rotation angles - graphics:\n");
            System.Console.Write("Rotation about global Y-axis:\t" + dBetaY_aux.ToString("0.000") + "rad\t " + (dBetaY_aux * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write auxiliary rotation about Y-axis
            System.Console.Write("Rotation about global Z-axis:\t" + dGammaZ_aux.ToString("0.000") + "rad\t " + (dGammaZ_aux * 180.0f / MathF.fPI).ToString("0.0") + "deg \n\n"); // Write auxiliary rotation about Z-axis
        }

      for (int i = 0; i < pointsCollection.Count; i++)
      {
          pointsCollection[i] = RotatePoint(eGCS, pA, pointsCollection[i], dBetaY_aux, dGammaZ_aux, dTheta_x, dDeltaX, dDeltaY, dDeltaZ);
      }

      return pointsCollection;
    }

    protected Point3D RotatePoint(EGCS eGCS, Point3D pA, Point3D p, double betaY, double gamaZ, double theta_x, double dDeltaX, double dDeltaY, double dDeltaZ)
    {
        Point3D p3Drotated = new Point3D();

        //http://sk.wikipedia.org/wiki/Trojrozmern%C3%A1_projekcia#D.C3.A1ta_nevyhnutn.C3.A9_pre_projekciu

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

      Point3D pTemp1 = new Point3D();
      Point3D pTemp2 = new Point3D();

      if (eGCS == EGCS.eGCSLeftHanded)
      {
          // Left-handed

          // Rotate about Y-axis
          pTemp1.X = (Math.Cos(betaY) * p.X) + (Math.Sin(betaY) * p.Z);
          pTemp1.Y = p.Y;
          pTemp1.Z = (-Math.Sin(betaY) * p.X) + (Math.Cos(betaY) * p.Z);

          // Rotate about Z-axis
          pTemp2.X = (Math.Cos(gamaZ) * pTemp1.X) - (Math.Sin(gamaZ) * pTemp1.Y);
          pTemp2.Y = (Math.Sin(gamaZ) * pTemp1.X) + (Math.Cos(gamaZ) * pTemp1.Y);
          pTemp2.Z = pTemp1.Z;

          // Translate
          pTemp1.X = pA.X + pTemp2.X;
          pTemp1.Y = pA.Y + pTemp2.Y;
          pTemp1.Z = pA.Z + pTemp2.Z;

          // Set output point data
          p3Drotated.X = pTemp1.X;
          p3Drotated.Y = pTemp1.Y;
          p3Drotated.Z = pTemp1.Z;

          // Rotate about local x-axis
          if (!MathF.d_equal(theta_x, 0.0))
          {
              double c = Math.Cos(theta_x);
              double s = Math.Sin(theta_x);
              double t = 1 - c;

              p3Drotated.X = (t * Math.Pow(dDeltaX, 2) + c) * pTemp1.X + (t * dDeltaX * dDeltaY - s * dDeltaZ) * pTemp1.Y + (t * dDeltaX * dDeltaZ + s * dDeltaY) * pTemp1.Z;
              p3Drotated.Y = (t * dDeltaX * dDeltaY + s * dDeltaZ) * pTemp1.X + (t * Math.Pow(dDeltaY, 2) + c) * pTemp1.Y + (t * dDeltaY * dDeltaZ - s * dDeltaX) * pTemp1.Z;
              p3Drotated.Z = (t * dDeltaX * dDeltaZ - s * dDeltaY) * pTemp1.X + (t * dDeltaY * dDeltaZ + s * dDeltaX) * pTemp1.Y + (t * Math.Pow(dDeltaZ, 2) + c) * pTemp1.Z;
          }
      }
      else
      {
          // Right-handed

          // Rotate about Y-axis
          pTemp1.X = (Math.Cos(betaY) * p.X) - (Math.Sin(betaY) * p.Z);
          pTemp1.Y = p.Y;
          pTemp1.Z = (Math.Sin(betaY) * p.X) + (Math.Cos(betaY) * p.Z);

          // Rotate about Z-axis
          pTemp2.X = (Math.Cos(gamaZ) * pTemp1.X) + (Math.Sin(gamaZ) * pTemp1.Y);
          pTemp2.Y = (-Math.Sin(gamaZ) * pTemp1.X) + (Math.Cos(gamaZ) * pTemp1.Y);
          pTemp2.Z = pTemp1.Z;

          // Translate
          pTemp1.X = pA.X + pTemp2.X;
          pTemp1.Y = pA.Y + pTemp2.Y;
          pTemp1.Z = pA.Z + pTemp2.Z;

          // Set output point data
          p3Drotated.X = pTemp1.X;
          p3Drotated.Y = pTemp1.Y;
          p3Drotated.Z = pTemp1.Z;

          // Rotate about local x-axis
          if (!MathF.d_equal(theta_x, 0.0))
          {
              double c = Math.Cos(theta_x);
              double s = Math.Sin(theta_x);
              double t = 1 - c;

              p3Drotated.X = (t * Math.Pow(dDeltaX, 2) + c) * pTemp1.X + (t * dDeltaX * dDeltaY + s * dDeltaZ) * pTemp1.Y + (t * dDeltaX * dDeltaZ - s * dDeltaY) * pTemp1.Z;
              p3Drotated.Y = (t * dDeltaX * dDeltaY - s * dDeltaZ) * pTemp1.X + (t * Math.Pow(dDeltaY, 2) + c) * pTemp1.Y + (t * dDeltaY * dDeltaZ + s * dDeltaX) * pTemp1.Z;
              p3Drotated.Z = (t * dDeltaX * dDeltaZ + s * dDeltaY) * pTemp1.X + (t * dDeltaY * dDeltaZ - s * dDeltaX) * pTemp1.Y + (t * Math.Pow(dDeltaZ, 2) + c) * pTemp1.Z;
          }

      }

        return p3Drotated;
    }

    protected Point3D RotatePoint_POKUSY(EGCS eGCS, Point3D pA, Point3D p, double alphaX, double betaY, double gamaZ, double dDeltaX, double dDeltaY, double dDeltaZ)
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
      //http://sk.wikipedia.org/wiki/Trojrozmern%C3%A1_projekcia#D.C3.A1ta_nevyhnutn.C3.A9_pre_projekciu
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

      if (eGCS == EGCS.eGCSLeftHanded)
      {
          // Left handed

          pTemp1.X = p.X;
          pTemp1.Y = (Math.Cos(alphaX) * p.Y) - (Math.Sin(alphaX) * p.Z);
          pTemp1.Z = (Math.Sin(alphaX) * p.Y) + (Math.Cos(alphaX) * p.Z);

          pTemp2.X = (Math.Cos(betaY) * pTemp1.X) + (Math.Sin(betaY) * pTemp1.Z);
          pTemp2.Y = pTemp1.Y;
          pTemp2.Z = (-Math.Sin(betaY) * pTemp1.X) + (Math.Cos(betaY) * pTemp1.Z);

          p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * pTemp2.X) - (Math.Sin(gamaZ) * pTemp2.Y));
          p3Drotated.Y = pA.Y + ((Math.Sin(gamaZ) * pTemp2.X) + (Math.Cos(gamaZ) * pTemp2.Y));
          p3Drotated.Z = pA.Z + pTemp2.Z;
      }
      else
      {
          // Right handed

          pTemp1.X = p.X;
          pTemp1.Y = (Math.Cos(alphaX) * p.Y) + (Math.Sin(alphaX) * p.Z);
          pTemp1.Z = (-Math.Sin(alphaX) * p.Y) + (Math.Cos(alphaX) * p.Z);

          pTemp2.X = (Math.Cos(betaY) * pTemp1.X) - (Math.Sin(betaY) * pTemp1.Z);
          pTemp2.Y = pTemp1.Y;
          pTemp2.Z = (Math.Sin(betaY) * pTemp1.X) + (Math.Cos(betaY) * pTemp1.Z);

          p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * pTemp2.X) + (Math.Sin(gamaZ) * pTemp2.Y));
          p3Drotated.Y = pA.Y + ((-Math.Sin(gamaZ) * pTemp2.X) + (Math.Cos(gamaZ) * pTemp2.Y));
          p3Drotated.Z = pA.Z + pTemp2.Z;
      }
      */

      // In case that member is parallel to global axis should be rotated only once
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

      // Cumulative 3D rotation and translation
      // Temp - pokus 1
      // Rotate around x, y, z
      double ax = Math.Cos(betaY) * Math.Cos(gamaZ);
      double ay = (Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ));
      double az = (Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ));

      p3Drotated.X = ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + ((Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.Y + ((Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.Z);

      double bx = Math.Cos(betaY) * Math.Sin(gamaZ);
      double by = (Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ));
      double bz = (-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ));

      p3Drotated.Y = ((Math.Cos(betaY) * Math.Sin(gamaZ)) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + ((-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Z);

      double cx = -Math.Sin(betaY);
      double cy = Math.Cos(betaY) * Math.Sin(alphaX);
      double cz = Math.Cos(alphaX) * Math.Cos(betaY);

      p3Drotated.Z = ((-Math.Sin(betaY)) * p.X + (Math.Cos(betaY) * Math.Sin(alphaX)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);


      // Temp - pokus 2

      if (gamaZ < Math.PI)
          gamaZ += Math.PI;
      else if(gamaZ > Math.PI)
          gamaZ -= Math.PI;


      // X Rotation
      ax = 1 * p.X + 0 * p.Y + 0 * p.Z;
      ay = 0 * p.X + Math.Cos(alphaX) * p.Y - Math.Sin(alphaX) * p.Z;
      az = Math.Sin(alphaX) * p.X + Math.Cos(alphaX) * p.Y + 0 * p.Z;

      // Y Rotation
      bx = Math.Cos(betaY) * ax + 0 * ay + Math.Sin(betaY) * az;
      by = 0 * ax + 1 * ay + 0 * az;
      bz = -Math.Sin(betaY) * ax + 0 * ay + Math.Cos(betaY) * az;

      // Z Rotation
      cx = Math.Cos(gamaZ) * bx - Math.Sin(gamaZ) * by + 0 * bz;
      cy = Math.Sin(gamaZ) * bx + Math.Cos(gamaZ) * by + 0 * bz;
      cz = 0 * bx + 0 * by + 1 * bz;

      p3Drotated.X = pA.X + cx;
      p3Drotated.Y = pA.Y + cy;
      p3Drotated.Z = pA.Z + cz;

      p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + ((Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.Y + ((Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.Z);
      p3Drotated.Y = pA.Y + ((Math.Cos(betaY) * Math.Sin(gamaZ)) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + ((-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Z);
      p3Drotated.Z = pA.Z + ((-Math.Sin(betaY)) * p.X + (Math.Cos(betaY) * Math.Sin(alphaX)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);

      // Rotate around z, y, x
      /*
      p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + (-Math.Cos(betaY) * Math.Sin(gamaZ)) * p.Y + (Math.Sin(betaY)) * p.Z);
      p3Drotated.Y = pA.Y + (((Math.Cos(alphaX) * Math.Sin(gamaZ)) + (Math.Sin(alphaX)*Math.Sin(betaY) * Math.Cos(gamaZ))) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) - (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + (-Math.Sin(alphaX) * Math.Cos(betaY)) * p.Z);
      p3Drotated.Z = pA.Z + (((Math.Sin(alphaX) * Math.Sin(gamaZ)) - (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Cos(gamaZ))) * p.X + ((Math.Sin(alphaX) * Math.Cos(gamaZ)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin (gamaZ))) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);
      */

      // Right-handed cumulative ???
      // http://scipp.ucsc.edu/~haber/ph216/rotation_12.pdf
      /*
      p3Drotated.X = pA.X + (((Math.Cos(alphaX) * Math.Cos(betaY) * Math.Cos(gamaZ)) - (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.X + (-Math.Cos(alphaX) * Math.Cos(betaY) * Math.Sin(gamaZ) - (Math.Sin(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Cos(alphaX) * Math.Sin(betaY)) * p.Z);
      p3Drotated.Y = pA.Y + (((Math.Sin(alphaX) * Math.Cos(betaY) * Math.Cos(gamaZ)) + (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.X + (-Math.Sin(alphaX) * Math.Cos(betaY) * Math.Sin(gamaZ) + (Math.Cos(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Sin(alphaX) * Math.Sin(betaY)) * p.Z);
      p3Drotated.Z = pA.Z + ((-Math.Sin(betaY) * Math.Cos(gamaZ)) * p.X + (Math.Sin(betaY) * Math.Sin(gamaZ)) * p.Y + (Math.Cos(betaY)) * p.Z);
      */

      //
      // http://inside.mines.edu/~gmurray/ArbitraryAxisRotation/
      /*
      p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * Math.Cos(betaY)) * p.X + (-Math.Sin(gamaZ) * Math.Cos(betaY)) * p.Y + Math.Sin(betaY) * p.Z);
      p3Drotated.Y = pA.Y + (((Math.Cos(gamaZ) * Math.Sin(betaY) * Math.Sin(alphaX)) + (Math.Sin(gamaZ) * Math.Cos(alphaX))) * p.X + ((Math.Cos(gamaZ) * Math.Cos(alphaX)) - (Math.Sin(gamaZ) * Math.Sin(betaY) * Math.Sin(alphaX))) * p.Y + (-Math.Cos(betaY) * Math.Sin(alphaX)) * p.Z);
      p3Drotated.Z = pA.Z + (((Math.Sin(gamaZ) * Math.Sin(alphaX)) - (Math.Cos(gamaZ) * Math.Sin(betaY) * Math.Cos(alphaX))) * p.X + ((Math.Sin(gamaZ) * Math.Sin(betaY) * Math.Cos(alphaX)) + (Math.Sin(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Cos(betaY) * Math.Cos(alphaX)) * p.Z);
      */


      return p3Drotated;


        // Mozno by som mal zapracovat toto
        //http://mathworld.wolfram.com/EulerAngles.html
    }
  }
}
