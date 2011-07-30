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

namespace sw_en_GUI
{
	/// <summary>
	/// Interaction logic for Window2.xaml
	/// </summary>
	public partial class Window2 : Window
	{
		private Int32Collection M_IProfileTriangelsIndices;

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

		public Window2()
		{
			InitializeComponent();
			loadIProfileTriangelIndices();

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
			float[,] res = test1.objCrSc.m_CrScPoint;
			for (int j = 0; j < 12; j++)
			{
				mesh.Positions.Add(new Point3D(res[j, 0], res[j, 1], 0));
			}
			for (int j = 0; j < 12; j++)
			{
				mesh.Positions.Add(new Point3D(res[j, 0], res[j, 1], -1000));
			}
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

			mesh.TriangleIndices = M_IProfileTriangelsIndices;
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
