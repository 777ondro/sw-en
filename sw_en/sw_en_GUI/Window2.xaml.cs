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
using System.Windows.Media.Media3D;
using System.Configuration;
using CENEX;
using _3DTools;
using MATH;
using CRSC;
using BaseClasses;
using BaseClasses.GraphObj;
using HelixToolkit.Wpf;

namespace sw_en_GUI
{
  /// <summary>
  /// Interaction logic for Window2.xaml
  /// </summary>
  public partial class Window2 : Window
  {
      private bool bDebugging;

    ///////////////////////////////////////////////////////////////
    // Create switch command for various sections, split code into separate objects / function of 3D drawing for each type
    /////////////////////////////////////////////////

    // Tutorial
    /// http://kindohm.com/technical/WPF3DTutorial.htm  ScreenSpaceLines3D

    /// <summary>
    /// ///////////////////////////////////////////////////////
    /// MAIN CONSTRUCTOR
    /// ///////////////////////////////////////////////////////
    /// </summary>
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------
    public Window2(bool bDebugging)
    {
      InitializeComponent();

      Model3DGroup gr = new Model3DGroup();
      //gr.Children.Add(new AmbientLight());

      GeometryModel3D SolidModel3D = new GeometryModel3D();
      MeshGeometry3D mesh = new MeshGeometry3D();
      mesh.Positions = new Point3DCollection();

      //ScreenSpaceLines3D line = new ScreenSpaceLines3D();
      //line.Color = Color.FromRgb(0,255,0);
      //line.Points.Add(mesh.Positions[0]);
      //line.Points.Add(mesh.Positions[1]);

      //Viewport3D view = new Viewport3D();
      //view.Children.Add(line);

      //gr.Children.Add(new AmbientLight());
      SolidModel3D.Geometry = mesh;
      SolidColorBrush br = new SolidColorBrush(Color.FromRgb(255, 0, 0));
      SolidModel3D.Material = new DiffuseMaterial(br);

      gr.Children.Add(SolidModel3D); // Add solid to model group

      _trackport.Model = (Model3D)gr; //CreateRectangle(p3, p2, p6, p7, Brushes.Red);

      _trackport.Trackball.TranslateScale = 1000;   //step for moving object (panning)

      _trackport.SetupScene();
    }

