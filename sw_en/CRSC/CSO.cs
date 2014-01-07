using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MATH;

namespace CRSC
{
    // THIN-WALLED OPENED CROSS-SECTION PROPERTIES CALCULATION
    // Opened cross section characteristic calculation (closed cell are not allowed)

    public class CSO
    {

        #region variables

        // VARIABLES
        // EN 1999-1-1:2007 Annex J - J.4, page 183

        List<double> y_suradnice;
        List<double> z_suradnice;
        List<double> t_hodnoty;
        double _A;                   // Cross-section area (J.6)
        double dA;                   // Area off segment (J.5)
        double _d_A_vy;              // Cross-section shear area
        double _d_A_vz;              // Cross-section shear area
        double _Sy0;                 // Static modulus to primary axis yO and zO  (J.7) and (J.9)
        double _Sz0;                 // Static modulus to primary axis zO
        double _Sy;                  // Static modulus to centre of gravity
        double _Sz;                  // Static modulus to centre of gravity
        double d_z_gc;               // Gentre of gravity coordinate // (J.7)
        double d_y_gc;               // (J.7)
        double _Iy0;                 // Moment of inertia (Second moment of area) y0-y0 and z0-z0 // (J.8)
        double _Iy;                  // Moment of inertia (Second moment of area) y-y and z-z // (J.8)
        double _Iz0;                 // (J.10)
        double _Iz;                  // (J.10)
        double _Iyz0;                // Deviacni moment k puvodnim osam y a z // (J.11)
        double _Iyz;                 // Deviacni moment k osam y a z prochazejicim tezistem // (J.11)
        double alfa;                 // Rotation of main axis / Natoceni hlavnich os  // (J.12)
        double _Iepsilon;            // Moment of inertia (Second moment of area) to main axis - greek letters XI and ETA  // (J.13)
        double _Imikro;              // (J.14)

        double[] omega0i;            // Vysecove souradnice (J.15)
        double[] omega;
        double _Iomega;              // Stredni vysecova souradnice  // (J.16)

        double _omega_mean           // Stredni vysecova souradnice  // (J.16)
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
             , d_z_ci;               // Partial coordinates of centre of cross-section segments

        double _Sw;                  // Sectorial product of area  Staticky vysecovy moment // missing formula
        double _Wy_el;               // Elastic cross-section modulus y-y and z-z
        double _Wz_el;               // Elastic cross-section modulus y-y and z-z
        double _Wy_pl;               // Plastic cross-section modulus y-y and z-z
        double _Wz_pl;    
        double[] d_omega_s;          // Vysecove souradnice ktere jsou vztazeny ke stredu smyku (J.23) // (J.23)
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

        public double Wy_el
        {
            get { return _Wy_el; }
            set { _Wy_el = value; }
        }

        public double Wz_el
        {
            get { return _Wz_el; }
            set { _Wz_el = value; }
        }

        // end of cross-section variables definition
        #endregion

        
        //KONSTRUCTOR

        public CSO () {}
        public CSO(List<double> y_suradnice,List<double> z_suradnice,List<double> t_hodnoty) 
        {
            int count = y_suradnice.Count;

            this.y_suradnice = y_suradnice;
            this.z_suradnice = z_suradnice;
            this.t_hodnoty = t_hodnoty;
            
            A = this.A_method(count);
            this.A_vy_method(count);
            this.A_vz_method(count);
            this.Sy0_Sz0_method(count);
            this.Iy0_Iz0_method(count);
            this.omega0i = new double[count];
            this.omega = new double[count];
            this.d_omega_s = new double[count];
            this.J_12_13_14_method();
            this.J_15_method(count);
            this.J_16_method(count);
            this.J_17_18_19_method(count);
            this.J_20_21_method();
            this.J_22_method(count);
            this.J_23_method(count);
            this.J_24_25_26_method();
            this.J_27_J_28_method(count);

        }

