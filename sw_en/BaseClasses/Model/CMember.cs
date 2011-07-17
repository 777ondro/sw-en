using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMember
    {
        private int      m_iLine_ID, m_fTime;

        public int ILine_ID
        {
          get { return m_iLine_ID; }
          set { m_iLine_ID = value; }
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

        // Konstruktor 1 CLine 
        public CMember()
        {
            m_iNode1 = new CNode();
            m_iNode2 = new CNode();
            m_cnRelease1 = null;
            m_cnRelease1 = null;
        }
        // Konstruktor 2 CLine
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            int fTime)
        {
            m_iLine_ID = iLine_ID;
            m_iNode1 = iNode1;
            m_iNode2 = iNode2;
            m_fTime = fTime;
        }

        // Konstruktor 3 CLine
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            bool haveRelease1,
            bool haveRelease2,
            int fTime)
        {
            m_iLine_ID = iLine_ID;
            m_iNode1 = iNode1;
            m_iNode2 = iNode2;
            if(haveRelease1)
                m_cnRelease1 = new CNRelease(iNode1);
            if(haveRelease2)
                m_cnRelease2 = new CNRelease(iNode2);
            m_fTime = fTime;
        }
    } // End of Class CMember
}
