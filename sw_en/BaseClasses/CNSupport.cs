using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    class CNSupport
    {
        //----------------------------------------------------------------------------
        private CNode m_Node;
        private float m_Value;
        private ENSupportType m_NSupportType;

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
    }
}
