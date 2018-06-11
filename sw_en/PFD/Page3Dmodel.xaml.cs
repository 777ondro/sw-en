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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaseClasses;
using sw_en_GUI;
using System.Windows.Media.Media3D;

namespace PFD
{
    /// <summary>
    /// Interaction logic for Page3Dmodel.xaml
    /// </summary>
    public partial class Page3Dmodel : Page
    {
        public Page3Dmodel(CModel model)
        {
            InitializeComponent();

            bool bDebugging = false;

            // Create 3D window
            Window2 win1 = new Window2(model, bDebugging);

            // Default view
            Point3D lookAtPoint = new Point3D(1, 0, -0.5);
            Point3D camera_position;

            camera_position = new Point3D(-0.8, 0.4, -0.1); // FrontSide of Panel

            _trackport.PerspectiveCamera.Position = camera_position;
            LookAt(_trackport.PerspectiveCamera, lookAtPoint);

            //OrthographicCamera camera = new OrthographicCamera(); // Doplnit moznost zobrazit v pravouhlom priemietani

            // Light Direction
            _trackport.Light.Direction = new Vector3D(-1, -10, 0);

            Model3DGroup gr = new Model3DGroup();

            gr = win1.gr;

            _trackport.Model = (Model3D)gr;
            _trackport.Trackball.TranslateScale = 1000;   //step for moving object (panning)
            _trackport.SetupScene();
        }
    }
}