        //method for calculations...
        public void calcutale(List<double> y_suradnice, List<double> z_suradnice, List<double> t_hodnoty) 
        {
            int count = y_suradnice.Count;

            this.y_suradnice = y_suradnice;
            this.z_suradnice = z_suradnice;
            this.t_hodnoty = t_hodnoty;

            A = this.A_method(count);
            this.Sy0_Sz0_method(count);
            this.Iy0_Iz0_method(count);
            this.omega0i = new double[count];
            this.omega = new double[count];
            this.d_omega_s = new double[count];
            this.J_12_13_14_method();
            this.J_15_method(count);
            this.J_16_method(count);
            this.J_17_18_19_method(count);
            this.J_20_21_method();
            this.J_22_method(count);
            this.J_23_method(count);
            this.J_24_25_26_method();
            this.J_27_J_28_method(count);
        }
        //(J.5) method
        private double dAi_method(int i)
        {
            double dAi = t_hodnoty[i] * Math.Sqrt(Math.Pow(y_suradnice[i] - y_suradnice[i - 1], 2)
                            + Math.Pow(z_suradnice[i] - z_suradnice[i - 1], 2));
            return dAi;
        }
        //(J.6) method
        private double A_method(int count) 
        {
            double sum = 0;
            for (int i = 1; i < count; i++) 
            {
                sum += dAi_method(i);
            }
            return sum;
        }
        // Shear Area Y
        //(sum of all parts paralel to y-Axis and) 
        private double A_vy_method(int count)
        {
            double sum = 0;
            for (int i = 1; i < count; i++)
            {
                sum += dAi_method(i);
            }
            double d_A_vy = sum / 2;
            return d_A_vy;
        }
        // Shear Area Z
        //(sum of all parts paralel to z-Axis and)
        private double A_vz_method(int count)
        {
            double sum = 0;
            for (int i = 1; i < count; i++)
            {
                sum += dAi_method(i);
            }
            double d_A_vz = sum / 2;
            return d_A_vz;
        }
        //(J.7) and (J.9) method
        private void Sy0_Sz0_method(int count) 
        {
            this._Sz0 = 0;
            this._Sy0 = 0;
            double dAi = 0;
            for (int i = 1; i < count; i++)
            {
                dAi = dAi_method(i) / 2;
                _Sy0 += (z_suradnice[i] + z_suradnice[i - 1]) * dAi;
                _Sz0 += (y_suradnice[i] + y_suradnice[i - 1]) * dAi;
            }

            this.d_z_gc = _Sy0 / A;
            this.d_y_gc = _Sz0 / A;
            
        }
        //(J.8) and (J.10) , (J.11) method
        private void Iy0_Iz0_method(int count) 
        {
            this._Iy0 = 0;
            this._Iz0 = 0;
            this._Iyz0 = 0;
            double dAi = 0;
            for (int i = 1; i < count; i++)
            {
                dAi = dAi_method(i);
                _Iy0 += (Math.Pow(z_suradnice[i],2) + Math.Pow(z_suradnice[i - 1],2) + z_suradnice[i]*z_suradnice[i-1])
                        * (dAi/3);
                _Iz0 += (Math.Pow(y_suradnice[i], 2) + Math.Pow(y_suradnice[i - 1], 2) + y_suradnice[i] * y_suradnice[i - 1])
                        * (dAi/3);
                _Iyz0 += (2 * y_suradnice[i - 1] * z_suradnice[i - 1] + 2 * y_suradnice[i] * z_suradnice[i]
                        + y_suradnice[i - 1] * z_suradnice[i] + y_suradnice[i] * z_suradnice[i - 1])*dAi/6;
            }
            
            this._Iy = _Iy0 + A * Math.Pow(d_z_gc, 2);
            this._Iz = _Iz0 + A * Math.Pow(d_y_gc, 2);
            this._Iyz = _Iyz0 - (_Sy0 * _Sz0 / A);
            
        }
        //J.12,J.13,J.14 method
        private void J_12_13_14_method()
        {
            if ((_Iz - _Iy) != 0)
                alfa = Math.Atan(2 * _Iyz / (_Iz - _Iy)) / 2;
            else alfa = 0;
            double temp = Math.Sqrt(Math.Pow(_Iz - _Iy, 2) + 4 * Math.Pow(_Iyz, 2));
            _Iepsilon = 1 / 2 * (_Iy + _Iz + temp);
            _Imikro = 1 / 2 * (_Iy + _Iz - temp);
        }
        //J.15 method
        private void J_15_method(int count) 
        {
            omega0i[0] = 0;
            omega[0] = 0;
            for (int i = 1; i < count; i++) 
            {
                omega0i[i] = y_suradnice[i - 1] * z_suradnice[i] - y_suradnice[i] * z_suradnice[i - 1];
                omega[i] = omega[i - 1] * omega0i[i]; 
            }
        }

        //J.16 method
        private void J_16_method(int count) 
        {
            _Iomega = 0;
            for (int i = 1; i < count; i++)
            {
                _Iomega += (omega[i - 1] + omega[i]) * dAi_method(i) / 2;
            }
            _omega_mean = _Iomega / A;
        }

        //J.17,J18,J19 method 
        private void J_17_18_19_method(int count) 
        {
            _Iy_omega0 = 0;
            _Iz_omega0 = 0;
            _Iomega_omega0 = 0;

            for (int i = 1; i < count; i++)
            {
                double dAi = dAi_method(i);
                _Iy_omega0 += (2 * y_suradnice[i - 1] * omega[i - 1] + 2 * y_suradnice[i] * omega[i] + y_suradnice[i - 1] * omega[i] +
                                y_suradnice[i] * omega[i - 1]) * dAi / 6;
                _Iz_omega0 += (2 * omega[i - 1] * z_suradnice[i - 1] + 2 * omega[i] * z_suradnice[i] * omega[i - 1] * z_suradnice[i] +
                                omega[i] * z_suradnice[i - 1]) * dAi / 6;
                _Iomega_omega0 += (Math.Pow(omega[i], 2) + Math.Pow(omega[i - 1], 2) + omega[i] * omega[i - 1]) * dAi / 3;
            }
            _Iy_omega = _Iy_omega0 - (_Sz0 * _Iomega / _A);
            _Iz_omega = _Iz_omega0 - (_Sy0 * _Iomega / _A);
            _Iomega_omega = _Iomega_omega0 - (Math.Pow(_Iomega, 2) / _A);
        }

