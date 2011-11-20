using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_BASE
{
    public class Enums
    {
        // Element SupportType
        public enum EElemSuppType
        {
            eEl_00_00 = 0, // Start Node - restrained DOF,                                                    End Node - restrained DOF
            eEl_00_0_ = 1, // Start Node - restrained DOF,                                                    End Node - free rotation DOF
            eEl_0__00 = 2, // Start Node - free rotation DOF,                                                 End Node - restrained DOF
            eEl_0__0_ = 3, // Start Node - free rotation DOF,                                                 End Node - free rotation DOF
            eEl_00___ = 4, // Start Node - restrained DOF,                                                    End Node - restrained DOF
            eEl____00 = 5, // Start Node - free DOF,                                                          End Node - free DOF

            eEl_00__0 = 6, // Start Node - restrained DOF,                                                    End Node - free displacement DOF
            eEl__0_00 = 7, // Start Node - free displacement DOF,                                             End Node - restrained DOF
            eEl__0__0 = 8, // Start Node - free displacement DOF,                                             End Node - free displacement DOF

            eEl_0____ = 9, // Start Node - free rotation DOF,                                                 End Node - free DOF
            eEl____0_ = 10, // Start Node - free DOF,                                                         End Node - free rotation DOF

            eEl______ = 99  // Start Node - free DOF,                                                         End Node - free DOF
        }    }
}
