using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using CRSC;

namespace BaseClasses
{
    [Serializable]
    public class CMember:CEntity
    {
        private int      m_iMember_ID, m_fTime;

        public int IMember_ID
        {
          get { return m_iMember_ID; }
          set { m_iMember_ID = value; }
        }
        private CNode    m_NodeStart;

        public CNode NodeStart
        {
          get { return m_NodeStart; }
          set { m_NodeStart = value; }
        }
        private CNode    m_NodeEnd;

        public CNode NodeEnd
        {
          get { return m_NodeEnd; }
          set { m_NodeEnd = value; }
        }

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

        private CCrSc m_CrSc;

        public CCrSc CrSc
        {
            get { return m_CrSc; }
            set { m_CrSc = value; }
        }

        private float m_fLength;

        public float FLength
        {
          get { return m_fLength; }
          set { m_fLength = value; }
        }

        public double m_dTheta_x;

        public double DTheta_x
        {
            get { return m_dTheta_x; }
            set { m_dTheta_x = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------

        // Constructor 1
        public CMember()
        {
            m_NodeStart = new CNode();
            m_NodeEnd = new CNode();
            m_cnRelease1 = null;
            m_cnRelease2 = null;
        }
        // Constructor 2
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            int fTime)
        {
            m_iMember_ID = iLine_ID;
            m_NodeStart = iNode1;
            m_NodeEnd = iNode2;
            m_cnRelease1 = null;
            m_cnRelease2 = null;
            m_fTime = fTime;

            Fill_Basic();
        }

        // Constructor 3
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            bool haveRelease1,
            bool haveRelease2,
            int fTime)
        {
            m_iMember_ID = iLine_ID;
            m_NodeStart = iNode1;
            m_NodeEnd = iNode2;
            if (haveRelease1)
                m_cnRelease1 = new CNRelease(iNode1);
            if (haveRelease2)
                m_cnRelease2 = new CNRelease(iNode2);
            m_fTime = fTime;

            Fill_Basic();
        }
        // Constructor 4
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            CCrSc objCrSc,
            int fTime
            )
        {
            m_iMember_ID = iLine_ID;
            m_NodeStart = iNode1;
            m_NodeEnd = iNode2;
            m_cnRelease1 = null;
            m_cnRelease2 = null;
            m_CrSc = objCrSc;
            m_fTime = fTime;

            Fill_Basic();
        }


        //Fill basic data
        public void Fill_Basic()
        {
            // Temporary !!!!!!!!!!!!!!!!!!!!!! Member Length for 3F 
            FLength = (float)Math.Sqrt((float)Math.Pow(m_NodeEnd.FCoord_X - m_NodeStart.FCoord_X, 2f) + (float)Math.Pow(m_NodeEnd.FCoord_Y - m_NodeStart.FCoord_Y, 2f) + (float)Math.Pow(m_NodeEnd.FCoord_Z - m_NodeStart.FCoord_Z, 2f));

        }

    } // End of Class CMember


    public class CCompare_MemberID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CMember)x).IMember_ID - ((CMember)y).IMember_ID;
        }
    }
}
