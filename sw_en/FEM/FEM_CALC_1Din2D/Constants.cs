using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_1Din2D
{
    public class Constants
    {
        // Number of Node Degress of freedom in 2D (DOF)
        public int iNodeDOFNo = 3;

        //  DOF Array Constants

        public int UX = 0;
        public int UY = 1;
        public int RZ = 2;

        //  Nodal Load Array Constants

        public int FX = 0;
        public int FY = 1;
        public int MZ = 2;


        // Internal Forces

        public int N = 0;
        public int V = 1;
        public int M = 2;

       public Constants() {}
    }
}
