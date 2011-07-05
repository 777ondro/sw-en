using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CNRelease
    {
        //---------------------------------------------------------------------------------
        private CNode m_Node;
        private CMember m_Member;

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
            m_Member = null;
        }

        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        
        
    }
}
