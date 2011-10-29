using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
        [Serializable]
    public class CLoadCombination
    {
        //----------------------------------------------------------------------------
        private int m_iLoadComb_ID;

        public int ILoadComb_ID
        {
            get { return m_iLoadComb_ID; }
            set { m_iLoadComb_ID = value; }
        }
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CLoadCombination()
        { 
        
        }
    }
}
