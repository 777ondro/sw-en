using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATERIAL;
using CENEX;

namespace CRSC
{
    [Serializable]
    public class CCrSc
    {

        public CMat_00 m_Mat = new CMat_00();

        // Constructor 1
        public CCrSc()
        { 
        
        }
        // Constructor 2
        public CCrSc(CMat_00 objMat)
        {
            m_Mat = objMat; // !!! Nevytvarat lokalne kopie !!!
        }

    }
}
