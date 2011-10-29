using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CENEX
{
	[Serializable]
    public class CCrSc
    {
        private int m_iCrSc_ID;

        public int ICrSc_ID
        {
            get { return m_iCrSc_ID; }
            set { m_iCrSc_ID = value; }
        }

        public CMat_00 m_Mat = new CMat_00();
        // Predok pre jednotlive tvary prierezov

        // Constructor 1
        public CCrSc()
        { 
        
        
        
        
        }
        // Constructor 2
        public CCrSc(ref CMat_00 objMat)
        {
            m_Mat = objMat; // !!! Nevytvarat lokalne kopie !!!
        }
    }
}
