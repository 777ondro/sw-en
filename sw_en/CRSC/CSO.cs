using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATH;
using System.Windows.Media;
using System.Windows.Forms;

namespace CRSC
{
    // THIN-WALLED OPENED CROSS-SECTION PROPERTIES CALCULATION
    // Opened cross section characteristic calculation (closed cell are not allowed)

    public class CSO : CCrSc_TW
    {
        // CONSTRUCTOR

        public CSO() { }
        public CSO(List<double> y_suradnice, List<double> z_suradnice, List<double> t_hodnoty)
        {
            int count = y_suradnice.Count;

            this.y_suradnice = y_suradnice;
            this.z_suradnice = z_suradnice;
            this.t_hodnoty = t_hodnoty;

            this.J_Calc_Dimensions();

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
            this.J_W_el();
            this.Calc_Beta_y_method(count);
            this.Calc_Beta_z_method(count);
        }

        public void CrScDefPoints_EX_01()
        {
            // Fill example data
            IsShapeSolid = true;
            const int number_rows = 9;

            arrPointCoord = new float[number_rows, 3] {
            {-8.0f,  17.0f,  0.0f},
            {-6.0f,  20.0f,  1.0f},
            { 6.0f,  20.0f,  1.0f},
            { 8.0f,  17.0f,  1.0f},
            { 6.0f,  20.0f,  0.0f},
            { 0.0f,  20.0f,  0.0f},
            { 0.0f,   0.0f,  0.8f},
            { 6.0f,   0.0f,  0.0f},
            {-6.0f,   0.0f,  1.0f}
            };
        }

        public void CrScDefPoints_EX_02()
        {
            // Fill example data - Duragal C300x90x8.0

            const int number_rows = 6;

            float fh = 300f;
            float fb = 90f;
            float ft = 8f;
            float fr_1 = 8f;


            arrPointCoord = new float[number_rows, 3] {
            { fb - 0.5f * ft,  -0.5f * fh + 0.5f * ft,  0.0f}, //0
            { 0.5f * ft + fr_1,  -0.5f * fh + 0.5f * ft,  ft}, //1
            { 0, -0.5f * fh + 0.5f * ft  + fr_1,  ft}, //2
            { 0, 0,  ft}, //3
            { 0, 0,  ft}, //4
            { 0, 0,  ft}, //5
            };

            // Fill coordinates (symmetry about horizontal y-y axis)

            for (int i = number_rows / 2; i < number_rows; i++)
            {
                arrPointCoord[i, 0] = arrPointCoord[number_rows - i - 1, 0];
                arrPointCoord[i, 1] = -arrPointCoord[number_rows - i - 1, 1];
            }
        }

