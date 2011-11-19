using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    [Serializable]
    public class CNRelease
    {
        //---------------------------------------------------------------------------------
        private CNode m_Node;
        private CMember m_Member;
        public int [] m_iMembCollection; // List / Collection of members IDs where release is defined
        public int [] m_iNodeCollection; // List / Collection of nodes IDs where release is defined (need for local stiffeness matrix)
        public bool m_nRelease1;  // true - release in start point of member, false - release in end point
        public int m_eNDOF;
        public bool[] m_bRestrain; // DOF is rigid - 1, DOF is free - 0
        public int m_fTime;

        //---------------------------------------------------------------------------------
        public CNode Node
        {
            get { return m_Node; }
            set { m_Node = value; }
        }
        public CMember Member
        {
            get { return m_Member; }
            set { m_Member = value; }
        }

        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        public CNRelease(CNode node, CMember member)
        {
            m_Node = node;
            m_Member = member;
        }

        public CNRelease(CNode node)
        {
            m_Node = node;
        }

        public CNRelease(int eNDOF, bool[] bRestrain, int fTime)
        {
            m_eNDOF = eNDOF;
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }

        public CNRelease(int eNDOF, CNode Node, bool[] bRestrain, int fTime)
        {
            m_eNDOF = eNDOF;
            m_Node = Node;
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }

        public CNRelease(int eNDOF, CNode Node, CMember Member, bool[] bRestrain, int fTime)
        {
            m_eNDOF = eNDOF;
            m_Node = Node;
            m_Member = Member;
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }

        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        
        
    }
}
