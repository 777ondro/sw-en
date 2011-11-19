using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_BASE
{
    public class Enums
    {



        // Element SupportType in 3D
        public enum EElemSuppType
        {
            e3DEl_000000_000000 = 0, // Start Node - restrained DOF,                                                    End Node - restrained DOF
            e3DEl_000000_______ = 1, // Start Node - restrained DOF,                                                    End Node - free DOF
            e3DEl________000000 = 2, // Start Node - free DOF,                                                          End Node - restrained DOF
            e3DEl_000000_000___ = 3, // Start Node - restrained DOF,                                                    End Node - rotation hinge
            e3DEl_000____000000 = 4, // Start Node - rotation hinge,                                                    End Node - restrained DOF
            e3DEl_000000_0_00_0 = 5, // Start Node - restrained DOF,                                                    End Node - free displacement in y-Axis nad rotation about y-Axis
            e3DEl_0_00_0_000000 = 6, // Start Node - free displacement in y-Axis nad rotation about y-Axis,             End Node - restrained DOF
            e3DEl_000____000___ = 7  // Start Node - rotation hinge,                                                    End Node - rotation hinge
        }
    }
}
