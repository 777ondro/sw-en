﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses.GraphObj
{
    public class CLine : CEntity
    {
        public int[] m_iPointsCollection; // List / Collection of points IDs

        public CLine()
        {
        
        }

        // Constructor 2
        public CLine(int iLine_ID, int[] iPCollection, int fTime)
        {
            ID = iLine_ID;
            m_iPointsCollection = iPCollection;
            FTime = fTime;
        }
    }
}
