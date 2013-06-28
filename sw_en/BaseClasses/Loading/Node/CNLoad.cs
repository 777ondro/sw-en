using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;

namespace BaseClasses
{
    [Serializable]
    public class CNLoad : CEntity
    {
        //----------------------------------------------------------------------------
        private int m_iNLoad_ID;
        private CNode m_Node;
        private int[] m_iNodeCollection; // List / Collection of nodes IDs where load is defined
        private float m_Value;
        private ENLoadType m_nLoadType;
        public int m_fTime;

        //----------------------------------------------------------------------------
        public int INLoad_ID
        {
            get { return m_iNLoad_ID; }
            set { m_iNLoad_ID = value; }
        }
        public CNode Node
        {
            get { return m_Node; }
            set { m_Node = value; }
        }
        public int[] INodeCollection
        {
            get { return m_iNodeCollection; }
            set { m_iNodeCollection = value; }
        }
        public float Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
        public ENLoadType NLoadType
        {
            get { return m_nLoadType; }
            set { m_nLoadType = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CNLoad()
        {

        }
        public CNLoad(CNode Node,
              ENLoadType nLoadType,
              float fValue,
              int fTime)
        {
            m_Node = Node;
            m_nLoadType = nLoadType;
            m_Value = fValue;
            m_fTime = fTime;
        }

    }
}
