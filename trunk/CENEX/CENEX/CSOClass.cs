using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CENEX
{
    
    public class CSOClass
    {
// THIN-WALLED OPENED CROSS-SECTION PROPERTIES CALCULATION
// Opened cross section characteristic calculation (closed cell are not allowed)
// Coordinates of KEYPOINTS

#region VARIABLES
        // VARIABLES
int k0_num = 0;

public int K0_num
{
  get { return k0_num; }
  set { k0_num = value; }
}
double k0_y = 0;
public double K0_y
{
  get { return k0_y; }
  set { k0_y = value; }
}
double k0_z = 0;

public double K0_z
{
  get { return k0_z; }
  set { k0_z = value; }
}
//User points

int k1_num = 1;

public int K1_num
{
  get { return k1_num; }
  set { k1_num = value; }
}
double k1_y = 100; // LCS y coordinate

public double K1_y
{
  get { return k1_y; }
  set { k1_y = value; }
}
double k1_z = 0; // LCS z coordinate

public double K1_z
{
  get { return k1_z; }
  set { k1_z = value; }
}
double k1_t = 10; // segment thickness between k0 and k1

public double K1_t
{
  get { return k1_t; }
  set { k1_t = value; }
}

int k2_num = 2;

public int K2_num
{
  get { return k2_num; }
  set { k2_num = value; }
}
        double k2_y = 50;

public double K2_y
{
  get { return k2_y; }
  set { k2_y = value; }
}
        double k2_z = 0;

public double K2_z
{
  get { return k2_z; }
  set { k2_z = value; }
}
        double k2_t = 0;

public double K2_t
{
  get { return k2_t; }
  set { k2_t = value; }
}

        int k3_num = 3;
        double k3_y = 50;
        double k3_z = 300;
        double k3_t = 6;

        int k4_num = 4;
        double k4_y = 100;
        double k4_z = 300;
        double k4_t = 0;

        int k5_num = 5;
        double k5_y = 0;
        double k5_z = 300;
        double k5_t = 10;


        double n_segments = 4; // number of segments

        public double N_segments
        {
            get { return n_segments; }
            set { n_segments = value; }
        }
        double n_points = 5; // number of keypoints

        public double N_points
        {
            get { return n_points; }
            set { n_points = value; }
        }
        #endregion      
#region VARIABLES - CALCULATION
        // VARIABLES
        // EN 1999-1-1:2007 Annex J - J.4, page 183

        // Area off segment (J.5)

        double dA;

        public double DA
        {
            get { return dA; }
            set { dA = value; }
        }



        //Area (J.6)

        double _A;

        public double A
        {
            get { return _A; }
            set { _A = value; }
        }
        
        // Cross-section shear areas
        double _Ay_v;

        public double Ay_v
        {
            get { return _Ay_v; }
            set { _Ay_v = value; }
        }
        double _Az_v;

        public double Az_v
        {
            get { return _Az_v; }
            set { _Az_v = value; }
        }

        // Static modulus

        // to primary axis yO and zO  (J.7) and (J.9)

        double _Sy0; // (J.7)

        public double Sy0
        {
            get { return _Sy0; }
            set { _Sy0 = value; }
        }
        double _Sz0;// (J.9)

        public double Sz0
        {
            get { return _Sz0; }
            set { _Sz0 = value; }
        }

        // to centre of gravity

        double _Sy;

        public double Sy
        {
            get { return _Sy; }
            set { _Sy = value; }
        }
        double _Sz;

        public double Sz
        {
            get { return _Sz; }
            set { _Sz = value; }
        }
        
        // Gentre of gravity coordinate

        double ygc; // (J.9)

        public double Ygc
        {
            get { return ygc; }
            set { ygc = value; }
        }
        double zgc; // (J.7)

        public double Zgc
        {
            get { return zgc; }
            set { zgc = value; }
        }


        // Moment of inertia (Second moment of area) y0-y0 and z0-z0

        double _Iy0; // (J.8)

        public double Iy0
        {
            get { return _Iy0; }
            set { _Iy0 = value; }
        }

        double _Iz0; // (J.10)

        public double Iz0
        {
            get { return _Iz0; }
            set { _Iz0 = value; }
        }
        
        // Moment of inertia (Second moment of area) y-y and z-z
        
        double _Iy; // (J.8)

        public double Iy
        {
            get { return _Iy; }
            set { _Iy = value; }
        }
        double _Iz; // (J.10)

        public double Iz
        {
            get { return _Iz; }
            set { _Iz = value; }
        }

        // Deviacni moment k puvodnim osam y a z

        double _Iyz0; // (J.11)

        public double Iyz0
        {
            get { return _Iyz0; }
            set { _Iyz0 = value; }
        }
        
        // Deviacni moment k osam y a z prochazejicim tezistem

        double _Iyz; // (J.11)

        public double Iyz
        {
            get { return _Iyz; }
            set { _Iyz = value; }
        }

        // Rotation of main axis / Natoceni hlavnich os

        double alpha; // (J.12)

        // Moment of inertia (Second moment of area) to main axis - greek letters XI and ETA

        double _Ixi; // (J.13)

        public double Ixi
        {
            get { return _Ixi; }
            set { _Ixi = value; }
        }

        double _Ieta; // (J.14)

        public double Ieta
        {
            get { return _Ieta; }
            set { _Ieta = value; }
        }

        // Vysecove souradnice (J.15)

        double omega_0;

        public double Omega_0
        {
            get { return omega_0; }
            set { omega_0 = value; }
        }
        double omega_0i;

        public double Omega_0i
        {
            get { return omega_0i; }
            set { omega_0i = value; }
        }
        double omega_i;

        public double Omega_i
        {
            get { return omega_i; }
            set { omega_i = value; }
        }
            
        // Stredni vysecova souradnice

        double _I_omega; // (J.16)

        public double I_omega
        {
            get { return _I_omega; }
            set { _I_omega = value; }
        }

        double omega_mean; // (J.16)

        public double Omega_mean
        {
            get { return omega_mean; }
            set { omega_mean = value; }
        }

        // Staticky vysecový moment

        double _Iy_omega_0; // (J.17)

        public double Iy_omega_0
        {
            get { return _Iy_omega_0; }
            set { _Iy_omega_0 = value; }
        }
        double _Iy_omega; // (J.17)

        public double Iy_omega
        {
            get { return _Iy_omega; }
            set { _Iy_omega = value; }
        }
        double _Iz_omega_0; // (J.18)

        public double Iz_omega_0
        {
            get { return _Iz_omega_0; }
            set { _Iz_omega_0 = value; }
        }
        double _Iz_omega; // (J.18)

        public double Iz_omega
        {
            get { return _Iz_omega; }
            set { _Iz_omega = value; }
        }
        double _Iomega_omega_0; // (J.19)

        public double Iomega_omega_0
        {
            get { return _Iomega_omega_0; }
            set { _Iomega_omega_0 = value; }
        }
        double _Iomega_omega; // (J.19)

        public double Iomega_omega
        {
            get { return _Iomega_omega; }
            set { _Iomega_omega = value; }
        }

                // Souradnice stredu smyku (J.20)

        double y_sc; // (J.20)

        public double Y_sc
        {
            get { return y_sc; }
            set { y_sc = value; }
        }
        double z_sc; // (J.20)

        public double Z_sc
        {
            get { return z_sc; }
            set { z_sc = value; }
        }
        

        // Vysecovy moment setrvacnosti (J.21)
        // Warping constant, Iw - Vysecovy moment setrvacnosti
        double _Iw; // (J.21)

        public double Iw
        {
            get { return _Iw; }
            set { _Iw = value; }
        }

        // St. Venant torsional constant / Moment tuhosti v prostem krouceni
        double _IT; // (J.22)

        public double IT
        {
            get { return _IT; }
            set { _IT = value; }
        }

        // Modul odporu prierezu v kruteni / Modul tuhosti v prostem krouceni
        double _Wt; // (J.22)

        public double Wt
        {
            get { return _Wt; }
            set { _Wt = value; }
        }

        // Vysecove souradnice ktere jsou vztazeny ke stredu smyku (J.23)

        double omega_sj; // (J.23)

        public double Omega_sj
        {
            get { return omega_sj; }
            set { omega_sj = value; }
        }

        // Nejvetsi vysecove poradnice a vysecovy modul

        double omega_max; // (J.24)

        public double Omega_max
        {
            get { return omega_max; }
            set { omega_max = value; }
        }

        // Vysecova suradnica
        double omega_w; // missing formula

        public double Omega_w
        {
            get { return omega_w; }
            set { omega_w = value; }
        }

        // Vysecovy modul (J.24)
        double _Ww;

        public double Ww
        {
            get { return _Ww; }
            set { _Ww = value; }
        }
        // Sectorial product of area  Staticky vysecovy moment
        double _Sw; // missing formula

        public double Sw
        {
            get { return _Sw; }
            set { _Sw = value; }
        }

        // Vzdalenost stredu smyku a teziste

        double ys; // (J.25)

        public double Ys
        {
            get { return ys; }
            set { ys = value; }
        }
        double zs; // (J.25)

        public double Zs
        {
            get { return zs; }
            set { zs = value; }
        }

        // Polarni moment setrvacnosti 

        double _Ip; // (J.26)

        public double Ip
        {
            get { return _Ip; }
            set { _Ip = value; }
        }


        // Factors of asymetry (J.27) and (J.28)

        // according Annex I


        double zj;// (J.27)

        public double Zj
        {
            get { return zj; }
            set { zj = value; }
        }
        double yj;// (J.28)

        public double Yj
        {
            get { return yj; }
            set { yj = value; }
        }

        //partial coordinates of centre of cross-section segments

        double zci;// (J.29)

        public double Zci
        {
            get { return zci; }
            set { zci = value; }
        }
        double yci;// (J.29)

        public double Yci
        {
            get { return yci; }
            set { yci = value; }
        }

        // Elastic cross-section modulus y-y and z-z
        double _Wy_el;

        public double Wy_el
        {
            get { return _Wy_el; }
            set { _Wy_el = value; }
        }
        double _Wz_el;

        public double Wz_el
        {
            get { return _Wz_el; }
            set { _Wz_el = value; }
        }
        // Plastic cross-section modulus y-y and z-z
        double _Wy_pl;

        public double Wy_pl
        {
            get { return _Wy_pl; }
            set { _Wy_pl = value; }
        }
        double _Wz_pl;

        public double Wz_pl
        {
            get { return _Wz_pl; }
            set { _Wz_pl = value; }
        }



        // end of cross-section variables definition

        #endregion
#region CONSTRUCTOR
        public CSOClass () 
        {
        










       }      
#endregion
# region COMMENTS
// x - real variable 
// boundary - interval of real line
// a = i = 1 - first segment
// b = n - number of cs segments
// Function y = Function dA
# endregion



static double Area (double _A)
        {
            // Area
            int n_segments = 1;///
            int ii = n_segments;
            for (ii = 0; ii != n_segments; ii++)
            {
                double y1 = 10;///////
                double z1 = 20;
                double y2 = 10;
                double z2 = 20;
                double t = 3;


                double l = Math.Sqrt(Math.Pow(Math.Abs(y2 - y1), 2) + Math.Pow(Math.Abs(z2 - z1), 2)); // length of segment
                double _dA = l * t;
                _A = 0;
                _A = _A + _dA;

                // Area 2
                //double a = 1;
                //double b = n;

                //return ((l * t));





            }
                return (_A);

               

             
            
            
           




        }
public delegate double Function (double x);
public Function y = new Function (Area);     
public void CSOMessage ()
{
    // Coordinates and thickness table - input check
    MessageBox.Show("Coordinates and thickness table:"
                    + "\n"
                 + k0_num.ToString() + "  " + k0_y.ToString() + "  " + k0_z.ToString() + "\n"
                 + k1_num.ToString() + "  " + k1_y.ToString() + "  " + k1_z.ToString() + "  " + k1_t.ToString() + "\n"
                 + k2_num.ToString() + "  " + k2_y.ToString() + "  " + k2_z.ToString() + "  " + k2_t.ToString() + "\n"
                 + k3_num.ToString() + "  " + k3_y.ToString() + "  " + k3_z.ToString() + "  " + k3_t.ToString() + "\n"
                 + k4_num.ToString() + "  " + k4_y.ToString() + "  " + k4_z.ToString() + "  " + k4_t.ToString() + "\n"
                 + k5_num.ToString() + "  " + k5_y.ToString() + "  " + k5_z.ToString() + "  " + k5_t.ToString() + "\n");

}

    /*
public double RombergIntegration(Function y, double a, double b, int n) 
{ 
  int i, j, m = 0; 
  double h, trap; 
  double[,] I = new double[n, n]; 
  h = b - a; 
  I[0, 0] = (y(a) + y(b)) * (h / 2); 
  if (n > 15) n = 15; 
  do 
  { 
  m++; 
  h = h / 2; 
  trap = (y(a) + y(b)) / 2; 
  for (i = 1; i < Math.Pow(2, m); i++) trap += y(a + h * i); 
  I[0, m] = trap * h; 
  for (i = 1; i < m + 1; i++) 
  { 
  j = m - i; 
  I[i, j] = (Math.Pow(4, i) * I[i - 1, j + 1] - I[i - 1, j]) / (Math.Pow(4, i) - 1); 
  } 
  } while (m < n); 
  return I[n, 0]; 
} 

*/















}
}
    