    public Window2(CModel cmodel, bool bDebugging)
    {
      InitializeComponent();

      if (cmodel != null)
      {
          Model3DGroup gr = new Model3DGroup();
          //gr.Children.Add(new AmbientLight());

          // Default color
          SolidColorBrush brushDefault = new SolidColorBrush(Color.FromRgb(255, 0, 0));

          EGCS eGCS = EGCS.eGCSLeftHanded;
          //EGCS eGCS = EGCS.eGCSRightHanded;

          // Global coordinate system - axis
          ScreenSpaceLines3D sAxisX_3D = new ScreenSpaceLines3D();
          ScreenSpaceLines3D sAxisY_3D = new ScreenSpaceLines3D();
          ScreenSpaceLines3D sAxisZ_3D = new ScreenSpaceLines3D();
          Point3D pGCS_centre = new Point3D(0,0,0);
          Point3D pAxisX = new Point3D(1, 0, 0);
          Point3D pAxisY = new Point3D(0, 1, 0);
          Point3D pAxisZ = new Point3D(0, 0, 1);

          sAxisX_3D.Points.Add(pGCS_centre);
          sAxisX_3D.Points.Add(pAxisX);
          sAxisX_3D.Color = Colors.Red;
          sAxisX_3D.Thickness = 2;

          sAxisY_3D.Points.Add(pGCS_centre);
          sAxisY_3D.Points.Add(pAxisY);
          sAxisY_3D.Color = Colors.Green;
          sAxisY_3D.Thickness = 2;

          sAxisZ_3D.Points.Add(pGCS_centre);
          sAxisZ_3D.Points.Add(pAxisZ);
          sAxisZ_3D.Color = Colors.Blue;
          sAxisZ_3D.Thickness = 2;

          //I made ViewPort public property to Access ViewPort object inside TrackPort3D
          //to ViewPort add 3 children (3 axis)
          _trackport.ViewPort.Children.Add(sAxisX_3D);
          _trackport.ViewPort.Children.Add(sAxisY_3D);
          _trackport.ViewPort.Children.Add(sAxisZ_3D);
          
          // Check that real model exists and create model geometry
          if (cmodel != null)
          {
              if (cmodel.m_arrMembers != null) // Some members exist
              {
                  // Auxialiary for generation of colors numbers
                  float j = 0;

                  // Model Group of Members
                  // Prepare member model
                  for (int i = 0; i < cmodel.m_arrMembers.Length; i++) // !!! BUG pocet prvkov sa nacitava z xls aj z prazdnych riadkov pokial su nejako formatovane / nie default
                  {
                      if (cmodel.m_arrMembers[i] != null &&
                          cmodel.m_arrMembers[i].NodeStart != null &&
                          cmodel.m_arrMembers[i].NodeEnd != null &&
                          cmodel.m_arrMembers[i].CrScStart != null) // Member object is valid (not empty)
                      {
                          if (bDebugging)
                          {
                              System.Console.Write("\n" + "Member ID:" + (i + 1).ToString() + "\n"); // Write Member ID in console window
                              System.Console.Write("Start Node ID:" + cmodel.m_arrMembers[i].NodeStart.ID.ToString() + "\n"); // Write Start Node ID and coordinates in console window
                              System.Console.Write(cmodel.m_arrMembers[i].NodeStart.X.ToString() + "\t" + cmodel.m_arrMembers[i].NodeStart.Y.ToString() + "\t" + cmodel.m_arrMembers[i].NodeStart.Z.ToString() + "\n");
                              System.Console.Write("End Node ID:" + cmodel.m_arrMembers[i].NodeEnd.ID.ToString() + "\n");     // Write   End Node ID and coordinates in console window
                              System.Console.Write(cmodel.m_arrMembers[i].NodeEnd.X.ToString() + "\t" + cmodel.m_arrMembers[i].NodeEnd.Y.ToString() + "\t" + cmodel.m_arrMembers[i].NodeEnd.Z.ToString() + "\n\n");

                              cmodel.m_arrMembers[i].BIsDebugging = bDebugging;
                          }

                          if (cmodel.m_arrMembers[i].CrScStart.CrScPointsOut != null) // CCrSc is abstract without geometrical properties (dimensions), only centroid line could be displayed
                          {
                              // Member material color
                              byte R = (byte)(250);
                              byte G = (byte)(240);
                              byte B = (byte)(230);

                              SolidColorBrush br = new SolidColorBrush(Color.FromRgb(R, G, B)); // Material color
                              br.Opacity = 0.8;

                              // Set different color for each member
                              bool bDiffMemberColors = false;

                              if (bDiffMemberColors)
                              {
                                  if (j < 20) // 20*10 = 200, 200 + 55 - 255 (maxium number of color)
                                  {
                                      br.Color = Color.FromRgb((byte)(55 + j * 10), (byte)(55 + j * 7), (byte)(55 + j * 5));
                                      j++;
                                  }
                                  else
                                  {
                                      j = 0;
                                  }
                              }

                              bool bFastRendering = false;

                              if (bFastRendering ||
                                  (cmodel.m_arrMembers[i].CrScStart.TriangleIndicesFrontSide == null ||
                                      cmodel.m_arrMembers[i].CrScStart.TriangleIndicesShell == null ||
                                      cmodel.m_arrMembers[i].CrScStart.TriangleIndicesBackSide == null)
                                   ) // Check if are particular surfaces defined
                              {
                                  // Create Member model - one geometry model
                                  // GeometryModel3D memberModel3D;
                                  // Add current member model to the model group
                                  gr.Children.Add((Model3D)cmodel.m_arrMembers[i].getG_M_3D_Member(eGCS, br));
                              }
                              else
                              {
                                  // Create Member model - consist of 3 geometry models (member is one model group)
                                  // Model3DGroup memberModel3D;
                                  // Add current member model to the model group

                                  SolidColorBrush br1 = new SolidColorBrush(Color.FromRgb(255, 64, 64)); // Material color - Front Side (red)
                                  SolidColorBrush br2 = new SolidColorBrush(Color.FromRgb(141, 238, 238)); // Material color - Shell (red)
                                  SolidColorBrush br3 = new SolidColorBrush(Color.FromRgb(238, 154, 73)); // Material color - Back Side (orange)
                                  br1.Opacity = br3.Opacity = 0.8;
                                  br2.Opacity = 0.4;
                                  gr.Children.Add(cmodel.m_arrMembers[i].getM_3D_G_Member(eGCS, br1, br2, br3));
                              }
                          }
                          else
                          {
                          // Display axis line, member is not valid to display in 3D
                          }
                      }
                  }
              }

              if (cmodel.m_arrGOAreas != null) // Some areas exist
              {
                  // Model Groups of Areas







              }

              if (cmodel.m_arrGOVolumes != null) // Some volumes exist
              {
                  // Model Groups of Volumes
                  for (int i = 0; i < cmodel.m_arrGOVolumes.Length; i++)
                  {
                      if (cmodel.m_arrGOVolumes[i] != null &&
                          cmodel.m_arrGOVolumes[i].m_pControlPoint != null &&
                          cmodel.m_arrGOVolumes[i].BIsDisplayed == true) // Volume object is valid (not empty) and should be displayed
                      {
                          // Get shape - prism , sphere, ...
                          gr.Children.Add(cmodel.m_arrGOVolumes[i].CreateM_3D_G_Volume_8Edges()); // Add solid to model group
                      }
                  }
              }

              if (cmodel.m_arrGOStrWindows != null) // Some windows exist
              {
                  // Model Groups of Windows
                  for (int i = 0; i < cmodel.m_arrGOStrWindows.Length; i++)
                  {
                      if (cmodel.m_arrGOStrWindows[i] != null &&
                          cmodel.m_arrGOStrWindows[i].m_pControlPoint != null &&
                          cmodel.m_arrGOStrWindows[i].BIsDisplayed == true) // Volume object is valid (not empty) and should be displayed
                      {
                          if (cmodel.m_arrGOStrWindows[i].EShapeType == EWindowShapeType.eClassic)
                              gr.Children.Add(cmodel.m_arrGOStrWindows[i].CreateM_3D_G_Window()); // Add solid to model group
                          else
                          {
                              //Exception - not implemented
                          }
                      }
                  }
              }

              if (cmodel.m_arrNSupports != null) // Some nodal supports exist
              {
                  // Model Groups of Nodal Suports
                  for (int i = 0; i < cmodel.m_arrNSupports.Length; i++)
                  {
                      if (cmodel.m_arrNSupports[i] != null && cmodel.m_arrNSupports[i].BIsDisplayed == true) // Support object is valid (not empty) and should be displayed
                      {
                          gr.Children.Add(cmodel.m_arrNSupports[i].CreateM_3D_G_NSupport()); // Add solid to model group

                          // Set support for all assigned nodes


                      }
                  }
              }

              if (cmodel.m_arrNReleases != null) // Some nodal supports exist
              {
                  // Model Groups of Nodal Suports
                  for (int i = 0; i < cmodel.m_arrNReleases.Length; i++)
                  {
                      if (cmodel.m_arrNReleases[i] != null && cmodel.m_arrNReleases[i].BIsDisplayed == true) // Support object is valid (not empty) and should be displayed
                      {
                          // Transform modelgroup from LCS to GCS

                          Model3DGroup model_gr = new Model3DGroup();
                          model_gr = cmodel.m_arrNReleases[i].CreateM_3D_G_MNRelease();
                          cmodel.m_arrNReleases[i].Transform3D_OnMemberEntity_fromLCStoGCS(model_gr, cmodel.m_arrNReleases[i].Member);

                          gr.Children.Add(model_gr); // Add Release to model group

                          // Set release for all assigned members (member nodes)



                      }
                  }
              }

              if (cmodel.m_arrNLoads != null) // Some nodal loads exist
              {
                  // Model Groups of Nodal Loads
                  for (int i = 0; i < cmodel.m_arrNLoads.Length; i++)
                  {
                      if (cmodel.m_arrNLoads[i] != null && cmodel.m_arrNLoads[i].BIsDisplayed == true) // Load object is valid (not empty) and should be displayed
                      {
                          gr.Children.Add(cmodel.m_arrNLoads[i].CreateM_3D_G_NLoad()); // Add solid to model group

                          // Set load for all assigned nodes
                      }
                  }
              }

              /*
                The following lights derive from the base class Light:
                AmbientLight : Provides ambient lighting that illuminates all objects uniformly regardless of their location or orientation.
                DirectionalLight : Illuminates like a distant light source. Directional lights have a Direction specified as a Vector3D, but no specified location.
                PointLight : Illuminates like a nearby light source. PointLights have a position and cast light from that position. Objects in the scene are illuminated depending on their position and distance with respect to the light. PointLightBase exposes a Range property, which determines a distance beyond which models will not be illuminated by the light. PointLight also exposes attenuation properties which determine how the light's intensity diminishes over distance. You can specify constant, linear, or quadratic interpolations for the light's attenuation.
                SpotLight : Inherits from PointLight. Spotlights illuminate like PointLight and have both position and direction. They project light in a cone-shaped area set by InnerConeAngle and OuterConeAngle properties, specified in degrees.
              */

              // Directional Light
              DirectionalLight Dir_Light = new DirectionalLight();
              Dir_Light.Color = Colors.White;
              Dir_Light.Direction = new Vector3D(0, 0, -1);
              gr.Children.Add(Dir_Light);

              // Point light values
              PointLight Point_Light = new PointLight();
              Point_Light.Position = new Point3D(0, 0, 30);
              Point_Light.Color = System.Windows.Media.Brushes.White.Color;
              Point_Light.Range = 30.0;
              Point_Light.ConstantAttenuation=0;
              Point_Light.LinearAttenuation=0;
              Point_Light.QuadraticAttenuation = 0.2f;
              Point_Light.ConstantAttenuation = 5.0;
              gr.Children.Add(Point_Light);

              SpotLight Spot_Light = new SpotLight();
              Spot_Light.InnerConeAngle = 30;
              Spot_Light.OuterConeAngle = 30;
              Spot_Light.Color = System.Windows.Media.Brushes.White.Color;
              Spot_Light.Direction = new Vector3D(0, 0, -1);
              Spot_Light.Position = new Point3D(8.5, 8.5, 20);
              Spot_Light.Range = 30;
              gr.Children.Add(Spot_Light);

              //Set Ambient Light
              AmbientLight Ambient_Light = new AmbientLight();
              Ambient_Light.Color = Color.FromRgb(250, 250, 230);
              gr.Children.Add(new AmbientLight());

              if (cmodel.m_arrGOLines != null) // Some lines exist
              {
                  Point3D solidCenter = new Point3D(-5, 0, 0);

                  float fa = 0.5f;

                  Point3D p0 = new Point3D(-fa, -fa, -fa);
                  Point3D p1 = new Point3D(fa, -fa, -fa);
                  Point3D p2 = new Point3D(fa, fa, -fa);
                  Point3D p3 = new Point3D(-fa, fa, -fa);
                  Point3D p4 = new Point3D(-fa, -fa, fa);
                  Point3D p5 = new Point3D(fa, -fa, fa);
                  Point3D p6 = new Point3D(fa, fa, fa);
                  Point3D p7 = new Point3D(-fa, fa, fa);

                  // Lines

                  ScreenSpaceLines3D line1 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line2 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line3 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line4 = new ScreenSpaceLines3D();

                  Color lineColor = Color.FromRgb(250, 30, 30);
                  line1.Color = lineColor;
                  line1.Points.Add(p0);
                  line1.Points.Add(p1);

                  line2.Color = lineColor;
                  line2.Points.Add(p1);
                  line2.Points.Add(p2);

                  line3.Color = lineColor;
                  line3.Points.Add(p2);
                  line3.Points.Add(p3);

                  line4.Color = lineColor;
                  line4.Points.Add(p3);
                  line4.Points.Add(p0);

                  _trackport.ViewPort.Children.Add(line1);
                  _trackport.ViewPort.Children.Add(line2);
                  _trackport.ViewPort.Children.Add(line3);
                  _trackport.ViewPort.Children.Add(line4);

                  ScreenSpaceLines3D line5 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line6 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line7 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line8 = new ScreenSpaceLines3D();

                  line5.Color = lineColor;
                  line5.Points.Add(p4);
                  line5.Points.Add(p5);

                  line6.Color = lineColor;
                  line6.Points.Add(p5);
                  line6.Points.Add(p6);

                  line7.Color = lineColor;
                  line7.Points.Add(p6);
                  line7.Points.Add(p7);

                  line8.Color = lineColor;
                  line8.Points.Add(p7);
                  line8.Points.Add(p4);

                  _trackport.ViewPort.Children.Add(line5);
                  _trackport.ViewPort.Children.Add(line6);
                  _trackport.ViewPort.Children.Add(line7);
                  _trackport.ViewPort.Children.Add(line8);

                  ScreenSpaceLines3D line09 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line10 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line11 = new ScreenSpaceLines3D();
                  ScreenSpaceLines3D line12 = new ScreenSpaceLines3D();

                  line09.Color = lineColor;
                  line09.Points.Add(p0);
                  line09.Points.Add(p4);

                  line10.Color = lineColor;
                  line10.Points.Add(p1);
                  line10.Points.Add(p5);

                  line11.Color = lineColor;
                  line11.Points.Add(p2);
                  line11.Points.Add(p6);

                  line12.Color = lineColor;
                  line12.Points.Add(p3);
                  line12.Points.Add(p7);

                  _trackport.ViewPort.Children.Add(line09);
                  _trackport.ViewPort.Children.Add(line10);
                  _trackport.ViewPort.Children.Add(line11);
                  _trackport.ViewPort.Children.Add(line12);
              }
          }

          // Get model centre
          float fTempMax_X;
          float fTempMin_X;
          float fTempMax_Y;
          float fTempMin_Y;
          float fTempMax_Z;
          float fTempMin_Z;

          CalculateModelLimits(cmodel, out fTempMax_X, out fTempMin_X, out fTempMax_Y, out fTempMin_Y, out fTempMax_Z, out fTempMin_Z);

          float fModel_Length_X = fTempMax_X - fTempMin_X;
          float fModel_Length_Y = fTempMax_Y - fTempMin_Y;
          float fModel_Length_Z = fTempMax_Z - fTempMin_Z;

          Point3D pModelGeomCentre = new Point3D(fModel_Length_X / 2.0f, fModel_Length_Y / 2.0f, fModel_Length_Z / 2.0f);

          Point3D cameraPosition = new Point3D(pModelGeomCentre.X, pModelGeomCentre.Y + 300, pModelGeomCentre.Z + 100);

          //SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
          //GeometryModel3D model = getGeometryModel3D(brush, obj_CrSc, new Point3D(10, 10, 10), new Point3D(500, 300, 200));
          //gr.Children.Add(model);

          ////Point3D cameraPosition = ((MeshGeometry3D)model.Geometry).Positions[0];
          ////cameraPosition.Z -= 1000;

          //brush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
          //model = getGeometryModel3D(brush, obj_CrSc, new Point3D(110, 110, 10), new Point3D(600, 400, 200));
          //gr.Children.Add(model);

          //IMPORTANT: this is the best way to do it, but we can't use it because of trackball
          //because camera is set by trackball Transform this.Camera.Transform = _trackball.Transform;
          //and headlite too:  this.Headlight.Transform = _trackball.Transform;

          _trackport.PerspectiveCamera.Position = cameraPosition;
          //_trackport.PerspectiveCamera.LookDirection = new Vector3D(cameraPosition.X, cameraPosition.Y, cameraPosition.Z - 100);

          _trackport.PerspectiveCamera.LookDirection = new Vector3D(0, -1, -0.2);

          _trackport.Model = (Model3D)gr;

          _trackport.SetupScene();
      }
    }

