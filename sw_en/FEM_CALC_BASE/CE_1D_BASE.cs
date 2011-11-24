using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using CRSC;
using MATH;
using CENEX;

namespace FEM_CALC_BASE
{
    public class CE_1D_BASE : CE
    {
        private CMember m_Member = new CMember(); // Reference to topological member

        public CMember Member
        {
            get { return m_Member; }
            set { m_Member = value; }
        }

        private float m_fLength; // FEM 1D element length

        public float FLength
        {
            get { return m_fLength; }
            set { m_fLength = value; }
        }

        private CN m_NodeStart = new CN();

        public CN NodeStart
        {
            get { return m_NodeStart; }
            set { m_NodeStart = value; }
        }
        private CN m_NodeEnd = new CN();

        public CN NodeEnd
        {
            get { return m_NodeEnd; }
            set { m_NodeEnd = value; }
        }
        public CCrSc_0_00 CrSc = new CCrSc_0_00 (); // FEM 1D element object of cross-section properties

        private CNRelease m_cnRelease1;

        public CNRelease CnRelease1
        {
            get { return m_cnRelease1; }
            set { m_cnRelease1 = value; }
        }
        private CNRelease m_cnRelease2;

        public CNRelease CnRelease2
        {
            get { return m_cnRelease2; }
            set { m_cnRelease2 = value; }
        }

        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        public CE_1D_BASE()
        { }

        public CE_1D_BASE(CN NStart, CN NEnd)
        {
            m_NodeStart = NStart;
            m_NodeEnd = NEnd;
        }

    }
}
