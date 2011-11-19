using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
        [Serializable]
    public class CMLoad : CEntity
    {
        //----------------------------------------------------------------------------
        private int m_iMLoad_ID;
        private CMember m_Member;
        public int[] m_iMemberCollection; // List / Collection of members where load is defined
        //private float m_Value;
        private EMLoadType1 m_mLoadType;

        //----------------------------------------------------------------------------
        public int IMLoad_ID
        {
            get { return m_iMLoad_ID; }
            set { m_iMLoad_ID = value; }
        }
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
