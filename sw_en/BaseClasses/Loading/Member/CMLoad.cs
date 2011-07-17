using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad
    {
        //----------------------------------------------------------------------------
        private CMember m_Member;
        //private float m_Value;
        private EMLoadType1 m_mLoadType;

        //----------------------------------------------------------------------------
        public CMember Member
        {
          get { return m_Member; }
          set { m_Member = value; }
        }
        /*
        public float Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
        */
        public EMLoadType1 MLoadType
        {
            get { return m_mLoadType; }
            set { m_mLoadType = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad()
        {

        
        }
    }
}
