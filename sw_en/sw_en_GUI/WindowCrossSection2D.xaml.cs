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
using System.IO;
using BaseClasses;
using CRSC;

namespace sw_en_GUI
{
    /// <summary>
    /// Interaction logic for WindowCrossSection2D.xaml
    /// </summary>
    public partial class WindowCrossSection2D : Window
    {
        public WindowCrossSection2D()
        {
            InitializeComponent();
            Point p = new Point(10, 10);

            DrawPoint(p, Brushes.Red, Brushes.Red, 4, canvasForImage); // Temporary

            //CCrSc_3_51_BOX_TEMP crsc_temp = new CCrSc_3_51_BOX_TEMP();
            CCrSc_3_51_TRIANGLE_TEMP crsc_temp = new CCrSc_3_51_TRIANGLE_TEMP(0.866025f * 0.5f, 0.5f, 0.002f);
            canvasForImage.Children.Clear();

            DrawCrSc(crsc_temp);
        }

        public void SaveImage(Visual visual, int width, int height, string filePath)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, 96, 96,
                                                         PixelFormats.Pbgra32);
            bitmap.Render(visual);

            PngBitmapEncoder image = new PngBitmapEncoder();
            image.Frames.Add(BitmapFrame.Create(bitmap));
            using (Stream fs = File.Create(filePath))
            {
                image.Save(fs);
            }
        }

        private void menuItemTest1_Click(object sender, RoutedEventArgs e)
        {
            //CCrSc_3_51_BOX_TEMP crsc_temp = new CCrSc_3_51_BOX_TEMP();
            CCrSc_3_51_TRIANGLE_TEMP crsc_temp = new CCrSc_3_51_TRIANGLE_TEMP(0.866025f * 0.5f, 0.5f, 0.002f);
            canvasForImage.Children.Clear();

            DrawCrSc(crsc_temp);
        }

        public void DrawCrSc(CCrSc_TW crsc)
        {
            int scale_unit = 1000; // mm
            int modelposition_x = 700;
            int modelposition_y = 500;

            // Definition Points
            bool bDrawPoints = true;
            bool bDrawOutLine = true;

            if (bDrawPoints)
            {
                // Outer outline points
                if (crsc.CrScPointsOut != null) // If is array of points not empty
                {
                    for (int i = 0; i < crsc.INoPointsOut; i++)
                    {
                        DrawPoint(new Point(modelposition_x + scale_unit * crsc.CrScPointsOut[i, 0], modelposition_y + scale_unit * crsc.CrScPointsOut[i, 1]), Brushes.Red, Brushes.Red, 4, canvasForImage);
                    }
                }

                // Internal outline points
                if (crsc.CrScPointsIn != null) // If is array of points not empty
                {
                    for (int i = 0; i < crsc.INoPointsIn; i++)
                    {
                        DrawPoint(new Point(modelposition_x + scale_unit * crsc.CrScPointsIn[i, 0], modelposition_y + scale_unit * crsc.CrScPointsIn[i, 1]), Brushes.Red, Brushes.Red, 4, canvasForImage);
                    }
                }
            }

            if (bDrawOutLine)
            {
                // Outer outline lines
                if (crsc.CrScPointsOut != null) // If is array of points not empty
                {
                    for (int i = 0; i < crsc.INoPointsOut; i++)
                    {
                        // Add a Line
                        Line l = new Line();

                        l.X1 = modelposition_x + scale_unit * crsc.CrScPointsOut[i, 0];
                        l.Y1 = modelposition_y + scale_unit * crsc.CrScPointsOut[i, 1];

                        if (i < (crsc.INoPointsOut - 1))
                        {
                            l.X2 = modelposition_x + scale_unit * crsc.CrScPointsOut[i + 1, 0];
                            l.Y2 = modelposition_y + scale_unit * crsc.CrScPointsOut[i + 1, 1];
                        }
                        else
                        {
                            l.X2 = modelposition_x + scale_unit * crsc.CrScPointsOut[0, 0];
                            l.Y2 = modelposition_y + scale_unit * crsc.CrScPointsOut[0, 1];
                        }

                        DrawLine(l, Brushes.Black, PenLineCap.Flat, PenLineCap.Flat, 2, canvasForImage);
                    }
                }

                // Internal outline lines
                if (crsc.CrScPointsIn != null) // If is array of points not empty
                {
                    for (int i = 0; i < crsc.INoPointsIn; i++)
                    {
                        // Add a Line
                        Line l = new Line();
                        l.X1 = modelposition_x + scale_unit * crsc.CrScPointsIn[i, 0];
                        l.Y1 = modelposition_y + scale_unit * crsc.CrScPointsIn[i, 1];

                        if (i < (crsc.INoPointsIn - 1))
                        {
                            l.X2 = modelposition_x + scale_unit * crsc.CrScPointsIn[i + 1, 0];
                            l.Y2 = modelposition_y + scale_unit * crsc.CrScPointsIn[i + 1, 1];
                        }
                        else
                        {
                            l.X2 = modelposition_x + scale_unit * crsc.CrScPointsIn[0, 0];
                            l.Y2 = modelposition_y + scale_unit * crsc.CrScPointsIn[0, 1];
                        }

                        DrawLine(l, Brushes.Black, PenLineCap.Flat, PenLineCap.Flat, 2, canvasForImage);
                    }
                }
            }
        }

        public void DrawPoint(Point point, SolidColorBrush strokeColor, SolidColorBrush fillColor, double thickness, Canvas imageCanvas)
		{
			DrawRectangle(strokeColor,fillColor, thickness, imageCanvas, new Point(point.X, point.Y), new Point(point.X + 4, point.Y + 4));
		}

		public void DrawLine(Line line, SolidColorBrush color, PenLineCap startCap, PenLineCap endCap, double thickness, Canvas imageCanvas)
		{
			Line myLine = new Line();
			myLine.Stretch = Stretch.Fill;
			myLine.Stroke = color;
            myLine.X1 = line.X1;
            myLine.X2 = line.X2;
            myLine.Y1 = line.Y1;
            myLine.Y2 = line.Y2;
            myLine.StrokeThickness = thickness;
			myLine.StrokeStartLineCap = startCap;
			myLine.StrokeEndLineCap = endCap;
			myLine.HorizontalAlignment = HorizontalAlignment.Left;
			myLine.VerticalAlignment = VerticalAlignment.Center;
			Canvas.SetTop(myLine, line.Y1);
			Canvas.SetLeft(myLine, line.X1);
			imageCanvas.Children.Add(myLine);
		}

		/// <summary>
		/// Draw methods for each Draw Element Type
		/// </summary>
		public void DrawRectangle(SolidColorBrush strokeColor, SolidColorBrush fillColor, double thickness, Canvas imageCanvas, Point lt, Point br)
		{
			Rectangle rect = new Rectangle();
			rect.Stretch = Stretch.Fill;
			rect.Fill = fillColor;
			rect.Stroke = strokeColor;
			rect.Width = br.X - lt.X;
			rect.Height = br.Y - lt.Y;
			Canvas.SetTop(rect, lt.Y);
			Canvas.SetLeft(rect, lt.X);
			imageCanvas.Children.Add(rect);
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.OemPlus) 
			{
				zoomIn2D();
				Console.WriteLine("zoomIn");
			}
			else if (e.Key == Key.OemMinus) 
			{
				zoomOut2D();
				Console.WriteLine("zoomOut");
			}
			//else MessageBox.Show("else");
		}

		private void zoomIn2D() 
		{
			Line l = (Line)canvasForImage.Children[0];
			l.X2 *= 2;
			l.Y2 *= 2;
		}

		private void zoomOut2D()
		{
			Line l = (Line)canvasForImage.Children[0];
			l.X2 /= 2;
			l.Y2 /= 2;
		}
	}
}
