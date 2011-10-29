using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BaseClasses
{
    // Class CNode
    [Serializable]
    public class CNode
    {
        private int m_iNode_ID;
        private float m_fCoord_X;
        private float m_fCoord_Y;
        private float m_fCoord_Z;
        private int m_fTime;


        public int INode_ID
        {
            get { return m_iNode_ID; }
            set { m_iNode_ID = value; }
        }
        
        public float FCoord_X
        {
            get { return m_fCoord_X; }
            set { m_fCoord_X = value; }
        }

        public float FCoord_Y
        {
            get { return m_fCoord_Y; }
            set { m_fCoord_Y = value; }
        }

        public float FCoord_Z
        {
            get { return m_fCoord_Z; }
            set { m_fCoord_Z = value; }
        }
        
        public int FTime
        {
            get { return m_fTime; }
            set { m_fTime = value; }
        }

        // Konstruktor1 CNode
        public CNode()
        {

        }
        // Konstruktor2 CNode
        public CNode(
            int iNode_ID,
            int fCoord_X,
            int fCoord_Y,
            int fCoord_Z,
            int fTime
            )
        {
            m_iNode_ID = iNode_ID;
            m_fCoord_X = fCoord_X;
            m_fCoord_Y = fCoord_Y;
            m_fCoord_Z = fCoord_Z;
            m_fTime = fTime;
        }


        #region IComparer Members

        public int Compare(object x, object y)
        {
            return ((CNode)x).INode_ID - ((CNode)y).INode_ID;
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.m_iNode_ID - ((CNode)obj).m_iNode_ID;
        }

        #endregion
    } // End of Class CNode
    public class CCompare_NodeID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CNode)x).INode_ID - ((CNode)y).INode_ID;
        }
    }
}