    private void CalculateModelLimits(CModel cmodel,
        out float fTempMax_X,
        out float fTempMin_X,
        out float fTempMax_Y,
        out float fTempMin_Y,
        out float fTempMax_Z,
        out float fTempMin_Z
        )
    {
        fTempMax_X = float.MinValue;
        fTempMin_X = float.MaxValue;
        fTempMax_Y = float.MinValue;
        fTempMin_Y = float.MaxValue;
        fTempMax_Z = float.MinValue;
        fTempMin_Z = float.MaxValue;

        if (cmodel.m_arrNodes != null) // Some nodes exist
        {
            for (int i = 0; i < cmodel.m_arrNodes.Length; i++)
            {
                // Maximum X - coordinate
                if (cmodel.m_arrNodes[i].X > fTempMax_X)
                    fTempMax_X = cmodel.m_arrNodes[i].X;

                // Minimum X - coordinate
                if (cmodel.m_arrNodes[i].X < fTempMin_X)
                    fTempMin_X = cmodel.m_arrNodes[i].X;

                // Maximum Y - coordinate
                if (cmodel.m_arrNodes[i].Y > fTempMax_Y)
                    fTempMax_Y = cmodel.m_arrNodes[i].Y;

                // Minimum Y - coordinate
                if (cmodel.m_arrNodes[i].Y < fTempMin_Y)
                    fTempMin_Y = cmodel.m_arrNodes[i].Y;

                // Maximum Z - coordinate
                if (cmodel.m_arrNodes[i].Z > fTempMax_Z)
                    fTempMax_Z = cmodel.m_arrNodes[i].Z;

                // Minimum Z - coordinate
                if (cmodel.m_arrNodes[i].Z < fTempMin_Z)
                    fTempMin_Z = cmodel.m_arrNodes[i].Z;
            }
        }
        else if (cmodel.m_arrGOPoints != null) // Some points exist
        {
            for (int i = 0; i < cmodel.m_arrGOPoints.Length; i++)
            {
                // Maximum X - coordinate
                if (cmodel.m_arrGOPoints[i].X > fTempMax_X)
                    fTempMax_X = (float)cmodel.m_arrGOPoints[i].X;

                // Minimum X - coordinate
                if (cmodel.m_arrGOPoints[i].X < fTempMin_X)
                    fTempMin_X = (float)cmodel.m_arrGOPoints[i].X;

                // Maximum Y - coordinate
                if (cmodel.m_arrGOPoints[i].Y > fTempMax_Y)
                    fTempMax_Y = (float)cmodel.m_arrGOPoints[i].Y;

                // Minimum Y - coordinate
                if (cmodel.m_arrGOPoints[i].Y < fTempMin_Y)
                    fTempMin_Y = (float)cmodel.m_arrGOPoints[i].Y;

                // Maximum Z - coordinate
                if (cmodel.m_arrGOPoints[i].Z > fTempMax_Z)
                    fTempMax_Z = (float)cmodel.m_arrGOPoints[i].Z;

                // Minimum Z - coordinate
                if (cmodel.m_arrGOPoints[i].Z < fTempMin_Z)
                    fTempMin_Z = (float)cmodel.m_arrGOPoints[i].Z;
            }
        }
        else
        {
            // Exception - no definition nodes or points
        }
    }
  }
}
