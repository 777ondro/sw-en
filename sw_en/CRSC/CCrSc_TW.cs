﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MATH;

namespace CRSC
{
    // THIN-WALLED CROSS-SECTION PROPERTIES CALCULATION

    public abstract class CCrSc_TW : CCrSc
    {
        #region variables

        // VARIABLES
        // EN 1999-1-1:2007 Annex J - J.4, page 183

        public float[,] arrPointCoord;
        public List<double> y_suradnice;
        public List<double> z_suradnice;
        public List<double> t_hodnoty;
        public double _h;
        public double _b;
        public double _A;                   // Cross-section area (J.6)
        public double dA;                   // Area off segment (J.5)
        public double _d_A_vy;              // Cross-section shear area
        public double _d_A_vz;              // Cross-section shear area
        public double _Sy0;                 // Static modulus to primary axis yO and zO  (J.7) and (J.9)
        public double _Sz0;                 // Static modulus to primary axis zO
        public double _Sy;                  // Static modulus to centre of gravity
        public double _Sz;                  // Static modulus to centre of gravity
        public double d_z_gc;               // Gentre of gravity coordinate // (J.7)
        public double d_y_gc;               // (J.7)
        public double _Iy0;                 // Moment of inertia (Second moment of area) y0-y0 and z0-z0 // (J.8)
        public double _Iy;                  // Moment of inertia (Second moment of area) y-y and z-z // (J.8)
        public double _Iz0;                 // (J.10)
        public double _Iz;                  // (J.10)
        public double _Iyz0;                // Deviacni moment k puvodnim osam y a z // (J.11)
        public double _Iyz;                 // Deviacni moment k osam y a z prochazejicim tezistem // (J.11)
        public double alfa;                 // Rotation of main axis / Natoceni hlavnich os  // (J.12)
        public double _Iepsilon;            // Moment of inertia (Second moment of area) to main axis - greek letters XI and ETA  // (J.13)
        public double _Imikro;              // (J.14)

        public double[] omega0i;            // Vysecove souradnice (J.15)
        public double[] omega;
        public double _Iomega;              // Stredni vysecova souradnice  // (J.16)

        public double _omega_mean           // Stredni vysecova souradnice  // (J.16)
             , _Iy_omega0            // Staticky vysecový moment // (J.17)
             , _Iz_omega0            // (J.18)
             , _Iomega_omega0        // (J.19)
             , _Iy_omega             // Staticky vysecový moment // (J.17)
             , _Iz_omega             // Staticky vysecový moment // (J.18) 
             , _Iomega_omega         // Staticky vysecový moment // (J.19)
             , _Ip                   // Polarni moment setrvacnosti // (J.26)
             , d_y_sc                // Souradnice stredu smyku (J.20) // (J.20)
             , d_z_sc                // (J.20)
             , d_y_s                 // Vzdalenost stredu smyku a teziste // (J.25)
             , d_z_s                 // (J.25)
             , d_I_w                 // Vysecovy moment setrvacnosti (J.21)  // Warping constant, Iw - Vysecovy moment setrvacnosti
             , d_I_t                 // St. Venant torsional constant / Moment tuhosti v prostem krouceni // (J.22)
             , d_W_t                 // Modul odporu prierezu v kruteni / Modul tuhosti v prostem krouceni // (J.22)
             , omega_max             // Nejvetsi vysecove poradnice a vysecovy modul // (J.24)
             , d_W_w                 // Vysecovy modul (J.24)
             , d_z_j                 // Factors of asymetry (J.27) and (J.28)  // according Annex I
             , d_y_j                 // (J.28)
             , d_y_ci                // Partial coordinates of centre of cross-section segments // (J.29)
             , d_z_ci                // Partial coordinates of centre of cross-section segments
             , _Sw                   // Sectorial product of area  Staticky vysecovy moment // missing formula
             , _Wy_el_1              // Elastic cross-section modulus y-y and z-z
             , _Wy_el_2              // Elastic cross-section modulus y-y and z-z
             , _Wz_el_1              // Elastic cross-section modulus y-y and z-z
             , _Wz_el_2              // Elastic cross-section modulus y-y and z-z
             , _Wy_pl                // Plastic cross-section modulus y-y and z-z
             , _Wz_pl;               // Plastic cross-section modulus y-y and z-z
        public double[] d_omega_s;          // Vysecove souradnice ktere jsou vztazeny ke stredu smyku (J.23) // (J.23)

        public double _Beta_y, _Beta_z;      // Monosymmetry constant AS / NZS standards

        public double y_min
               , y_max
               , z_min
               , z_max;

        #endregion

        #region Properties

