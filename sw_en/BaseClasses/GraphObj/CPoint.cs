using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses.GraphObj
{
    public class CPoint : CEntity
    {
        private float m_fCoord_X;
        private float m_fCoord_Y;
        private float m_fCoord_Z;

        public float FCoord_X
        {
            get { return m_fCoord_X; }
            set { m_fCoord_X = value; }
        }

        public float FCoord_Y
        {
            get { return m_fCoord_Y; }
            set { m_fCoord_Y = value; }
        }

        public float FCoord_Z
        {
            get { return m_fCoord_Z; }
            set { m_fCoord_Z = value; }
        }

        public CPoint()
        {
        
        }
    }
}
