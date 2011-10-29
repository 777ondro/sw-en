using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CNLoad
    {
        //----------------------------------------------------------------------------
        private CNode m_Node;
        private float m_Value;
        private ENLoadType m_nLoadType;
        public int m_fTime;

        //----------------------------------------------------------------------------
        public CNode Node
        {
            get { return m_Node; }
            set { m_Node = value; }
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
