using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_1Din2D
{
    //  DOF Vector Enum
    public enum e2D_DOF
    {
        eUX = 0,
        eUY = 1,
        eRZ = 2
    }

    //  Nodal Load Array Constants
    public enum e2D_E_F
    {
        eFX = 0,
        eFY = 1,
        eMZ = 2
    }
    
    // Internal Forces Enum
    public enum e2D_I_F
    {
      eN = 0,
      eV = 1,
      eM = 2
    }
}
