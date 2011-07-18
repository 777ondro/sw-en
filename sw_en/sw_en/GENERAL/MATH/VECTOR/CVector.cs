using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_1D
{
    class CVector
    {

        //// GENERAL VECTOR OPERATIONS

        // Return sum of vectors / 1D arrays
        public float[] GetVectorSum(int iV_jRowsMax, params float[]fV)
        {
             float[] fVa = new float[iV_jRowsMax];

            // Output Vector
            for(int i = 0 ; i < fV.Length; i++)  // for each vector
            {
                for (int j = 0; j < iV_jRowsMax; j++)  // for each vector member
                    {
                    // j-vector of i-vector 
                    //   fV[i];
                    }
            }
            return fVa;
        }
    }
}
