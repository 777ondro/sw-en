using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using CRSC;

namespace BaseClasses
{
    [Serializable]
    public class CMember
    {
        private int      m_iMember_ID, m_fTime;

        public int IMember_ID
        {
          get { return m_iMember_ID; }
          set { m_iMember_ID = value; }
        }
        private CNode    m_iNode1;

        public CNode INode1
        {
          get { return m_iNode1; }
          set { m_iNode1 = value; }
        }
        private CNode    m_iNode2;

        public CNode INode2
        {
          get { return m_iNode2; }
          set { m_iNode2 = value; }
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

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------

        // Constructor 1
        public CMember()
        {
            m_iNode1 = new CNode();
            m_iNode2 = new CNode();
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
            m_iNode1 = iNode1;
            m_iNode2 = iNode2;
            m_cnRelease1 = null;
            m_cnRelease2 = null;
            m_fTime = fTime;
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
            m_iNode1 = iNode1;
            m_iNode2 = iNode2;
            if (haveRelease1)
                m_cnRelease1 = new CNRelease(iNode1);
            if (haveRelease2)
                m_cnRelease2 = new CNRelease(iNode2);
            m_fTime = fTime;
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
            m_iNode1 = iNode1;
            m_iNode2 = iNode2;
            m_cnRelease1 = null;
            m_cnRelease2 = null;
            m_CrSc = objCrSc;
            m_fTime = fTime;
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
