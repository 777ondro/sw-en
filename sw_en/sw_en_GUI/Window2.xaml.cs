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
        short sShapeType = 0; // 0 - solid, 1 - hollow

        // Collection of Indices for Various Sections
        private Int32Collection M_TriangelsIndices;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Auxialiry Functions
        // Draw Rectangle
        private void DrawRectangleIndices(Int32Collection Indices,
              int point1, int point2,
              int point3, int point4)
        {
           // Main Numbering is clockwise

            // 1  _______  2
            //   |_______| 
            // 4           3

            // Triangles Numbering is Counterclockwise
            // Top Right
            Indices.Add(point1);
            Indices.Add(point3);
            Indices.Add(point2);

            // Bottom Left
            Indices.Add(point1);
            Indices.Add(point4);
            Indices.Add(point3);
        }

        // Draw Prism CaraLaterals
        // Kresli plast hranola pre kontinualne pravidelne cislovanie bodov
        private void DrawCaraLaterals(int secNum, Int32Collection TriangelsIndices)
        {
            // secNum - number of one base edges / - pocet rohov - hranicnych bodov jednej podstavy

            // Shell (Face)Surface
            // Cycle for regular numbering of section points

            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(TriangelsIndices, i, secNum + i, secNum + i + 1, i + 1);
                else
                    DrawRectangleIndices(TriangelsIndices, i, secNum + i, secNum, 0); // Last Element
            }
        }

        // Draw Prism CaraLaterals
        // Kresli plast hranola pre pravidelne cislovanie bodov s vynechanim pociatocnych uzlov - pomocne 
        private void DrawCaraLaterals(int iAuxNum, int secNum, Int32Collection TriangelsIndices)
        {
            // iAuxNum - number of auxialiary points - start ofset
            // secNum - number of one base edges / - pocet rohov - hranicnych bodov jednej podstavy

            // Shell (Face)Surface
            // Cycle for regular numbering of section points

            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(TriangelsIndices, iAuxNum + i, 2*iAuxNum + secNum + i, 2*iAuxNum + secNum + i + 1, iAuxNum + i + 1);
                else
                    DrawRectangleIndices(TriangelsIndices, iAuxNum + i, 2*iAuxNum + secNum + i, 2*iAuxNum + secNum, iAuxNum + 0); // Last Element
            }
        }

        // Draw Sector of Solid Circle
        // Kresli vyrez kruhu,
        // Parametre:
        // pocet pomocnych uzlov;  ID stredu vyrezu; ID  prvy bod obluka; pocet segmentov (trojuholnikov); kolekcia, do ktorej sa zapisuju trojice, vzostupne cislovanie CW
        private void DrawSolidCircleSector(int iCentrePointID, int iArcFirstPointID, int iRadiusSegment, Int32Collection TriangelsIndices, bool bAscendingNumCW)
        {
            for (int i = 0; i < iRadiusSegment; i++)
            {
                TriangelsIndices.Add(iCentrePointID); // Centre point
                if (!bAscendingNumCW) // Clock-wise
                {
                    TriangelsIndices.Add(iArcFirstPointID + 1 + i);
                    TriangelsIndices.Add(iArcFirstPointID + i);
                }
                else // Counter Clock-wise
                {
                    TriangelsIndices.Add(iArcFirstPointID + i);
                    TriangelsIndices.Add(iArcFirstPointID + 1 + i);
                }
            }
        }
        
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                DrawRectangleIndices(M_TriangelsIndices, i, secNum + i, secNum + i + 1, i + 1);

            // Flat Sides - !!! Clock-wise points of arc generation
            DrawRectangleIndices(M_TriangelsIndices, 0, secNum - 1, 2*secNum - 1,secNum);
            DrawRectangleIndices(M_TriangelsIndices, secNum-1, secNum - 2, 2*secNum - 2, 2*secNum - 1);

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
                    DrawRectangleIndices(M_TriangelsIndices, i, secNum + i, secNum + i + 1, i + 1);
                else
                    DrawRectangleIndices(M_TriangelsIndices, i, secNum + i, secNum, 0); // Last Element
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
            DrawRectangleIndices(M_TriangelsIndices, 0, 3, 4, 1);
            DrawRectangleIndices(M_TriangelsIndices, 1, 4, 5, 2);
            DrawRectangleIndices(M_TriangelsIndices, 2, 5, 3, 0);
        }

        private void load_0_05_TriangelIndices()
        {
            // const int secNum = 4;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 2, 3);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 4, 7, 6, 5);

            // Shell Surface
            DrawRectangleIndices(M_TriangelsIndices, 0, 4, 5, 1);
            DrawRectangleIndices(M_TriangelsIndices, 1, 5, 6, 2);
            DrawRectangleIndices(M_TriangelsIndices, 2, 6, 7, 3);
            DrawRectangleIndices(M_TriangelsIndices, 3, 7, 4, 0);
        }

        private void load_0_20_TriangelIndices()
        {
            const int secNum = 37;  // Number of points in section (2D) 36+1 -centroid
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead

            for (int i = 0; i < secNum-1; i++)
            {
                if (i < secNum - 2)
                    DrawRectangleIndices(M_TriangelsIndices, i, i + 1, i + secNum + 1, i + secNum);
                else
                    DrawRectangleIndices(M_TriangelsIndices, i, 0, i + 1, i + secNum); // Last Element
            }

            // Back Side
            for (int i = 0; i < secNum-1; i++)
            {
                if (i < secNum - 2)
                    DrawRectangleIndices(M_TriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + secNum + 1, 2 * secNum + i + 1);
                else
                    DrawRectangleIndices(M_TriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + 1, 2 * secNum + 0); // Last Element
            }

            //// Shell Surface OutSide
            for (int i = 0; i < secNum - 2; i++)
            {
                DrawRectangleIndices(M_TriangelsIndices, i, 2 * secNum + i, 2 * secNum + i + 1, i + 1);
            }
            // Shell Surface Inside
            for (int i = 0; i < secNum - 2; i++)
            {
                DrawRectangleIndices(M_TriangelsIndices, secNum + i, secNum + i + 1, 2 * secNum + i + secNum + 1, 2 * secNum + i + secNum);
            }

            // Base
            DrawRectangleIndices(M_TriangelsIndices, 0, secNum, 3 * secNum, 2 * secNum);
            DrawRectangleIndices(M_TriangelsIndices, secNum - 2,  3 * secNum - 2, 4 * secNum-2, secNum + (secNum - 2));
        }

        private void load_0_22_23_TriangelIndices()
        {
            // const int secNum = 2*36;  // Number of points in section (2D)
            const int secNum = 72; // Number of points in one circle of section (2D) /Number of points is equal to Number of cells par section

            load_0_26_28_TriangelIndices(secNum);
        }

        private void load_0_24_TriangelsIndices()
        {
            // const int secNum = 6;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 4, 3);
            DrawRectangleIndices(M_TriangelsIndices, 4, 1, 2, 5);
            DrawRectangleIndices(M_TriangelsIndices, 0, 3, 5, 2);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 6, 8, 11, 9);
            DrawRectangleIndices(M_TriangelsIndices, 10, 11, 8, 7);
            DrawRectangleIndices(M_TriangelsIndices, 6, 9, 10, 7);

            // Shell Surface
            DrawRectangleIndices(M_TriangelsIndices, 0, 6, 7, 1);
            DrawRectangleIndices(M_TriangelsIndices, 1, 7, 8, 2);
            DrawRectangleIndices(M_TriangelsIndices, 2, 8, 6, 0);

            DrawRectangleIndices(M_TriangelsIndices, 3, 4, 10, 9);
            DrawRectangleIndices(M_TriangelsIndices, 4, 5, 11, 10);
            DrawRectangleIndices(M_TriangelsIndices, 4, 9, 11, 5);
        }

        private void load_0_25_TriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 5, 4);
            DrawRectangleIndices(M_TriangelsIndices, 1, 2, 6, 5);
            DrawRectangleIndices(M_TriangelsIndices, 7, 6, 2, 3);
            DrawRectangleIndices(M_TriangelsIndices, 0, 4, 7, 3);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 9, 8, 12, 13);
            DrawRectangleIndices(M_TriangelsIndices, 9, 13, 14, 10);
            DrawRectangleIndices(M_TriangelsIndices, 14, 15, 11, 10);
            DrawRectangleIndices(M_TriangelsIndices, 8, 11, 15, 12);

            // Shell Surface
            // Outside
            DrawRectangleIndices(M_TriangelsIndices, 0, 8, 9, 1);
            DrawRectangleIndices(M_TriangelsIndices, 1, 9, 10, 2);
            DrawRectangleIndices(M_TriangelsIndices, 2, 10, 11, 3);
            DrawRectangleIndices(M_TriangelsIndices, 8, 0, 3, 11);
            // Inside
            DrawRectangleIndices(M_TriangelsIndices, 13, 5, 6, 14);
            DrawRectangleIndices(M_TriangelsIndices, 7, 15, 14, 6);
            DrawRectangleIndices(M_TriangelsIndices, 4, 12, 15, 7);
            DrawRectangleIndices(M_TriangelsIndices, 12, 4, 5, 13);
        }

        private void load_0_26_28_TriangelIndices(int secNum)
        {
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead

            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(M_TriangelsIndices, i, i + 1, i + secNum + 1, i + secNum);
                else
                    DrawRectangleIndices(M_TriangelsIndices, i, 0, i + 1, i + secNum); // Last Element
            }

            // Back Side
            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(M_TriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + secNum + 1, 2 * secNum + i + 1);
                else
                    DrawRectangleIndices(M_TriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + 1, 2 * secNum + 0); // Last Element
            }

            // Shell Surface OutSide
            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(M_TriangelsIndices, i, 2 * secNum + i, 2 * secNum + i + 1, i + 1);
                else
                    DrawRectangleIndices(M_TriangelsIndices, i, 2 * secNum + i, 2 * secNum, 0); // Last Element
            }

            // Shell Surface Inside
            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(M_TriangelsIndices, secNum + i, secNum + i + 1, 2 * secNum + i + secNum + 1, 2 * secNum + i + secNum);
                else
                    DrawRectangleIndices(M_TriangelsIndices, 2 * secNum + i + 1, 2 * secNum + i + secNum, secNum + i, secNum); // Last Element
            }
        }

        private void load_0_50_TriangelIndices()
        {
            // const int secNum = 12;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 2, 11);
            DrawRectangleIndices(M_TriangelsIndices, 10, 3, 4, 9);
            DrawRectangleIndices(M_TriangelsIndices, 8, 5, 6, 7);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 13, 12, 23, 14);
            DrawRectangleIndices(M_TriangelsIndices, 15, 22, 21, 16);
            DrawRectangleIndices(M_TriangelsIndices, 17, 20, 19, 18);

            // Shell Surface 
            DrawCaraLaterals(12, M_TriangelsIndices);
        }

        private void load_0_52_TriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 2, 3);
            DrawRectangleIndices(M_TriangelsIndices, 0, 3, 4, 7);
            DrawRectangleIndices(M_TriangelsIndices, 4, 5, 6, 7);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 9, 8, 11, 10);
            DrawRectangleIndices(M_TriangelsIndices, 11, 8, 15, 12);
            DrawRectangleIndices(M_TriangelsIndices, 13, 12, 15, 14);

            // Shell Surface 
            DrawCaraLaterals(8, M_TriangelsIndices);
        }

        private void load_0_54_TriangelIndices()
        {
            // const int secNum = 6;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 2, 5);
            DrawRectangleIndices(M_TriangelsIndices, 2, 3, 4, 5);
            
            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 7, 6, 11, 8);
            DrawRectangleIndices(M_TriangelsIndices, 9, 8, 11, 10);

            // Shell Surface
            DrawCaraLaterals(6, M_TriangelsIndices);
        }

        private void load_0_56_TriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 2, 7);
            DrawRectangleIndices(M_TriangelsIndices, 6, 3, 4, 5);
            
            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 9, 8, 15, 10);
            DrawRectangleIndices(M_TriangelsIndices, 11, 14, 13, 12);

            // Shell Surface
            DrawCaraLaterals(8, M_TriangelsIndices);
        }

        private void load_0_58_TriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 6, 7);
            DrawRectangleIndices(M_TriangelsIndices, 6, 1, 2, 5);
            DrawRectangleIndices(M_TriangelsIndices, 2, 3, 4, 5);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 9, 8, 15, 14);
            DrawRectangleIndices(M_TriangelsIndices, 9, 14, 13, 10);
            DrawRectangleIndices(M_TriangelsIndices, 11, 10, 13, 12);

            // Shell Surface
            DrawCaraLaterals(8, M_TriangelsIndices);
        }

        private void load_0_60_TriangelIndices(int secNum)
        {
            // const int secNum = 12;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 2, 11);
            DrawRectangleIndices(M_TriangelsIndices, 2, 3, 4, 5);
            DrawRectangleIndices(M_TriangelsIndices, 5, 6, 7, 8);
            DrawRectangleIndices(M_TriangelsIndices, 8, 9, 10, 11);
            DrawRectangleIndices(M_TriangelsIndices, 2, 5, 8, 11);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 23, 14, 13, 12);
            DrawRectangleIndices(M_TriangelsIndices, 17, 16, 15, 14);
            DrawRectangleIndices(M_TriangelsIndices, 20, 19, 18, 17);
            DrawRectangleIndices(M_TriangelsIndices, 23, 22, 21, 20);
            DrawRectangleIndices(M_TriangelsIndices, 23, 20, 17, 14);

            // Shell
            DrawCaraLaterals(12, M_TriangelsIndices);
        }

        private void load_0_61_TriangelIndices()
        {
            // const int secNum = 9;  // Number of points in section (2D)
            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, 7, 8);
            DrawRectangleIndices(M_TriangelsIndices, 1, 2, 3, 4);
            DrawRectangleIndices(M_TriangelsIndices, 4, 5, 6, 7);
            // Front Side / Forehead
            M_TriangelsIndices.Add(1);
            M_TriangelsIndices.Add(7);
            M_TriangelsIndices.Add(4);

            // Back Side 
            DrawRectangleIndices(M_TriangelsIndices, 17, 16, 10, 9);
            DrawRectangleIndices(M_TriangelsIndices, 13, 12, 11, 10);
            DrawRectangleIndices(M_TriangelsIndices, 16, 15, 14, 13);
            // Back Side 
            M_TriangelsIndices.Add(10);
            M_TriangelsIndices.Add(13);
            M_TriangelsIndices.Add(16);

            // Shell
            DrawCaraLaterals(9, M_TriangelsIndices);
        }

        private void load_3_00_TriangelIndices(int iAux, int iRadiusSegment)
        {
            // const int secNum = iAux + iRadiusPoints * 8;  // Number of points in section (2D)
            int iRadiusPoints = iRadiusSegment + 1;

            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TriangelsIndices, 0, 1, iAux + 7 + 7 * iRadiusSegment, iAux + 8 + 7 * iRadiusSegment);
            DrawRectangleIndices(M_TriangelsIndices, 1, 2, iAux + 2 + iRadiusSegment, iAux + 7 + 7 * iRadiusSegment);
            DrawRectangleIndices(M_TriangelsIndices, 2, 3, iAux + 1 + iRadiusSegment, iAux + 2 + iRadiusSegment);

            DrawRectangleIndices(M_TriangelsIndices, 4, 5, 10, 11);

            DrawRectangleIndices(M_TriangelsIndices, 6, 7, iAux + 3 + 3 * iRadiusSegment, iAux + 4 + 3 * iRadiusSegment);
            DrawRectangleIndices(M_TriangelsIndices, 7, 8, iAux + 6 + 5 * iRadiusSegment, iAux + 3 + 3 * iRadiusSegment);
            DrawRectangleIndices(M_TriangelsIndices, 8, 9, iAux + 5 + 5 * iRadiusSegment, iAux + 6 + 5 * iRadiusSegment);

            // Arc sectors
            // 1st SolidCircleSector
            DrawSolidCircleSector(3, iAux + 1, iRadiusSegment, M_TriangelsIndices, false);
            // 2nd SolidCircleSector
            DrawSolidCircleSector(4, iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 3rd SolidCircleSector
            DrawSolidCircleSector(5, iAux + 1 + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 4th SolidCircleSector
            DrawSolidCircleSector(6, iAux + 1 + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 5th SolidCircleSector
            DrawSolidCircleSector(9, iAux + 1 + 4 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 6th SolidCircleSector
            DrawSolidCircleSector(10, iAux + 1 + 5 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 7th SolidCircleSector
            DrawSolidCircleSector(11, iAux + 1 + 6 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 8th SolidCircleSector
            DrawSolidCircleSector(0, iAux + 1 + 7 * iRadiusPoints, iRadiusSegment - 1, M_TriangelsIndices, false); // Segments number = iRadiusSegment-1

            // Last Triangle 
            M_TriangelsIndices.Add(0); // 1st Point
            M_TriangelsIndices.Add(iAux); // 1st Point of Radii (1st after auxiliary)
            M_TriangelsIndices.Add(iAux + 8 * iRadiusPoints - 1); // Last Point


            // Back Side 

            // Arc sectors
            int iPointNumbersOffset = iAux + 8 * iRadiusPoints; // Number of nodes per section - Nodes offset

            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 8 + 7 * iRadiusSegment, iPointNumbersOffset + iAux + 7 + 7 * iRadiusSegment, iPointNumbersOffset + 1);
            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 7 + 7 * iRadiusSegment, iPointNumbersOffset + iAux + 2 + iRadiusSegment, iPointNumbersOffset + 2);
            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 + iRadiusSegment, iPointNumbersOffset + iAux + 1 + iRadiusSegment, iPointNumbersOffset + 3);

            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 4, iPointNumbersOffset + 11, iPointNumbersOffset + 10, iPointNumbersOffset + 5);

            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 6, iPointNumbersOffset + iAux + 4 + 3 * iRadiusSegment, iPointNumbersOffset + iAux + 3 + 3 * iRadiusSegment, iPointNumbersOffset + 7);
            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 7, iPointNumbersOffset + iAux + 3 + 3 * iRadiusSegment, iPointNumbersOffset + iAux + 6 + 5 * iRadiusSegment, iPointNumbersOffset + 8);
            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 8, iPointNumbersOffset + iAux + 6 + 5 * iRadiusSegment, iPointNumbersOffset + iAux + 5 + 5 * iRadiusSegment, iPointNumbersOffset + 9);

            // 1st SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1, iRadiusSegment, M_TriangelsIndices, true);
            // 2nd SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 4, iPointNumbersOffset + iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 3rd SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 5, iPointNumbersOffset + iAux + 1 + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 4th SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 6, iPointNumbersOffset + iAux + 1 + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 5th SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 9, iPointNumbersOffset + iAux + 1 + 4 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 6th SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 10, iPointNumbersOffset + iAux + 1 + 5 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 7th SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 11, iPointNumbersOffset + iAux + 1 + 6 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 8th SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 1 + 7 * iRadiusPoints, iRadiusSegment - 1, M_TriangelsIndices, true); // Segments number = iRadiusSegment-1

            // Last Triangle 
            M_TriangelsIndices.Add(iPointNumbersOffset + 0); // 1st Point
            M_TriangelsIndices.Add(iPointNumbersOffset + iAux + 8 * iRadiusPoints - 1); // Last Point
            M_TriangelsIndices.Add(iPointNumbersOffset + iAux); // 1st Point of Radii (1st after auxiliary)


           // Shell
            DrawCaraLaterals(iAux, 8 * iRadiusPoints, M_TriangelsIndices);
        }

        private void load_3_02_TriangelIndices(int iAux, int iRadiusSegment)
        {
            // const int secNum = iAux + iRadiusPoints * 4;  // Number of points in section (2D)
            int iRadiusPoints = iRadiusSegment + 1;

            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            // Points order 1,2,3,4

            DrawRectangleIndices(M_TriangelsIndices, 0, 1, iAux + 1 + iRadiusSegment, iAux + 2 + iRadiusSegment);
            DrawRectangleIndices(M_TriangelsIndices, 0, iAux + 2 + iRadiusSegment, 2, 6);

            DrawRectangleIndices(M_TriangelsIndices, 2, 3, iAux + 2 + 4 * iRadiusPoints - 1, 6);

            DrawRectangleIndices(M_TriangelsIndices, 3, iAux + 3 * iRadiusPoints, 5, iAux + 1 + 4 * iRadiusPoints);
            DrawRectangleIndices(M_TriangelsIndices, iAux + 3 * iRadiusPoints, iAux + 3 * iRadiusPoints + 1, 4, 5);

            // Arc sectors
            // 1st SolidCircleSector
            DrawSolidCircleSector(1, iAux + 1, iRadiusSegment, M_TriangelsIndices, false);
            // 2nd SolidCircleSector
            DrawSolidCircleSector(2, iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 3rd SolidCircleSector
            DrawSolidCircleSector(3, iAux + 1 + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 4th SolidCircleSector
            DrawSolidCircleSector(4, iAux + 1 + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);

            // Back Side
            // Points order 1,4,3,2

            // Arc sectors
            int iPointNumbersOffset = iAux + 2 + 4 * iRadiusPoints; // Number of nodes per section - Nodes offset

            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 2 + iRadiusSegment, iPointNumbersOffset + iAux + 1 + iRadiusSegment, iPointNumbersOffset + 1);
            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + 6, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 + iRadiusSegment);

            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 2, iPointNumbersOffset + 6, iPointNumbersOffset + iAux + 2 + 4 * iRadiusPoints - 1, iPointNumbersOffset + 3);

            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1 + 4 * iRadiusPoints, iPointNumbersOffset + 5, iPointNumbersOffset + iAux + 3 * iRadiusPoints);
            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + iAux + 3 * iRadiusPoints, iPointNumbersOffset + 5, iPointNumbersOffset + 4 , iPointNumbersOffset + iAux + 3 * iRadiusPoints + 1);

            // 1st SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 1, iRadiusSegment, M_TriangelsIndices, true);
            // 2nd SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 1 + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 3rd SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 3, iPointNumbersOffset + iAux + 1 + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 4th SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 4, iPointNumbersOffset + iAux + 1 + 3 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);

            // Shell
            DrawCaraLaterals(iAux, 2 + 4 * iRadiusPoints, M_TriangelsIndices);
        }

        private void load_3_03_04_TriangelIndices(int iAux, int iRadiusSegment)
        {
            // const int secNum = iAux + iRadiusPoints * 3 + 1;  // Number of points in section (2D)
            int iRadiusPoints = iRadiusSegment + 1;

            M_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            // Points order 1,2,3,4

            DrawRectangleIndices(M_TriangelsIndices, 0, iAux + iRadiusSegment, 1, iAux + 3 * iRadiusPoints);
            DrawRectangleIndices(M_TriangelsIndices, 1, iAux + 2 * iRadiusPoints, 2, iAux + 3 * iRadiusPoints);

            // Arc sectors
            // 1st SolidCircleSector
            DrawSolidCircleSector(0, iAux, iRadiusSegment, M_TriangelsIndices, false);
            // 2nd SolidCircleSector
            DrawSolidCircleSector(1, iAux + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);
            // 3rd SolidCircleSector
            DrawSolidCircleSector(2, iAux + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, false);

            // Back Side
            // Points order 1,4,3,2

            // Arc sectors
            int iPointNumbersOffset = iAux + 1 + 3 * iRadiusPoints; // Number of nodes per section - Nodes offset

            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 0, iPointNumbersOffset + iAux + 3 * iRadiusPoints, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + iRadiusSegment);
            DrawRectangleIndices(M_TriangelsIndices, iPointNumbersOffset + 1, iPointNumbersOffset + iAux + 3 * iRadiusPoints, iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 * iRadiusPoints);

            // 1st SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 0, iPointNumbersOffset + iAux, iRadiusSegment, M_TriangelsIndices, true);
            // 2nd SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 1, iPointNumbersOffset + iAux + iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);
            // 3rd SolidCircleSector
            DrawSolidCircleSector(iPointNumbersOffset + 2, iPointNumbersOffset + iAux + 2 * iRadiusPoints, iRadiusSegment, M_TriangelsIndices, true);

            // Shell
            DrawCaraLaterals(iAux, 1 + 3 * iRadiusPoints, M_TriangelsIndices);
        }



        public Window2()
		{
			InitializeComponent();

            CTest1 test1 = new CTest1(); // Basic Cross Section Data

            // Temp
            // Half Circle Bar
            // Quater Circle Bar
            // load_0_00_01_TriangelsIndices();
            // Round or Ellipse Bar
            // load_0_02_03_TriangelIndices();
            // Triangular Prism 
            //load_0_04_TriangelsIndices();
            // Flat Bar
            //load_0_05_TriangelIndices();
            // Half Circle
            // load_0_20_TriangelIndices();
            // TUBE / PIPE Circle or Ellipse Shape
            //load_0_22_23_TriangelIndices();
            // Triangular Prism with Opening
            //load_0_24_TriangelsIndices();
            // HL-section / Rectanglular Hollow Cross-section
            //load_0_25_TriangelIndices();
            // Polygonal Hollow Section
            //load_0_26_28_TriangelIndices(test1.objCrScHollow.INoPoints);
            // I - section
            //load_0_50_TriangelIndices();
            // U-section
            //load_0_52_TriangelIndices();
            // L-section / Angle section
            //load_0_54_TriangelIndices();
            // T-section / T section
            //load_0_56_TriangelIndices();
            // Z-section / Z section
            // load_0_58_TriangelIndices();
            // Cruciform Bar
            //load_0_60_TriangelIndices(test1.objCrScSolid.ITotNoPoints);
            // Y-section / Y section
            // load_0_61_TriangelIndices();

            // Rolled I profile, Tapered flanges
            // load_3_00_TriangelIndices(12,8); // Number of auxiliary point , number of segments of arc
            // Rolled U profile, Tapered flanges, channel section
            // load_3_02_TriangelIndices(6, 8); // Number of auxiliary point , number of segments of arc
            // Rolled L profile, angle section
            load_3_03_04_TriangelIndices(3, 8); // Number of auxiliary point , number of segments of arc

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


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Number of Points per section
            short iNoCrScPoints2D;
            // Length of Element
            float fELength = -500; // Temporary load flor Member Segment geometry

            // Points 2D Coordinate Array

            if (sShapeType == 0) // Solid I,U,Z,HL,L, ..............
            {
                iNoCrScPoints2D = test1.objCrScSolid.ITotNoPoints; // Depends on Section Type 

                // Solid section
                float[,] res = test1.objCrScSolid.m_CrScPoint; // I,U,Z,HL,L, ..............

                // Fill Mesh Positions for Start and End Section of Element - Defines Edge Points of Element

                //// I,U,Z,HL, L, ....
                for (int j = 0; j < iNoCrScPoints2D; j++)
                {
                    mesh.Positions.Add(new Point3D(res[j, 0], res[j, 1], 0));
                }
                for (int j = 0; j < iNoCrScPoints2D; j++)
                {
                    mesh.Positions.Add(new Point3D(res[j, 0], res[j, 1], fELength));
                }
            }
            else
            {
                // Tubes , Polygonal Hollow Sections
                iNoCrScPoints2D = (short)(2 * test1.objCrScHollow.INoPoints);
                float[,] res1 = test1.objCrScHollow.m_CrScPointOut; // TU
                float[,] res2 = test1.objCrScHollow.m_CrScPointIn; // TU

                // Tube, regular hollow sections
                // TU

                // Start
                // OutSide Radius Points
                for (int j = 0; j < test1.objCrScHollow.INoPoints; j++)
                {
                    mesh.Positions.Add(new Point3D(res1[j, 0], res1[j, 1], 0));
                }
                // Inside Radius Points
                for (int j = 0; j < test1.objCrScHollow.INoPoints; j++)
                {
                    mesh.Positions.Add(new Point3D(res2[j, 0], res2[j, 1], 0));
                }

                // End
                // OutSide Radius Points
                for (int j = 0; j < test1.objCrScHollow.INoPoints; j++)
                {
                    mesh.Positions.Add(new Point3D(res1[j, 0], res1[j, 1], fELength));
                }
                // Inside Radius Points
                for (int j = 0; j < test1.objCrScHollow.INoPoints; j++)
                {
                    mesh.Positions.Add(new Point3D(res2[j, 0], res2[j, 1], fELength));
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



			//mesh.Positions.Add(new Point3D(-1,-1,1));
			//mesh.Positions.Add(new Point3D(1,-1,1));
			//mesh.Positions.Add(new Point3D(1,1,1));
			//mesh.Positions.Add(new Point3D(-1,1,1));

			//mesh.TriangleIndices = new Int32Collection();
			//mesh.TriangleIndices.Add(0);
			//mesh.TriangleIndices.Add(1);
			//mesh.TriangleIndices.Add(2);
			//mesh.TriangleIndices.Add(0);
			//mesh.TriangleIndices.Add(2);
			//mesh.TriangleIndices.Add(3);

            // Mesh Triangles - various cross-sections shapes defined
			mesh.TriangleIndices = M_TriangelsIndices;
            
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
			_trackport.SetupScene();
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

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.Key) 
			{
				case Key.LeftCtrl: _trackport.Trackball.IsCtrlDown = e.IsDown; break;
					
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.LeftCtrl: _trackport.Trackball.IsCtrlDown = e.IsDown; break;
			}
		}
	}
}
