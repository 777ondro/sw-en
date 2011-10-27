using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CENEX
{
    public class CCrSc_3_05 : CCrSc_0_22
    {
      public CCrSc_3_05(float fd, float ft)
      {
          INoPoints = 72; // vykreslujeme ako n-uholnik, pocet bodov n
          Fd = fd;
          Ft = ft;

          Fd_in = Fd - 2 * Ft;

          // Radii
          Fr_out = Fd / 2f;
          Fr_in = Fd_in / 2f;

          if (Fr_in == Fr_out)
              return;

          // Create Array - allocate memory
          m_CrScPointOut = new float[INoPoints, 2];
          m_CrScPointIn = new float[INoPoints, 2];

          // Fill Array Data
          CalcCrSc_Coord();
      }
    }
}
