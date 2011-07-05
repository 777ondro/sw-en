using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FEM_CALC_1D;
using System.Collections;

namespace CENEX
{
    // Class CNSupport
    public class CNSupport
    {
        public int   m_iSupport_ID;
        public CNode m_iNode = new CNode();
        public bool  [] m_bRestrain;
        public int   m_fTime;
 


                       

        // Konstruktor CNSupport
        public CNSupport(
              int iSupport_ID, 
              CNode iNode,
              bool  [] bRestrain,
              int fTime
            )
        {
            m_iSupport_ID = iSupport_ID;
            m_iNode = iNode;
            m_bRestrain = bRestrain;
            m_fTime = fTime;
        }
    } // End of Class CNSupport

    // Objekt, ktery porovnava Supports podle ID
    public class CCompare_SupportID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CNSupport)x).m_iSupport_ID - ((CNSupport)y).m_iSupport_ID;
        }
    }
}
