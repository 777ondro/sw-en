﻿using System;
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
using CRSC;
using FEM_CALC_1Din3D;
using _3DTools;

namespace sw_en_GUI
{
	/// <summary>
	/// Interaction logic for WindowMain.xaml
	/// </summary>
	public partial class WindowMain : Window
	{
		private CModel model = null;

		List<Trackport3D> list_trackports;
		public WindowMain()
		{
			InitializeComponent();
			Container.Children.CollectionChanged += (o, e) => Menu_RefreshWindows();
			Window2 win = new Window2();
			list_trackports = new List<Trackport3D>();
			list_trackports.Add(win._trackport);

			Container.Children.Add(new MdiChild { Content = (UIElement)win.Content, Title = "Window " + (Container.Children.Count + 1) });

			
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
			Window2 win = new Window2();
			list_trackports.Add(win._trackport);

			Container.Children.Add(new MdiChild { Content = (UIElement)win.Content, Title = "Window " + (Container.Children.Count + 1) });
			//Container.Children.Add(new MdiChild { Content = (UIElement)new Window1().Content, Title = "Window " + (Container.Children.Count + 1) });
		}

		private void menuItemSave_Click(object sender, RoutedEventArgs e)
		{
			if (model != null)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.DefaultExt = ".cnx";
				sfd.Filter = "Cenex binary documents (.cnx)|*.cnx";
				if (sfd.ShowDialog() == true)
				{
					try
					{
						string filename = sfd.FileName;
						FileStream fs = new FileStream(filename, FileMode.Create);
						// Create a BinaryFormatter object to perform the serialization
						BinaryFormatter bf = new BinaryFormatter();
						// Use the BinaryFormatter object to serialize the data to the file
						bf.Serialize(fs, model);
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
			else 
			{
				MessageBox.Show("Model not initialized. Import data first!!!");
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
					model = (CModel)bf.Deserialize(fs);
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
                // Osetrit nacitavanie dat z prazdnych naformatovanych riadkov a pod
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
					//dataGridMemberElasticFoundations.ItemsSource = ds.Tables[7].DefaultView;
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
			model = new CModel(fileName);
			model.m_arrNodes = getNodes(((DataSet)dataGridNodes.DataContext).Tables[0]);
			//model.m_arrMat materials are in CENEX project  Class should be moved to BaseClassess project. 
			model.m_arrCrSc = getCrossSections(((DataSet)dataGridNodes.DataContext).Tables[2]);
			//model.m_arrNReleases 
			//member Eccentricities  
			model.m_arrMembers = getMembers(((DataSet)dataGridNodes.DataContext).Tables[5]);

            // Set properties to the Member (nodes, crsc)

            for(int i = 0; i < model.m_arrMembers.Length; i++)
            {
                // Nodes
                model.m_arrMembers[i].NodeStart = model.m_arrNodes[model.m_arrMembers[i].NodeStart.INode_ID-1];
                model.m_arrMembers[i].NodeEnd   = model.m_arrNodes[model.m_arrMembers[i].NodeEnd.INode_ID-1];

                // Set Cross-section
                model.m_arrMembers[i].CrSc = model.m_arrCrSc[model.m_arrMembers[i].CrSc.ICrSc_ID];

                // Temp - nacitava sa z tabulky alebo z databazy, dopracovat
                // Parametre pre IPN 300
                model.m_arrMembers[i].CrSc = new CCrSc_3_00(0, 8, 0.300f, 0.125f, 0.0162f, 0.0108f, 0.0108f, 0.0065f, 0.2416f);
                model.m_arrMembers[i].CrSc.FI_t  = 5.69e-07f;
                model.m_arrMembers[i].CrSc.FI_y  = 9.79e-05f;
                model.m_arrMembers[i].CrSc.FI_z  = 4.49e-06f;
                model.m_arrMembers[i].CrSc.FA_g  = 6.90e-03f;
                model.m_arrMembers[i].CrSc.FA_vy = 4.01e-03f;
                model.m_arrMembers[i].CrSc.FA_vz = 2.89e-03f;
            }

			model.m_arrNSupports = getNSupports(((DataSet)dataGridNodes.DataContext).Tables[6]);
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
					nodes.Add(node);
				}
				catch (Exception)
				{
					throw;
				}
			}

			//foreach (CNode n in nodes) 
			//{
			//    Console.WriteLine(string.Format("{0},{1},{2},{3},{4}", 
			//        n.INode_ID, n.FCoord_X, n.FCoord_Y, n.FCoord_Z, n.FTime));
			//}
			return nodes.ToArray();
		}

		private CMember[] getMembers(DataTable dt)
		{
			List<CMember> members = new List<CMember>();
			CMember member = null;

			int Line_ID;
			CNode Node1;
			int node1ID;
			CNode Node2;
			int node2ID;
            float fRotation;
            CRSC.CCrSc_3_00 CrSc1; // Temp
            int iCrSc1ID;
            CRSC.CCrSc_3_00 CrSc2; // Temp
            int iCrSc2ID;
            // ReleaseStartID
            // ReleaseEndID
			int Time = 100;
			float Length;

			foreach (DataRow row in dt.Rows)
			{
				try
				{
                    // Nodes
					int.TryParse(row["MemberID"].ToString(), out Line_ID);
					Node1 = new CNode();
					int.TryParse(row["NodeStartID"].ToString(), out node1ID);
					Node1.INode_ID = node1ID;

					Node2 = new CNode();
					int.TryParse(row["NodeEndID"].ToString(), out node2ID);
					Node2.INode_ID = node2ID;

                    //Cross-sections
                    CrSc1 = new CRSC.CCrSc_3_00();
                    int.TryParse(row["CrossSectionStartID"].ToString(), out iCrSc1ID);
                    CrSc1.ICrSc_ID = iCrSc1ID;

                    CrSc2 = new CRSC.CCrSc_3_00();
                    int.TryParse(row["CrossSectionEndID"].ToString(), out iCrSc2ID);
                    CrSc2.ICrSc_ID = iCrSc2ID;

                    // Create member
					member = new CMember(Line_ID, Node1, Node2, CrSc1, Time);

					float.TryParse(row["Length"].ToString(), out Length);
					member.FLength = Length;

					members.Add(member);
				}
				catch (Exception)
				{
					throw;
				}
			}

			foreach (CMember m in members)
			{
				Console.WriteLine(string.Format("IMember_ID: {0},NodeStart.INode_ID: {1},NodeEnd.INode_ID: {2},m.FLength: {3}",
					m.IMember_ID, m.NodeStart.INode_ID, m.NodeEnd.INode_ID, m.FLength));
			}
			return members.ToArray();
		}


		private CRSC.CCrSc[] getCrossSections(DataTable dt)
		{
            List<CRSC.CCrSc> list_crsc = new List<CRSC.CCrSc>();
            CRSC.CCrSc crsc = null;

			int CrSc_ID;
			float fI_t, fI_y, fI_z, fA_g, fA_vy, fA_vz;

			foreach (DataRow row in dt.Rows)
			{
				try
				{

                    crsc = new CCrSc_3_00(0, 8, 300, 125, 16.2f, 10.8f, 10.8f, 6.5f, 241.6f); // I 300 section
                    //crsc = new CCrSc_0_05(200.0f, 100.0f);

					int.TryParse(row["MaterialID"].ToString(), out CrSc_ID);
					crsc.ICrSc_ID = CrSc_ID;

					float.TryParse(row["fI_t"].ToString(), out fI_t);
					crsc.FI_t = fI_t;

					float.TryParse(row["fI_y"].ToString(), out fI_y);
					crsc.FI_y = fI_y;

					float.TryParse(row["fI_z"].ToString(), out fI_z);
					crsc.FI_z = fI_z;

					float.TryParse(row["fA_g"].ToString(), out fA_g);
					crsc.FA_g = fA_g;

                    float.TryParse(row["fA_vy"].ToString(), out fA_vy);
                    crsc.FA_vy = fA_vy;

                    float.TryParse(row["fA_vz"].ToString(), out fA_vz);
                    crsc.FA_vz = fA_vz;

					list_crsc.Add(crsc);
				}
				catch (Exception)
				{
					throw;
				}
			}
			return list_crsc.ToArray();
		}


		private CNSupport[] getNSupports(DataTable dt)
		{
			List<CNSupport> nsupports = new List<CNSupport>();
			CNSupport nsupport = null;

			int eNDOF = 0;
			int iSupport_ID;
			List<int> nodeCollection;
			bool[] bRestrain = new bool[6];
			int fTime = 100;
			bool bux, buy, buz, brx, bry, brz;

			foreach (DataRow row in dt.Rows)
			{
				try
				{
					nodeCollection = new List<int>();
					nsupport = new CNSupport(eNDOF);
					int.TryParse(row["NSupportID"].ToString(), out iSupport_ID);
					nsupport.ISupport_ID = iSupport_ID;
					foreach( string s in row["NodesIDCollection"].ToString().Split(','))
					{
						nodeCollection.Add(int.Parse(s));
					}
					nsupport.m_iNodeCollection = nodeCollection.ToArray();

					bool.TryParse(row["bux"].ToString(), out bux);
					bRestrain[0] = bux;

					bool.TryParse(row["buy"].ToString(), out buy);
					bRestrain[1] = buy;

					bool.TryParse(row["buz"].ToString(), out buz);
					bRestrain[2] = buz;

					bool.TryParse(row["brx"].ToString(), out brx);
					bRestrain[3] = brx;

					bool.TryParse(row["bry"].ToString(), out bry);
					bRestrain[4] = bry;

					bool.TryParse(row["brz"].ToString(), out brz);
					bRestrain[5] = brz;
					nsupport.m_bRestrain = bRestrain;
					nsupport.m_fTime = fTime;

					nsupports.Add(nsupport);
				}
				catch (Exception)
				{
					throw;
				}
			}
			return nsupports.ToArray();
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

		private void Container_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.LeftCtrl:
					foreach (Trackport3D t in list_trackports)
					{
						t.Trackball.IsCtrlDown = e.IsDown;
					}
					break;
			}
		}

