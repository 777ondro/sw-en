using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MATH;

namespace CENEX
{
    public class CSO
    {
        #region variables
        List<int> y_suradnice;
        List<int> z_suradnice;
        List<int> t_hodnoty;
        double _A;
        double _d_A_vy;
        double _d_A_vz;
        double _Sy0;
        double _Sz0;
        double d_z_gc;
        double d_y_gc;
        double _Iy0;
        double _Iy;
        double _Iz0;
        double _Iz;
        double _Iyz0;
        double _Iyz;
        double alfa;
        double _Iepsilon;
        double _Imikro;
       
        double[] omega0i;
        double[] omega;
        double _Iomega;
        
        double _omega_mean
             , _Iy_omega0
             , _Iz_omega0
             , _Iomega_omega0
             , _Iy_omega
             , _Iz_omega
             , _Iomega_omega
             , _Ip
             , d_y_sc
             , d_z_sc
             , d_y_s
             , d_z_s
             , d_I_w
             , d_I_t
             , d_W_t
             , omega_max
             , d_W_w
             , d_z_j
             , d_y_j
             , d_y_ci
             , d_z_ci;

        

        double[] d_omega_s;
        #endregion

        #region Properties
        public double A
        {
            get { return _A; }
            set { _A = value; }
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
        #endregion

        
        //KONSTRUCTOR

        public CSO(List<int> y_suradnice,List<int>z_suradnice,List<int> t_hodnoty) 
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
        public void calcutale(List<int> y_suradnice, List<int> z_suradnice, List<int> t_hodnoty) 
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
            double dAi = t_hodnoty[i] * Math.Sqrt(Math.Pow(Math.Abs(y_suradnice[i] - y_suradnice[i - 1]), 2)
                            + Math.Pow(Math.Abs(z_suradnice[i] - z_suradnice[i - 1]), 2));
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
                _Sy0 += Math.Abs(z_suradnice[i] + z_suradnice[i - 1]) * (dAi);
                _Sz0 += Math.Abs(y_suradnice[i] + y_suradnice[i - 1]) * (dAi);
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

            if (MathF.Min(t_hodnoty) != 0)
                d_W_t = d_I_t / MathF.Min(t_hodnoty);
            else MessageBox.Show("ERROR. Minimalny prvok v t_hodnoty je nula!!!!.");

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
