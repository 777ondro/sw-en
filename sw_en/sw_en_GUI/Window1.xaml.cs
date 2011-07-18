﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using CENEX;
using System.Windows.Media;

namespace sw_en_GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //--------------------------------------------------------------------------------------------
        public Window1()
        {
            InitializeComponent();

            Model3DGroup models = CreateCube();
            ModelVisual3D visual = new ModelVisual3D();
            visual.Content = models;
            myViewport3D.Children.Add(visual);

            Model3DGroup models1 = CreateCube(3, 3, 3);
            ModelVisual3D visual1 = new ModelVisual3D();
            visual1.Content = models1;
            myViewport3D.Children.Add(visual1);
            //models = CreateCube(2, 2, 2);
            //visual.Content = models;
            //myViewport3D.Children.Add(visual);

        }

        //--------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------
        public Model3DGroup CreateCube(double x, double y, double z) 
        {
            Model3DGroup models = new Model3DGroup();

            Point3D p0 = new Point3D(x-1, y-1, z-1);
            Point3D p1 = new Point3D(x+1, y-1, z-1);
            Point3D p2 = new Point3D(x+1, y-1, z+1);
            Point3D p3 = new Point3D(x-1, y-1, z+1);
            Point3D p4 = new Point3D(x-1, y+1, z-1);
            Point3D p5 = new Point3D(x+1, y+1, z-1);
            Point3D p6 = new Point3D(x+1, y+1, z+1);
            Point3D p7 = new Point3D(x-1, y+1, z+1);

            models.Children.Add(CreateRectangle(p3, p2, p6, p7, Brushes.Red));  // front
            models.Children.Add(CreateRectangle(p2, p1, p5, p6, Brushes.Red));  // right
            models.Children.Add(CreateRectangle(p1, p0, p4, p5, Brushes.Red));  // back
            models.Children.Add(CreateRectangle(p0, p3, p7, p4, Brushes.Red));  // left
            models.Children.Add(CreateRectangle(p7, p6, p5, p4, Brushes.Red));  // top
            models.Children.Add(CreateRectangle(p2, p3, p0, p1, Brushes.Red));  // bottom

            return models;
        }

        //--------------------------------------------------------------------------------------------
        public Model3DGroup CreateCube()
        {
            Model3DGroup models = new Model3DGroup();

            Point3D p0 = new Point3D(-1, -1, -1);
            Point3D p1 = new Point3D(1, -1, -1);
            Point3D p2 = new Point3D(1, -1, 1);
            Point3D p3 = new Point3D(-1, -1, 1);
            Point3D p4 = new Point3D(-1, 1, -1);
            Point3D p5 = new Point3D(1, 1, -1);
            Point3D p6 = new Point3D(1, 1, 1);
            Point3D p7 = new Point3D(-1, 1, 1);

            models.Children.Add(CreateRectangle(p3, p2, p6, p7, Brushes.Red));  // front
            models.Children.Add(CreateRectangle(p2, p1, p5, p6, Brushes.Red));  // right
            models.Children.Add(CreateRectangle(p1, p0, p4, p5, Brushes.Red));  // back
            models.Children.Add(CreateRectangle(p0, p3, p7, p4, Brushes.Red));  // left
            models.Children.Add(CreateRectangle(p7, p6, p5, p4, Brushes.Red));  // top
            models.Children.Add(CreateRectangle(p2, p3, p0, p1, Brushes.Red));  // bottom

            return models;
        }

        //--------------------------------------------------------------------------------------------
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




        //--------------------------------------------------------------------------------------------
        private void LookAt(PerspectiveCamera camera, Point3D lookAtPoint)
        {
            camera.LookDirection = lookAtPoint - camera.Position;
        }

        //--------------------------------------------------------------------------------------------
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Key.ToString());
            switch (e.Key)
            {
                case Key.Down: Console.WriteLine("Down"); break;
                case Key.Up: Console.WriteLine("Up");  break;
                case Key.Left: Console.WriteLine("Left"); break;
                case Key.Right: Console.WriteLine("Right");  break;
                case Key.Add: Console.WriteLine("Add"); zoomIn(); break;
                case Key.Subtract: Console.WriteLine("Subtract"); zoomOut(); break;
                
            }
        }

        //--------------------------------------------------------------------------------------------
        private void zoomIn() 
        {
            double z = myCamera.Position.Z;
            myCamera.Position = new Point3D(0, 0, z+1);
        }

        //--------------------------------------------------------------------------------------------
        private void zoomOut() 
        {
            double z = myCamera.Position.Z;
            myCamera.Position = new Point3D(0, 0, z - 1);
        }


        //--------------------------------------------------------------------------------------------
        private void drawNodes() 
        {
            CTest4 test4 = new CTest4();
            CNode[] nodes = test4.arrNodes;
            foreach (CNode c in nodes) 
            {
                Model3DGroup models = CreateCube(c.m_fCoord_X/100, c.m_fCoord_Y/100, c.m_fCoord_Z/100);
                ModelVisual3D visual = new ModelVisual3D();
                visual.Content = models;
                myViewport3D.Children.Add(visual);
            }
        }

        //--------------------------------------------------------------------------------------------
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0) zoomIn();
            else zoomOut();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            drawNodes();
        }

        
    }
}