        public double A
        {
            get { return _A; }
            set { _A = value; }
        }

        public double DA
        {
            get { return dA; }
            set { dA = value; }
        }

        public double d_A_vy
        {
            get { return _d_A_vy; }
            set { _d_A_vy = value; }
        }
        public double d_A_vz
        {
            get { return _d_A_vz; }
            set { _d_A_vz = value; }
        }
        public double Sy0
        {
            get { return _Sy0; }
            set { _Sy0 = value; }
        }
        public double Sz0
        {
            get { return _Sz0; }
            set { _Sz0 = value; }
        }
        public double D_z_gc
        {
            get { return d_z_gc; }
            set { d_z_gc = value; }
        }
        public double D_y_gc
        {
            get { return d_y_gc; }
            set { d_y_gc = value; }
        }
        public double Iy
        {
            get { return _Iy; }
            set { _Iy = value; }
        }
        public double Iz
        {
            get { return _Iz; }
            set { _Iz = value; }
        }
        public double Iyz
        {
            get { return _Iyz; }
            set { _Iyz = value; }
        }
        public double Alfa
        {
            get { return alfa; }
            set { alfa = value; }
        }
        public double Iepsilon
        {
            get { return _Iepsilon; }
            set { _Iepsilon = value; }
        }
        public double Imikro
        {
            get { return _Imikro; }
            set { _Imikro = value; }
        }
        public double Iomega
        {
            get { return _Iomega; }
            set { _Iomega = value; }
        }
        public double Omega_mean
        {
            get { return _omega_mean; }
            set { _omega_mean = value; }
        }

        public double D_y_j
        {
            get { return d_y_j; }
            set { d_y_j = value; }
        }

        public double D_z_j
        {
            get { return d_z_j; }
            set { d_z_j = value; }
        }

        public double D_W_w
        {
            get { return d_W_w; }
            set { d_W_w = value; }
        }

        public double Omega_max
        {
            get { return omega_max; }
            set { omega_max = value; }
        }

        public double D_W_t
        {
            get { return d_W_t; }
            set { d_W_t = value; }
        }

        public double D_I_t
        {
            get { return d_I_t; }
            set { d_I_t = value; }
        }

        public double D_I_w
        {
            get { return d_I_w; }
            set { d_I_w = value; }
        }

        public double D_z_s
        {
            get { return d_z_s; }
            set { d_z_s = value; }
        }

        public double D_y_s
        {
            get { return d_y_s; }
            set { d_y_s = value; }
        }

        public double Ip
        {
            get { return _Ip; }
            set { _Ip = value; }
        }

        public double Iomega_omega
        {
            get { return _Iomega_omega; }
            set { _Iomega_omega = value; }
        }

        public double Iz_omega
        {
            get { return _Iz_omega; }
            set { _Iz_omega = value; }
        }

        public double Iy_omega
        {
            get { return _Iy_omega; }
            set { _Iy_omega = value; }
        }

        public double Sw
        {
            get { return _Sw; }
            set { _Sw = value; }
        }

        public double Wy_pl
        {
            get { return _Wy_pl; }
            set { _Wy_pl = value; }
        }

        public double Wz_pl
        {
            get { return _Wz_pl; }
            set { _Wz_pl = value; }
        }

        public double Wy_el_1
        {
            get { return _Wy_el_1; }
            set { _Wy_el_1 = value; }
        }

        public double Wz_el_1
        {
            get { return _Wz_el_1; }
            set { _Wz_el_1 = value; }
        }

        public double Wy_el_2
        {
            get { return _Wy_el_2; }
            set { _Wy_el_2 = value; }
        }

        public double Wz_el_2
        {
            get { return _Wz_el_2; }
            set { _Wz_el_2 = value; }
        }

        public double Beta_y
        {
            get { return _Beta_y; }
            set { _Beta_y = value; }
        }

        public double Beta_z
        {
            get { return _Beta_z; }
            set { _Beta_z = value; }
        }

        // end of cross-section variables definition
        #endregion

        // Default example
        public CCrSc_TW()
        {
        }

        // Data in datagrid
        public CCrSc_TW(List<double> y_suradnice, List<double> z_suradnice, List<double> t_hodnoty)
        {
            int count = y_suradnice.Count;

            this.y_suradnice = y_suradnice;
            this.z_suradnice = z_suradnice;
            this.t_hodnoty = t_hodnoty;
        }

        protected override void loadCrScIndices()
        {
        }

        protected override void loadCrScIndicesFrontSide()
        {
        }

        protected override void loadCrScIndicesShell()
        {
        }

        protected override void loadCrScIndicesBackSide()
        {
        }
    }
}
