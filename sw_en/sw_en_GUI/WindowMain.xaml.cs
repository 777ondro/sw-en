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
using Microsoft.Win32;
using CENEX;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace sw_en_GUI
{
	/// <summary>
	/// Interaction logic for WindowMain.xaml
	/// </summary>
	public partial class WindowMain : Window
	{
		public WindowMain()
		{
			InitializeComponent();
		}

		
		private void menuItemNew_Click(object sender, RoutedEventArgs e)
		{
			//mdiWindow.Content = new Window2();
			//Window2 win1 = new Window2();
			//win1.Owner = this;
			//win1.Show();
			//Window2 win2 = new Window2();
			//win2.Owner = this;
			//win2.Show();
		}

		private void menuItemSave_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.DefaultExt = ".cnx";
			sfd.Filter = "Cenex binary documents (.cnx)|*.cnx";
			if (sfd.ShowDialog() == true)
			{
				string filename = sfd.FileName;
				CTest2 test2 = new CTest2();
				FileStream fs = new FileStream(filename, FileMode.Create);
				// Create a BinaryFormatter object to perform the serialization
				BinaryFormatter bf = new BinaryFormatter();
				// Use the BinaryFormatter object to serialize the data to the file
				bf.Serialize(fs, test2);
				// Close the file
				fs.Close();
			}
		}

		private void menuItemOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".cnx";
			ofd.Filter = "Cenex binary documents (.cnx)|*.cnx";
			if (ofd.ShowDialog() == true)
			{
				try
				{
					// Open file from which to read the data
					FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
					// Create a BinaryFormatter object to perform the deserialization
					BinaryFormatter bf = new BinaryFormatter();
					// Create the object to store the deserialized data
					CTest2 test2 = (CTest2)bf.Deserialize(fs);
					// Close the file
					fs.Close();
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
		}

		
	}
}
