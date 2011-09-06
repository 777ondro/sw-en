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
			imageButton00.Source = (ImageSource)TryFindResource("GEN_F_00");
			imageButton01.Source = (ImageSource)TryFindResource("GEN_F_01");
			imageButton02.Source = (ImageSource)TryFindResource("GEN_F_02");
			imageButton03.Source = (ImageSource)TryFindResource("GEN_F_03");
			imageButton04.Source = (ImageSource)TryFindResource("GEN_F_04");
			imageButton05.Source = (ImageSource)TryFindResource("GEN_F_05");
			imageButton10.Source = (ImageSource)TryFindResource("GEN_F_06");
			imageButton11.Source = (ImageSource)TryFindResource("GEN_F_07");
			imageButton12.Source = (ImageSource)TryFindResource("GEN_F_08");
			imageButton13.Source = (ImageSource)TryFindResource("GEN_F_20");
			imageButton14.Source = (ImageSource)TryFindResource("GEN_F_21");
			imageButton15.Source = (ImageSource)TryFindResource("GEN_F_22");
			imageButton20.Source = (ImageSource)TryFindResource("GEN_F_23");
			imageButton21.Source = (ImageSource)TryFindResource("GEN_F_24");
			imageButton22.Source = (ImageSource)TryFindResource("GEN_F_25");
			imageButton23.Source = (ImageSource)TryFindResource("GEN_F_26");
			imageButton24.Source = (ImageSource)TryFindResource("GEN_F_27");
			imageButton25.Source = (ImageSource)TryFindResource("GEN_F_28");
			imageButton30.Source = (ImageSource)TryFindResource("GEN_F_50");
			imageButton31.Source = (ImageSource)TryFindResource("GEN_F_51");
			imageButton32.Source = (ImageSource)TryFindResource("GEN_F_52");
			imageButton33.Source = (ImageSource)TryFindResource("GEN_F_53");
			imageButton34.Source = (ImageSource)TryFindResource("GEN_F_54");
			imageButton35.Source = (ImageSource)TryFindResource("GEN_F_55");
			imageButton40.Source = (ImageSource)TryFindResource("GEN_F_56");
			imageButton41.Source = (ImageSource)TryFindResource("GEN_F_57");
			imageButton42.Source = (ImageSource)TryFindResource("GEN_F_58");
			imageButton43.Source = (ImageSource)TryFindResource("GEN_F_59");
			imageButton44.Source = (ImageSource)TryFindResource("GEN_F_60");
			imageButton45.Source = (ImageSource)TryFindResource("GEN_F_61");


			imageButton67.Source = (ImageSource)TryFindResource("0_MASS");
			imageButton68.Source = (ImageSource)TryFindResource("0_THIN");


			//concrete
			imgBtnConcrete00.Source = (ImageSource)TryFindResource("CON_F_00");
			imgBtnConcrete01.Source = (ImageSource)TryFindResource("CON_F_01");
			imgBtnConcrete02.Source = (ImageSource)TryFindResource("CON_F_02");
			imgBtnConcrete03.Source = (ImageSource)TryFindResource("CON_F_03");
			imgBtnConcrete04.Source = (ImageSource)TryFindResource("CON_F_04");
			imgBtnConcrete05.Source = (ImageSource)TryFindResource("CON_F_05");
			imgBtnConcrete10.Source = (ImageSource)TryFindResource("CON_F_06");
			imgBtnConcrete11.Source = (ImageSource)TryFindResource("CON_F_07");
			imgBtnConcrete12.Source = (ImageSource)TryFindResource("CON_F_20");
			imgBtnConcrete13.Source = (ImageSource)TryFindResource("CON_F_21");
			imgBtnConcrete14.Source = (ImageSource)TryFindResource("CON_F_22");
			imgBtnConcrete15.Source = (ImageSource)TryFindResource("CON_F_23");
			imgBtnConcrete20.Source = (ImageSource)TryFindResource("CON_F_24");
			imgBtnConcrete21.Source = (ImageSource)TryFindResource("CON_F_25");
			imgBtnConcrete22.Source = (ImageSource)TryFindResource("CON_F_26");
			imgBtnConcrete23.Source = (ImageSource)TryFindResource("CON_F_27");
			imgBtnConcrete24.Source = (ImageSource)TryFindResource("CON_F_28");
			imgBtnConcrete25.Source = (ImageSource)TryFindResource("CON_F_40");
			imgBtnConcrete30.Source = (ImageSource)TryFindResource("CON_F_41");
			imgBtnConcrete31.Source = (ImageSource)TryFindResource("CON_F_42");
			imgBtnConcrete32.Source = (ImageSource)TryFindResource("CON_F_43");
			imgBtnConcrete33.Source = (ImageSource)TryFindResource("CON_F_44");
			imgBtnConcrete34.Source = (ImageSource)TryFindResource("CON_F_45");
			imgBtnConcrete35.Source = (ImageSource)TryFindResource("CON_F_46");
			imgBtnConcrete40.Source = (ImageSource)TryFindResource("CON_F_47");
			imgBtnConcrete41.Source = (ImageSource)TryFindResource("CON_F_48");
			imgBtnConcrete42.Source = (ImageSource)TryFindResource("CON_F_49");
			imgBtnConcrete43.Source = (ImageSource)TryFindResource("CON_F_50");
			imgBtnConcrete68.Source = (ImageSource)TryFindResource("0_MASS");
		}

		private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
		{
			if (listBoxMenu.SelectedItem != null)
			Console.WriteLine(listBoxMenu.SelectedItem.ToString());
			e.Handled = true;
		}
	}
}