        public void CrScDefPoints_EX_2710095()
        {
            // Fill example data
            IsShapeSolid = true;
            const int number_rows = 28;

            float fh = 270f;
            float fb = 70f;
            float ft = 0.95f;
            float fr_1 = 4f;
            float fr_2 = 8f;
            float fr_3 = 5f;

            float fy_1 = 10f; // flange edge stiffener
            float fy_2 = 25f;
            //float fy_3 = 20f;
            //float fy_4 = 25f;

            float fy_5 = 10f; // web stiffener

            float fz_1 = 20f; // flange edge stiffener
            //float fz_2 = 50f;
            float fz_3 = 20f; // web stiffener
            float fz_4 = 15f; // web stiffener
            float fz_5 = 20f; // web stiffener
            float fz_6 = 60f;

            arrPointCoord = new float[number_rows, 3] {
            { fb - fy_1 - ft,  -0.5f * fh + fz_1 - 0.5f * ft,  0.0f}, //0
            { fb - ft - fr_1,  -0.5f * fh + fz_1 - 0.5f * ft,  ft}, //1
            { fb - ft, -0.5f * fh + fz_1 - 0.5f * ft - fr_3,  ft}, //2
            { fb - ft, -0.5f * fh + 0.5f * ft + fr_1,  ft}, //3
            { fb - ft - fr_1,  -0.5f * fh + 0.5f * ft,  ft}, //4
            { fb - 0.5f * ft - fy_2, -0.5f * fh + 0.5f * ft,  ft}, //5
            { 0.5f * (fb - ft), -0.5f * fh + 0.5f * ft + fr_1,  ft}, //6
            { fy_2 - 0.5f * ft, -0.5f * fh + 0.5f * ft,  ft},
            { fr_2 + 0.5f * ft, -0.5f * fh + 0.5f * ft,  ft},
            { 0f,  -0.5f * fh + 0.5f * ft + fr_2,  ft}, //9
            { 0f,  - 0.5f * fz_6 - fz_5 - fz_4 - fz_3,  ft}, //10
            { fy_5 - ft,  - 0.5f * fz_6 - fz_5 - fz_4,  ft},
            { fy_5 - ft,  - 0.5f * fz_6 - fz_5,  ft},
            { 0f,  - 0.5f * fz_6,  ft}, //13
            { 0f,  0f,  ft}, //14
            { 0f,  0f,  ft},
            { 0f,  0f,  ft},
            { 0f,  0f,  ft}, //17
            { 0f,  0f,  ft}, //18
            { 0f,  0f,  ft},
            { 0f,  0f,  ft},
            { 0f,  0f,  ft}, //21
            { 0f,  0f,  ft}, //22
            { 0f,  0f,  ft},
            { 0f,  0f,  ft},
            { 0f,  0f,  ft}, //25
            { 0f,  0f,  ft}, //26
            {-0f,  0f,  ft}  //27
            };

            // Fill coordinates (symmetry about horizontal y-y axis)

            for (int i = number_rows / 2; i < number_rows; i++)
            {
                arrPointCoord[i, 0] = arrPointCoord[number_rows - i - 1, 0];
                arrPointCoord[i, 1] = -arrPointCoord[number_rows - i - 1, 1];
            }
        }

        public void CrScDefPoints_EX_2710115()
        {
            // Fill example data
            IsShapeSolid = true;
            const int number_rows = 28;

            float fh = 270f;
            float fb = 70f;
            float ft = 1.15f;
            float fr_1 = 4f;
            float fr_2 = 8f;
            float fr_3 = 5f;

            float fy_1 = 10f; // flange edge stiffener
            float fy_2 = 25f;
            //float fy_3 = 20f;
            //float fy_4 = 25f;

            float fy_5 = 10f; // web stiffener

            float fz_1 = 20f; // flange edge stiffener
            //float fz_2 = 50f;
            float fz_3 = 20f; // web stiffener
            float fz_4 = 15f; // web stiffener
            float fz_5 = 20f; // web stiffener
            float fz_6 = 60f;

            arrPointCoord = new float[number_rows, 3] {
            { fb - fy_1 - ft,  -0.5f * fh + fz_1 - 0.5f * ft,  0.0f}, //0
            { fb - ft - fr_1,  -0.5f * fh + fz_1 - 0.5f * ft,  ft}, //1
            { fb - ft, -0.5f * fh + fz_1 - 0.5f * ft - fr_3,  ft}, //2
            { fb - ft, -0.5f * fh + 0.5f * ft + fr_1,  ft}, //3
            { fb - ft - fr_1,  -0.5f * fh + 0.5f * ft,  ft}, //4
            { fb - 0.5f * ft - fy_2, -0.5f * fh + 0.5f * ft,  ft}, //5
            { 0.5f * (fb - ft), -0.5f * fh + 0.5f * ft + fr_1,  ft}, //6
            { fy_2 - 0.5f * ft, -0.5f * fh + 0.5f * ft,  ft},
            { fr_2 + 0.5f * ft, -0.5f * fh + 0.5f * ft,  ft},
            { 0f,  -0.5f * fh + 0.5f * ft + fr_2,  ft}, //9
            { 0f,  - 0.5f * fz_6 - fz_5 - fz_4 - fz_3,  ft}, //10
            { fy_5 - ft,  - 0.5f * fz_6 - fz_5 - fz_4,  ft},
            { fy_5 - ft,  - 0.5f * fz_6 - fz_5,  ft},
            { 0f,  - 0.5f * fz_6,  ft}, //13
            { 0f,  0f,  ft}, //14
            { 0f,  0f,  ft},
            { 0f,  0f,  ft},
            { 0f,  0f,  ft}, //17
            { 0f,  0f,  ft}, //18
            { 0f,  0f,  ft},
            { 0f,  0f,  ft},
            { 0f,  0f,  ft}, //21
            { 0f,  0f,  ft}, //22
            { 0f,  0f,  ft},
            { 0f,  0f,  ft},
            { 0f,  0f,  ft}, //25
            { 0f,  0f,  ft}, //26
            {-0f,  0f,  ft}  //27
            };

            // Fill coordinates (symmetry about horizontal y-y axis)

            for (int i = number_rows / 2; i < number_rows; i++)
            {
                arrPointCoord[i, 0] = arrPointCoord[number_rows - i - 1, 0];
                arrPointCoord[i, 1] = -arrPointCoord[number_rows - i - 1, 1];
            }
        }


