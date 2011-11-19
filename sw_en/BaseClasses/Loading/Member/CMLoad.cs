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
        //private float m_Value;
        private EMLoadType1 m_mLoadType;
        private CVector m_V_EIF_MembStart = new CVector(3); // Vector or Member end forces at start node in LCS , Define size acc. to main settings 2D - 3 or 3D  - 6 items
        private CVector m_V_EIF_MembEnd = new CVector(3);   // Vector or Member end forces at end node in LCS , Define size acc. to main settings 2D - 3 or 3D  - 6 items
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

        public CVector V_EIF_MembStart
        {
            get { return m_V_EIF_MembStart; }
            set { m_V_EIF_MembStart = value; }
        }
        public CVector V_EIF_MembEnd
        {
            get { return m_V_EIF_MembEnd; }
            set { m_V_EIF_MembEnd = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------


        public CMLoad()
        {
            // m_V_EIF_MembStart = new CVector(6); // Vector size depending on DOF of node in D or 3D environment
            // m_V_EIF_MembEnd = new CVector(6);
            Fill_Load_Init(); // Initial values
        }

        void Fill_Load_Init()
        {
            // Zero load initial values
            for (int i = 0; i < V_EIF_MembStart.FVectorItems.Length; i++)
            {
                m_V_EIF_MembStart.FVectorItems[i] = 0f;
                m_V_EIF_MembEnd.FVectorItems[i] = 0f;
            }
        }
    }
}
