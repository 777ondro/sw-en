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


					//ArrayList a = new ArrayList();
					//a.Add(dataGridNodes.DataContext);
					//a.Add(sfd.FileName);
					//a.Add("Nodes");
					//ThreadPool.QueueUserWorkItem(exportData, (object) a);
					//ArrayList a1 = new ArrayList();
					//a1.Add(dataGridMaterials.DataContext);
					//a1.Add(sfd.FileName);
					//a1.Add("Materials");
					//ThreadPool.QueueUserWorkItem(exportData, (object)a1);
					//ArrayList a2 = new ArrayList();
					//a2.Add(dataGridCrossSections.DataContext);
					//a2.Add(sfd.FileName);
					//a2.Add("Cross Sections");
					//ThreadPool.QueueUserWorkItem(exportData, (object)a2);
					//ArrayList a3 = new ArrayList();
					//a3.Add(dataGridMemberReleases.DataContext);
					//a3.Add(sfd.FileName);
					//a3.Add("Member Releases");
					//ThreadPool.QueueUserWorkItem(exportData, (object)a3);
					//ArrayList a4 = new ArrayList();
					//a4.Add(dataGridMemberEccentricities.DataContext);
					//a4.Add(sfd.FileName);
					//a4.Add("Member Eccentricities");
					//ThreadPool.QueueUserWorkItem(exportData, (object)a4);
					//ArrayList a5 = new ArrayList();
					//a5.Add(dataGridMemberDivisions.DataContext);
					//a5.Add(sfd.FileName);
					//a5.Add("Members Divisions");
					//ThreadPool.QueueUserWorkItem(exportData, (object)a5);
					//ArrayList a6 = new ArrayList();
					//a6.Add(dataGridNodalSupport.DataContext);
					//a6.Add(sfd.FileName);
					//a6.Add("Nodal Support");
					//ThreadPool.QueueUserWorkItem(exportData, (object)a6);

					//CExportToExcel.ExportToExcel(dataGridNodes.DataContext as DataTable, sfd.FileName, "Nodes");
					//CExportToExcel.ExportToExcel(dataGridMaterials.DataContext as DataTable, sfd.FileName, "Materials");
					//CExportToExcel.ExportToExcel(dataGridCrossSections.DataContext as DataTable, sfd.FileName, "Cross Sections");
					//CExportToExcel.ExportToExcel(dataGridMemberReleases.DataContext as DataTable, sfd.FileName, "Member Releases");
					//CExportToExcel.ExportToExcel(dataGridMemberEccentricities.DataContext as DataTable, sfd.FileName, "Member Eccentricities");
					//CExportToExcel.ExportToExcel(dataGridMemberDivisions.DataContext as DataTable, sfd.FileName, "Members Divisions");
					//CExportToExcel.ExportToExcel(dataGridNodalSupport.DataContext as DataTable, sfd.FileName, "Nodal Support");
					//CExportToExcel.ExportToExcel(dataGridMemberElasticFoundations.DataContext as DataTable, sfd.FileName, "Members Elastic Foundations");

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
