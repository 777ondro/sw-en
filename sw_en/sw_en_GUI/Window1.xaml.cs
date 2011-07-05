using System;
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

namespace sw_en_GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            LookAt((PerspectiveCamera)viewPort1.Camera, new Point3D(0, 0, 0));
            CConsoleHelper.Create();
        }

        private void LookAt(PerspectiveCamera camera, Point3D lookAtPoint)
        {
            camera.LookDirection = lookAtPoint - camera.Position;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down: Console.WriteLine("Down"); break;
                case Key.Up: Console.WriteLine("Up");  break;
                case Key.Left: Console.WriteLine("Left"); break;
                case Key.Right: Console.WriteLine("Right");  break;
            }
        }



        private void drawNodes() 
        {
            CTest8 test8 = new CTest8();
            CNode[] nodes = test8.arrNodes;
            //viewPort1.Children.
        }

        
    }
}
