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
        int scale_unit = 1000; // mm
        int modelposition_x = 700;
        int modelposition_y = 500;

        bool bDrawPoints = true;
        bool bDrawOutLine = false;
        bool bUsePolylineforDrawing = true;

        bool bDrawPointNumbers = true;

        public WindowCrossSection2D()
        {
            InitializeComponent();
            // Temporary
            //Point p = new Point(10, 10);
            //DrawPoint(p, Brushes.Red, Brushes.Red, 4, canvasForImage);
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
            CCrSc_3_51_BOX_TEMP crsc_temp = new CCrSc_3_51_BOX_TEMP(0.5f, 0.4f, 0.01f, Colors.LawnGreen);
            //CCrSc_3_51_TRIANGLE_TEMP crsc_temp = new CCrSc_3_51_TRIANGLE_TEMP(0.866025f * 0.5f, 0.5f, 0.01f);
            canvasForImage.Children.Clear();

            DrawCrSc(crsc_temp);
        }

        public void DrawCrSc(CCrSc_TW crsc)
        {
            // Definition Points
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

            // Outlines
            if (bDrawOutLine)
            {
                if (bUsePolylineforDrawing)
                {
                    // Outer outline lines
                    if (crsc.CrScPointsOut != null) // If is array of points not empty
                    {
                        DrawPolyLine(crsc.CrScPointsOut, Brushes.Black, PenLineCap.Flat, PenLineCap.Flat, 2, canvasForImage);
                    }

                    // Internal outline lines
                    if (crsc.CrScPointsIn != null) // If is array of points not empty
                    {
                        DrawPolyLine(crsc.CrScPointsIn, Brushes.Black, PenLineCap.Flat, PenLineCap.Flat, 2, canvasForImage);
                    }
                }
                else
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

            // Definition Point Numbers
            if (bDrawPointNumbers)
            {
                // Outer outline points
                if (crsc.CrScPointsOut != null) // If is array of points not empty
                {
                    for (int i = 0; i < crsc.INoPointsOut; i++)
                    {
                        DrawText((i + 1).ToString(), modelposition_x + scale_unit * crsc.CrScPointsOut[i, 0], modelposition_x + scale_unit * crsc.CrScPointsOut[i, 1], Brushes.Blue, canvasForImage);
                    }
                }

                // Internal outline points
                if (crsc.CrScPointsIn != null && crsc.CrScPointsOut != null) // If is array of points not empty
                {
                    for (int i = 0; i < crsc.INoPointsIn; i++)
                    {
                        DrawText((/*crsc.INoPointsOut +*/ i + 1).ToString(), modelposition_x + scale_unit * crsc.CrScPointsIn[i,0], modelposition_x + scale_unit * crsc.CrScPointsIn[i, 1], Brushes.Green, canvasForImage);
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

        public void DrawPolyLine(float [,] arrPoints, SolidColorBrush color, PenLineCap startCap, PenLineCap endCap, double thickness, Canvas imageCanvas)
        {
            PointCollection points = new PointCollection();

            for (int i = 0; i < arrPoints.Length / 2; i++)
            {
                if(i < ((arrPoints.Length / 2)-1))
                   points.Add(new Point(modelposition_x + scale_unit * arrPoints[i, 0], modelposition_y + scale_unit * arrPoints[i, 1]));
                else
                    points.Add(new Point(modelposition_x + scale_unit * arrPoints[0, 0], modelposition_y + scale_unit * arrPoints[0, 1])); // Last point is same as first one
            }

            Polyline myLine = new Polyline();
            myLine.Stretch = Stretch.Fill;
            myLine.Stroke = color;
            myLine.Points = points;
            myLine.StrokeThickness = thickness;
            myLine.StrokeStartLineCap = startCap;
            myLine.StrokeEndLineCap = endCap;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetTop(myLine, myLine.Points[0].Y);
            Canvas.SetLeft(myLine, myLine.Points[0].X);
            imageCanvas.Children.Add(myLine);
        }

        public void DrawText(string text, double posx, double posy, SolidColorBrush color, Canvas imageCanvas)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = color;
            Canvas.SetLeft(textBlock, posx);
            Canvas.SetTop(textBlock, posy);
            //textBlock.RenderTransform = new RotateTransform(0, 0, 0);
            textBlock.Margin = new Thickness(5, -200, 0, 0);
            textBlock.FontSize = 30;
            imageCanvas.Children.Add(textBlock);
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