		private void Container_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.LeftCtrl: 
					foreach (Trackport3D t in list_trackports) 
					{
						t.Trackball.IsCtrlDown = e.IsDown;
					}
					break;
			}
		}

		private void menuItemViewShowModel_Click(object sender, RoutedEventArgs e)
		{
			Window2 win = new Window2(model);
			list_trackports.Add(win._trackport);

			Container.Children.Add(new MdiChild { Content = (UIElement)win.Content, Title = "Window " + (Container.Children.Count + 1) });
		}

        private void menuItemCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Create calculation object and run calculation
            // FEM_CALC_1Din3D.CFEM_CALC obj_Calc = new CFEM_CALC(model); // Nove vypoctove jadro
            FEM_CALC_1Din3D.CFEM_CALC obj_Calc = new CFEM_CALC();         // Povodny priklad - zadanie c 4






            // Auxialiary string - result data

            int iDispDecPrecision = 3; // Precision of numerical values of displacement and rotations
            string sDOFResults = null;

            for (int i = 0; i < obj_Calc.m_V_Displ.FVectorItems.Length; i++)
            {
                int iNodeNumber = obj_Calc.m_fDisp_Vector_CN[i, 1] + 1; // Incerase index (1st member "0" to "1"
                int iNodeDOFNumber = obj_Calc.m_fDisp_Vector_CN[i, 2] + 1;

                sDOFResults += "Node No:" + "\t" + iNodeNumber + "\t" +
                               "Node DOF No:" + "\t" + iNodeDOFNumber + "\t" +
                               "Value:" + "\t" + String.Format("{0:0.000}", Math.Round(obj_Calc.m_V_Displ.FVectorItems[i], iDispDecPrecision))
                               + "\n";
            }

            // Main String
            string sMessageCalc =
                "Calculation was successful!" + "\n\n" +
                "Result - vector of calculated values of unrestraint DOF displacement or rotation" + "\n\n" + sDOFResults;

            // Display Message
            MessageBox.Show(sMessageCalc, "Solver Message",MessageBoxButton.OK);
        }






	}
}
