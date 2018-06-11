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
using sw_en_GUI.EXAMPLES._3D;

namespace PFD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CModel model = new CModel();

            // Load Example Model Data
            model = new CExample_3D_901_PF();

            // Create 3D window
            Page3Dmodel page1 = new Page3Dmodel(model);

            // Display example model in 3D preview frame
            Frame1.Content = page1;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