        // Methods for calculations...
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
            this.Calc_Beta_y_method(count);
            this.Calc_Beta_z_method(count);
        }
        //(J.5) method
        public double dAi_method(int i)
        {
            double dAi = t_hodnoty[i] * Math.Sqrt(Math.Pow(y_suradnice[i] - y_suradnice[i - 1], 2)
                            + Math.Pow(z_suradnice[i] - z_suradnice[i - 1], 2));
            return dAi;
        }
        //(J.6) method
        public double A_method(int count)
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
        public double A_vy_method(int count)
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
        public double A_vz_method(int count)
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
        public void Sy0_Sz0_method(int count)
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
        public void Iy0_Iz0_method(int count)
        {
            this._Iy0 = 0;
            this._Iz0 = 0;
            this._Iyz0 = 0;
            double dAi = 0;
            for (int i = 1; i < count; i++)
            {
                dAi = dAi_method(i);
                _Iy0 += (Math.Pow(z_suradnice[i], 2) + Math.Pow(z_suradnice[i - 1], 2) + z_suradnice[i] * z_suradnice[i - 1])
                        * (dAi / 3);
                _Iz0 += (Math.Pow(y_suradnice[i], 2) + Math.Pow(y_suradnice[i - 1], 2) + y_suradnice[i] * y_suradnice[i - 1])
                        * (dAi / 3);
                _Iyz0 += (2 * y_suradnice[i - 1] * z_suradnice[i - 1] + 2 * y_suradnice[i] * z_suradnice[i]
                        + y_suradnice[i - 1] * z_suradnice[i] + y_suradnice[i] * z_suradnice[i - 1]) * dAi / 6;
            }

            this._Iy = _Iy0 - A * Math.Pow(d_z_gc, 2);
            this._Iz = _Iz0 - A * Math.Pow(d_y_gc, 2);
            this._Iyz = _Iyz0 - (_Sy0 * _Sz0 / A);
        }
        //J.12,J.13,J.14 method
        public void J_12_13_14_method()
        {
            if ((_Iz - _Iy) != 0)
                alfa = Math.Atan(2 * _Iyz / (_Iz - _Iy)) / 2;
            else alfa = 0;
            double temp = Math.Sqrt(Math.Pow(_Iz - _Iy, 2) + 4 * Math.Pow(_Iyz, 2));
            this._Iepsilon = 0.5 * (_Iy + _Iz + temp);
            this._Imikro = 0.5 * (_Iy + _Iz - temp);
        }
        //J.15 method
        public void J_15_method(int count)
        {
            omega0i[0] = 0;
            omega[0] = 0;
            for (int i = 1; i < count; i++)
            {
                omega0i[i] = y_suradnice[i - 1] * z_suradnice[i] - y_suradnice[i] * z_suradnice[i - 1];
                omega[i] = omega[i - 1] + omega0i[i];
            }
        }
        //J.16 method
        public void J_16_method(int count)
        {
            _Iomega = 0;
            for (int i = 1; i < count; i++)
            {
                _Iomega += (omega[i - 1] + omega[i]) * dAi_method(i) / 2;
            }
            _omega_mean = _Iomega / A;
        }
        //J.17,J18,J19 method 
        public void J_17_18_19_method(int count)
        {
            _Iy_omega0 = 0;
            _Iz_omega0 = 0;
            _Iomega_omega0 = 0;

            for (int i = 1; i < count; i++)
            {
                double dAi = dAi_method(i);
                _Iy_omega0 += (2 * y_suradnice[i - 1] * omega[i - 1] +
                               2 * y_suradnice[i] * omega[i] +
                               y_suradnice[i - 1] * omega[i] +
                               y_suradnice[i] * omega[i - 1]) * dAi / 6;
                _Iz_omega0 += (2 * omega[i - 1] * z_suradnice[i - 1] +
                               2 * omega[i] * z_suradnice[i] +
                               omega[i - 1] * z_suradnice[i] +
                               omega[i] * z_suradnice[i - 1]) * dAi / 6;
                _Iomega_omega0 += (Math.Pow(omega[i], 2) + Math.Pow(omega[i - 1], 2) + omega[i] * omega[i - 1]) * dAi / 3;
            }
            _Iy_omega = _Iy_omega0 - (_Sz0 * _Iomega / _A);
            _Iz_omega = _Iz_omega0 - (_Sy0 * _Iomega / _A);
            _Iomega_omega = _Iomega_omega0 - (Math.Pow(_Iomega, 2) / _A);
        }
        //J.20 and J.21 method
        public void J_20_21_method()
        {
            try
            {
                double temp = _Iy * _Iz - Math.Pow(_Iyz, 2);
                d_y_sc = (_Iz_omega * _Iz - _Iy_omega * _Iyz) / temp;
                d_z_sc = (-_Iy_omega * _Iy - _Iz_omega * _Iyz) / temp;
                d_I_w = _Iomega_omega + d_z_sc * _Iy_omega - d_y_sc * _Iz_omega;
            }
            catch (DivideByZeroException) { MessageBox.Show("ERROR. Divide by zero, J.20 method."); }
        }
        //J.22 method
        public void J_22_method(int count)
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
                double min_more_than_zero, max;

