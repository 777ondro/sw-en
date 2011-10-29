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
        public int [] m_iMembCollection; // List / Collection of members where release is defined
        public bool m_nRelease1;  // true - release in start point of member, false - release in end point
        public bool[] m_bRestrain; // DOF
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

        public CNRelease(bool[] bRestrain, int fTime)
        {
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }

        public CNRelease(CNode Node,bool[] bRestrain,int fTime)
        {
            m_Node = Node;
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }

        public CNRelease(CNode Node, CMember Member, bool[] bRestrain, int fTime)
        {
            m_Node = Node;
            m_Member = Member;
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }

        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        
        
    }
}
