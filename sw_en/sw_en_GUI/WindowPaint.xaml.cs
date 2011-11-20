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
	/// Interaction logic for WindowPaint.xaml
	/// </summary>
	public partial class WindowPaint : Window
	{
		public WindowPaint()
		{
			InitializeComponent();
			//myDrawing.Geometry.
			
			DrawingVisual ghostVisual = new DrawingVisual();
			using (DrawingContext dc = ghostVisual.RenderOpen())
			{
				// The body
				dc.DrawGeometry(Brushes.Blue, null, Geometry.Parse(
				@"M 240,250
				C 200,375 200,250 175,200
				C 100,400 100,250 100,200
					C 0,350 0,250 30,130
				C 75,0 100,0 150,0
				C 200,0 250,0 250,150 Z"));
				// Left eye
				dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
				new Point(95, 95), 15, 15);
				// Right eye
				dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
				new Point(170, 105), 15, 15);
				// The mouth
				Pen p = new Pen(Brushes.Black, 10);
				p.StartLineCap = PenLineCap.Round;
				p.EndLineCap = PenLineCap.Round;
				dc.DrawLine(p, new Point(75, 160), new Point(175, 150));
			}
			AddVisualChild(ghostVisual);


		}
	}
}
