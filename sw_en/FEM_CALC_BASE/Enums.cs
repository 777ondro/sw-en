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
            eEl_0_0 = 0, // Start Node - restrained DOF,                                                    End Node - restrained DOF
            eEl_0__ = 1, // Start Node - restrained DOF,                                                    End Node - free DOF
            eEl___0 = 2, // Start Node - free DOF,                                                          End Node - restrained DOF
            eEl____ = 3  // Start Node - free DOF,                                                          End Node - free DOF
        }
    }
}
