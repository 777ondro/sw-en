using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M_EC8.SOLV.CS.EQ
{
    class CL_4
    {
        public float Eq_412_____(float fx, float fL_e)
        {
            if (fL_e > 0f)
                return 1f + 0.6f * (fx / fL_e); // Eq. (4.12) delta
            else
                return 0f;
        }












    }
}
