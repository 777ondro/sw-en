using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using BaseClasses;
using BaseClasses.GraphObj;
using _3DTools;

namespace AAC
{
    /// <summary>
    /// Interaction logic for PanelPreview.xaml
    /// </summary>
    public partial class PanelPreview : Page
    {
        //--------------------------------------------------------------------------------------------
        public PanelPreview()
        {
            InitializeComponent();
            CreatePreviewModel(5, 0.625, 0.25); // Todo - hodnoty natvrdo, zapracovat nacitanie z GUI Main Window a update 3D modelu podla zakladnych hodnot
        }

        public PanelPreview(double L, double b, double h)
        {
            InitializeComponent();
            CreatePreviewModel(L, b, h);
        }

        //--------------------------------------------------------------------------------------------
        public void CreatePreviewModel(double L, double b, double h)
        {
            // Default view
            Point3D lookAtPoint = new Point3D(0.0, 0.6, 0.1);

            _trackport.PerspectiveCamera.Position = new Point3D(0, -1.5, 0.4);
            LookAt(_trackport.PerspectiveCamera, lookAtPoint);

            //OrthographicCamera camera = new OrthographicCamera(); // Doplnit moznost zobrazit v pravouhlom priemietani

            // Light Direction
            _trackport.Light.Direction = new Vector3D(0, 5, -10);

            Model3DGroup model = CreateFloorPanel(L, b, h, 0.8);

            //Model3DGroup model_reinf = CreateFloorPanel(2,0.3, 0.1, 1.0);

            //model.Children.Add(model_reinf);

            _trackport.Model = (Model3D)model;

            _trackport.Model = (Model3D)model;
            _trackport.Trackball.TranslateScale = 1000;   //step for moving object (panning)
            _trackport.SetupScene();
        }

        //--------------------------------------------------------------------------------------------
        public Model3DGroup CreateFloorPanel(double L, double b, double h, double opacity)
        {
            Model3DGroup models = new Model3DGroup();

            double xb = 0.2 * b;
            double xa = 0.18 * b;
            double xc = 0.03;

            double ya = 0.5 * h;
            double yb = 0.8 * h;

            Point3D p0 = new Point3D(0, 0, h);
            Point3D p1 = new Point3D(b - xa, 0, h);
            Point3D p2 = new Point3D(b - xb, 0, yb);
            Point3D p3 = new Point3D(b, 0, ya);
            Point3D p4 = new Point3D(b, 0, 0);
            Point3D p5 = new Point3D(0, 0, 0);
            Point3D p6 = new Point3D(0, 0, ya);
            Point3D p7 = new Point3D(xc, 0, ya + 0.5 * xc);
            Point3D p8 = new Point3D(xc, 0, yb);
            Point3D p9 = new Point3D(0, 0, yb + 0.5 * xc);

            Point3D p10 = new Point3D(0, L, h);
            Point3D p11 = new Point3D(b - xa, L, h);
            Point3D p12 = new Point3D(b - xb, L, yb);
            Point3D p13 = new Point3D(b, L, ya);
            Point3D p14 = new Point3D(b, L, 0);
            Point3D p15 = new Point3D(0, L, 0);
            Point3D p16 = new Point3D(0, L, ya);
            Point3D p17 = new Point3D(xc, L, ya + 0.5 * xc);
            Point3D p18 = new Point3D(xc, L, yb);
            Point3D p19 = new Point3D(0, L, yb + 0.5 * xc);

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(200,200,200));
            brush.Opacity = opacity;
            DiffuseMaterial DiffMat = new DiffuseMaterial(brush);

            // Front Side
            models.Children.Add(CreateRectangle(p0, p9, p2, p1, DiffMat));
            models.Children.Add(CreateTriangle(p9, p8, p2, DiffMat));
            models.Children.Add(CreateTriangle(p8, p7, p2, DiffMat));
            models.Children.Add(CreateTriangle(p7, p3, p2, DiffMat));
            models.Children.Add(CreateTriangle(p7, p6, p3, DiffMat));
            models.Children.Add(CreateRectangle(p6, p5, p4, p3, DiffMat));

            // Back Side
            models.Children.Add(CreateRectangle(p10, p11, p12, p19, DiffMat));
            models.Children.Add(CreateTriangle(p19, p12, p18, DiffMat));
            models.Children.Add(CreateTriangle(p18, p12, p17, DiffMat));
            models.Children.Add(CreateTriangle(p17, p12, p13, DiffMat));
            models.Children.Add(CreateTriangle(p17, p13, p16, DiffMat));
            models.Children.Add(CreateRectangle(p16, p13, p14, p15, DiffMat));

            //
            models.Children.Add(CreateRectangle(p0, p1, p11, p10, DiffMat));
            models.Children.Add(CreateRectangle(p1, p2, p12, p11, DiffMat));
            models.Children.Add(CreateRectangle(p2, p3, p13, p12, DiffMat));
            models.Children.Add(CreateRectangle(p3, p4, p14, p13, DiffMat));
            models.Children.Add(CreateRectangle(p4, p5, p15, p14, DiffMat));
            models.Children.Add(CreateRectangle(p5, p6, p16, p15, DiffMat));
            models.Children.Add(CreateRectangle(p6, p7, p17, p16, DiffMat));
            models.Children.Add(CreateRectangle(p7, p8, p18, p17, DiffMat));
            models.Children.Add(CreateRectangle(p8, p9, p19, p18, DiffMat));
            models.Children.Add(CreateRectangle(p9, p0, p10, p19, DiffMat));

            return models;
        }

        //--------------------------------------------------------------------------------------------
        public GeometryModel3D CreateRectangle(
              Point3D point1, Point3D point2,
              Point3D point3, Point3D point4,
              DiffuseMaterial DiffMat)
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

            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));

            return new GeometryModel3D(mesh, DiffMat);
        }

        //--------------------------------------------------------------------------------------------
        public GeometryModel3D CreateTriangle(
              Point3D point1, Point3D point2,
              Point3D point3,
              DiffuseMaterial DiffMat)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(point1);
            mesh.Positions.Add(point2);
            mesh.Positions.Add(point3);


            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));

            return new GeometryModel3D(mesh, DiffMat);
        }
        //--------------------------------------------------------------------------------------------
        private void LookAt(PerspectiveCamera camera, Point3D lookAtPoint)
        {
            camera.LookDirection = lookAtPoint - camera.Position;
        }
    }
}

