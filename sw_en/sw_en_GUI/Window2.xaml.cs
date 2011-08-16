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

        // Collection for various sections
        private Int32Collection M_IProfileTriangelsIndices;
        private Int32Collection M_UProfileTriangelsIndices;
        private Int32Collection M_HLProfileTriangelsIndices;
        private Int32Collection M_LProfileTriangelsIndices;
        private Int32Collection M_TProfileTriangelsIndices;
        private Int32Collection M_ZProfileTriangelsIndices;
        private Int32Collection M_RBProfileTriangelsIndices;
        private Int32Collection M_FBProfileTriangelsIndices;
        private Int32Collection M_TUProfileTriangelsIndices;
        private Int32Collection M_B_TRIAN_TriangelsIndices;
        private Int32Collection M_0_24_TriangelsIndices;

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
        
        
        /*
        private void loadIProfileTriangelIndices()
		{
			const int secNum = 12;
			M_IProfileTriangelsIndices = new Int32Collection();
			//celo predne
			M_IProfileTriangelsIndices.Add(0);
			M_IProfileTriangelsIndices.Add(2);
			M_IProfileTriangelsIndices.Add(1);

			M_IProfileTriangelsIndices.Add(0);
			M_IProfileTriangelsIndices.Add(11);
			M_IProfileTriangelsIndices.Add(2);

			M_IProfileTriangelsIndices.Add(10);
			M_IProfileTriangelsIndices.Add(4);
			M_IProfileTriangelsIndices.Add(3);

			M_IProfileTriangelsIndices.Add(10);
			M_IProfileTriangelsIndices.Add(9);
			M_IProfileTriangelsIndices.Add(4);

			M_IProfileTriangelsIndices.Add(8);
			M_IProfileTriangelsIndices.Add(6);
			M_IProfileTriangelsIndices.Add(5);

			M_IProfileTriangelsIndices.Add(8);
			M_IProfileTriangelsIndices.Add(7);
			M_IProfileTriangelsIndices.Add(6);
			//celo zadne
			M_IProfileTriangelsIndices.Add(0 + secNum);
			M_IProfileTriangelsIndices.Add(1 + secNum);
			M_IProfileTriangelsIndices.Add(2 + secNum);

			M_IProfileTriangelsIndices.Add(0 + secNum);
			M_IProfileTriangelsIndices.Add(2 + secNum);
			M_IProfileTriangelsIndices.Add(11 + secNum);

			M_IProfileTriangelsIndices.Add(10 + secNum);
			M_IProfileTriangelsIndices.Add(3 + secNum);
			M_IProfileTriangelsIndices.Add(4 + secNum);

			M_IProfileTriangelsIndices.Add(10 + secNum);
			M_IProfileTriangelsIndices.Add(4 + secNum);
			M_IProfileTriangelsIndices.Add(9 + secNum);

			M_IProfileTriangelsIndices.Add(8 + secNum);
			M_IProfileTriangelsIndices.Add(5 + secNum);
			M_IProfileTriangelsIndices.Add(6 + secNum);

			M_IProfileTriangelsIndices.Add(8 + secNum);
			M_IProfileTriangelsIndices.Add(6 + secNum);
			M_IProfileTriangelsIndices.Add(7 + secNum);

			//pozdlzne
			M_IProfileTriangelsIndices.Add(0);
			M_IProfileTriangelsIndices.Add(13);
			M_IProfileTriangelsIndices.Add(12);

			M_IProfileTriangelsIndices.Add(0);
			M_IProfileTriangelsIndices.Add(1);
			M_IProfileTriangelsIndices.Add(13);

			M_IProfileTriangelsIndices.Add(1);
			M_IProfileTriangelsIndices.Add(14);
			M_IProfileTriangelsIndices.Add(13);

			M_IProfileTriangelsIndices.Add(1);
			M_IProfileTriangelsIndices.Add(2);
			M_IProfileTriangelsIndices.Add(14);


			M_IProfileTriangelsIndices.Add(2);
			M_IProfileTriangelsIndices.Add(15);
			M_IProfileTriangelsIndices.Add(14);

			M_IProfileTriangelsIndices.Add(2);
			M_IProfileTriangelsIndices.Add(3);
			M_IProfileTriangelsIndices.Add(15);

			M_IProfileTriangelsIndices.Add(3);
			M_IProfileTriangelsIndices.Add(16);
			M_IProfileTriangelsIndices.Add(15);

			M_IProfileTriangelsIndices.Add(3);
			M_IProfileTriangelsIndices.Add(4);
			M_IProfileTriangelsIndices.Add(16);

			M_IProfileTriangelsIndices.Add(4);
			M_IProfileTriangelsIndices.Add(17);
			M_IProfileTriangelsIndices.Add(16);

			M_IProfileTriangelsIndices.Add(4);
			M_IProfileTriangelsIndices.Add(5);
			M_IProfileTriangelsIndices.Add(17);

			M_IProfileTriangelsIndices.Add(5);
			M_IProfileTriangelsIndices.Add(18);
			M_IProfileTriangelsIndices.Add(17);

			M_IProfileTriangelsIndices.Add(5);
			M_IProfileTriangelsIndices.Add(6);
			M_IProfileTriangelsIndices.Add(18);

			M_IProfileTriangelsIndices.Add(6);
			M_IProfileTriangelsIndices.Add(19);
			M_IProfileTriangelsIndices.Add(18);

			M_IProfileTriangelsIndices.Add(6);
			M_IProfileTriangelsIndices.Add(7);
			M_IProfileTriangelsIndices.Add(19);

			//normala orientovana opacne
			M_IProfileTriangelsIndices.Add(20);
			M_IProfileTriangelsIndices.Add(7);
			M_IProfileTriangelsIndices.Add(8);

			M_IProfileTriangelsIndices.Add(20);
			M_IProfileTriangelsIndices.Add(19);
			M_IProfileTriangelsIndices.Add(7);

			M_IProfileTriangelsIndices.Add(21);
			M_IProfileTriangelsIndices.Add(8);
			M_IProfileTriangelsIndices.Add(9);

			M_IProfileTriangelsIndices.Add(21);
			M_IProfileTriangelsIndices.Add(20);
			M_IProfileTriangelsIndices.Add(8);

			M_IProfileTriangelsIndices.Add(22);
			M_IProfileTriangelsIndices.Add(9);
			M_IProfileTriangelsIndices.Add(10);

			M_IProfileTriangelsIndices.Add(22);
			M_IProfileTriangelsIndices.Add(21);
			M_IProfileTriangelsIndices.Add(9);


			M_IProfileTriangelsIndices.Add(23);
			M_IProfileTriangelsIndices.Add(10);
			M_IProfileTriangelsIndices.Add(11);


			M_IProfileTriangelsIndices.Add(23);
			M_IProfileTriangelsIndices.Add(22);
			M_IProfileTriangelsIndices.Add(10);


			M_IProfileTriangelsIndices.Add(12);
			M_IProfileTriangelsIndices.Add(11);
			M_IProfileTriangelsIndices.Add(0);

			M_IProfileTriangelsIndices.Add(12);
			M_IProfileTriangelsIndices.Add(23);
			M_IProfileTriangelsIndices.Add(11);
		}
        */

        private void loadIProfileTriangelIndices()
        {
            // const int secNum = 12;  // Number of points in section (2D)
            M_IProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_IProfileTriangelsIndices, 0, 1, 2, 11);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 10, 3, 4, 9);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 8, 5, 6, 7);

            // Back Side 
            DrawRectangleIndices(M_IProfileTriangelsIndices, 13, 12, 23, 14);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 15, 22, 21, 16);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 17, 20, 19, 18);

            // Shell Surface 
            DrawRectangleIndices(M_IProfileTriangelsIndices, 0, 12, 13, 1);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 1, 13, 14, 2);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 2, 14, 15, 3);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 3, 15, 16, 4);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 4, 16, 17, 5);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 5, 17, 18, 6);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 6, 18, 19, 7);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 20, 8, 7, 19);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 21, 9, 8, 20);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 22, 10, 9, 21);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 23, 11, 10, 22);
            DrawRectangleIndices(M_IProfileTriangelsIndices, 12, 0, 11, 23);
        }

        private void loadUProfileTriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_UProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_UProfileTriangelsIndices, 0, 1, 2, 3);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 0, 3, 4, 7);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 4, 5, 6, 7);
            
            // Back Side 
            DrawRectangleIndices(M_UProfileTriangelsIndices, 9, 8, 11, 10);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 11, 8, 15, 12);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 13, 12, 15, 14);

            // Shell Surface 
            DrawRectangleIndices(M_UProfileTriangelsIndices, 9, 1, 0, 8);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 1, 9, 10, 2);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 2, 10, 11, 3);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 3, 11, 12, 4);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 4, 12, 13, 5);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 5, 13, 14, 6);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 6, 14, 15, 7);
            DrawRectangleIndices(M_UProfileTriangelsIndices, 8, 0, 7, 15);
        }

        private void loadHLProfileTriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_HLProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 0, 1, 5, 4);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 1, 2, 6, 5);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 7, 6, 2, 3);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 0, 4, 7, 3);
            
            // Back Side 
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 9, 8, 12, 13);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 9, 13, 14, 10);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 14, 15, 11, 10);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 8, 11, 15, 12);

            // Shell Surface
            // Outside
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 0, 8, 9, 1);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 1, 9, 10, 2);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 2, 10, 11, 3);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 8, 0, 3, 11);
            // Inside
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 13, 5, 6, 14);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 7, 15, 14, 6);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 4, 12, 15, 7);
            DrawRectangleIndices(M_HLProfileTriangelsIndices, 12, 4, 5, 13);
        }

        private void loadLProfileTriangelIndices()
        {
            // const int secNum = 6;  // Number of points in section (2D)
            M_LProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_LProfileTriangelsIndices, 0, 1, 2, 5);
            DrawRectangleIndices(M_LProfileTriangelsIndices, 2, 3, 4, 5);
            
            // Back Side 
            DrawRectangleIndices(M_LProfileTriangelsIndices, 7, 6, 11, 8);
            DrawRectangleIndices(M_LProfileTriangelsIndices, 9, 8, 11, 10);

            // Shell Surface
            DrawRectangleIndices(M_LProfileTriangelsIndices, 0, 6, 7, 1);
            DrawRectangleIndices(M_LProfileTriangelsIndices, 1, 7, 8, 2);
            DrawRectangleIndices(M_LProfileTriangelsIndices, 2, 8, 9, 3);
            DrawRectangleIndices(M_LProfileTriangelsIndices, 3, 9, 10, 4);
            DrawRectangleIndices(M_LProfileTriangelsIndices, 4, 10, 11, 5);
            DrawRectangleIndices(M_LProfileTriangelsIndices, 6, 0, 5, 11);
        }

        private void loadTProfileTriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_TProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_TProfileTriangelsIndices, 0, 1, 2, 7);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 6, 3, 4, 5);
            
            // Back Side 
            DrawRectangleIndices(M_TProfileTriangelsIndices, 9, 8, 15, 10);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 11, 14, 13, 12);

            // Shell Surface
            DrawRectangleIndices(M_TProfileTriangelsIndices, 0, 8, 9, 1);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 1, 9, 10, 2);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 2, 10, 11, 3);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 3, 11, 12, 4);

            DrawRectangleIndices(M_TProfileTriangelsIndices, 4, 12, 13, 5);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 14, 6, 5, 13);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 15, 7, 6, 14);
            DrawRectangleIndices(M_TProfileTriangelsIndices, 8, 0, 7, 15);
        }

        private void loadZProfileTriangelIndices()
        {
            // const int secNum = 8;  // Number of points in section (2D)
            M_ZProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 0, 1, 6, 7);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 6, 1, 2, 5);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 2, 3, 4, 5);

            // Back Side 
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 9, 8, 15, 14);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 9, 14, 13, 10);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 11, 10, 13, 12);

            // Shell Surface
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 0, 8, 9, 1);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 1, 9, 10, 2);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 2, 10, 11, 3);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 3, 11, 12, 4);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 4, 12, 13, 5);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 5, 13, 14, 6);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 6, 14, 15, 7);
            DrawRectangleIndices(M_ZProfileTriangelsIndices, 7, 15, 8, 0);
        }

        private void loadRBProfileTriangelIndices()
        {
            const int secNum = 37;  // Number of points in section (2D) includes centroid
            M_RBProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            for (int i = 0; i < secNum-1; i++)
            {
                if (i < secNum - 2)
                {
                    M_RBProfileTriangelsIndices.Add(i);
                    M_RBProfileTriangelsIndices.Add(secNum-1);
                    M_RBProfileTriangelsIndices.Add(i + 1);
                }
                else // Last Element
                {
                    M_RBProfileTriangelsIndices.Add(i);
                    M_RBProfileTriangelsIndices.Add(secNum-1);
                    M_RBProfileTriangelsIndices.Add(0);
                }
            }

            // Back Side
            for (int i = 0; i < secNum-1; i++)
            {
                if (i < secNum - 2)
                {
                    M_RBProfileTriangelsIndices.Add(secNum+i);
                    M_RBProfileTriangelsIndices.Add(secNum + i + 1);
                    M_RBProfileTriangelsIndices.Add(secNum + secNum-1);
                }
                else // Last Element
                {
                    M_RBProfileTriangelsIndices.Add(secNum + i);
                    M_RBProfileTriangelsIndices.Add(secNum);
                    M_RBProfileTriangelsIndices.Add(secNum + secNum - 1);
                }
            }

            // Shell Surface OutSide
            for (int i = 0; i < secNum-1; i++)
            {
                if (i < secNum - 2)
                    DrawRectangleIndices(M_RBProfileTriangelsIndices, i, secNum + i, secNum + i + 1, i + 1);
                else
                    DrawRectangleIndices(M_RBProfileTriangelsIndices, i, secNum + i, secNum, 0); // Last Element
            }
        }

        private void loadFBProfileTriangelIndices()
        {
            // const int secNum = 4;  // Number of points in section (2D)
            M_FBProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_FBProfileTriangelsIndices, 0, 1, 2, 3);

            // Back Side 
            DrawRectangleIndices(M_FBProfileTriangelsIndices, 4, 7, 6, 5);

            // Shell Surface
            DrawRectangleIndices(M_FBProfileTriangelsIndices, 0, 4, 5, 1);
            DrawRectangleIndices(M_FBProfileTriangelsIndices, 1, 5, 6, 2);
            DrawRectangleIndices(M_FBProfileTriangelsIndices, 2, 6, 7, 3);
            DrawRectangleIndices(M_FBProfileTriangelsIndices, 3, 7, 4, 0);
        }

        private void loadTUProfileTriangelIndices()
        {
            // const int secNum = 2*36;  // Number of points in section (2D)
            const int secNum = 36; // Number of points in one circle of section (2D) /Number of points is equal to Number of cells par section

            M_TUProfileTriangelsIndices = new Int32Collection();

            // Front Side / Forehead

            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum-1)
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, i, i + 1, i + secNum + 1, i + secNum);
                else
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, i, 0, i + 1, i + secNum); // Last Element
            }

            // Back Side
            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + secNum + 1, 2 * secNum + i + 1);
                else
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, 2 * secNum + i, 2 * secNum + i + secNum, 2 * secNum + i + 1, 2 * secNum + 0); // Last Element
            }

            // Shell Surface OutSide
            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, i, 2 * secNum + i, 2 * secNum + i + 1, i + 1);
                else
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, i, 2 * secNum + i, 2 * secNum, 0); // Last Element
            }

            // Shell Surface Inside
            for (int i = 0; i < secNum; i++)
            {
                if (i < secNum - 1)
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, secNum+i, secNum + i+1, 2 * secNum + i +secNum + 1, 2 * secNum + i+ secNum);
                else
                    DrawRectangleIndices(M_TUProfileTriangelsIndices, 2 * secNum + i + 1, 2 * secNum + i + secNum, secNum + i, secNum); // Last Element
            }
        }

        private void load_B_TRIAN_TriangelsIndices()
        {
            // const int secNum = 3;  // Number of points in section (2D)
            M_B_TRIAN_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            M_B_TRIAN_TriangelsIndices.Add(0);
            M_B_TRIAN_TriangelsIndices.Add(2);
            M_B_TRIAN_TriangelsIndices.Add(1);

            // Back Side 
            M_B_TRIAN_TriangelsIndices.Add(3);
            M_B_TRIAN_TriangelsIndices.Add(4);
            M_B_TRIAN_TriangelsIndices.Add(5);

            // Shell Surface
            DrawRectangleIndices(M_B_TRIAN_TriangelsIndices, 0, 3, 4, 1);
            DrawRectangleIndices(M_B_TRIAN_TriangelsIndices, 1, 4, 5, 2);
            DrawRectangleIndices(M_B_TRIAN_TriangelsIndices, 2, 5, 3, 0);
        }

        private void load_0_24_TriangelsIndices()
        {
            // const int secNum = 6;  // Number of points in section (2D)
            M_0_24_TriangelsIndices = new Int32Collection();

            // Front Side / Forehead
            DrawRectangleIndices(M_0_24_TriangelsIndices, 0, 1, 4, 3);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 4, 1, 2, 5);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 0, 3, 5, 2);

            // Back Side 
            DrawRectangleIndices(M_0_24_TriangelsIndices, 6, 8, 11, 9);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 10, 11, 8, 7);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 6, 9, 10, 7);

            // Shell Surface
            DrawRectangleIndices(M_0_24_TriangelsIndices, 0, 6, 7, 1);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 1, 7, 8, 2);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 2, 8, 6, 0);

            DrawRectangleIndices(M_0_24_TriangelsIndices, 3, 4, 10, 9);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 4, 5, 11, 10);
            DrawRectangleIndices(M_0_24_TriangelsIndices, 4, 9, 11, 5);
        }


        
		public Window2()
		{
			InitializeComponent();
            // Temp
            // I - section
			// loadIProfileTriangelIndices();
            // U-section
            //loadUProfileTriangelIndices();
            // HL-section / Rectanglular Hollow Cross-section
            // loadHLProfileTriangelIndices();
            // L-section / Angle section
            // loadLProfileTriangelIndices();
            // T-section / T section
            // loadTProfileTriangelIndices();
            // Z-section / Z section
            // loadZProfileTriangelIndices();
            // TUBE / PIPE
            // loadTUProfileTriangelIndices();
            // Round Bar
            // loadRBProfileTriangelIndices();
            // Flat Bar
             loadFBProfileTriangelIndices();
            // Triangular Prism 
            // load_B_TRIAN_TriangelsIndices();
            // Triangular Prism with Opening
            // load_0_24_TriangelsIndices()

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
			CTest1 test1 = new CTest1();

            // Number of Points per section
            int iNoCrScPoints2D = 4; // Depends on Section Type - nacitavat priamo z objektu objCrSc // I,U,Z,HL, L, ....

            // Points 2D Coordinate Array
			float[,] res = test1.objCrSc.m_CrScPoint; // I,U,Z,HL, L, ....


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Tube !!!
            //float[,] res1 = test1.objCrSc.m_CrScPointOut; // RB, TU
            //float[,] res2 = test1.objCrSc.m_CrScPointIn; // RB, TU
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            // Length of Element
            float fELength = -500; // Temporary load flor Member Segment geometry

            // Fill Mesh Positions for Start and End Section of Element - Defines Edge Points of Element

            // I,U,Z,HL, L, ....
            for (int j = 0; j < iNoCrScPoints2D; j++)
			{
				mesh.Positions.Add(new Point3D(res[j, 0], res[j, 1], 0));
			}
            for (int j = 0; j < iNoCrScPoints2D; j++)
			{
                mesh.Positions.Add(new Point3D(res[j, 0], res[j, 1], fELength));
			}

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Tube
            // RB, TU

            //// Start
            //// OutSide Radius Points
            //for (int j = 0; j < test1.objCrSc.INoPoints; j++)
            //{
            //    mesh.Positions.Add(new Point3D(res1[j, 0], res1[j, 1], 0));
            //}
            //// Inside Radius Points
            //for (int j = 0; j < test1.objCrSc.INoPoints; j++)
            //{
            //    mesh.Positions.Add(new Point3D(res2[j, 0], res2[j, 1], 0));
            //}

            //// End
            //// OutSide Radius Points
            //for (int j = 0; j < test1.objCrSc.INoPoints; j++)
            //{
            //    mesh.Positions.Add(new Point3D(res1[j, 0], res1[j, 1], fELength));
            //}
            //// Inside Radius Points
            //for (int j = 0; j < test1.objCrSc.INoPoints; j++)
            //{
            //    mesh.Positions.Add(new Point3D(res2[j, 0], res2[j, 1], fELength));
            //}

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

			// mesh.TriangleIndices = M_IProfileTriangelsIndices;
            // mesh.TriangleIndices = M_UProfileTriangelsIndices;
            // mesh.TriangleIndices = M_HLProfileTriangelsIndices;
            // mesh.TriangleIndices = M_LProfileTriangelsIndices;
            // mesh.TriangleIndices = M_TProfileTriangelsIndices;
            // mesh.TriangleIndices = M_ZProfileTriangelsIndices;
            // mesh.TriangleIndices = M_RBProfileTriangelsIndices;
             mesh.TriangleIndices = M_FBProfileTriangelsIndices;
            // mesh.TriangleIndices = M_TUProfileTriangelsIndices;
            // mesh.TriangleIndices = M_B_TRIAN_TriangelsIndices;
            // mesh.TriangleIndices = M_0_24_TriangelsIndices;

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
