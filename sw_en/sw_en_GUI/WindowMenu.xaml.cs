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

namespace sw_en_GUI
{
	/// <summary>
	/// Interaction logic for WindowMenu.xaml
	/// </summary>
	public partial class WindowMenu : Window
	{
		public WindowMenu()
		{
			InitializeComponent();
			imageButton00.Source = (ImageSource)TryFindResource("GEN_F_00");
			imageButton01.Source = (ImageSource)TryFindResource("GEN_F_01");
			imageButton02.Source = (ImageSource)TryFindResource("GEN_F_02");
			imageButton10.Source = (ImageSource)TryFindResource("GEN_F_03");
			imageButton11.Source = (ImageSource)TryFindResource("GEN_F_04");
			imageButton12.Source = (ImageSource)TryFindResource("GEN_F_05");
			imageButton20.Source = (ImageSource)TryFindResource("GEN_F_06");
			imageButton21.Source = (ImageSource)TryFindResource("GEN_F_07");
			imageButton22.Source = (ImageSource)TryFindResource("GEN_F_08");
		}

		private void Button00_Click(object sender, RoutedEventArgs e)
		{
			Viewer3D view = new Viewer3D();
			view.Show();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
