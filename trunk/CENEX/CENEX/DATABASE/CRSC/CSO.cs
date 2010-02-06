using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX.GENERAL.MATH;
using System.Windows.Forms;

namespace CENEX
{
    public class CSO
    {
        List<int> y_suradnice;
        List<int> z_suradnice;
        List<int> t_hodnoty;
        double _A;
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
        double _Iomega_mean
             , _Iy_omega0
             , _Iz_omega0
             , _Iomega_omega0
             , _Iy_omega
             , _Iz_omega
             , _Iomega_omega
             , d_y_sc
             , d_z_sc
             , d_I_w
             , d_I_t
             , d_W_t
             , d_t_max;
        double[] d_omega_sj;
        double[] d_omega_j;
        double d_omega_mean;



        public double A
        {
            get { return _A; }
            set { _A = value; }
        }


        public CSO(List<int> y_suradnice,List<int>z_suradnice,List<int> t_hodnoty) 
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
            this.J_12_13_14_method();
            this.J_15_method(count);

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
            _Iomega_mean = _Iomega / A;
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
                double dAi = dAi_method(i);
                d_I_t += dAi * Math.Pow(t_hodnoty[i], 2) / 3;
                d_W_t = d_I_t / d_t_max;

            }

        }
        //J.23 method
        private void J_23_method(int count)
        {
            d_omega_sj[0] = 0;
            for (int i = 1; i < count; i++)
            {
                d_omega_sj[i] = d_omega_j[i] - d_omega_mean + d_z_sc * (y_suradnice[i] - d_y_gc) - d_y_sc * (z_suradnice[i] - d_z_gc);
            }
        }






}
}
