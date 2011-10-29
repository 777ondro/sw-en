using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CNLoadAll:CNLoad
    {
        //----------------------------------------------------------------------------
        private CNode m_Node;
        public int[] m_iNodeCollection; // List / Collection of nodes where support is defined
        private float m_Value_FX;
        private float m_Value_FY;
        private float m_Value_FZ;
        private float m_Value_MX;
        private float m_Value_MY;
        private float m_Value_MZ;
        public int m_fTime;

        //----------------------------------------------------------------------------
        public CNode Node
        {
            get { return m_Node; }
            set { m_Node = value; }
        }
        public float Value_FX
        {
            get { return m_Value_FX; }
            set { m_Value_FX = value; }
        }
        public float Value_FY
        {
            get { return m_Value_FY; }
            set { m_Value_FY = value; }
        }
        public float Value_FZ
        {
            get { return m_Value_FZ; }
            set { m_Value_FZ = value; }
        }
        public float Value_MX
        {
            get { return m_Value_MX; }
            set { m_Value_MX = value; }
        }
        public float Value_MY
        {
            get { return m_Value_MY; }
            set { m_Value_MY = value; }
        }
        public float Value_MZ
        {
            get { return m_Value_MZ; }
            set { m_Value_MZ = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CNLoadAll()
        {

        }
        public CNLoadAll(CNode Node,
              float fValue_FX,
              float fValue_FY,
              float fValue_FZ,
              float fValue_MX,
              float fValue_MY,
              float fValue_MZ,
              int fTime
            )
        {
            m_Node = Node;
            m_Value_FX = fValue_FX;
            m_Value_FY = fValue_FY;
            m_Value_FZ = fValue_FZ;
            m_Value_MX = fValue_MX;
            m_Value_MY = fValue_MY;
            m_Value_MZ = fValue_MZ;
            m_fTime = fTime;
        }
    }
}
