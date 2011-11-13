using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    [Serializable]
    public class CNSupport
    {
        //----------------------------------------------------------------------------
        private int m_iSupport_ID;
        public int[] m_iNodeCollection; // List / Collection of nodes where support is defined
        private CNode m_Node;
        private ENSupportType m_NSupportType;

        // Restraints - list of node degreess of freedom
        // false - 0 - free DOF
        // true - 1 - restrained (rigid)

        public bool[] m_bRestrain = new bool [6]; // Array of boolean values, UX, UY, UZ, RX, RY, RZ
        public int m_fTime;

        //----------------------------------------------------------------------------
        public int ISupport_ID
        {
            get { return m_iSupport_ID; }
            set { m_iSupport_ID = value; }
        }
        public CNode Node
        {
            get { return m_Node; }
            set { m_Node = value; }
        }
        public ENSupportType NSupportType
        {
            get { return m_NSupportType; }
            set { m_NSupportType = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CNSupport()
        {

        }

        public CNSupport(int iSupport_ID,CNode Node, bool[] bRestrain, int fTime)
        {
            m_iSupport_ID = iSupport_ID;
            m_Node = Node;
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }
    } // End of CNSupport class

    public class CCompare_NSupportID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CNSupport)x).ISupport_ID - ((CNSupport)y).ISupport_ID;
        }
    }
}
