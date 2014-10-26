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
                  // Model Group of Members
                  // Prepare member model
                  for (int i = 0; i < cmodel.m_arrMembers.Length; i++) // !!! BUG pocet prvkov sa nacitava z xls aj z prazdnych riadkov pokial su nejako formatovane / nie default
                  {
                      if (cmodel.m_arrMembers[i] != null &&
                          cmodel.m_arrMembers[i].NodeStart != null &&
                          cmodel.m_arrMembers[i].NodeEnd != null &&
                          cmodel.m_arrMembers[i].CrScStart != null) // Member object is valid (not empty)
                      {
                          // Start Node of Member
                          Point3D mpA = new Point3D(cmodel.m_arrMembers[i].NodeStart.FCoord_X, cmodel.m_arrMembers[i].NodeStart.FCoord_Y, cmodel.m_arrMembers[i].NodeStart.FCoord_Z);
                          // End node of Member
                          Point3D mpB = new Point3D(cmodel.m_arrMembers[i].NodeEnd.FCoord_X, cmodel.m_arrMembers[i].NodeEnd.FCoord_Y, cmodel.m_arrMembers[i].NodeEnd.FCoord_Z);

                          // Angle of rotation about local x-axis
                          cmodel.m_arrMembers[i].DTheta_x = 0; // Temporary

                          if (bDebugging)
                          {
                              System.Console.Write("\n" + "Member ID:" + (i + 1).ToString() + "\n"); // Write Member ID in console window
                              System.Console.Write("Start Node ID:" + cmodel.m_arrMembers[i].NodeStart.ID.ToString() + "\n"); // Write Start Node ID and coordinates in console window
                              System.Console.Write(mpA.X.ToString() + "\t" + mpA.Y.ToString() + "\t" + mpA.Z.ToString() + "\n");
                              System.Console.Write("End Node ID:" + cmodel.m_arrMembers[i].NodeEnd.ID.ToString() + "\n");     // Write   End Node ID and coordinates in console window
                              System.Console.Write(mpB.X.ToString() + "\t" + mpB.Y.ToString() + "\t" + mpB.Z.ToString() + "\n\n");
                          }

                          if (cmodel.m_arrMembers[i].CrScStart.CrScPointsOut != null) // CCrSc is is abstract without geometrical properties (dimensions), only centroid line could be displayed
                          {
                              // Member material color
                              byte R = (byte)(i / 2 == 0 ? 255 : 252);
                              byte G = (byte)(i / 2 == 0 ? 234 : 241);
                              byte B = (byte)(i / 2 == 0 ? 233 : 230);

                              SolidColorBrush br = new SolidColorBrush(Color.FromRgb(R, G, B)); // Material color

                              if (i <= 10)
                                  br.Color = Colors.White;
                              else if (i <= 20)
                                  br.Color = Colors.Red;
                              else if (i <= 30)
                                  br.Color = Colors.LightGreen;
                              else if (i <= 40)
                                  br.Color = Colors.White;
                              else if (i <= 50)
                                  br.Color = Colors.Red;
                              else if (i <= 60)
                                  br.Color = Colors.Green;
                              else if (i <= 70)
                                  br.Color = Colors.LightSalmon;
                              else if (i <= 80)
                                  br.Color = Colors.Red;
                              else if (i <= 90)
                                  br.Color = Colors.Green;
                              else if (i <= 100)
                                  br.Color = Colors.Red;
                              else if (i <= 110)
                                  br.Color = Colors.GreenYellow;
                              else if (i <= 12)
                                  br.Color = Colors.White;
                              else if (i <= 130)
                                  br.Color = Colors.LightBlue;
                              else if (i <= 140)
                                  br.Color = Colors.Green;
                              else if (i <= 150)
                                  br.Color = Colors.Orange;
                              else if (i <= 160)
                                  br.Color = Colors.Red;
                              else if (i <= 170)
                                  br.Color = Colors.LightCyan;
                              else if (i <= 180)
                                  br.Color = Colors.White;
                              else if (i <= 190)
                                  br.Color = Colors.Yellow;
                              else if (i <= 200)
                                  br.Color = Colors.LightCyan;
                              else
                                  br.Color = Colors.Gold;

                              br.Opacity = 0.6; // Doesnt work :-/

                              // Create Member model
                              GeometryModel3D memberModel3D = getMemberGeometryModel3D(eGCS, br, cmodel.m_arrMembers[i].CrScStart, cmodel.m_arrMembers[i].CrScEnd, mpA, mpB, cmodel.m_arrMembers[i].DTheta_x);

                              // Add current member model to the model group
                              gr.Children.Add(memberModel3D);
                          }
                      }
                  }
              }

              if (cmodel.m_arrGOAreas != null) // Some areas exist
              {
                  // Model Group of Areas







              }

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

              if (cmodel.m_arrGOVolumes != null) // Some volumes exist
              {
                  for (int i = 0; i < cmodel.m_arrGOVolumes.Length; i++)
                  {
                      if (cmodel.m_arrGOVolumes[i] != null &&
                          cmodel.m_arrGOVolumes[i].m_pControlPoint != null &&
                          cmodel.m_arrGOVolumes[i].BIsDisplayed == true) // Volume object is valid (not empty) and should be displayed
                      {
                          if (cmodel.m_arrGOVolumes[i].m_eShapeType == EVolumeShapeType.eShape3DPrism_8Edges)
                              gr.Children.Add(CreateGM_3D_Volume_8Edeges(cmodel.m_arrGOVolumes[i])); // Add solid to model group
                          else
                          {
                              //Exception - not implemented
                          }
                      }
                  }


                  // Sphere

                  GeometryModel3D sphereModel3D = new GeometryModel3D();

                  Point3D sphereCenter = new Point3D(5, -5, 0.5f);
                  SphereMeshGenerator objSphere = new SphereMeshGenerator(sphereCenter);

                  SolidColorBrush brushSolid2 = new SolidColorBrush(Color.FromRgb(204, 000, 102));
                  brushSolid2.Opacity = 0.8f;

                  sphereModel3D.Material = new DiffuseMaterial(brushSolid2);

                  sphereModel3D.Geometry = objSphere.Geometry;

                  gr.Children.Add(sphereModel3D);

                  // Window

                  Point3D p1_W = new Point3D(2,0,1);
                  gr.Children.Add(CreateGM_3D_Window(2, p1_W, 2, 1.5f, 0.1f, 0));
                  Point3D p2_W = new Point3D(8, 0, 1);
                  gr.Children.Add(CreateGM_3D_Window(2, p2_W, 2, 1.5f, 0.1f, 0));

                  Point3D p3_W = new Point3D(12, 3, 1);
                  gr.Children.Add(CreateGM_3D_Window(2, p3_W, 2, 1.5f, 0.1f, (float)Math.PI / 2));

                  // Doors
                  Point3D p4_W = new Point3D(12, 8, 0);
                  gr.Children.Add(CreateGM_3D_Window(1,p4_W, 1, 2.5f, 0.1f, (float)Math.PI / 2));

                  Point3D p5_W = new Point3D(2, 11.5, 1);
                  gr.Children.Add(CreateGM_3D_Window(2, p5_W, 2, 1.5f, 0.1f, 0));
                  Point3D p6_W = new Point3D(8, 11.5, 1);
                  gr.Children.Add(CreateGM_3D_Window(2, p6_W, 2, 1.5f, 0.1f, 0));

                  Point3D p7_W = new Point3D(0.5f, 3, 1);
                  gr.Children.Add(CreateGM_3D_Window(2, p7_W, 2, 1.5f, 0.1f, (float)Math.PI / 2));
                  Point3D p8_W = new Point3D(0.5f, 7, 1);
                  gr.Children.Add(CreateGM_3D_Window(2, p8_W, 2, 1.5f, 0.1f, (float)Math.PI / 2));

                  //French window
                  Point3D p9_W = new Point3D(7, 5, 3);
                  gr.Children.Add(CreateGM_3D_Window(3, p9_W, 2, 2.5f, 0.1f, 0));

                  Point3D p10_W = new Point3D(13, 5, 4);
                  gr.Children.Add(CreateGM_3D_Window(2, p10_W, 2, 1.5f, 0.1f, 0));

                  Point3D p11_W = new Point3D(5.5f, 8, 4);
                  gr.Children.Add(CreateGM_3D_Window(2, p11_W, 2, 1.5f, 0.1f, (float)Math.PI / 2));
                  Point3D p12_W = new Point3D(5.5f, 12, 4);
                  gr.Children.Add(CreateGM_3D_Window(2, p12_W, 2, 1.5f, 0.1f, (float)Math.PI / 2));

                  Point3D p13_W = new Point3D(7, 16.5, 4);
                  gr.Children.Add(CreateGM_3D_Window(2, p13_W, 2, 1.5f, 0.1f, 0));
                  Point3D p14_W = new Point3D(13, 16.5, 4);
                  gr.Children.Add(CreateGM_3D_Window(2, p14_W, 2, 1.5f, 0.1f, 0));

                  Point3D p15_W = new Point3D(17, 8, 4);
                  gr.Children.Add(CreateGM_3D_Window(2, p15_W, 2, 1.5f, 0.1f, (float)Math.PI / 2));
                  Point3D p16_W = new Point3D(17, 12, 4);
                  gr.Children.Add(CreateGM_3D_Window(2, p16_W, 2, 1.5f, 0.1f, (float)Math.PI / 2));

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
              //gr.Children.Add(new AmbientLight());

              if (cmodel.m_arrGOLines != null) // Some lines exist
              {
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
                if (cmodel.m_arrNodes[i].FCoord_X > fTempMax_X)
                    fTempMax_X = cmodel.m_arrNodes[i].FCoord_X;

                // Minimum X - coordinate
                if (cmodel.m_arrNodes[i].FCoord_X < fTempMin_X)
                    fTempMin_X = cmodel.m_arrNodes[i].FCoord_X;

                // Maximum Y - coordinate
                if (cmodel.m_arrNodes[i].FCoord_Y > fTempMax_Y)
                    fTempMax_Y = cmodel.m_arrNodes[i].FCoord_Y;

                // Minimum Y - coordinate
                if (cmodel.m_arrNodes[i].FCoord_Y < fTempMin_Y)
                    fTempMin_Y = cmodel.m_arrNodes[i].FCoord_Y;

                // Maximum Z - coordinate
                if (cmodel.m_arrNodes[i].FCoord_Z > fTempMax_Z)
                    fTempMax_Z = cmodel.m_arrNodes[i].FCoord_Z;

                // Minimum Z - coordinate
                if (cmodel.m_arrNodes[i].FCoord_Z < fTempMin_Z)
                    fTempMin_Z = cmodel.m_arrNodes[i].FCoord_Z;
            }
        }
        else if (cmodel.m_arrGOPoints != null) // Some points exist
        {
            for (int i = 0; i < cmodel.m_arrGOPoints.Length; i++)
            {
                // Maximum X - coordinate
                if (cmodel.m_arrGOPoints[i].FCoord_X > fTempMax_X)
                    fTempMax_X = cmodel.m_arrGOPoints[i].FCoord_X;

                // Minimum X - coordinate
                if (cmodel.m_arrGOPoints[i].FCoord_X < fTempMin_X)
                    fTempMin_X = cmodel.m_arrGOPoints[i].FCoord_X;

                // Maximum Y - coordinate
                if (cmodel.m_arrGOPoints[i].FCoord_Y > fTempMax_Y)
                    fTempMax_Y = cmodel.m_arrGOPoints[i].FCoord_Y;

                // Minimum Y - coordinate
                if (cmodel.m_arrGOPoints[i].FCoord_Y < fTempMin_Y)
                    fTempMin_Y = cmodel.m_arrGOPoints[i].FCoord_Y;

                // Maximum Z - coordinate
                if (cmodel.m_arrGOPoints[i].FCoord_Z > fTempMax_Z)
                    fTempMax_Z = cmodel.m_arrGOPoints[i].FCoord_Z;

                // Minimum Z - coordinate
                if (cmodel.m_arrGOPoints[i].FCoord_Z < fTempMin_Z)
                    fTempMin_Z = cmodel.m_arrGOPoints[i].FCoord_Z;
            }
        }
        else
        {
            // Exception - no definition nodes or points
        }
    }

    private GeometryModel3D getMemberGeometryModel3D(EGCS eGCS, SolidColorBrush brush, CCrSc obj_CrScA, CCrSc obj_CrScB, Point3D mpA, Point3D mpB, double dTheta_x)
    {
      GeometryModel3D model = new GeometryModel3D();

      MeshGeometry3D mesh = getMeshMemberGeometry3DFromCrSc(eGCS, obj_CrScA, obj_CrScB, mpA, mpB, dTheta_x); // Mesh one member

      model.Geometry = mesh;

      model.Material = new DiffuseMaterial(brush);  // Set MemberModel Material

      return model;
    }

    private MeshGeometry3D getMeshMemberGeometry3DFromCrSc(EGCS eGCS, CCrSc obj_CrScA, CCrSc obj_CrScB, Point3D mpA, Point3D mpB, double dTheta_x)
    {
      MeshGeometry3D mesh = new MeshGeometry3D();
      mesh.Positions = new Point3DCollection();

      // Main Nodes of Member
      Point3D m_pA = mpA;
      Point3D m_pB = mpB;

      // Angle of rotation about local x-axis
      double m_dTheta_x = dTheta_x;

      // Priemet do osi GCS - rozdiel suradnic v GCS
      double m_dDelta_X = m_pB.X - m_pA.X;
      double m_dDelta_Y = m_pB.Y - m_pA.Y;
      double m_dDelta_Z = m_pB.Z - m_pA.Z;

      // Realna dlzka prvku // Length of member - straigth segment of member
      // Prečo je záporná ???
      // double m_dLength = -Math.Sqrt(Math.Pow(m_dDelta_X, 2) + Math.Pow(m_dDelta_Y, 2) + Math.Pow(m_dDelta_Z, 2));
         double m_dLength = Math.Sqrt(Math.Pow(m_dDelta_X, 2) + Math.Pow(m_dDelta_Y, 2) + Math.Pow(m_dDelta_Z, 2));

      // Number of Points per section
      short iNoCrScPoints2D;
      // Points 2D Coordinate Array
      if (obj_CrScA.IsShapeSolid) // Solid I,U,Z,HL,L, ..............
      {
          iNoCrScPoints2D = obj_CrScA.ITotNoPoints; // Depends on Section Type
        // Fill Mesh Positions for Start and End Section of Element - Defines Edge Points of Element

        if (obj_CrScA.CrScPointsOut != null) // Check that data are available
        {
          for (int j = 0; j < iNoCrScPoints2D; j++)
          {
              // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1]));
          }
          for (int j = 0; j < iNoCrScPoints2D; j++)
          {
              // X - end, Y, Z
              if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1])); // Constant size member
              else
                mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsOut[j, 0], obj_CrScB.CrScPointsOut[j, 1])); // Tapered member
          }
        }
        else
        {
          // Exception
        }
      }
      else if (obj_CrScA.INoPointsOut == obj_CrScA.INoPointsIn) // Closed cross-section with same number out ouside and insdide definiton points
      {
        // Tubes , Polygonal Hollow Sections
        iNoCrScPoints2D = (short)(2 * obj_CrScA.INoPointsOut); // Twice number of one surface

        // Tube, regular hollow sections
        // TU

        // Start
        if (obj_CrScA.CrScPointsOut != null) // Check that data are available
        {
          // OutSide Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsOut; j++)
          {
            // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1]));
          }
        }
        else
        {
          // Exception
        }

        if (obj_CrScA.CrScPointsIn != null) // Check that data are available
        {
          // Inside Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsIn; j++)
          {
            // X - start, Y, Z
            mesh.Positions.Add(new Point3D(0,obj_CrScA.CrScPointsIn[j, 0], obj_CrScA.CrScPointsIn[j, 1]));
          }
        }
        else
        {
          // Exception
        }

        // End
        if (obj_CrScA.CrScPointsOut != null) // Check that data are available
        {
          // OutSide Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsOut; j++)
          {
              // X - end, Y, Z
              if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1])); // Constant size member
              else
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsOut[j, 0], obj_CrScB.CrScPointsOut[j, 1])); // Tapered member
          }
        }
        else
        {
          // Exception
        }

        if (obj_CrScA.CrScPointsIn != null) // Check that data are available
        {
          // Inside Radius Points
          for (int j = 0; j < obj_CrScA.INoPointsIn; j++)
          {
              // X - end, Y, Z
              if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsIn[j, 0], obj_CrScA.CrScPointsIn[j, 1])); // Constant size member
              else
                  mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsIn[j, 0], obj_CrScB.CrScPointsIn[j, 1])); // Tapered member
          }
        }
        else
        {
          // Exception
        }
      }
      else
      {
         // Exception
         // Closed cross-section with different number out ouside and insdide definiton points

          iNoCrScPoints2D = 0; // Temp
      }

      // Dislay data in the output window

      string sOutput = "Before transformation \n\n"; // create temporary string

      for (int i = 0; i < 2 * iNoCrScPoints2D; i++) // for all mesh positions (start and end of member, number of edge points of whole member = 2 * number in one section)
      {
          Point3D p3D = mesh.Positions[i]; // Get mesh element/item (returns Point3D)

          sOutput += "Node ID: " + i.ToString();
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.X.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Y.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Z.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns

          sOutput += "\n"; // New row
      }

        if(bDebugging)
            System.Console.Write(sOutput); // Write in console window


      // Transform coordinates
      TransformMember_LCStoGCS(eGCS, m_pA, m_pB, m_dDelta_X, m_dDelta_Y, m_dDelta_Z, m_dTheta_x, mesh.Positions);

      // Mesh Triangles - various cross-sections shapes defined
      mesh.TriangleIndices = obj_CrScA.TriangleIndices;


     // Dislay data in the output window

      sOutput = null;
      sOutput = "After transformation \n\n"; // create temporary string

      for (int i = 0; i < 2 * iNoCrScPoints2D; i++) // for all mesh positions (start and end of member, number of edge points of whole member = 2 * number in one section)
      {
          Point3D p3D = mesh.Positions[i]; // Get mesh element/item (returns Point3D)

          sOutput += "Node ID: " + i.ToString();
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.X.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Y.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns
          sOutput += p3D.Z.ToString("0.0000");
          sOutput += "\t"; // New Tab between columns

          sOutput += "\n"; // New row
      }

        if(bDebugging)
            System.Console.Write(sOutput); // Write in console window

      // Change mesh triangle indices
      // Change orientation of normals

      //if (eGCS == EGCS.eGCSLeftHanded)
      //{
          int iSecond = 1;
          int iThird = 2;

          int iTIcount = mesh.TriangleIndices.Count;
          for (int i = 0; i < iTIcount / 3; i++)
          {
              int iTI_2 = mesh.TriangleIndices[iSecond];
              int iTI_3 = mesh.TriangleIndices[iThird];

              mesh.TriangleIndices[iThird] = iTI_2;
              mesh.TriangleIndices[iSecond] = iTI_3;

              iSecond += 3;
              iThird += 3;
          }
      //}


      return mesh;
    }

    public Point3DCollection TransformMember_LCStoGCS(EGCS eGCS, Point3D pA, Point3D pB, double dDeltaX, double dDeltaY, double dDeltaZ, double dTheta_x, Point3DCollection pointsCollection)
    {
      // Returns transformed coordinates of member nodes

      // Angles
      double dAlphaX = 0, dBetaY = 0, dGammaZ = 0;

      // Priemet do rovin GCS - dlzka priemetu do roviny
      double dLength_XY = 0,
             dLength_YZ = 0,
             dLength_XZ = 0;

      if (!MathF.d_equal(dDeltaX, 0.0) || !MathF.d_equal(dDeltaY, 0.0))
        dLength_XY = Math.Sqrt(Math.Pow(dDeltaX, 2) + Math.Pow(dDeltaY, 2));

      if (!MathF.d_equal(dDeltaY, 0.0) || !MathF.d_equal(dDeltaZ, 0.0))
        dLength_YZ = Math.Sqrt(Math.Pow(dDeltaY, 2) + Math.Pow(dDeltaZ, 2));

      if (!MathF.d_equal(dDeltaX, 0.0) || !MathF.d_equal(dDeltaZ, 0.0))
        dLength_XZ = Math.Sqrt(Math.Pow(dDeltaX, 2) + Math.Pow(dDeltaZ, 2));

      if (bDebugging)
      {
          // Temporary console output
          System.Console.Write("\n" + "Lengths - projection of element into global coordinate system:\n");
          System.Console.Write("Length - global X-axis:\t" + dDeltaX.ToString("0.000") + "\n"); // Write length in X-axis
          System.Console.Write("Length - global Y-axis:\t" + dDeltaY.ToString("0.000") + "\n"); // Write length in Y-axis
          System.Console.Write("Length - global Z-axis:\t" + dDeltaZ.ToString("0.000") + "\n\n"); // Write length in Z-axis
      }
      // Uhly pootocenia LCS okolo osi GCS
      // Angles
      dAlphaX = Geom2D.GetAlpha2D_CW(dDeltaY, dDeltaZ);
      dBetaY = Geom2D.GetAlpha2D_CW_2(dDeltaX, dDeltaZ); // !!! Pre pootocenie okolo Y su pouzite ine kvadranty !!!
      dGammaZ = Geom2D.GetAlpha2D_CW(dDeltaX, dDeltaY);

      // Auxialiary angles for members graphics
      double dBetaY_aux = Geom2D.GetAlpha2D_CW_3(dDeltaX, dDeltaZ, Math.Sqrt(Math.Pow(dLength_XY,2) + Math.Pow(dDeltaZ,2)));
      double dGammaZ_aux = dGammaZ;
        if(Math.PI / 2 < dBetaY && dBetaY < 1.5*Math.PI)
        { 
          if(dGammaZ < Math.PI)
            dGammaZ_aux = dGammaZ + Math.PI;
          else
            dGammaZ_aux = dGammaZ - Math.PI;
        }

        if (bDebugging)
        {
            // Temporary console output
            System.Console.Write("\n" + "Rotation angles:\n");
            System.Console.Write("Rotation about global X-axis:\t" + dAlphaX.ToString("0.000") + "rad\t " + (dAlphaX * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about X-axis
            System.Console.Write("Rotation about global Y-axis:\t" + dBetaY.ToString("0.000") + "rad\t " + (dBetaY * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about Y-axis
            System.Console.Write("Rotation about global Z-axis:\t" + dGammaZ.ToString("0.000") + "rad\t " + (dGammaZ * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about Z-axis
            System.Console.Write("\n" + "Auxiliary rotation angles - graphics:\n");
            System.Console.Write("Rotation about global Y-axis:\t" + dBetaY_aux.ToString("0.000") + "rad\t " + (dBetaY_aux * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write auxiliary rotation about Y-axis
            System.Console.Write("Rotation about global Z-axis:\t" + dGammaZ_aux.ToString("0.000") + "rad\t " + (dGammaZ_aux * 180.0f / MathF.fPI).ToString("0.0") + "deg \n\n"); // Write auxiliary rotation about Z-axis
        }

      for (int i = 0; i < pointsCollection.Count; i++)
      {
          pointsCollection[i] = RotatePoint(eGCS, pA, pointsCollection[i], dBetaY_aux, dGammaZ_aux, dTheta_x, dDeltaX, dDeltaY, dDeltaZ);
      }

      return pointsCollection;
    }

    protected Point3D RotatePoint(EGCS eGCS, Point3D pA, Point3D p, double betaY, double gamaZ, double theta_x, double dDeltaX, double dDeltaY, double dDeltaZ)
    {
        Point3D p3Drotated = new Point3D();

        //http://sk.wikipedia.org/wiki/Trojrozmern%C3%A1_projekcia#D.C3.A1ta_nevyhnutn.C3.A9_pre_projekciu

        // Left Handed
        // * Where (alphaX) represents the rotation about the X axis, (betaY) represents the rotation about the Y axis, and (gamaZ) represents the rotation about the Z axis
        /*
        X Rotation *

        1     0                0                  0
        0     cos (alphaX)    -sin (alphaX)       0
        0     sin (alphaX)     cos (alphaX)       0
        0     0                0                  1
        */

        /*
        Y Rotation *

        cos (betaY)    0     sin (betaY)    0
        0              1     0              0
        -sin (betaY)   0     cos (betaY)    0
        0              0     0              1
        */

        /*
        Z Rotation *

        cos (gamaZ)     -sin (gamaZ)     0      0
        sin (gamaZ)     cos (gamaZ)      0      0
        0                 0              1      0
        0                 0              0      1
        */

        ////////////////////////////////////////////////////////////////////////////////////////////
        // Right Handed

        /*
        X Rotation *

        1           0             0               0
        0     cos (alphaX)     sin (alphaX)       0
        0     -sin (alphaX)    cos (alphaX)       0
        0           0             0               1
        */

        /*
        Y Rotation *

        cos (betaY)    0     -sin (betaY)   0
        0              1     0              0
        sin (betaY)    0     cos (betaY)    0
        0              0     0              1
        */

        /*
        Z Rotation *

        cos (gamaZ)     sin (gamaZ)      0      0
        -sin (gamaZ)    cos (gamaZ)      0      0
        0                 0              1      0
        0                 0              0      1
        */

      Point3D pTemp1 = new Point3D();
      Point3D pTemp2 = new Point3D();

      if (eGCS == EGCS.eGCSLeftHanded)
      {
          // Left-handed

          // Rotate about Y-axis
          pTemp1.X = (Math.Cos(betaY) * p.X) + (Math.Sin(betaY) * p.Z);
          pTemp1.Y = p.Y;
          pTemp1.Z = (-Math.Sin(betaY) * p.X) + (Math.Cos(betaY) * p.Z);

          // Rotate about Z-axis
          pTemp2.X = (Math.Cos(gamaZ) * pTemp1.X) - (Math.Sin(gamaZ) * pTemp1.Y);
          pTemp2.Y = (Math.Sin(gamaZ) * pTemp1.X) + (Math.Cos(gamaZ) * pTemp1.Y);
          pTemp2.Z = pTemp1.Z;

          // Translate
          pTemp1.X = pA.X + pTemp2.X;
          pTemp1.Y = pA.Y + pTemp2.Y;
          pTemp1.Z = pA.Z + pTemp2.Z;

          // Set output point data
          p3Drotated.X = pTemp1.X;
          p3Drotated.Y = pTemp1.Y;
          p3Drotated.Z = pTemp1.Z;

          // Rotate about local x-axis
          if (!MathF.d_equal(theta_x, 0.0))
          {
              double c = Math.Cos(theta_x);
              double s = Math.Sin(theta_x);
              double t = 1 - c;

              p3Drotated.X = (t * Math.Pow(dDeltaX, 2) + c) * pTemp1.X + (t * dDeltaX * dDeltaY - s * dDeltaZ) * pTemp1.Y + (t * dDeltaX * dDeltaZ + s * dDeltaY) * pTemp1.Z;
              p3Drotated.Y = (t * dDeltaX * dDeltaY + s * dDeltaZ) * pTemp1.X + (t * Math.Pow(dDeltaY, 2) + c) * pTemp1.Y + (t * dDeltaY * dDeltaZ - s * dDeltaX) * pTemp1.Z;
              p3Drotated.Z = (t * dDeltaX * dDeltaZ - s * dDeltaY) * pTemp1.X + (t * dDeltaY * dDeltaZ + s * dDeltaX) * pTemp1.Y + (t * Math.Pow(dDeltaZ, 2) + c) * pTemp1.Z;
          }
      }
      else
      {
          // Right-handed

          // Rotate about Y-axis
          pTemp1.X = (Math.Cos(betaY) * p.X) - (Math.Sin(betaY) * p.Z);
          pTemp1.Y = p.Y;
          pTemp1.Z = (Math.Sin(betaY) * p.X) + (Math.Cos(betaY) * p.Z);

          // Rotate about Z-axis
          pTemp2.X = (Math.Cos(gamaZ) * pTemp1.X) + (Math.Sin(gamaZ) * pTemp1.Y);
          pTemp2.Y = (-Math.Sin(gamaZ) * pTemp1.X) + (Math.Cos(gamaZ) * pTemp1.Y);
          pTemp2.Z = pTemp1.Z;

          // Translate
          pTemp1.X = pA.X + pTemp2.X;
          pTemp1.Y = pA.Y + pTemp2.Y;
          pTemp1.Z = pA.Z + pTemp2.Z;

          // Set output point data
          p3Drotated.X = pTemp1.X;
          p3Drotated.Y = pTemp1.Y;
          p3Drotated.Z = pTemp1.Z;

          // Rotate about local x-axis
          if (!MathF.d_equal(theta_x, 0.0))
          {
              double c = Math.Cos(theta_x);
              double s = Math.Sin(theta_x);
              double t = 1 - c;

              p3Drotated.X = (t * Math.Pow(dDeltaX, 2) + c) * pTemp1.X + (t * dDeltaX * dDeltaY + s * dDeltaZ) * pTemp1.Y + (t * dDeltaX * dDeltaZ - s * dDeltaY) * pTemp1.Z;
              p3Drotated.Y = (t * dDeltaX * dDeltaY - s * dDeltaZ) * pTemp1.X + (t * Math.Pow(dDeltaY, 2) + c) * pTemp1.Y + (t * dDeltaY * dDeltaZ + s * dDeltaX) * pTemp1.Z;
              p3Drotated.Z = (t * dDeltaX * dDeltaZ + s * dDeltaY) * pTemp1.X + (t * dDeltaY * dDeltaZ - s * dDeltaX) * pTemp1.Y + (t * Math.Pow(dDeltaZ, 2) + c) * pTemp1.Z;
          }

      }

        return p3Drotated;
    }

    protected Point3D RotatePoint_POKUSY(EGCS eGCS, Point3D pA, Point3D p, double alphaX, double betaY, double gamaZ, double dDeltaX, double dDeltaY, double dDeltaZ)
    {
      Point3D p3Drotated = new Point3D();

      /* Commented 25.5.2013
      // Left Handed

      p3Drotated.X = pA.X + p.X;
      p3Drotated.Y = pA.Y + (p.Y * Math.Cos(alphaX) - p.Z * Math.Sin(alphaX));
      p3Drotated.Z = pA.Z + (p.Y * Math.Sin(alphaX) + p.Z * Math.Cos(alphaX));

      p3Drotated.X = pA.X + (p.X * Math.Cos(betaY) + p.Z * Math.Sin(betaY));
      p3Drotated.Y = pA.Y + p.Y;
      p3Drotated.Z = pA.Z + (-p.X * Math.Sin(betaY) + p.Z * Math.Cos(betaY));

      p3Drotated.X = pA.X + (p.X * Math.Cos(gamaZ) - p.Y * Math.Sin(gamaZ));
      p3Drotated.Y = pA.Y + (p.X * Math.Sin(gamaZ) + p.Y * Math.Cos(gamaZ));
      p3Drotated.Z = pA.Z + p.Z;
      */

      // Left Handed
      //http://sk.wikipedia.org/wiki/Trojrozmern%C3%A1_projekcia#D.C3.A1ta_nevyhnutn.C3.A9_pre_projekciu
      // * Where (alphaX) represents the rotation about the X axis, (betaY) represents the rotation about the Y axis, and (gamaZ) represents the rotation about the Z axis
      /*
      X Rotation *

      1     0                0                  0
      0     cos (alphaX)    -sin (alphaX)       0
      0     sin (alphaX)     cos (alphaX)       0
      0     0                0                  1
      */

      /*
      Y Rotation *

      cos (betaY)    0     sin (betaY)    0
      0              1     0              0
      -sin (betaY)   0     cos (betaY)    0
      0              0     0              1
      */

      /*
      Z Rotation *

      cos (gamaZ)     -sin (gamaZ)     0      0
      sin (gamaZ)     cos (gamaZ)      0      0
      0                 0              1      0
      0                 0              0      1
      */

      ////////////////////////////////////////////////////////////////////////////////////////////
      // Right Handed

      /*
      X Rotation *

      1           0             0               0
      0     cos (alphaX)     sin (alphaX)       0
      0     -sin (alphaX)    cos (alphaX)       0
      0           0             0               1
      */

      /*
      Y Rotation *

      cos (betaY)    0     -sin (betaY)   0
      0              1     0              0
      sin (betaY)    0     cos (betaY)    0
      0              0     0              1
      */

      /*
      Z Rotation *

      cos (gamaZ)     sin (gamaZ)      0      0
      -sin (gamaZ)    cos (gamaZ)      0      0
      0                 0              1      0
      0                 0              0      1
      */

      /*
      Point3D pTemp1 = new Point3D();
      Point3D pTemp2 = new Point3D();

      if (eGCS == EGCS.eGCSLeftHanded)
      {
          // Left handed

          pTemp1.X = p.X;
          pTemp1.Y = (Math.Cos(alphaX) * p.Y) - (Math.Sin(alphaX) * p.Z);
          pTemp1.Z = (Math.Sin(alphaX) * p.Y) + (Math.Cos(alphaX) * p.Z);

          pTemp2.X = (Math.Cos(betaY) * pTemp1.X) + (Math.Sin(betaY) * pTemp1.Z);
          pTemp2.Y = pTemp1.Y;
          pTemp2.Z = (-Math.Sin(betaY) * pTemp1.X) + (Math.Cos(betaY) * pTemp1.Z);

          p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * pTemp2.X) - (Math.Sin(gamaZ) * pTemp2.Y));
          p3Drotated.Y = pA.Y + ((Math.Sin(gamaZ) * pTemp2.X) + (Math.Cos(gamaZ) * pTemp2.Y));
          p3Drotated.Z = pA.Z + pTemp2.Z;
      }
      else
      {
          // Right handed

          pTemp1.X = p.X;
          pTemp1.Y = (Math.Cos(alphaX) * p.Y) + (Math.Sin(alphaX) * p.Z);
          pTemp1.Z = (-Math.Sin(alphaX) * p.Y) + (Math.Cos(alphaX) * p.Z);

          pTemp2.X = (Math.Cos(betaY) * pTemp1.X) - (Math.Sin(betaY) * pTemp1.Z);
          pTemp2.Y = pTemp1.Y;
          pTemp2.Z = (Math.Sin(betaY) * pTemp1.X) + (Math.Cos(betaY) * pTemp1.Z);

          p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * pTemp2.X) + (Math.Sin(gamaZ) * pTemp2.Y));
          p3Drotated.Y = pA.Y + ((-Math.Sin(gamaZ) * pTemp2.X) + (Math.Cos(gamaZ) * pTemp2.Y));
          p3Drotated.Z = pA.Z + pTemp2.Z;
      }
      */

      // In case that member is parallel to global axis should be rotated only once
      if (dDeltaX < 0 && MathF.d_equal(dDeltaY, 0.0) && MathF.d_equal(dDeltaZ, 0.0))      // Parallel to X-axis with negative orientation
          betaY = 0; // Do not rotate about Y-axis
      else if (MathF.d_equal(dDeltaX, 0.0) && dDeltaY < 0 && MathF.d_equal(dDeltaZ, 0.0)) // Parallel to Y-axis with negative orientation
          alphaX = 0; // Do not rotate about X-axis
      else if (MathF.d_equal(dDeltaX, 0.0) && MathF.d_equal(dDeltaY, 0.0) && dDeltaZ < 0) // Parallel to Z-axis with negative orientation
          betaY = 0; // Do not rotate about Y-axis
      else
      {
          // No action - General position of member in space
      }

      // Cumulative 3D rotation and translation
      // Temp - pokus 1
      // Rotate around x, y, z
      double ax = Math.Cos(betaY) * Math.Cos(gamaZ);
      double ay = (Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ));
      double az = (Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ));

      p3Drotated.X = ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + ((Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.Y + ((Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.Z);

      double bx = Math.Cos(betaY) * Math.Sin(gamaZ);
      double by = (Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ));
      double bz = (-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ));

      p3Drotated.Y = ((Math.Cos(betaY) * Math.Sin(gamaZ)) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + ((-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Z);

      double cx = -Math.Sin(betaY);
      double cy = Math.Cos(betaY) * Math.Sin(alphaX);
      double cz = Math.Cos(alphaX) * Math.Cos(betaY);

      p3Drotated.Z = ((-Math.Sin(betaY)) * p.X + (Math.Cos(betaY) * Math.Sin(alphaX)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);


      // Temp - pokus 2

      if (gamaZ < Math.PI)
          gamaZ += Math.PI;
      else if(gamaZ > Math.PI)
          gamaZ -= Math.PI;


      // X Rotation
      ax = 1 * p.X + 0 * p.Y + 0 * p.Z;
      ay = 0 * p.X + Math.Cos(alphaX) * p.Y - Math.Sin(alphaX) * p.Z;
      az = Math.Sin(alphaX) * p.X + Math.Cos(alphaX) * p.Y + 0 * p.Z;

      // Y Rotation
      bx = Math.Cos(betaY) * ax + 0 * ay + Math.Sin(betaY) * az;
      by = 0 * ax + 1 * ay + 0 * az;
      bz = -Math.Sin(betaY) * ax + 0 * ay + Math.Cos(betaY) * az;

      // Z Rotation
      cx = Math.Cos(gamaZ) * bx - Math.Sin(gamaZ) * by + 0 * bz;
      cy = Math.Sin(gamaZ) * bx + Math.Cos(gamaZ) * by + 0 * bz;
      cz = 0 * bx + 0 * by + 1 * bz;

      p3Drotated.X = pA.X + cx;
      p3Drotated.Y = pA.Y + cy;
      p3Drotated.Z = pA.Z + cz;

      p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + ((Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.Y + ((Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.Z);
      p3Drotated.Y = pA.Y + ((Math.Cos(betaY) * Math.Sin(gamaZ)) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + ((-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Z);
      p3Drotated.Z = pA.Z + ((-Math.Sin(betaY)) * p.X + (Math.Cos(betaY) * Math.Sin(alphaX)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);

      // Rotate around z, y, x
      /*
      p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + (-Math.Cos(betaY) * Math.Sin(gamaZ)) * p.Y + (Math.Sin(betaY)) * p.Z);
      p3Drotated.Y = pA.Y + (((Math.Cos(alphaX) * Math.Sin(gamaZ)) + (Math.Sin(alphaX)*Math.Sin(betaY) * Math.Cos(gamaZ))) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) - (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + (-Math.Sin(alphaX) * Math.Cos(betaY)) * p.Z);
      p3Drotated.Z = pA.Z + (((Math.Sin(alphaX) * Math.Sin(gamaZ)) - (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Cos(gamaZ))) * p.X + ((Math.Sin(alphaX) * Math.Cos(gamaZ)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin (gamaZ))) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);
      */

      // Right-handed cumulative ???
      // http://scipp.ucsc.edu/~haber/ph216/rotation_12.pdf
      /*
      p3Drotated.X = pA.X + (((Math.Cos(alphaX) * Math.Cos(betaY) * Math.Cos(gamaZ)) - (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.X + (-Math.Cos(alphaX) * Math.Cos(betaY) * Math.Sin(gamaZ) - (Math.Sin(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Cos(alphaX) * Math.Sin(betaY)) * p.Z);
      p3Drotated.Y = pA.Y + (((Math.Sin(alphaX) * Math.Cos(betaY) * Math.Cos(gamaZ)) + (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.X + (-Math.Sin(alphaX) * Math.Cos(betaY) * Math.Sin(gamaZ) + (Math.Cos(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Sin(alphaX) * Math.Sin(betaY)) * p.Z);
      p3Drotated.Z = pA.Z + ((-Math.Sin(betaY) * Math.Cos(gamaZ)) * p.X + (Math.Sin(betaY) * Math.Sin(gamaZ)) * p.Y + (Math.Cos(betaY)) * p.Z);
      */

      //
      // http://inside.mines.edu/~gmurray/ArbitraryAxisRotation/
      /*
      p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * Math.Cos(betaY)) * p.X + (-Math.Sin(gamaZ) * Math.Cos(betaY)) * p.Y + Math.Sin(betaY) * p.Z);
      p3Drotated.Y = pA.Y + (((Math.Cos(gamaZ) * Math.Sin(betaY) * Math.Sin(alphaX)) + (Math.Sin(gamaZ) * Math.Cos(alphaX))) * p.X + ((Math.Cos(gamaZ) * Math.Cos(alphaX)) - (Math.Sin(gamaZ) * Math.Sin(betaY) * Math.Sin(alphaX))) * p.Y + (-Math.Cos(betaY) * Math.Sin(alphaX)) * p.Z);
      p3Drotated.Z = pA.Z + (((Math.Sin(gamaZ) * Math.Sin(alphaX)) - (Math.Cos(gamaZ) * Math.Sin(betaY) * Math.Cos(alphaX))) * p.X + ((Math.Sin(gamaZ) * Math.Sin(betaY) * Math.Cos(alphaX)) + (Math.Sin(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Cos(betaY) * Math.Cos(alphaX)) * p.Z);
      */


      return p3Drotated;


        // Mozno by som mal zapracovat toto
        //http://mathworld.wolfram.com/EulerAngles.html
    }

    public GeometryModel3D CreateGM_3D_Volume_8Edeges(CVolume volume)
    {

        Point3D solidControlEdge = new Point3D(volume.m_pControlPoint.FCoord_X, volume.m_pControlPoint.FCoord_Y, volume.m_pControlPoint.FCoord_Z);

        /*
        Point3D p0 = new Point3D(solidControlEdge.X               , solidControlEdge.Y               , solidControlEdge.Z);
        Point3D p1 = new Point3D(solidControlEdge.X + volume.m_fDim1, solidControlEdge.Y               , solidControlEdge.Z);
        Point3D p2 = new Point3D(solidControlEdge.X + volume.m_fDim1, solidControlEdge.Y + volume.m_fDim2, solidControlEdge.Z);
        Point3D p3 = new Point3D(solidControlEdge.X               , solidControlEdge.Y + volume.m_fDim2, solidControlEdge.Z);
        Point3D p4 = new Point3D(solidControlEdge.X, solidControlEdge.Y, solidControlEdge.Z + volume.m_fDim3);
        Point3D p5 = new Point3D(solidControlEdge.X + volume.m_fDim1, solidControlEdge.Y, solidControlEdge.Z + volume.m_fDim3);
        Point3D p6 = new Point3D(solidControlEdge.X + volume.m_fDim1, solidControlEdge.Y + volume.m_fDim2, solidControlEdge.Z + volume.m_fDim3);
        Point3D p7 = new Point3D(solidControlEdge.X, solidControlEdge.Y + volume.m_fDim2, solidControlEdge.Z + volume.m_fDim3);
        */

        return CreateGM_3D_Volume_8Edeges(solidControlEdge, volume.m_fDim1, volume.m_fDim2, volume.m_fDim3, volume.m_volColor, volume.m_fvolOpacity);

    }
    public GeometryModel3D CreateGM_3D_Volume_8Edeges(Point3D solidControlEdge, float fDim1, float fDim2, float fDim3, Color volColor, float fvolOpacity)
    {
        Point3D p0 = new Point3D(solidControlEdge.X, solidControlEdge.Y, solidControlEdge.Z);
        Point3D p1 = new Point3D(solidControlEdge.X + fDim1, solidControlEdge.Y, solidControlEdge.Z);
        Point3D p2 = new Point3D(solidControlEdge.X + fDim1, solidControlEdge.Y + fDim2, solidControlEdge.Z);
        Point3D p3 = new Point3D(solidControlEdge.X, solidControlEdge.Y + fDim2, solidControlEdge.Z);
        Point3D p4 = new Point3D(solidControlEdge.X, solidControlEdge.Y, solidControlEdge.Z + fDim3);
        Point3D p5 = new Point3D(solidControlEdge.X + fDim1, solidControlEdge.Y, solidControlEdge.Z + fDim3);
        Point3D p6 = new Point3D(solidControlEdge.X + fDim1, solidControlEdge.Y + fDim2, solidControlEdge.Z + fDim3);
        Point3D p7 = new Point3D(solidControlEdge.X, solidControlEdge.Y + fDim2, solidControlEdge.Z + fDim3);

        MeshGeometry3D meshGeom3D = new MeshGeometry3D(); // Create geometry mesh

        meshGeom3D.Positions = new Point3DCollection();

        meshGeom3D.Positions.Add(p0);
        meshGeom3D.Positions.Add(p1);
        meshGeom3D.Positions.Add(p2);
        meshGeom3D.Positions.Add(p3);
        meshGeom3D.Positions.Add(p4);
        meshGeom3D.Positions.Add(p5);
        meshGeom3D.Positions.Add(p6);
        meshGeom3D.Positions.Add(p7);

        Int32Collection TriangleIndices = new Int32Collection();

        //Bottom
        TriangleIndices.Add(0);
        TriangleIndices.Add(2);
        TriangleIndices.Add(1);

        TriangleIndices.Add(0);
        TriangleIndices.Add(3);
        TriangleIndices.Add(2);


        // Top
        TriangleIndices.Add(4);
        TriangleIndices.Add(6);
        TriangleIndices.Add(7);

        TriangleIndices.Add(4);
        TriangleIndices.Add(5);
        TriangleIndices.Add(6);

        // Side
        TriangleIndices.Add(0);
        TriangleIndices.Add(5);
        TriangleIndices.Add(4);

        TriangleIndices.Add(0);
        TriangleIndices.Add(1);
        TriangleIndices.Add(5);

        TriangleIndices.Add(1);
        TriangleIndices.Add(6);
        TriangleIndices.Add(5);

        TriangleIndices.Add(1);
        TriangleIndices.Add(2);
        TriangleIndices.Add(6);

        TriangleIndices.Add(2);
        TriangleIndices.Add(7);
        TriangleIndices.Add(6);

        TriangleIndices.Add(2);
        TriangleIndices.Add(3);
        TriangleIndices.Add(7);

        TriangleIndices.Add(3);
        TriangleIndices.Add(4);
        TriangleIndices.Add(7);

        TriangleIndices.Add(3);
        TriangleIndices.Add(0);
        TriangleIndices.Add(4);

        meshGeom3D.TriangleIndices = TriangleIndices;

        GeometryModel3D geomModel3D = new GeometryModel3D();

        geomModel3D.Geometry = meshGeom3D; // Set mesh to model

        SolidColorBrush brushSolid = new SolidColorBrush(volColor);
        brushSolid.Opacity = fvolOpacity;

        geomModel3D.Material = new DiffuseMaterial(brushSolid);

        return geomModel3D;
    }

    // Temporary auxiliary function - glass window (3D HOUSE)

    public Model3DGroup CreateGM_3D_Window(int iSegmentNum, Point3D pControlPoint, float fL_X, float fH_Z, float fT_Y, float fRotationZRadians)
    {
        Model3DGroup gr = new Model3DGroup();

        float fGlassThickness = 0.016f;

        Point3D p01_HB = new Point3D(0,0,0);
        Point3D p02_HU = new Point3D(0,0, fH_Z - fT_Y);
        Point3D p03_V = new Point3D(0,0,fT_Y);
        Point3D p04_V = new Point3D(0.5 * fL_X - 0.5f * fT_Y, 0, fT_Y);
        Point3D p05_V = new Point3D(fL_X - fT_Y, 0, fT_Y);

        Point3D p06_GlassTable = new Point3D(fT_Y, 0.5f * fT_Y - 0.5f * fGlassThickness, fT_Y);
        Point3D p07_GlassTable = new Point3D(0.5f * fL_X + 0.5f * fT_Y, 0.5f * fT_Y - 0.5f * fGlassThickness, fT_Y);

        Point3D [] pArray = new Point3D[7];

        pArray[0] = p01_HB;
        pArray[1] = p02_HU;
        pArray[2] = p03_V;
        pArray[3] = p04_V;
        pArray[4] = p05_V;
        pArray[5] = p06_GlassTable;
        pArray[6] = p07_GlassTable;

        for (int i = 0; i < pArray.Length; i++)
        {
            // Rotate objects - 2D about vertical axis

            double dx = pArray[i].X;
            double dy = pArray[i].Y + (0.5f * (0.5f - fT_Y));

            pArray[i].X = dx * Math.Cos(fRotationZRadians) - dy * Math.Sin(fRotationZRadians);
            pArray[i].Y = dx * Math.Sin(fRotationZRadians) + dy * Math.Cos(fRotationZRadians);

            // Move objects

            pArray[i].X += pControlPoint.X;
            pArray[i].Y += pControlPoint.Y;
            pArray[i].Z += pControlPoint.Z;
        }

        Color cFrameColor = new Color();
        cFrameColor = Color.FromRgb(51, 0, 0);
        float fFrameOpacity = 0.99f;

        Color cGlassColor = new Color();
        cGlassColor = Color.FromRgb(102, 255, 255);
        float fGlassOpacity = 0.5f;

        GeometryModel3D mFrame_01_HB = new GeometryModel3D(); // Horizontal bottom
        GeometryModel3D mFrame_02_HU = new GeometryModel3D(); // Horizontal upper

        GeometryModel3D mFrame_03_V = new GeometryModel3D(); // Vertical
        GeometryModel3D mFrame_04_V = new GeometryModel3D(); // Vertical
        GeometryModel3D mFrame_05_V = new GeometryModel3D(); // Vertical

        GeometryModel3D mGlassTable_01 = new GeometryModel3D(); // Glass No 1
        GeometryModel3D mGlassTable_02 = new GeometryModel3D(); // Glass No 2

        if (fRotationZRadians == 0)
        {
            mFrame_01_HB = CreateGM_3D_Volume_8Edeges(pArray[0], fL_X, fT_Y, fT_Y, cFrameColor, fFrameOpacity);
            mFrame_02_HU = CreateGM_3D_Volume_8Edeges(pArray[1], fL_X, fT_Y, fT_Y, cFrameColor, fFrameOpacity);
            mFrame_03_V = CreateGM_3D_Volume_8Edeges(pArray[2], fT_Y, fT_Y, fH_Z - 2 * fT_Y, cFrameColor, fFrameOpacity);
            mFrame_04_V = CreateGM_3D_Volume_8Edeges(pArray[3], fT_Y, fT_Y, fH_Z - 2 * fT_Y, cFrameColor, fFrameOpacity);
            mFrame_05_V = CreateGM_3D_Volume_8Edeges(pArray[4], fT_Y, fT_Y, fH_Z - 2 * fT_Y, cFrameColor, fFrameOpacity);

            mGlassTable_01 = CreateGM_3D_Volume_8Edeges(pArray[5], 0.5f * (fL_X - 3 * fT_Y), fGlassThickness, fH_Z - 2 * fT_Y, cGlassColor, fGlassOpacity);
            mGlassTable_02 = CreateGM_3D_Volume_8Edeges(pArray[6], 0.5f * (fL_X - 3 * fT_Y), fGlassThickness, fH_Z - 2 * fT_Y, cGlassColor, fGlassOpacity);
        }
        else if (fRotationZRadians == (float)Math.PI / 2)
        {
            mFrame_01_HB = CreateGM_3D_Volume_8Edeges(pArray[0], fT_Y, fL_X, fT_Y, cFrameColor, fFrameOpacity);
            mFrame_02_HU = CreateGM_3D_Volume_8Edeges(pArray[1], fT_Y, fL_X, fT_Y, cFrameColor, fFrameOpacity);
            mFrame_03_V = CreateGM_3D_Volume_8Edeges(pArray[2], fT_Y, fT_Y, fH_Z - 2 * fT_Y, cFrameColor, fFrameOpacity);
            mFrame_04_V = CreateGM_3D_Volume_8Edeges(pArray[3], fT_Y, fT_Y, fH_Z - 2 * fT_Y, cFrameColor, fFrameOpacity);
            mFrame_05_V = CreateGM_3D_Volume_8Edeges(pArray[4], fT_Y, fT_Y, fH_Z - 2 * fT_Y, cFrameColor, fFrameOpacity);

            mGlassTable_01 = CreateGM_3D_Volume_8Edeges(pArray[5], fGlassThickness, 0.5f * (fL_X - 3 * fT_Y), fH_Z - 2 * fT_Y, cGlassColor, fGlassOpacity);
            mGlassTable_02 = CreateGM_3D_Volume_8Edeges(pArray[6], fGlassThickness, 0.5f * (fL_X - 3 * fT_Y), fH_Z - 2 * fT_Y, cGlassColor, fGlassOpacity);
        }


        gr.Children.Add(mFrame_01_HB);
        gr.Children.Add(mFrame_02_HU);
        gr.Children.Add(mFrame_03_V);
        if (iSegmentNum == 2)
        {
          gr.Children.Add(mFrame_04_V);
        }

        gr.Children.Add(mFrame_05_V);

        gr.Children.Add(mGlassTable_01);

        if (iSegmentNum == 2)
        {
            gr.Children.Add(mGlassTable_02);
        }
        return gr;
    }
  }

  public class SphereMeshGenerator
  {
      private int _slices = 32;
      private int _stacks = 16;
      private Point3D _center = new Point3D();
      private double _radius = 0.5f;

      public int Slices
      {
          get { return _slices; }
          set { _slices = value; }
      }

      public int Stacks
      {
          get { return _stacks; }
          set { _stacks = value; }
      }

      public Point3D Center
      {
          get { return _center; }
          set { _center = value; }
      }

      public double Radius
      {
          get { return _radius; }
          set { _radius = value; }
      }

      public MeshGeometry3D Geometry
      {
          get
          {
              return CalculateMesh();
          }
      }

      public SphereMeshGenerator(Point3D c)
      {
          Center = c; // Set Center
      }

      private MeshGeometry3D CalculateMesh()
      {
          MeshGeometry3D mesh = new MeshGeometry3D();

          for (int stack = 0; stack <= Stacks; stack++)
          {
              double phi = Math.PI / 2 - stack * Math.PI / Stacks; // kut koji zamisljeni pravac povucen iz sredista koordinatnog sustava zatvara sa XZ ravninom. 
              double y = _radius * Math.Sin(phi); // Odredi poziciju Y koordinate. 
              double scale = -_radius * Math.Cos(phi);

              for (int slice = 0; slice <= Slices; slice++)
              {
                  double theta = slice * 2 * Math.PI / Slices; // Kada gledamo 2D koordinatni sustav osi X i Z... ovo je kut koji zatvara zamisljeni pravac povucen iz sredista koordinatnog sustava sa Z osi ( Z = Y ). 
                  double x = scale * Math.Sin(theta); // Odredi poziciju X koordinate. Uoči da je scale = -_radius * Math.Cos(phi)
                  double z = scale * Math.Cos(theta); // Odredi poziciju Z koordinate. Uoči da je scale = -_radius * Math.Cos(phi)

                  Vector3D normal = new Vector3D(x, y, z); // Normala je vektor koji je okomit na površinu. U ovom slučaju normala je vektor okomit na trokut plohu trokuta. 
                  mesh.Normals.Add(normal);
                  mesh.Positions.Add(normal + Center);     // Positions dobiva vrhove trokuta. 
                  mesh.TextureCoordinates.Add(new Point((double)slice / Slices, (double)stack / Stacks));
                  // TextureCoordinates kaže gdje će se neka točka iz 2D-a preslikati u 3D svijet. 
              }
          }

          for (int stack = 0; stack <= Stacks; stack++)
          {
              int top = (stack + 0) * (Slices + 1);
              int bot = (stack + 1) * (Slices + 1);

              for (int slice = 0; slice < Slices; slice++)
              {
                  if (stack != 0)
                  {
                      mesh.TriangleIndices.Add(top + slice);
                      mesh.TriangleIndices.Add(bot + slice);
                      mesh.TriangleIndices.Add(top + slice + 1);
                  }

                  if (stack != Stacks - 1)
                  {
                      mesh.TriangleIndices.Add(top + slice + 1);
                      mesh.TriangleIndices.Add(bot + slice);
                      mesh.TriangleIndices.Add(bot + slice + 1);
                  }
              }
          }

          return mesh;
      }
  }

}
