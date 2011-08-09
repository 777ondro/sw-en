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
	/// Interaction logic for Viewer3D.xaml
	/// </summary>
	public partial class Viewer3D : Window
	{
		private Int32Collection M_FBProfileTriangelsIndices;
		private Trackball _trackball = new Trackball();
		ScreenSpaceLines3D wireframe = new ScreenSpaceLines3D();
		public Trackball Trackball
		{
			get { return _trackball; }
			set { _trackball = value; }
		}

		//------------------------------------------------------------------------------------------------
		public Viewer3D()
		{
			InitializeComponent();

			this.Camera.Transform = _trackball.Transform;
			this.Headlight.Transform = _trackball.Transform;

			this.Camera.Position = new Point3D(0, 0, 400);

			loadFBProfileTriangelIndices();
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
			mesh.TriangleIndices = M_FBProfileTriangelsIndices;
			model.Geometry = mesh;
			SolidColorBrush br = new SolidColorBrush(Color.FromRgb(255, 0, 0));
			model.Material = new DiffuseMaterial(br);
			gr.Children.Add(model);

			Root.Content = (Model3D)gr;


			
			wireframe.Color = Colors.Aquamarine;
			wireframe.Thickness = 3;
			makeWireframe(mesh.Positions);

			Viewport.Children.Add(wireframe);
		}

		private void makeWireframe(Point3DCollection collection) 
		{
			//front rectangle
			wireframe.Points.Add(collection[0]);
			wireframe.Points.Add(collection[1]);

			wireframe.Points.Add(collection[1]);
			wireframe.Points.Add(collection[2]);

			wireframe.Points.Add(collection[2]);
			wireframe.Points.Add(collection[3]);

			wireframe.Points.Add(collection[3]);
			wireframe.Points.Add(collection[0]);

			//back rectangle
			wireframe.Points.Add(collection[4]);
			wireframe.Points.Add(collection[5]);

			wireframe.Points.Add(collection[5]);
			wireframe.Points.Add(collection[6]);

			wireframe.Points.Add(collection[6]);
			wireframe.Points.Add(collection[7]);

			wireframe.Points.Add(collection[7]);
			wireframe.Points.Add(collection[4]);


			// 4 side edges
			wireframe.Points.Add(collection[0]);
			wireframe.Points.Add(collection[4]);

			wireframe.Points.Add(collection[1]);
			wireframe.Points.Add(collection[5]);

			wireframe.Points.Add(collection[2]);
			wireframe.Points.Add(collection[6]);

			wireframe.Points.Add(collection[3]);
			wireframe.Points.Add(collection[7]);

		}



		//------------------------------------------------------------------------------------------------
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

		//------------------------------------------------------------------------------------------------
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

		//------------------------------------------------------------------------------------------------
		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			_trackball.EventSource = CaptureBorder;
		}

		//------------------------------------------------------------------------------------------------
		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.LeftCtrl: _trackball.IsCtrlDown = e.IsDown; break;

			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.LeftCtrl: _trackball.IsCtrlDown = e.IsDown; break;
			}
		}


		//------------------------------------------------------------------------------------------------
		
	}
}
