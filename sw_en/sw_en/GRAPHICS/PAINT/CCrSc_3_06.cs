using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CENEX
{
    public class CCrSc_3_06 : CCrSc_0_23
    {
      public CCrSc_3_06(float fa, float fb, float ft)
      {
          INoPoints = 72; // vykreslujeme ako n-uholnik, pocet bodov n
          Fa = fa;
          Fb = fb;
          Ft = ft;

          FAngle = 90f;

          // Radii
          Fr_out_major = Fa / 2f;
          Fr_in_major = Fa / 2f - Ft;

          Fr_out_minor = Fb / 2f;
          Fr_in_minor = Fb / 2f - Ft;

          if (Fr_in_major == Fr_out_major || Fr_in_minor == Fr_out_minor)
              return;

          // Create Array - allocate memory
          m_CrScPointOut = new float[INoPoints, 2];
          m_CrScPointIn = new float[INoPoints, 2];

          // Fill Array Data
          CalcCrSc_Coord();
      }
    }
}
