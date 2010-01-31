using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CENEX.GENERAL.MATH
{
    //trieda je verejna staticka co znamena ze objekt tejto triedy sa nevytvara,ale pouziva sa podobne ako trieda Math
    public static class MathF
    {

        //tymto metodam by bolo potrebne najprv inicializovat pole a potom im ho odovzdat ako argument

        //public static double  Min(double[] data)
        //{
        //    double min;
        //    min = data[0];
        //    foreach (double num in data)
        //        if (num < min) min = num;
        //    return min;
        //}
        //public static double Max(double[] data)
        //{
        //    double max;
        //    max = data[0];
        //    foreach (double num in data)
        //        if (num > max) max = num;
        //    return max;
        //}


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

