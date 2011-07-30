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

namespace sw_en_GUI
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            
            //MeshGeometry3D mesh = new MeshGeometry3D();
            
            Point3D p0 = new Point3D(-1, -1, -1);
            Point3D p1 = new Point3D(1, -1, -1);
            Point3D p2 = new Point3D(1, -1, 1);
            Point3D p3 = new Point3D(-1, -1, 1);
            Point3D p4 = new Point3D(-1, 1, -1);
            Point3D p5 = new Point3D(1, 1, -1);
            Point3D p6 = new Point3D(1, 1, 1);
            Point3D p7 = new Point3D(-1, 1, 1);

            //mesh = 
            //GeometryModel3D geoModel3D = new GeometryModel3D();
            //geoModel3D.Geometry = mesh;
            

            Model3DGroup gr = new Model3DGroup();
            

            GeometryModel3D model = new GeometryModel3D();
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions = new Point3DCollection();
            mesh.Positions.Add(new Point3D(-1,-1,1));
            mesh.Positions.Add(new Point3D(1,-1,1));
            mesh.Positions.Add(new Point3D(1,1,1));
            mesh.Positions.Add(new Point3D(-1,1,1));
            mesh.TriangleIndices = new Int32Collection();
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            model.Geometry = mesh;
            SolidColorBrush br = new SolidColorBrush(Color.FromRgb(255,255,255));
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
    }
}
