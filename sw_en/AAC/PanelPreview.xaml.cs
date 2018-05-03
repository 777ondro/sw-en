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
using MATH;

namespace AAC
{
    /// <summary>
    /// Interaction logic for PanelPreview.xaml
    /// </summary>
    public partial class PanelPreview : Page
    {
        AAC_Panel obj_panel = new AAC_Panel();

        //--------------------------------------------------------------------------------------------
        public PanelPreview(AAC_Panel panel)
        {
            obj_panel = panel;

            InitializeComponent();

            CreatePreviewModel();
        }

        //--------------------------------------------------------------------------------------------
        public void CreatePreviewModel()
        {
            // Default view
            Point3D lookAtPoint = new Point3D(1, 0, -0.5);

            _trackport.PerspectiveCamera.Position = new Point3D(-0.75, 0.4, -0.1);
            LookAt(_trackport.PerspectiveCamera, lookAtPoint);

            //OrthographicCamera camera = new OrthographicCamera(); // Doplnit moznost zobrazit v pravouhlom priemietani

            // Light Direction
            _trackport.Light.Direction = new Vector3D(-1, -10, 0);

            // Define a lighting model
            //DirectionalLight qLight = new DirectionalLight();
            //qLight.Color = Colors.White;
            //qLight.Direction = new Vector3D(-0.5, -0.25, -0.5);

            Model3DGroup model = new Model3DGroup();

            EGCS eGCS = EGCS.eGCSLeftHanded;

            //!!!!!!! POZOR PRIEHLADNOST ZAVISI NA PORADI VYKRESLOVANIA OBJEKTOV!!!!!!!!!
            bool bIsReinfocementSurfaceTransparent = false;
            bool bIsPanelSurfaceTransparent = false;

            // Reinforcement
            SolidColorBrush brRein_1 = new SolidColorBrush(Color.FromRgb(255, 0, 0)); // Material color - Front Side
            SolidColorBrush brRein_2 = new SolidColorBrush(Color.FromRgb(200, 0, 0)); // Material color - Shell
            SolidColorBrush brRein_3 = new SolidColorBrush(Color.FromRgb(255, 0, 0)); // Material color - Back Side

            double y1_lower = 0.102;

            for(int i = 0; i < obj_panel.Long_Bottom_Bars_Array.Length; i++)
              model.Children.Add(obj_panel.Long_Bottom_Bars_Array[i].GetMemberModel(eGCS, bIsReinfocementSurfaceTransparent, new Point3D(obj_panel.fc_1, y1_lower + i * obj_panel.fsl_lower, obj_panel.fc_1 + 0.5 * obj_panel.fd_long_lower), new Point3D(obj_panel.Long_Bottom_Bars_Array[0].fL - obj_panel.fc_1, y1_lower + i * obj_panel.fsl_lower, obj_panel.fc_1 + 0.5 * obj_panel.fd_long_lower), obj_panel.Long_Bottom_Bars_Array[i].Cross_Section, brRein_1, brRein_2, brRein_3, null));

            double y2_upper = 0.06;
            for (int i = 0; i < obj_panel.Long_Upper_Bars_Array.Length; i++)
                model.Children.Add(obj_panel.Long_Upper_Bars_Array[i].GetMemberModel(eGCS, bIsReinfocementSurfaceTransparent, new Point3D(obj_panel.fc_2, y2_upper + i * obj_panel.fsl_upper, obj_panel.Cross_Section.Fh - obj_panel.fc_2 - 0.5 *obj_panel.fd_long_upper), new Point3D(obj_panel.Long_Upper_Bars_Array[0].fL - obj_panel.fc_2, y2_upper + i * obj_panel.fsl_upper, obj_panel.Cross_Section.Fh - obj_panel.fc_2 - 0.5 * obj_panel.fd_long_upper), obj_panel.Long_Upper_Bars_Array[i].Cross_Section, brRein_1, brRein_2, brRein_3, null));

            //double[] trans_rein_position_x_array = new double[obj_panel.number_trans_lower_bars];
            double x1 = 0.03;
            double x2 = 0.10;
            double x3 = 0.32;
            double x4 = 0.50;

            double[] trans_rein_position_x_array = new double[] {
                x1,
                x1 + x2,
                x1 + 2 * x2,
                x1 + 3 * x2,
                x1 + 4 * x2,
                x1 + 4 * x2 + x3,
                x1 + 4 * x2 + x3 + x4,
                x1 + 4 * x2 + x3 + 2 * x4,
                x1 + 4 * x2 + x3 + 3 * x4,
                x1 + 4 * x2 + x3 + 4 * x4,
                x1 + 4 * x2 + x3 + 5 * x4,
                x1 + 4 * x2 + x3 + 6 * x4,
                x1 + 4 * x2 + x3 + 7 * x4,
                x1 + 4 * x2 + x3 + 7 * x4 + x3,
                x1 + 4 * x2 + x3 + 7 * x4 + x3 + x2,
                x1 + 4 * x2 + x3 + 7 * x4 + x3 + 2 * x2,
                x1 + 4 * x2 + x3 + 7 * x4 + x3 + 3 * x2,
                x1 + 4 * x2 + x3 + 7 * x4 + x3 + 4 * x2
            };

            double trans_lower_start = y1_lower - obj_panel.fc_trans_lower;
            double l_trans_lower = (obj_panel.number_long_lower_bars - 1) * obj_panel.fsl_lower + 2 * obj_panel.fc_trans_lower;

            double trans_upper_start = y2_upper - obj_panel.fc_trans_upper;
            double l_trans_upper = (obj_panel.number_long_upper_bars - 1) * obj_panel.fsl_upper + 2 * obj_panel.fc_trans_upper;

            for (int i = 0; i < obj_panel.Trans_Bottom_Bars_Array.Length; i++)
                model.Children.Add(obj_panel.Trans_Bottom_Bars_Array[i].GetMemberModel(eGCS, bIsReinfocementSurfaceTransparent, new Point3D(trans_rein_position_x_array[i], trans_lower_start, obj_panel.fc_1 + obj_panel.fd_long_lower + 0.5 * obj_panel.fd_trans_lower), new Point3D(trans_rein_position_x_array[i], trans_lower_start + l_trans_lower, obj_panel.fc_1 + obj_panel.fd_long_lower + 0.5 * obj_panel.fd_trans_lower), obj_panel.Trans_Bottom_Bars_Array[i].Cross_Section, brRein_1, brRein_2, brRein_3, null));

            for (int i = 0; i < obj_panel.Trans_Upper_Bars_Array.Length; i++)
                model.Children.Add(obj_panel.Trans_Upper_Bars_Array[i].GetMemberModel(eGCS, bIsReinfocementSurfaceTransparent, new Point3D(trans_rein_position_x_array[i], trans_upper_start, obj_panel.Cross_Section.Fh - obj_panel.fc_2 - obj_panel.fd_long_upper - 0.5 * obj_panel.fd_trans_upper), new Point3D(trans_rein_position_x_array[i], trans_upper_start + l_trans_upper, obj_panel.Cross_Section.Fh - obj_panel.fc_2 - obj_panel.fd_long_upper - 0.5 * obj_panel.fd_trans_upper), obj_panel.Trans_Upper_Bars_Array[i].Cross_Section, brRein_1, brRein_2, brRein_3, null));

            // Panel
            SolidColorBrush br1 = new SolidColorBrush(Color.FromRgb(130, 130, 130));  // Material color - Front Side
            SolidColorBrush br2 = new SolidColorBrush(Color.FromRgb(210, 210, 210));  // Material color - Shell
            SolidColorBrush br3 = new SolidColorBrush(Color.FromRgb(131, 131, 131));  // Material color - Back Side

            br1.Opacity = 0.5;
            br2.Opacity = 0.7;
            br3.Opacity = 0.5;

            DiffuseMaterial  qDiffTrans = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(130, 130, 130, 1)));
            SpecularMaterial qSpecTrans = new SpecularMaterial(new SolidColorBrush(Color.FromArgb(210, 210, 210, 210)), 90.0);

            MaterialGroup qOuterMaterial = new MaterialGroup();
            qOuterMaterial.Children.Add(qDiffTrans);
            qOuterMaterial.Children.Add(qSpecTrans);

            model.Children.Add(obj_panel.GetMemberModel(eGCS, bIsPanelSurfaceTransparent, new Point3D(0, 0, 0), new Point3D(obj_panel.fL, 0, 0), obj_panel.Cross_Section, br1, br2, br3, qOuterMaterial));

            RotateTransform3D myRotateTransform3D = new RotateTransform3D();
            AxisAngleRotation3D rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 270);
            myRotateTransform3D.Rotation = rotation;
            model.Transform = myRotateTransform3D;

            _trackport.Model = (Model3D)model;
            _trackport.Trackball.TranslateScale = 1000;   //step for moving object (panning)
            _trackport.SetupScene();
        }


        //--------------------------------------------------------------------------------------------
        private void LookAt(PerspectiveCamera camera, Point3D lookAtPoint)
        {
            camera.LookDirection = lookAtPoint - camera.Position;
        }
    }
}

