using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses.GraphObj
{
    public class CVolume : CEntity
    {
        //public int[] m_iPointsCollection; // List / Collection of points IDs
        public int[] m_iPointsCollection; // List / Collection of points IDs

        // Constructor 1
        public CVolume()
        {
        }

        // Constructor 2
        public CVolume(int iVolume_ID, int[] iPCollection, int fTime)
        {
            ID = iVolume_ID;
            m_iPointsCollection = iPCollection;
            FTime = fTime;
        }

    }
}
