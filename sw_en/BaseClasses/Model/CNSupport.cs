using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    [Serializable]
    public class CNSupport : CEntity
    {
        //----------------------------------------------------------------------------
        public int[] m_iNodeCollection; // List / Collection of nodes IDs where support is defined [First member index is 0]
        private CNode m_Node;
        private ENSupportType m_NSupportType;

        // Restraints - list of node degreess of freedom
        // false - 0 - free DOF
        // true - 1 - restrained (rigid)

        public int m_eNDOF;
        public bool[] m_bRestrain; // Array of boolean values, UX, UY, UZ, RX, RY, RZ

        //----------------------------------------------------------------------------
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
        public CNSupport(int eNDOF)
        {
            m_bRestrain = new bool[(int)eNDOF];
        }

        public CNSupport(int eNDOF, int iSupport_ID,CNode Node, bool[] bRestrain, int fTime)
        {
            m_eNDOF = eNDOF;
            m_bRestrain = new bool[(int)eNDOF];
            ID = iSupport_ID;
            m_Node = Node;
            m_bRestrain = bRestrain;
            FTime = fTime;
        }
    } // End of CNSupport class

    public class CCompare_NSupportID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CNSupport)x).ID - ((CNSupport)y).ID;
        }
    }
}