                max = t_hodnoty[0]; // Set first item
                foreach (double num in t_hodnoty)
                    if (num > max) max = num; // Set new maximum

                min_more_than_zero = max; // Set minimum to maximum
                foreach (double num in t_hodnoty)
                    if (num != 0 && num < min_more_than_zero) min_more_than_zero = num; // Set non zero minimum

                d_W_t = d_I_t / min_more_than_zero;
            }
            else
                MessageBox.Show("ERROR. Minimalny prvok v t_hodnoty je nula!!!!.");
        }
        //J.23 method   ????? nerozumiem vzorcu...je potrebne upresnit
        public void J_23_method(int count)
        {
            d_omega_s[0] = 0;
            for (int i = 1; i < count; i++)
            {
                d_omega_s[i] = omega[i] - _omega_mean + d_z_sc * (y_suradnice[i] - d_y_gc) - d_y_sc * (z_suradnice[i] - d_z_gc);
            }
        }
        //J.24,J.25,J.26 method
        public void J_24_25_26_method()
        {
            omega_max = MathF.Max(d_omega_s);
            d_W_w = d_I_w / omega_max;
            d_y_s = d_y_sc - d_y_gc;
            d_z_s = d_z_sc - d_z_gc;
            _Ip = _Iy + _Iz + A * (Math.Pow(d_y_s, 2) + Math.Pow(d_z_s, 2));

        }
        //J.29 method
        public void J_29_method(int num)
        {
            d_y_ci = (y_suradnice[num] + y_suradnice[num - 1]) / 2 - d_y_gc;
            d_z_ci = (z_suradnice[num] + z_suradnice[num - 1]) / 2 - d_z_gc;
        }
        //J.27,J.28 method
        //This method uses J.29 method to count actual d_y_ci and d_z_ci numbers
        public void J_27_J_28_method(int count)
        {
            double zj_temp = 0, yj_temp = 0, dAi;
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
        // Calculate dimensions
        public void J_Calc_Dimensions()
        {
            y_min = y_suradnice[0];
            y_max = y_suradnice[0];
            z_min = z_suradnice[0];
            z_max = z_suradnice[0];

            foreach (double num in y_suradnice)
            {
                if (num > y_max) y_max = num; // Set new maximum
                if (num < y_min) y_min = num; // Set new minimum
            }

            foreach (double num in z_suradnice)
            {
                if (num > z_max) z_max = num; // Set new maximum
                if (num < z_min) z_min = num; // Set new minimum
            }

            _b = Math.Abs(y_max - y_min);
            _h = Math.Abs(z_max - z_min);

        }
        // Calculate elastic cross-section moduli
        public void J_W_el()
        {
            this._Wy_el_1 = _Iy / (z_max - d_z_gc);
            this._Wy_el_2 = _Iy / (d_z_gc - z_min);

            this._Wz_el_1 = _Iz / (y_max - d_y_gc);
            this._Wz_el_2 = _Iz / (d_y_gc - y_min);
        }
        // Calculate Monosymmetry section constant Beta y
        public void Calc_Beta_y_method(int count)
        {
            double Beta_y_temp = 0, dAi;
            for (int i = 1; i < count; i++)
            {
                this.J_29_method(i);
                dAi = this.dAi_method(i);

                //Beta_y_temp += (Math.Pow((y_suradnice[i] - d_y_gc) - (y_suradnice[i - 1] - d_y_gc), 2) * ((z_suradnice[i] - d_z_gc) - (z_suradnice[i - 1] - d_z_gc)) + Math.Pow((z_suradnice[i] - d_z_gc) - (z_suradnice[i - 1] - d_z_gc), 3)) * dAi;
                Beta_y_temp += (Math.Pow(d_y_ci, 2) * (d_z_ci) + Math.Pow(d_z_ci, 3)) * dAi;
            }

            Beta_y = (1 / _Iy) * Beta_y_temp - 2 * d_z_s;
        }
        // Calculate Monosymmetry section constant Beta z
        public void Calc_Beta_z_method(int count)
        {
            double Beta_z_temp = 0, dAi;
            for (int i = 1; i < count; i++)
            {
                this.J_29_method(i);
                dAi = this.dAi_method(i);

                // Beta_z_temp += (Math.Pow((z_suradnice[i] - d_z_gc) - (z_suradnice[i - 1] - d_z_gc), 2) * ((y_suradnice[i] - d_y_gc) - (y_suradnice[i - 1] - d_y_gc)) + Math.Pow((y_suradnice[i] - d_y_gc) - (y_suradnice[i - 1] - d_y_gc), 3)) * dAi;
                Beta_z_temp += (Math.Pow(d_z_ci, 2) * (d_y_ci) + Math.Pow(d_y_ci, 3)) * dAi;
            }

            Beta_z = (1 / _Iz) * Beta_z_temp - 2 * d_y_s;

            //Table E1
            // Pokusny vypocet - c s vystuhami na koncoch
            double a, b, c;

            a = 270 - 0.95;
            b = 70 - 0.95;
            c = 20;
            double t = 0.95;

            double x_ = b * b / (a + 2 * b);
            double x_o = x_ + ((3 * b * b) / (6 * b + a));
            double beta_w = (1 / 12) * t * x_ * a * a * a + t * x_ * x_ * x_ * a;
            double beta_f = (0.5 * t * (Math.Pow(b + x_, 4) - Math.Pow(x_, 4))) + (0.25 * a * a * t * (Math.Pow(b + x_, 2) + x_ * x_));
            double beta_L = 0;
            double m = (a * a * b * b * t) / _Iy * (0.25 + c / (2 * b) - (2 * c * c * c) / (3 * a * a * b));
            double beta_yps = ((beta_w + beta_f + beta_L) / _Iy) - 2 * x_o;
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
