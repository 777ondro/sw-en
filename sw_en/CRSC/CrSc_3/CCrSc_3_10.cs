using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CRSC
{
    public class CCrSc_3_10 : CCrSc_0_05
    {
      // Solid rectangle

      public CCrSc_3_10(float fh, float fb, float ft)
      {
          ITotNoPoints = 4;
          Fh = fh;
          Fb = fb;

          // Create Array - allocate memory
          m_CrScPoint = new float[ITotNoPoints, 2];
          // Fill Array Data
          CalcCrSc_Coord();
      }
    }
}
