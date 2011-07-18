using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MATH
{
    //trieda je verejna staticka co znamena ze objekt tejto triedy sa nevytvara,ale pouziva sa podobne ako trieda Math
    public static class MathF
    {

        //tymto metodam by bolo potrebne najprv inicializovat pole a potom im ho odovzdat ako argument
        //metody sluzia pre najdenie minimalneho(maximalneho prvku v zozname Int)
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



        //tieto metody potrebuju lubovolny pocet argumentov a z tychto argumentov vratia minimalny alebo maximalny prvok
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
        //overloaded methods for equation of real numbers (method d_equal)
        #region d_equal
        public static bool d_equal(double a, double b, double limit) 
        {
            if (limit<0) limit = Math.Abs(limit);
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

        //----------------------------------------------------------------------------------------------------------------------------
        // Square Root / Druha odmocnina
        //----------------------------------------------------------------------------------------------------------------------------
        
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

        //----------------------------------------------------------------------------------------------------------------------------



































        #region before
        // General maths methods
        // Minimum - double
        //double Min_3(
        //double a,
        //double b,
        //double c)
        //{
        //    double d_Min = Math.Min(a,
        //                   Math.Min(b, c));
        //    return d_Min;
        //}
        //double Min_4(
        //double a,
        //double b,
        //double c,
        //double d)
        //{
        //    double d_Min = Math.Min(a,
        //                   Math.Min(b,
        //                   Math.Min(c, d)));
        //    return d_Min;
        //}
        //double Min_5(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e)
        //{
        //    double d_Min = Math.Min(a,
        //                   Math.Min(b,
        //                   Math.Min(c,
        //                   Math.Min(d, e))));
        //    return d_Min;
        //}
        //double Min_6(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e,
        //double f)
        //{
        //    double d_Min = Math.Min(a,
        //                   Math.Min(b,
        //                   Math.Min(c,
        //                   Math.Min(d,
        //                   Math.Min(e, f)))));
        //    return d_Min;

        //}
        //double Min_7(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e,
        //double f,
        //double g)
        //{
        //    double d_Min = Math.Min(a,
        //                Math.Min(b,
        //                Math.Min(c,
        //                Math.Min(d,
        //                Math.Min(e,
        //                Math.Min(f, g))))));
        //    return d_Min;
        //}
        //double Min_8(
        //   double a,
        //   double b,
        //   double c,
        //   double d,
        //   double e,
        //   double f,
        //   double g,
        //   double h)
        //{
        //    double d_Min = Math.Min(a,
        //                   Math.Min(b,
        //                   Math.Min(c,
        //                   Math.Min(d,
        //                   Math.Min(e,
        //                   Math.Min(f,
        //                   Math.Min(g, h)))))));
        //    return d_Min;
        //}
        //double Min_9(
        //   double a,
        //   double b,
        //   double c,
        //   double d,
        //   double e,
        //   double f,
        //   double g,
        //   double h,
        //   double i)
        //{
        //    double d_Min = Math.Min(a,
        //                Math.Min(b,
        //                Math.Min(c,
        //                Math.Min(d,
        //                Math.Min(e,
        //                Math.Min(f,
        //                Math.Min(g,
        //                Math.Min(h, i))))))));
        //    return d_Min;
        //}
        //double Min_10(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e,
        //double f,
        //double g,
        //double h,
        //double i,
        //double j)
        //{
        //    double d_Min = Math.Min(a,
        //                Math.Min(b,
        //                Math.Min(c,
        //                Math.Min(d,
        //                Math.Min(e,
        //                Math.Min(f,
        //                Math.Min(g,
        //                Math.Min(h,
        //                Math.Min(i, j)))))))));
        //    return d_Min;
        //}

        //// Maximum - double
        //double Max_3(
        //double a,
        //double b,
        //double c)
        //{
        //    double d_Max = Math.Max(a,
        //                   Math.Max(b, c));
        //    return d_Max;
        //}
        //double Max_4(
        //double a,
        //double b,
        //double c,
        //double d)
        //{
        //    double d_Max = Math.Max(a,
        //                   Math.Max(b,
        //                   Math.Max(c, d)));
        //    return d_Max;
        //}
        //double Max_5(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e)
        //{
        //    double d_Max = Math.Max(a,
        //                   Math.Max(b,
        //                   Math.Max(c,
        //                   Math.Max(d, e))));
        //    return d_Max;
        //}
        //double Max_6(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e,
        //double f)
        //{
        //    double d_Max = Math.Max(a,
        //                   Math.Max(b,
        //                   Math.Max(c,
        //                   Math.Max(d,
        //                   Math.Max(e, f)))));
        //    return d_Max;


        //}
        //double Max_7(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e,
        //double f,
        //double g)
        //{
        //    double d_Max = Math.Max(a,
        //                Math.Max(b,
        //                Math.Max(c,
        //                Math.Max(d,
        //                Math.Max(e,
        //                Math.Max(f, g))))));
        //    return d_Max;
        //}
        //double Max_8(
        //   double a,
        //   double b,
        //   double c,
        //   double d,
        //   double e,
        //   double f,
        //   double g,
        //   double h)
        //{
        //    double d_Max = Math.Max(a,
        //                   Math.Max(b,
        //                   Math.Max(c,
        //                   Math.Max(d,
        //                   Math.Max(e,
        //                   Math.Max(f,
        //                   Math.Max(g, h)))))));
        //    return d_Max;
        //}
        //double Max_9(
        //   double a,
        //   double b,
        //   double c,
        //   double d,
        //   double e,
        //   double f,
        //   double g,
        //   double h,
        //   double i)
        //{
        //    double d_Max = Math.Max(a,
        //                Math.Max(b,
        //                Math.Max(c,
        //                Math.Max(d,
        //                Math.Max(e,
        //                Math.Max(f,
        //                Math.Max(g,
        //                Math.Max(h, i))))))));
        //    return d_Max;
        //}
        //double Max_10(
        //double a,
        //double b,
        //double c,
        //double d,
        //double e,
        //double f,
        //double g,
        //double h,
        //double i,
        //double j)
        //{
        //    double d_Max = Math.Max(a,
        //                Math.Max(b,
        //                Math.Max(c,
        //                Math.Max(d,
        //                Math.Max(e,
        //                Math.Max(f,
        //                Math.Max(g,
        //                Math.Max(h,
        //                Math.Max(i, j)))))))));
        //    return d_Max;
        //}
        #endregion






    }
}

