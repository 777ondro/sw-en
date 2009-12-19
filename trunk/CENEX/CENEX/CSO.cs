using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        double _Zgc;
        double _Ygc;
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
        double _Iomega_mean;



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

            this._Zgc = _Sy0 / A;
            this._Ygc = _Sz0 / A;
            
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
            
            this._Iy = _Iy0 + A * Math.Pow(_Zgc, 2);
            this._Iz = _Iz0 + A * Math.Pow(_Ygc, 2);
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


       


    }
}
