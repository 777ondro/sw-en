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
using WPF.MDI;
using SharedLibraries.EXPIMP;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using BaseClasses;

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
			Container.Children.CollectionChanged += (o, e) => Menu_RefreshWindows();

			Container.Children.Add(new MdiChild { Content = (UIElement)new Window2().Content, Title = "Window " + (Container.Children.Count + 1) });
		}

		/// <summary>
		/// Refresh windows list
		/// </summary>
		void Menu_RefreshWindows()
		{
			WindowsMenu.Items.Clear();
			MenuItem mi;
			for (int i = 0; i < Container.Children.Count; i++)
			{
				MdiChild child = Container.Children[i];
				mi = new MenuItem { Header = child.Title };
				mi.Click += (o, e) => child.Focus();
				WindowsMenu.Items.Add(mi);
			}
			WindowsMenu.Items.Add(new Separator());
			WindowsMenu.Items.Add(mi = new MenuItem { Header = "Cascade" });
			mi.Click += (o, e) => Container.MdiLayout = MdiLayout.Cascade;
			WindowsMenu.Items.Add(mi = new MenuItem { Header = "Horizontally" });
			mi.Click += (o, e) => Container.MdiLayout = MdiLayout.TileHorizontal;
			WindowsMenu.Items.Add(mi = new MenuItem { Header = "Vertically" });
			mi.Click += (o, e) => Container.MdiLayout = MdiLayout.TileVertical;

			WindowsMenu.Items.Add(new Separator());
			WindowsMenu.Items.Add(mi = new MenuItem { Header = "Close all" });
			mi.Click += (o, e) => Container.Children.Clear();
		}


		private void menuItemNew_Click(object sender, RoutedEventArgs e)
		{
			Container.Children.Add(new MdiChild { Content = (UIElement)new Window2().Content, Title = "Window " + (Container.Children.Count + 1) });
		}

		private void menuItemSave_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.DefaultExt = ".cnx";
			sfd.Filter = "Cenex binary documents (.cnx)|*.cnx";
			if (sfd.ShowDialog() == true)
			{
				try
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
				catch (Exception ex)
				{
					if (!EventLog.SourceExists("sw_en"))
					{
						EventLog.CreateEventSource("sw_en", "Application");
					}
					EventLog.WriteEntry("sw_en", ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
				}
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
				catch (Exception ex)
				{
					if (!EventLog.SourceExists("sw_en"))
					{
						EventLog.CreateEventSource("sw_en", "Application");
					}
					EventLog.WriteEntry("sw_en", ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
				}
			}
		}

		private void menuItemView3Dview_Click(object sender, RoutedEventArgs e)
		{
			Window2 win2 = new Window2();
			win2.ShowDialog();
		}

		private void ButtonImport_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".xlsx";
			ofd.Filter = "Excel documents(.xlsx, .xls)|*.xlsx;*.xls";
			if (ofd.ShowDialog() == true)
			{
				try
				{
					DataSet ds = CImportFromExcel.ImportFromExcel(ofd.FileName);
					dataGridNodes.ItemsSource = ds.Tables[0].DefaultView;
					dataGridNodes.DataContext = ds;
					dataGridMaterials.ItemsSource = ds.Tables[1].DefaultView;
					dataGridMaterials.DataContext = ds;
					dataGridCrossSections.ItemsSource = ds.Tables[2].DefaultView;
					dataGridCrossSections.DataContext = ds;
					dataGridMemberReleases.ItemsSource = ds.Tables[3].DefaultView;
					dataGridMemberReleases.DataContext = ds;
					dataGridMemberEccentricities.ItemsSource = ds.Tables[4].DefaultView;
					dataGridMemberEccentricities.DataContext = ds;
					dataGridMemberDivisions.ItemsSource = ds.Tables[5].DefaultView;
					dataGridMemberDivisions.DataContext = ds;
					dataGridNodalSupport.ItemsSource = ds.Tables[6].DefaultView;
					dataGridNodalSupport.DataContext = ds;
					dataGridMemberElasticFoundations.ItemsSource = ds.Tables[7].DefaultView;
					dataGridMemberElasticFoundations.DataContext = ds;
					initModelObject(ofd.FileName);
				}
				catch (Exception ex)
				{
					if (!EventLog.SourceExists("sw_en"))
					{
						EventLog.CreateEventSource("sw_en", "Application");
					}
					EventLog.WriteEntry("sw_en", ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
				}
			}
		}

		private void initModelObject(string fileName)
		{
			CModel model = new CModel(fileName);
			model.m_arrNodes = getNodes(((DataSet)dataGridNodes.DataContext).Tables[0]);
		}

		private CNode[] getNodes(DataTable dt)
		{
			List<CNode> nodes = new List<CNode>();
			CNode node = null;

			int Node_ID;
			float Coord_X;
			float Coord_Y;
			float Coord_Z;
			int fTime = 100;

			foreach (DataRow row in dt.Rows)
			{
				try
				{
					int.TryParse(row["NodeID"].ToString(), out Node_ID);
					float.TryParse(row["NodeCoordinateX"].ToString(), out Coord_X);
					float.TryParse(row["NodeCoordinateY"].ToString(), out Coord_Y);
					float.TryParse(row["NodeCoordinateZ"].ToString(), out Coord_Z);

					node = new CNode(Node_ID, Coord_X, Coord_Y, Coord_Z, fTime);
				}
				catch (Exception)
				{
					throw;
				}
				
			}


			return nodes.ToArray();
		}

		private void ButtonExport_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.DefaultExt = ".xlsx";
			sfd.Filter = "Excel documents(.xlsx)|*.xlsx|Excel documents(.xls)|*.xls";
			if (sfd.ShowDialog() == true)
			{
				try
				{
					ArrayList a = new ArrayList();
					a.Add(dataGridNodes.DataContext);
					a.Add(sfd.FileName);
					ThreadPool.QueueUserWorkItem(exportData, (object)a);
				}
				catch (ArgumentNullException ex)
				{
					if (!EventLog.SourceExists("sw_en"))
					{
						EventLog.CreateEventSource("sw_en", "Application");
					}
					EventLog.WriteEntry("sw_en", ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);

				}
				catch (Exception ex)
				{
					if (!EventLog.SourceExists("sw_en"))
					{
						EventLog.CreateEventSource("sw_en", "Application");
					}
					EventLog.WriteEntry("sw_en", ex.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
				}

			}
		}

		public static void exportData(object obj)
		{
			ArrayList args = (ArrayList)obj;
			CExportToExcel.ExportToExcel(args[0] as DataSet, args[1] as string);
		}

		private void menuItemView2Dview_Click(object sender, RoutedEventArgs e)
		{
			WindowPaint p = new WindowPaint();
			p.ShowDialog();
		}






	}
}
