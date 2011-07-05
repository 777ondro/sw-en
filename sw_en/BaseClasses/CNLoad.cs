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

    }
}
