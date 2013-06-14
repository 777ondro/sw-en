﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CRSC
{
    public class CCrSc_5_01 : CCrSc_0_05
    {
        // Solid rectangle
      public CCrSc_5_01(float fh, float fb, float ft)
      {
          //ITotNoPoints = 4;
          Fh = fh;
          Fb = fb;

          // Create Array - allocate memory
          CrScPointsOut = new float[ITotNoPoints, 2];
          // Fill Array Data
          CalcCrSc_Coord();
      }
    }
}
