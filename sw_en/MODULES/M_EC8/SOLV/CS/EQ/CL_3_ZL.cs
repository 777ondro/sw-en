using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M_EC8.SOLV.CS.EQ
{
    #region Helpful
    public static class MathF
    {
        //----------------------------------------------------------------------------------------------------------------------------
        // Minimum and Maximum
        //----------------------------------------------------------------------------------------------------------------------------
        #region MinMax
        // Tieto metody potrebuju lubovolny pocet argumentov a z tychto argumentov vratia minimalny alebo maximalny prvok
        // int
        // tymto metodam by bolo potrebne najprv inicializovat pole a potom im ho odovzdat ako argument
        // metody sluzia pre najdenie minimalneho(maximalneho prvku v zozname Int)
        public static int Min(List<int> data)
        {
            int min;
            min = data[0];
            foreach (int num in data)
                if (num < min) min = num;
            return min;
        }
        public static int Max(List<int> data)
        {
            int max;
            max = data[0];
            foreach (int num in data)
                if (num > max) max = num;
            return max;
        }

        // float
        public static float Min(params float[] data)
        {
            float min;
            min = data[0];
            foreach (float num in data)
                if (num < min) min = num;
            return min;
        }
        public static float Max(params float[] data)
        {
            float max;
            max = data[0];
            foreach (float num in data)
                if (num > max) max = num;
            return max;
        }

        // double
        public static double Min(params double[] data)
        {
            double min;
            min = data[0];
            foreach (double num in data)
                if (num < min) min = num;
            return min;
        }
        public static double Max(params double[] data)
        {
            double max;
            max = data[0];
            foreach (double num in data)
                if (num > max) max = num;
            return max;
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------
        // Equality of real numbers / Rovnost realnych cisel (float a double)
        //----------------------------------------------------------------------------------------------------------------------------
        #region Equal
        //overloaded methods for equation of real numbers (method d_equal)
        public static bool d_equal(double a, double b, double limit)
        {
            if (limit < 0) limit = Math.Abs(limit);
            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(double a, float b, double limit)
        {
            if (limit < 0) limit = Math.Abs(limit);
            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(float a, double b, double limit)
        {
            if (limit < 0) limit = Math.Abs(limit);
            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(float a, float b, double limit)
        {
            if (limit < 0) limit = Math.Abs(limit);
            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(double a, int b, double limit)
        {
            if (limit < 0) limit = Math.Abs(limit);
            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(float a, int b, double limit)
        {
            if (limit < 0) limit = Math.Abs(limit);
            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(double a, double b)
        {
            double limit;
            if (Math.Abs(b) > 0.0001)
                limit = 0.001 * Math.Abs(b);
            else limit = 0.00001;

            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(double a, float b)
        {
            double limit;
            if (Math.Abs(b) > 0.0001)
                limit = 0.001 * Math.Abs(b);
            else limit = 0.00001;

            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(float a, double b)
        {
            double limit;
            if (Math.Abs(b) > 0.0001)
                limit = 0.001 * Math.Abs(b);
            else limit = 0.00001;

            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(float a, float b)
        {
            double limit;
            if (Math.Abs(b) > 0.0001)
                limit = 0.001 * Math.Abs(b);
            else limit = 0.00001;

            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(double a, int b)
        {
            double limit;
            if (Math.Abs(b) > 0.0001)
                limit = 0.001 * Math.Abs(b);
            else limit = 0.00001;

            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        public static bool d_equal(float a, int b)
        {
            double limit;
            if (Math.Abs(b) > 0.0001)
                limit = 0.001 * Math.Abs(b);
            else limit = 0.00001;

            if ((a - limit) < b && (a + limit) > b) return true;
            else return false;
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------
        // Power / Mocniny
        //----------------------------------------------------------------------------------------------------------------------------
        #region Power
        public static float Pow2(float fx) { return fx * fx; }
        public static float Pow3(float fx) { return fx * fx * fx; }
        public static float Pow4(float fx) { float ftemp = fx * fx; return ftemp * ftemp; }
        public static float Pow5(float fx) { float ftemp = fx * fx; return ftemp * ftemp * fx; }
        public static float Pow6(float fx) { float ftemp = fx * fx * fx; return ftemp * ftemp; }
        public static float Pow7(float fx) { float ftemp = fx * fx * fx; return ftemp * ftemp * fx; }
        public static float Pow8(float fx) { float ftemp = fx * fx * fx * fx; return ftemp * ftemp; }
        public static float Pow9(float fx) { float ftemp = fx * fx * fx * fx; return ftemp * ftemp * fx; }
        // Prirodzeny mocnitel / kladny int
        public static float PowN(float fx, int iexp)
        {
            float pow = fx;
            if (iexp > 1)
            {
                for (int i = 0; i < iexp; i++)
                    pow *= fx;
                return pow;
            }
            else if (iexp == 1)
                return fx;
            else
                return 1f;
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------
        // Root / Odmocnina / Square Root / Druha odmocnina
        //----------------------------------------------------------------------------------------------------------------------------
        #region Root / Square Root
        /*
        public static short isqrt(short num)
        {
            short op = num;
            short res = 0;
            short one = 1 << 14; // The second-to-top bit is set: 1L<<30 for long

            // "one" starts at the highest power of four <= the argument.
            while (one > op)
                one >>= 2;

            while (one != 0)
            {
                if (op >= res + one)
                {
                    op -= res + one;
                    res = (res >> 1) + one;
                }
                else
                    res >>= 1;
                one >>= 2;
            }
            return res;
        }
        */


        /*
        public static float fastsqrt(float z)  
        {

        union
        {
                int tmp;
                float f;
        } u;
        u.f = z;
        u.tmp -= 1<<23; // Remove last bit so 1.0 gives 1.0
        // tmp is now an approximation to logbase2(z) 
        u.tmp >>= 1; // divide by 2
        u.tmp += 1<<29; // add 64 to exponent: (e+127)/2 =(e/2)+63,
        // that represents (e/2)-64 but want e/2
        return u.f;
        }
        */

        /*
        // Reciprocal of the square root
        public static float invSqrt(float x)
        {
        float xhalf = 0.5f*x;
        union
        {
                float x;
                int i;
        } u;
        u.x = x;
        u.i = 0x5f3759df - (u.i >> 1);
        x = u.x * (1.5f - xhalf * u.x * u.x);
        return x;
         }
        */



        /*
        public unsigned short sqrt(unsigned long a){
unsigned long rem = 0;
unsigned long root = 0;
for (int i = 0; i < 16; i++){
	root <<= 1;
	rem = ((rem << 2) + (a >> 30);
	a <<= 2;
	root ++;
	if(root <= rem){
		rem -= root;
		root ++;
	}
	else
		root --;
}
return (unsigned short)(root >> 1);
}
*/

        public static double Sqrt(double value)
        {
            //assert(value >= 1);

            if (value > 0)
            {
                double lo = 1.0;
                double hi = value;

                while (hi - lo > 0.00001)
                {
                    double mid = lo + (hi - lo) / 2;
                    //std::cout << lo << "," << hi << "," << mid << std::endl;
                    if (mid * mid - value > 0.00001)    //this is the predictors we are using 
                    {
                        hi = mid;
                    }
                    else
                    {
                        lo = mid;
                    }

                }

                return lo;
            }
            else
                return 1;
        }
        /*
 public static float Sqrt(float m)
 {
     float i = 0;
     float x1, x2;
     while ((i * i) <= m)
         i += 0.1;
     x1 = i;
     for (int j = 0; j < 10; j++)
     {
         x2 = m;
         x2 /= x1;
         x2 += x1;
         x2 /= 2;
         x1 = x2;
     }
     return x2;
 }
*/

        /*
        public static float Sqrt(float num)
        {
            if (num >= 0)
            {
                float x = num;
                int i;
                for (i = 0; i < 20; i++)
                {
                    x = (((x * x) + num) / (2 * x));
                }
                return x;
            }
            else
                return 1f; // Exception
        }
        */

        public static float Sqrt(float value)
        {
            if (value > 0f)
            {
                float lo = 1.0f;
                float hi = value;

                while (hi - lo > 0.00001f)
                {
                    float mid = lo + (hi - lo) / 2;
                    if (mid * mid - value > 0.00001f)    //this is the predictors we are using 
                    {
                        hi = mid;
                    }
                    else
                    {
                        lo = mid;
                    }

                }

                return lo;
            }
            else
                return 1f;
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------------
        // Constants / Konstatny 
        //----------------------------------------------------------------------------------------------------------------------------
        #region Constants
        // Napier's constant, or Euler's number, base of Natural logarithm
        // Represents the natural logarithmic base, specified by the constant, e.
        public const float fE = 2.7182818f;                                  // 7
        public const double dE = 2.718281828459045;                           // 15-16
        public const decimal mE = 2.718281828459045235360287471352662497m;     // 28-29
        // Pi, Archimedes' constant or Ludolph's number
        // Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        public const float fPI = 3.1415926f;                                 // 7
        public const double dPI = 3.14159265358979;                           // 15-16
        public const decimal mPI = 3.14159265358979323846264338327950288m;     // 28-29
        // Feigenbaum constant / Feigenbaumova konstanta delta δ
        public const decimal mFeigenDelta = 4.66920160910299067185320382m;
        // Feigenbaum constant / Feigenbaumova konstanta alfa α
        public const decimal mFeigenAlpha = 2.502907875095892822283902873218m;
        // Kaprekarova konstanta
        public const int iKaprekar = 6174;
        // Apéry's constant ζ(3)
        public const decimal Zeta3 = 1.202056903159594285399738161511449990764986292m;
        //Pythagoras' constant, square root of 2 (√2)
        public const float fSqrt2 = 1.4142135f;
        public const double dSqrt2 = 1.414213562373095;
        public const decimal mSqrt2 = 1.41421356237309504880168872420969807m;
        // Theodorus' constant, square root of 3 (√3)
        public const float fSqrt3 = 1.7320508f;
        public const double dSqrt3 = 1.732050807568877;
        public const decimal mSqrt3 = 1.73205080756887729352744634150587236m;
        // Square root of 5 (√5}
        public const float fSqrt5 = 2.236067f;
        public const double dSqrt5 = 2.236067977499789;
        public const decimal mSqrt5 = 2.23606797749978969640917366873127623m;
        #endregion
    }
    #endregion


    // Bugs
    // Pisat prednostne rovnice, ktore su jasne, ak je nieco nejake neiste radsej sa opytat alebo preskocit

    // 1)
    // Navratova hodnota +- nie je pripustna
    // return +-0.05f * fL_i;   // Eq. (4.3) ea,i

    // 2)
    // Nie je jasne, ktory vyraz sa nema rovnat nule
    // (fq || Math.Pow(fT, 2f) != 0)

    // 3)
    // rovnice ktore obsahuju sumu alebo "+" vynechavame !!!

    // 4)
    // pozor na prioritu znamienok a zatvroky
    // * / ma prioritu pred + - 
    // pozor na vyhodnocovanie zlomkov a prioritu, radsej pouzivat zatvorky

    // 5)
    // grecke symboly - prve pismeno velke
    // fgamma_Rd    fGamma_Rd

    // 6)
    // vysledkom <= by mal by podiel, to co je na <= lavej strane je v citateli a na pravej strane je v menovateli
    //                                            >= lavej strane je v citateli a na pravej strane je v menovateli
    // napr.

    /*
            public float Eq_62_____()
        {
            return 1.0f; // Eq. (6.2) MEd/Mpl,Rd
        }

            public float Eq_62_____(float fM_Ed, float fM_pl_Rd)
        {
            return (fM_Ed / fM_pl_Rd) / 1.0f; // Eq. (6.2) Design Ratio
        }
    */

    // alebo to moze znamenat ze nejaka hodnota sa rovna vzorcu a zaroven ma byt vysledok vzorca vacsi alebo mensi nez ina hodnota, preto sa potom pouzije Max, alebo Min z tychto dvoch vyrazov

    // 7)
    // pocet znakov v nazve funkcie by mal byt rovnaky - 10, tzn na koniec doplnit "_"

    // 8)
    // Pre porovnanie cisel medzi sebou alebo s nulou budeme pouzivat radsej specialnu funckiu
    // if (MathF.d_equal(ff_yd_T, 0))   // ak su rovnake ....
    // if (!MathF.d_equal(ff_yd_T, 0))  // ak nie su rovnake ....

    // 9)
    // rozlisovat pismena velke / male ! T vs t a pod. maju iny vyznam a moze to viest k zamene pri pouzivani rovnic


    public class CL_3_ZL
    {
        public float Eq_32______(float fa_g, float fS, float fT, float fT_B, float feta)
        {
            if (fT_B > 0f)
                return fa_g * fS * (1f + (fT / fT_B) * (feta * 2.5f - 1f)); // Eq. (3.2) Se(T)
            else
                return 0f;
        }
        public float Eq_33______(float fa_g, float fS, float feta)
        {
            return fa_g * fS * feta * 2.5f; // Eq. (3.3) Se(T)
        }
        public float Eq_34______(float fa_g, float fS, float feta, float fT_C, float fT)
        {
            if (fT > 0f)
                return fa_g * fS * feta * 2.5f * fT_C / fT; // Eq. (3.4) Se(T)
            else
                return 0f;
        }
        public float Eq_35______(float fa_g, float fS, float feta, float fT_C, float fT_D, float fT)
        {
            if (Math.Pow(fT, 2f) > 0f)
                return fa_g * fS * feta * 2.5f * ((fT_C * fT_D) / (float)Math.Pow(fT, 2f)); // Eq. (3.5) Se(T)
            else
                return 0f;
        }
        public float Eq_37______(float fS_e, float fT)
        {
            return fS_e * (fT) * (float)Math.Pow((fT / (2f * (float)Math.PI)), 2f); // Eq. (3.7) SDe(T)
        }
        public float Eq_38______(float fa_vg, float fT, float fT_B, float feta)
        {
            if (fT_B > 0f)
                return fa_vg * (1f + (fT / fT_B) * (feta * 3f - 1f)); // Eq. (3.8) Sve(T)
            else
                return 0f;
        }
        public float Eq_39______(float fa_vg, float feta)
        {
            return fa_vg * feta * 3f; // Eq. (3.9) Sve(T)
        }
        public float Eq_310_____(float fa_vg, float feta, float fT_C, float fT)
        {
            if (fT > 0f)
                return fa_vg * feta * 3f * fT_C / fT; // Eq. (3.10) Sve(T)
            else
                return 0f;
        }
        public float Eq_311_____(float fa_vg, float feta, float fT_C, float fT_D, float fT)
        {
            if (Math.Pow(fT, 2f) > 0f)
                return fa_vg * feta * 3f * ((fT_C * fT_D) / (float)Math.Pow(fT, 2f)); // Eq. (3.4) Se(T)
            else
                return 0f;
        }
        public float Eq_312_____(float fa_g, float fS, float fT_C, float fT_D)
        {
            return 0.025f * fa_g * fS * fT_C * fT_D; // Eq. (3.12) dg
        }
        //----------------------------------------10-----------------------------------------------------------------
        public float Eq_313_____(float fa_g, float fS, float fT, float fT_B, float fq)
        {
            if ((fT_B || fq) != 0)
                return fa_g * fS * (2 / 3f + fT / fT_B * (2.5f / fq - 2 / 3f)); // Eq. (3.13) Sd(T)
            else return 0f;
        }
        public float Eq_314_____(float fa_g, float fS, float fq)
        {
            if (fq != 0)
                return fa_g * fS * 2.5f / fq; // Eq. (3.14) Sd(T)
            else
                return 0f;
        }
        public float Eq_315_____(float fa_g, float fS, float fq, float fT_C, float fT, float fbeta)
        {
            if ((fq || fT) != 0)
                return fa_g * fS * 2.5f / fq * (fT_C / fT); // Eq. (3.15) Sd(T) ten vzorec obsahuje aj znamienko ≥ a neviem co s tym
            else
                return 0f;
        }
        public float Eq_316_____(float fa_g, float fS, float fT_D, float fT_D, float fT, float fq, float fbeta)
        {
            if ((fq || Math.Pow(fT, 2f) != 0))
                return ((fa_g * fS * 2.5f) / q) * ((fT_C * fT_D) / (float)Math.Pow(fT, 2f)) || (fbeta * fa_g); // Eq. (3.16) Sd(T), vzorec obsahuje znamieno >=
            else
                return 0f;
        }
        public float Eq_317_____(float fG_k_j, float fQ_k_j, float fpsy)
        {
            return (sum);  //vzorec obsahuje sumu a zlozeny index

        }

        public float Eq_41a_____(float fe_ox, float fr_x)
        {
            return 0.30f * fr_x; // Eq. (4.1a) eox
        }
        public float Eq_41b_____(float fr_x, float fl_s)
        {
            return fl_s;  // Eq. (4.1b) r_x
        }
        public float Eq_42_____(float fpsy_E_i, float fphi, float fpsy_2_i)
        {
            return fphi * fpsy_2_i; // Eq. (4.2) psyE,i
        }
        public float Eq_43_____(float fe_ai, float fL_i)
        {
            return +-0.05f * fL_i;   // Eq. (4.3) ea,i
        }
        public float Eq_44_____(float fT_C, float fs)
        {
            return (4f * fT_C) || (2.0f * fs); // Eq. (4.4) T1
        }
        //----------------------------------------20------------------------------------------------------------
        public float Eq_45_____(float fS_d, float fT_1, float fm, float flambda)
        {
            return fS_d * fT_1 * fm * flambda; // Eq. (4.5) Fb
        }
        public float Eq_46_____(float fC_t, float fH)
        {
            return (fC_t) * (float)Math.Pow(fH, 3 / 4f); // Eq. (4.6) T1
        }
        public float Eq_47_____(float fA_c)
        {
            if (MathF.Sqrt(fA_c) != 0)
                return 0.075f / (MathF.Sqrt(fA_c)); //  Eq. (4.7) C1 , vzorec obsahuje odmocninu
            else
                return 0f;
        }
        public float Eq_48_____(float fA_i, float fI_wi, float fH)
        {
            return fA_i * ((float)Math.Pow(0.2f + (fI_wi / fH), 2f)); // Eq. (4.8) Ac , vzorec obsahuje sumu
        }
        public float Eq_49_____(float fd)
        {
            return 2f * MathF.Sqrt(fd); // Eq. (4.9) T1 
        }
        public float Eq_410_____(float fF_b, float fs_i, float fm_i, float fs_j, float fm_j)
        {
            if ((fs_j || fm_j) != 0)
                return fF_b * ((fs_i * fm_i) / (fs_j * fm_j)); // Eq. (4.10) Fi , vorec obsahuje sumar
            else
                return 0f;
        }
        public float Eq_411_____(float fF_b, float fz_i, float fm_i, float fz_j, float fm_j)
        {
            return fF_b * ((fz_i * fm_i) / (fz_j * fm_j)); // Eq. (4.11) Fi , vzorec obsahuje sumar
        }
        public float Eq_412_____(float fx, float fL_e)
        {
            if (fL_e != 0)
                return 1f + (0.6f * (fx / fL_e)); // Eq. (4.12) delta
            else
                return 0f;
        }
        public float Eq_413_____(float fn)
        {
            return 3f * (MathF.Sqrt(fn)); // Eq. (4.13) k
        }
        public float Eq_414_____(float fs)
        {
            return 0.20f * fs; // Eq. (4.14) Tk
        }
        //-------------------------------------------30-------------------------------------------------------------------
        public float Eq_415_____(float fT_i)
        {
            return 0.9f * fT_i; //  Eq. (4.15) Tj
        }
        public float Eq_416_____(float fE_Ei)
        {
            return MathF.Sqrt((float)Math.Pow(fE_Ei, 2f));  // Eq. (4.16) Ee , vzorec obsahuje sumu
        }
        public float Eq_417_____(float fe_ai, float fF_i)
        {
            return fe_ai * fF_i; // Eq. (4.17) Mai
        }
        public float Eq_418_____(float fE_Edx, float fE_Edy)
        {
            return (fE_Edx)"+"(0.30f * fE_Edy); // Eq. (4.18) Eedx !!!!
        }
        public float Eq_419_____(float fE_Edx, float fE_Edy)
        {
            return (0.30f * fE_Edx); "+"(fE_Edy); // Eq. (4.19) 0.30 Eedx !!!!
        }
        public float Eq_420_____(float fE_Edx, float fE_Edy, float fE_Edz)
        {
            return (fE_Edx)"+"(0.30f * fE_Edy); "+"(0.30f * fE_Edz); // Eq. (4.20) !!!!
        }
        public float Eq_421_____(float fE_Edx, float fE_Edy, float fE_Edz)
        {
            return (0.30f * fE_Edx); "+"(fE_Edy); "+"(fE_Edz); // Eq. (4.21) !!!!!
        }
        public float Eq_422_____(float fE_Edx, float fE_Edy, float fE_Edz)
        {
            return (0.30f * fE_Edx); "+"(0.30f * fE_Edy); "+"(fE_Edz); // Eq. (4.22) !!!!
        }
        public float Eq_423_____(float fq_d, float fd_e)
        {
            return fq_d * fd_e; // Eq. (4.23) ds
        }
        public float Eq_424_____(float fS_a, float fW_a, float fgamma_a, float fq_a)
        {
            if (fq_a != 0)
                return (fS_a * fW_a * fgamma_a) / fq_a; // Eq. (4.24) Fa
            else
                return 0f;
        }
        //--------------------------------------------40-----------------------------------------------------------------------
        public float Eq_425_____(float falpha, float fS, float fz, float fH, float fT_a, float fT_1)
        {
            if ((fH || fT_1) != 0)
                return falpha * fS * (3f * (1f + (fz / fH)) / (1f + (float)Math.Pow(1f - (fT_a / fT_1), 2f)) - 0.5f); // Eq. (4.25) Sa
            else
                return 0f;
        }
        //public float Eq_426_____(float fdelta, float fV_RW, float fV_Ed, float fq)
        //{
        //  return (1f+fdelta*fV_RW/fV_Ed)==
        //}
        public float Eq_427_____(float fR_d)
        {
            return fR_d; // Eq. (4.27) Ed
        }
        public float Eq_428_____(float fP_tot, float fd_r, float fV_tot, float fh)
        {
            if ((fV_tot * fh) != 0)
                return ((fP_tot * fd_r) / (fV_tot * fh)) <= 0.1f; // Eq. (4.28) theta
            else
                return 0f;
        }
        public float Eq_429_____(float fM_Rb)
        {
            return 1.3f * sum(fM_Rb); // Eq. (4.29) sum MRc
        }
        public float Eq_430_____(float fE_F_G, float fgamma_Rd, float fomega, float fE_F_E)
        {
            return fE_F_G + fgamma_Rd * fomega * fE_F_E; // Eq. (4.30) EFd
        }
        public float Eq_431_____(float fh)
        {
            return 0.005f * fh; // Eq. (4.31) dr v
        }
        public float Eq_432_____(float fh)
        {
            return 0.0075f * fh; // Eq. dr v
        }
        public float Eq_433_____(float fh)
        {
            return 0.010f * fh; // Eq. (4.33) dr v
        }
        public float Eq_51_____(float fq_o, float fk_W)
        {
            return fq_o * fk_W >= 1.5f; // Eq. (5.1) q
        }
        public float Eq_52_____()
        {
            return 1f; // Eq. (5.2) kw
        }
        //-------------------------------------------------50------------------------------------------------------------
        public float Eq_53_____(float fh_Wi, float fl_Wi)
        {
            return (sum(fh_Wi)) / (sum(fl_Wi)); // Eq. (5.3) alpha o , vzorec obsahuje sumu
        }
        public float Eq_54_____(float fq_o, float fT_1, float fT_C)
        {
            if (fT_1 >= fT_C)
                return (2f * fq_o - 1f); // Eq. (5.4) mu
            else
                return 0f;
        }
        public float Eq_55_____(float fq_o, float fT_C, float fT_1)
        {
            if (fT_1 >= fT_C)
                return 1f + (2f * (fq_o - 1f)) * (fT_C / fT_1); // Eq. (5.5) mu phi
            else
                return 0f;
        }
        public float Eq_56_____(float fmin, float fb_c, float fh_W)
        {
            return fmin; { fb_c + (fh_Wi); (2f * fb_c); }; // Eq. (5.6) bw
        }
        public float Eq_57_____(float fmax, float fh_s)
        {
            return max; { 0.15f; fh_s / 20f; };// Eq. (5.7) bwo
        }
        public float Eq_58_____(float fgamma_Rd, float fM_Rb_i, float fmin, float fM_Rc, float fM_Rb)
        {
            return fgamma_Rd * fM_Rb_i * min(1, sum(fM_Rc) / sum(fM_Rb)); // Eq. (5.8) Mi,d  , vozrec obsahuje sumu
        }
        public float Eq_510_____(float fV_Ed, float fq)
        {
            return V_Ed * ((fq + 1f) / 2f); // Eq. (5.10) Ved
        }
        public float Eq_511_____(float fp, float ff_cd, float fmu_phi, float fepsilon_sy_d, float ff_yd)
        {
            if (fmu_phi != 0)
                return fp + (0.0018f / (fmu_phi * fepsilon_sy_d) * ff_cd * ff_yd); // Eq. (5.11) pmax ,epsilon ma zlozeny index
            else
                return 0f;
        }
        public float Eq_512_____(float ff_ctm, float ff_yk)
        {
            if (ff_yk != 0)
                return 0.5f * (ff_ctm / ff_yk); // Eq. (5.12) pmin
            else
                return 0f;
        }
        public float Eq_513_____(float fh_w, float fd_bw, float fd_bL)
        {
            return min; { fh_w / 4f; 24f * fd_bw; 255f; 8f * fd_bL; }; // Eq. (5.13) s
        }
        //------------------------------------------------60----------------------------------------------------------------------------
        public float Eq_514_____(float fh_c, float fl_d)
        {
            return max; { fh_c; fl_d / 6f; 0.45f; }; // Eq. (5.14) lcr
        }
        public float Eq_515_____(float fmu_phi, float fv_d, float fepsilon_sy_d, float fb_c, float fb_o)
        {
            if (fb_o != 0)
                return 30f * fmu_phi * fv_d * fepsilon_sy_d * (fb_c / fb_o) - 0.035f; // Eq. (5.15) alpha omega wd
            else
                return 0f;
        }
        public float Eq_516a_____(float fb_i, float fb_o, float fh_o)
        {
            return 1f - sum((float)Math.Pow(fb_i, 2f)) / (6f * fb_o * fh_o); // Eq. (5.16a) alpha n , vzorec obsahuje sumu
        }
        public float Eq_517a_____(float fs, float fb_o, float fh_o)
        {
            if ((fb_o || fh_o) != 0)
                return (1f - (fs / 2f * fb_o)) * (1f - fs / (2f * fh_o)); // Eq. (5.17a) alpha s
            else
                return 0f;
        }
        public float Eq_516b_____()
        {
            return 1f; // Eq. (5.16b) alpha n
        }
        public float Eq_517b_____(float fs, float fD_o)
        {
            if (fD_o != 0)
                return (float)Math.Pow(1f - (fs / (2f * fD_o)), 2f); // Eq. (5.17b) alpha s
            else
                return 0f;

        }
        public float Eq_516c_____()
        {
            return 1f; // Eq. (5.16c) alpha n
        }
        public float Eq_517c_____(float fs, float fD_o)
        {
            if (fD_o != 0)
                return (1 - (fs / (2f * fD_o))); // Eq. (5.17c)  alpha s
            else
                return 0f;
        }
        public float Eq_518_____(float fb_o, float fd_bL)
        {
            return min; { fb_o / 2f; 175f; 8f * fd_bL; }; // Eq. (5.18) s
        }
        public float Eq_520_____(float fmu_phi, float fv_d, float fomega_v, float fepsilon_sy_d, float fb_c, float fb_o)
        {
            if (fb_o != 0)
                return 30f * fmu_phi * (fv_d + fomega_v) * fepsilon_sy_d * (fb_c / fb_o) - 0.035f; // Eq. (5.20) alpha * omga wd
            else
                return 0f;
        }
        //-----------------------------------------------70--------------------------------------------------------------------------------
        public float Eq_521_____(float fv_d, float fomega_v, float fl_w, float fb_c, float fb_o)
        {
            if (fb_o != 0)
                return (fv_d + fomega_v) * (fl_w * fb_c) / fb_o; // Eq. (5.21) xu
            else
                return 0f;
        }
        public float Eq_522_____(float fgamma_Rd, float fA_s1, float fA_s2, float ff_yd, float fV_c)
        {
            return fgamma_rd * (fA_s1 + fA_s2) * ff_yd - fV_c; // Eq. (5.22) Vjhd
        }
        public float Eq_523_____(float fgamma_Rd, float fA_s1, float ff_yd, float fV_c)
        {
            return fgamma_Rd * fA_s1 * ff_yd - fV_c; // Eq. (5.23) Vjhd
        }
        public float Eq_524_____(float fepsilon, float fV_Ed)
        {
            return fepsilon * fV_Ed; // Eq. (5.24) Ved
        }
        public float Eq_525_____(float fgamma_Rd, float fM_Rd, float fS_e, float fT_c, float fq, float fM_Ed, float fT_1)
        {
            if ((fq || fM_Ed || fS_e * fT_1) != 0)
                return fq * MathF.Sqrt(((float)Math.Pow((fgamma_Rd / q) * (fM_Rd / fM_Ed), 2f) + 0.1f * ((float)Math.Pow(((fS_e * fT_c) / (fS_e * fT_1)), 2f)) <= fq)); // Eq. (5.25) epsilon
            else
                return 0f;
        }
        public float Eq_526_____(float fgamma, float fM_Rd, float fM_Ed, float fV_Ed, float fq)
        {
            if (fM_Ed != 0)
                return fgamma_Rd * (fM_Rd / fM_Ed) * fV_Ed <= fq * fV_Ed; // Eq. (5.26) Ved
            else
                return 0f;
        }
        public float Eq_527_____(float fxi, float ff_ctd, float fb_wd, float fd)
        {
            return (2f + fxi) * ff_ctd * fb_wd * fd; // Eq. (5.27) |Ve|max
        }
        public float Eq_528_____(float fA_s, float ff_yd, float falpha)
        {
            return 2f * fA_s * ff_yd * sin(falpha); // Eq. (5.28) 0.5 Vemax
        }
        public float Eq_529_____(float fh_w, float fd_bw, float fd_bL)
        {
            return min; { fh_w / 4f; 24f * fd_bw; 175f; 6f * d_bL; }; // Eq. (5.29)  s
        }
        public float Eq_530_____(float fh_c, float fl_c1)
        {
            return max; { 1.5f * fh_c; fl_c1 / 6f; 0.6f; }; // Eq. (5.30) lcr
        }
        //---------------------------------------80---------------------------------------------------------------
        public float Eq_531_____(float fd_bL_max, float ff_ydL, float ff_ydw)
        {
            if (ff_ydw != 0)
                return 0.4f * fd_bL_max * MathF.Sqrt((ff_ydL / ff_ydw)); // Eq. (5.31) dbw
            else
                return 0f;
        }
        public float Eq_532_____(float fb_o, float fd_bL)
        {
            return min; { fb_o / 3f; 125f; 6f * fd_bL; }; // Eq. (5.32) s
        }
        public float Eq_533_____(float feta, float ff_cd, float fv_d, float fb_j, float fh_jc)
        {
            if (feta != 0)
                return feta * ff_cd * MathF.Sqrt((1f - (fv_d / feta))) * fb_j * fh_jc; // Eq. (5.33) Vjhd
            else
                return 0f;
        }
        public float Eq_534a_____(float fb_c, float fb_w, float fh_c)
        {

            return min; { fb_c; (fb_w + 0.5f * fh_c); }; // Eq. (5.34a) bj

        }
        public float Eq_534b____(float fb_w, float fb_c, float fh_c)
        {

            return min; { fb_w; (fb_c + 0.5f * fh_c); }; // Eq: (5.34b) bj

        }
        public float Eq_535_____(float fV_jhd, float fb_j, float fh_jc, float ff_ctd, float fv_d, float ff_cd)
        {
            if ((fb_j * fh_jc) != 0)
                return (((float)Math.Pow(fV_jhd / (fb_j * fh_jc), 2f)) / (ff_ctd + fv_d * ff_cd)) - 1f; // Eq. (5.35) (Ash*fywd)/(bj*hjw)
            else
                return 0f;
        }
        public float Eq_536a_____(float fgamma_Rd, float fA_s1, float fA_s2, float ff_yd, float fv_d)
        {
            return fgamma_Rd * (fA_s1 + FA_s2) * ff_yd * (1f - 0.8f * fv_d); // Eq. (5.36a) Ash*fywd
        }
        public float Eq_536b_____(float fgamma_Rd, float fA_s2, float ff_yd, float fv_d)
        {
            return fgamma_Rd * fA_s2 * ff_yd * (1f - 0.8f * fv_d); // Eq. (5.36b) Ash*fywd
        }
        public float Eq_537_____(float fA_sh, float fh_jc, float fh_jw)
        {
            if (fh_jw != 0)
                return (2f / 3f) * fA_sh * (fh_jc / fh_jw); // Eq. (5.37) Asv,i
            else
                return 0f;
        }
        public float Eq_538_____(float fV_Rd_c, float frho_n, float ff_yd_h, float fb_wo, float falpha_s, float fl_w)
        {
            return fV_Rd_c + 0.75f * frho_n * ff_yd_h * fb_wo * falpha_s * fl_w; // Eq. (5.38) VEd
        }
        //-------------------------------------------90-------------------------------------------------------------------
        public float Eq_539_____(float frho_v, float ff_yd_v, float fb_wo, float fz, float fN_Ed)
        {
            return frho_v * ff_yd_v * fb_wo * fz + min(fN_Ed); // Eq. (5.39) rho n*fyd,h*bwo*z
        }
        public float Eq_540_____(float fV_dd, float fV_id, float fV_fd)
        {
            return fV_dd + fV_id + fV_fd; // Eq. (5.40) VRd,S
        }
        public float Eq_544_____(float ff_ck)
        {
            return 0.6f * (1f - (ff_ck / 250f)); // Eq. (5.44)  eta
        }
        public float Eq_548_____(float ff_ctd, float fb_w, float fd)
        {
            return ff_ctd * fb_w * fd; // Eq. (5.48) VEd
        }
        public float Eq_549_____(float fA_si, float ff_yd, float falpha)
        {
            return 2f * fA_si * ff_yd * sin(falpha); // Eq. (5.49) VEd
        }
        public float Eq_550b_____(float ff_ctm, float fv_d, float fgamma_Rd, float ff_yd)
        {
            if ((fgamma_Rd * ff_yd) != 0)
                return (7.5f * ff_ctm / fgamma_Rd * ff_yd) * (1f + 0.8f * fv_d); // Eq. (5.50b) dbL/hc
            else
                return 0f;
        }
        public float Eq_551_____(float fh)
        {
            return min; { fh / 4f; 100; }; // Eq.(5.51) s
        }
        public float Eq_552_____(float fs, float fd_bL, float ff_yld, float ff_ywd)
        {
            if (ff_ywd != 0)
                return fs * (fd_bL / 50f) * (ff_yld / ff_ywd); // Eq. (5.52) Ast
            else
                return 0f;
        }
        public float Eq_553_____(float fk_p, float fq)
        {
            return fk_p * fq; // Eq. (5.53) qp
        }
        public float Eq_61_____(float fgamma_ov, float fR_fy)
        {
            return 1.1f * fgamma_ov * fR_fy; // Eq. (6.1) Rd
        }
        //---------------------------------------100-------------------------------------------------------------
        public float Eq_62_____()
        {
            return 1.0f; // Eq. (6.2) MEd/Mpl,Rd
        }
        public float Eq_63_____()
        {
            return 0.15f; // Eq. (6.3) NEd/Npl,Rd
        }
        public float Eq_64_____()
        {
            return 0.5f; // Eq. (6.4) VEd/Vpl,Rd
        }
        public float Eq_65_____(float fV_Ed_G, float fV_Ed_M)
        {
            return fV_Ed_G + fV_Ed_M; // Eq. (6.5) VEd
        }
        public float Eq_66_____(float fM_Ed_G, float fgamma_ov, float fomega, float fM_Ed_E)
        {
            return fM_Ed_G + 1.1f * fgamma_ov * fomega * fM_Ed_E; // Eq. (6.6) MEd
        }
        public float Eq_67_____()
        {
            return 0.5f; // Eq. (6.7) VEd/Vpl,Rd
        }
        public float Eq_68_____()
        {
            return 1.0f; // Eq. (6.8) Vwp,Ed/Vwp,Rd
        }
        public float Eq_69_____(float fV_wb_Rd)
        {
            return fV_wb_Rd; // Eq. (6.9) Vwp,Ed
        }
        public float Eq_610_____(float fdelta, float fL)
        {
            if (fL != 0)
                return fdelta / 0.5f * fL; // Eq. (6.10) theta p
            else
                return 0f;
        }
        public float Eq_611_____()
        {
            return 0.05f; // Eq. (6.11) |A+ - A-|/A+ + A-
        }
        //----------------------------------------110--------------------------------------------------------------
        public float Eq_612_____(float fN_Ed_G, float fgamma_ov, float fomega, float fN_Ed_E)
        {
            return fN_Ed_G + 1.1f * fgamma_ov * fomega * fN_Ed_E; // Eq. (6.12) Npl,Rd(MEd)
        }
        public float Eq_613_____(float ff_y, float fb, float ft_f, float fd)
        {
            return ff_y * fb * ft_f * (fd - ft_f); // Eq. (6.13) Mp,link
        }
        public float Eq_614_____(float ff_y, float ft_w, float fd, float ft_f)
        {
            return (ff_y / MathF.Sqrt(3f)) * ft_w * (fd - ft_f); // Eq. (6.14) Vp,link
        }
        public float Eq_615____(float fV_p_link)
        {
            return fVp_p_link; // Eq. (6.15) VEd
        }
        public float Eq_616_____(float fM_p_link)
        {
            return fM_p_link; // Eq. (6.16) MEd
        }
        public float Eq_621_____(float fM_p_link, float fV_p_link)
        {
            if (fV_p_link != 0)
                return 1.6f * fM_p_link / fV_p_link; // Eq. (6.21) e<es
            else
                return 0f;
        }
        public float Eq_622_____(float fM_p_link, float fV_p_link)
        {
            if (fV_p_link != 0)
                return 3.0f * fM_p_link / fV_p_link; // Eq. (6.22) e>eL
            else
                return 0f;
        }
        public float Eq_623_____(float fe_L)
        {
            return fe_L; // Eq. (6.23) es<e<eL
        }
        public float Eq_624_____(float falpha, float fM_p_link, float fV_p_link)
        {
            if (fV_p_link != 0)
                return 0.8f * (1f + falpha) * (fM_p_link / fV_p_link); // Eq. (6.24) e<es
            else
                return 0f;
        }
        public float Eq_625_____(float falpha, float fM_p_link, float fV_p_link)
        {
            if (fV_p_link != 0)
                return 1.5f * (1f + falpha) * (fM_p_link / fV_p_link); // Eq. (6.25) e>el
            else
                return 0f;
        }
        //----------------------------------------120------------------------------------------------------------
        public float Eq_626_____(float e_L)
        {
            return e_L; // Eq. (6.26) es<e
        }
        public float Eq_630_____(float fN_Ed_G, float fgamma_ov, float fomega, float fN_Ed_E)
        {
            return fN_Ed_G + 1.1f * fgamma_ov * fomega * fN_Ed_E; // Eq. (6.30) NRd(MEd, VEd)
        }
        public float Eq_631_____(float fE_d_G, float fgamma_ov, float fomega_i, float fE_d_E)
        {
            return fE_d_G + 1.1f * fgamma_ov * fomega_i * fE_d_E; // Eq. (6.31) Ed
        }
        public float Eq_71_____(float fE_a, float fE_cm)
        {
            return fE_a / fE_cm = 7f; // Eq. (7.1) n
        }
        public float Eq_73_____(float fV_wp_Rd)
        {
            return 0.8f * fV_wp_Rd; // Eq. (7.3)  Vwp,Ed
        }
        public float Eq_74_____(float fepsilon_cu2, float fepsilon_a)
        {
            if ((fepsilon_cu2 + fepsilon_a) != 0)
                return fepsilon_cu2 / (fepsilon_cu2 + fepsilon_a);  // Eq. (7.4) x/d
            else
                return 0f;
        }
        public float Eq_75_____(float fmu_phi, float fv_d, float fepsilon_sy_d, float fb_c, float fb_o)
        {
            if (fb_o != 0)
                return 30f * fmu_phi * fv_d * fepsilon_sy_d * (fb_c / fb_o) - 0.035f; // Eq. (7.5) alpha*omega wd
            else
                return 0f;
        }
        public float Eq_76_____(float fN_Ed, float fA_a, float ff_yd, float fA_c, float ff_cd, float fA_s, float ff_sd)
        {
            if ((fA_a * ff_yd + fA_c * ff_cd + fA_s * ff_sd) != 0)
                return fN_Ed / (fA_a * ff_yd + fA_c * ff_cd + fA_s * ff_sd); // Eq. (7.6) vd=Ned/Npl,Rd
            else
                return 0f;
        }
        public float Eq_77_____(float fb_o, float fd_bL)
        {
            return MathF.Min(fb_o / 2f, 260f, 9f * d_bL); // Eq. (7.7) s
        }
        public float Eq_78_____(float fb_o, float fd_bL)
        {
            return min(fb_o / 2f, 175f, 8f * d_bL); ; // Eq. (7.8) s
        }
        //-----------------------------------------130----------------------------------------------------
        public float Eq_712_____(float fb, float ft_f, float ff_ydf, float ff_ydw)
        {
            if (ff_ydw != 0)
                return (float)Math.Pow((fb * ft_f / 8f) * (ff_ydf / ff_ydw), 0.5f); // Eq. (7.12) dbw
            else
                return 0f;
        }
        public float Eq_713_____(float fI_1, float fI_2)
        {
            return 0.6f * fI_1 + 0.4f * fI_2; // Eq. (7.13) Ieq
        }
        public float Eq_714_____(float fE, float fI_a, float fr, float fE_cm, float fI_c, float fI_s)
        {
            return 0.9f * (fE * fI_a + fr * fE_cm * fI_c + FE * fI_s); // Eq. (7.14) (EIc)
        }
        public float Eq_716_____(float fM_p_link, float fV_p_link)
        {
            if (fV_p_link != 0)
                return 2f * fM_p_link / fV_p_link;  // Eq. (7.16) e
            else
                return 0f;
        }
        public float Eq_717_____(float fM_p_link, float fV_p_link)
        {
            if (fV_p_link != 0)
                return 2f * fM_p_link / fV_p_link; // Eq. (7.17) e
            else
                return 0f;
        }
        public float Eq_718_____(float fV_Rd)
        {
            return fV_Rd; // Eq. (7.18) VEd
        }
        public float Eq_719_____(float fA_pl, float ff_yd)
        {
            return (fA_pl * ff_yd) / MathF.Sqrt(3f); // Eq. (7.19) VRd
        }
        public float Eq_A1_____(float fa_g, float fS, float fT_C, float fT_D, float feta, float fT, float fT_E, float fT_F)
        {
            if ((fT_F - fT_E) != 0)
                return 0.025f * fa_g * fS * fT_C * fT_D * (2.5f * feta + ((fT - fT_E) / (fT_F - fT_E)) * (1f - 2.5f * feta)); // Eq. (A.1) SDe(t)
            else
                return 0f;
        }
        public float Eq_A2_____(float fd_g)
        {
            return fd_g; // Eq. (A.2) SDe(t)
        }
        public float Eq_B1_____(float fm_i, float fphi_i)
        {
            return fm_i * fphi_i; // Eq. (B.1) Fi
        }
        //------------------------------------------------140----------------------------------------------------------------
        public float Eq_B4_____(float fF_b, float ftau)
        {
            return fF_b / ftau; // Eq. (B.4) F*
        }
        public float Eq_B5_____(float fd_n, float ftau)
        {
            return fd_n / ftau; // Eq. (B.5) d*
        }

        public float Eq_C4_____(float fb_b, float fd_eff, float ff_cd)
        {
            return fb_b * fd_eff * ff_cd; // Eq. (C.4) FRd1
        }
        public float Eq_C5_____(float fh_c, float fd_eff, float ff_cd)
        {
            return 0.7f * fh_c * fd_eff * ff_cd; // Eq. (C.5) FRd2
        }
        public float Eq_C6_____(float fF_Rd2, float ff_yd_T)
        {
            if (MathF.d_equal(ff_yd_T, 0))
                return fF_Rd2 / ff_yd_T; // Eq. (C.6) AT
            else
                return 0f;
        }
        public float Eq_C7_____(float fb_eff, float fd_eff, float ff_cd)
        {
            return fb_eff * fd_eff * ff_cd; // Eq. (C.7) FRd1+FRd2
        }
        public float Eq_C8_____(float fn, float fP_Rd)
        {
            return fn * fP_Rd; // Eq. (C.8) FRd3
        }
        public float Eq_C10_____(float fb_b, float fd_eff, float ff_cd)
        {
            return fb_b * fd_eff * ff_cd; // Eq. (C.10) FRd1
        }
        public float Eq_C11_____(float fh_c, float fd_eff, float ff_cd)
        {
            return 0.7f * fh_c * fd_eff * ff_cd; // Eq. (C.11) FRd2
        }
        public float Eq_C13_____(float fh_c, float fb_b, float fd_eff, float ff_cd)
        {
            return (0.7f * fh_c + fb_b) * fd_eff * ff_cd; // Eq. (C.13) FRd1+FRd2
        }
        //----------------------------------------------150---------------------------------------------------------
        public float Eq_C14_____(float fA_s, float ff_yd, float fb_eff, float fd_eff, float ff_cd)
        {
            return fA_s * ff_yd + fb_eff * fd_eff * ff_cd; // Eq. (C.14) Fst+Fsc
        }
        public float Eq_C15_____(float fF_Rd1, float fF_Rd2)
        {
            return fF_Rd1 + fF_Rd2; // Eq. (C.15) 1.2*(Fsc+Fst)
        }
        public float Eq_C16_____(float fn, float fP_Rd)
        {
            return fn * fP_Rd; // Eq. (C.16) FRcd
        }
        public float Eq_C17_____(float fh_c, float fb_b, float fd_eff, float ff_cd, float fn, float fP_Rd)
        {
            return (0.7f * fh_c + fb_b) * fd_eff * ff_cd + fn * fP_Rd; // Eq. (C.17) FRd1+Frd2+Frd3
        }
        public float Eq_C18_____(float fF_Rd1, float fF_Rd2, float fF_Rd3)
        {
            return fF_Rd1 + fF_Rd2 + fF_Rd3; // Eq. (C.18) 1.2*(Fsc+Fst)
        }
    }
}