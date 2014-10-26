using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace BaseClasses.GraphObj
{
    public class CVolume : CEntity
    {
        //public int[] m_iPointsCollection; // List / Collection of points IDs
        public int[] m_iPointsCollection; // List / Collection of points IDs

        public EVolumeShapeType m_eShapeType;

        public CPoint m_pControlPoint = new CPoint();

        public float m_fvolOpacity;
        public Color m_volColor = new Color(); // Default

        public float m_fDim1;
        public float m_fDim2;
        public float m_fDim3;
        public float m_fDim4;
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

        // Constructor 3
        // Rectangular Prism 8 Edges
        public CVolume(int iVolume_ID, EVolumeShapeType iShapeType, CPoint pControlEdgePoint, float fX, float fY, float fZ, Color volColor, float fvolOpacity, bool bIsDisplayed, float fTime)
        {
            ID = iVolume_ID;
            m_eShapeType = iShapeType;
            m_pControlPoint = pControlEdgePoint;
            m_fDim1 = fX;
            m_fDim2 = fY;
            m_fDim3 = fZ;
            m_volColor = volColor;
            m_fvolOpacity = fvolOpacity;
            BIsDisplayed = bIsDisplayed;
            FTime = fTime;

        }

        // Constructor 3
        // Sphere
        public CVolume(int iVolume_ID, EVolumeShapeType iShapeType, CPoint pControlCenterPoint, float fr, bool bIsDisplayed, float fTime)
        {
            ID = iVolume_ID;
            m_eShapeType = iShapeType;
            m_pControlPoint = pControlCenterPoint;
            m_fDim1 = fr;
            BIsDisplayed = bIsDisplayed;
            FTime = fTime;
        }

    }
}
