using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_13 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fM; // Moment Value
        private float m_fa;      // Distance from Member Start
        //----------------------------------------------------------------------------
        public float FM
        {
            get { return m_fM; }
            set { m_fM = value; }
        }
        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_13(float fM, float fa)
        {
            FM = fM;
            Fa = fa;
        }
    }
}
