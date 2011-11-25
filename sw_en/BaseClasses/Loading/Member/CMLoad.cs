using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace BaseClasses
{
        [Serializable]
    public class CMLoad : CEntity
    {
        //----------------------------------------------------------------------------
        private int m_iMLoad_ID;
        private CMember m_Member;
        private int[] m_iMemberCollection; // List / Collection of members where load is defined
        private EMLoadType1 m_mLoadType; // Type of external force
        private EMLoadDirPCC1 m_eDirPPC; // External Force Direction in Principal Coordinate System of Member
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
        public int[] IMemberCollection
        {
            get { return m_iMemberCollection; }
            set { m_iMemberCollection = value; }
        }
        public EMLoadType1 MLoadType
        {
            get { return m_mLoadType; }
            set { m_mLoadType = value; }
        }

        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------


        public CMLoad()
        {

        }


    }
}