        //J.20 and J.21 method
        private void J_20_21_method() 
        {
            try
            {
                double temp = _Iy * _Iz - Math.Pow(_Iyz, 2);
                d_y_sc = (_Iz_omega * _Iz - _Iy_omega * _Iyz)/temp;
                d_z_sc = (-_Iy_omega * _Iy - _Iz_omega * _Iyz) / temp;
                d_I_w = _Iomega_omega + d_z_sc * _Iy_omega - d_y_sc * _Iz_omega;
            }
            catch (DivideByZeroException) { MessageBox.Show("ERROR. Divide by zero, J.20 method."); }
        }
        //J.22 method
        private void J_22_method(int count)
        {
            d_I_t = 0;
            for (int i = 1; i < count; i++)
            {
                d_I_t += dAi_method(i) * Math.Pow(t_hodnoty[i], 2) / 3;
            }

            if (MathF.Min(t_hodnoty) != 0) // Existuje minimum rozne od nuly
                d_W_t = d_I_t / MathF.Min(t_hodnoty); // Pre nenulovu hrubku
            else if (MathF.Max(t_hodnoty) != 0) // Existuje maximum rozne od nuly
            {
                double min_more_than_zero;
                min_more_than_zero = t_hodnoty[0]; // Set first item

                foreach (int num in t_hodnoty)
                    if (num != 0 && num < min_more_than_zero) min_more_than_zero = num; // Set non zero minimum
            }
            else
              MessageBox.Show("ERROR. Minimalny prvok v t_hodnoty je nula!!!!.");

        }
        //J.23 method   ????? nerozumiem vzorcu...je potrebne upresnit
        private void J_23_method(int count)
        {
            d_omega_s[0] = 0;
            for (int i = 1; i < count; i++)
            {
                d_omega_s[i] = omega[i] - _omega_mean + d_z_sc * (y_suradnice[i] - d_y_gc) - d_y_sc * (z_suradnice[i] - d_z_gc);
            }
        }
        //J.24,J.25,J.26 method
        private void J_24_25_26_method() 
        {
            omega_max = MathF.Max(d_omega_s);
            d_W_w = d_I_w / omega_max;
            d_y_s = d_y_sc - d_z_gc;
            d_z_s = d_z_sc - d_z_gc;
            _Ip = _Iy + _Iz + A * (Math.Pow(d_y_s, 2) + Math.Pow(d_z_s, 2));

        }
        //J.29 method
        private void J_29_method(int num) 
        {
            d_y_ci = (y_suradnice[num] + y_suradnice[num - 1]) / 2 - d_y_gc;
            d_z_ci = (z_suradnice[num] + z_suradnice[num - 1]) / 2 - d_z_gc;
        }

        //J.27,J.28 method
        //This method uses J.29 method to count actual d_y_ci and d_z_ci numbers
        private void J_27_J_28_method(int count) 
        {
            double zj_temp=0, yj_temp=0,dAi;
            for (int i = 1; i < count; i++) 
            {
                this.J_29_method(i);
                dAi = this.dAi_method(i);
                zj_temp += (Math.Pow(d_z_ci, 3) + d_z_ci * (Math.Pow(z_suradnice[i] - z_suradnice[i - 1], 2) / 4 + Math.Pow(d_y_ci, 2) +
                        Math.Pow(y_suradnice[i] - y_suradnice[i - 1], 2) / 12) + d_y_ci * (y_suradnice[i] - y_suradnice[i - 1]) *
                        (z_suradnice[i] - z_suradnice[i - 1]) / 6) * dAi;
                yj_temp += (Math.Pow(d_y_ci, 3) + d_y_ci * (Math.Pow(y_suradnice[i] - y_suradnice[i - 1], 2) / 4 + Math.Pow(d_z_ci, 2) +
                        Math.Pow(z_suradnice[i] - z_suradnice[i - 1], 2) / 12) + d_z_ci * (z_suradnice[i] - z_suradnice[i - 1]) *
                        (y_suradnice[i] - y_suradnice[i - 1]) / 6) * dAi;
            }
            d_z_j = d_z_s - (0.5 / _Iy) * zj_temp;
            d_y_j = d_y_s - (0.5 / _Iz) * yj_temp;
        }



}
}
