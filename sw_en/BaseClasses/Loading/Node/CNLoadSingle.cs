using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;

namespace BaseClasses
{
    [Serializable]
    public class CNLoadSingle : CNLoad
    {
        //----------------------------------------------------------------------------
        private float m_Value;
        private ENLoadType m_nLoadType;

        //----------------------------------------------------------------------------
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
        public CNLoadSingle()
        {

        }
        public CNLoadSingle(CNode nNode,
              ENLoadType nLoadType,
              float fValue,
              int fTime)
        {
            Node = nNode;
            NLoadType = nLoadType;
            Value = fValue;
            FTime = fTime;
        }

    }
}
