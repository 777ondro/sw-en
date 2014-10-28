using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;

namespace BaseClasses.GraphObj
{
    [Serializable]
    public class CStructure_Window : CEntity3D
    {
        public CPoint m_pControlPoint = new CPoint();

        EWindowShapeType m_eShapeType = EWindowShapeType.eClassic;

        public float m_fvolOpacity;
        public Color m_volColor_1 = new Color(); // Default
        public Color m_volColor_2 = new Color();

        public DiffuseMaterial m_Material_1 = new DiffuseMaterial();
        public DiffuseMaterial m_Material_2 = new DiffuseMaterial();

        public float m_fDim1;
        public float m_fDim2;
        public float m_fDim3;

        // Constructor 1
        public CStructure_Window()
        {
        }

        // Constructor 2
        public CStructure_Window(int i_ID, int[] iPCollection, int fTime)
        {
            ID = i_ID;
            FTime = fTime;
        }

        // Constructor 4

        public CStructure_Window(int iVolume_ID, EWindowShapeType iShapeType, CPoint pControlEdgePoint, float fX, float fY, float fZ, DiffuseMaterial volMat1, DiffuseMaterial volMat2, bool bIsDisplayed, float fTime)
        {
            ID = iVolume_ID;
            m_eShapeType = iShapeType;
            m_pControlPoint = pControlEdgePoint;
            m_fDim1 = fX;
            m_fDim2 = fY;
            m_fDim3 = fZ;
            m_Material_1 = volMat1;
            m_volColor_2 = volMat1.Color;
            m_fvolOpacity = 1.0f;
            m_Material_2 = volMat2;
            m_volColor_2 = volMat2.Color;
            BIsDisplayed = bIsDisplayed;
            FTime = fTime;
        }

        // Constructor 5
        // Rectangular Prism 8 Edges
        public CStructure_Window(int iVolume_ID, EWindowShapeType iShapeType, CPoint pControlEdgePoint, float fX, float fY, float fZ, DiffuseMaterial volMat1, bool bIsDisplayed, float fTime)
        {
            ID = iVolume_ID;
            m_eShapeType = iShapeType;
            m_pControlPoint = pControlEdgePoint;
            m_fDim1 = fX;
            m_fDim2 = fY;
            m_fDim3 = fZ;
            m_Material_1 = volMat1;
            m_volColor_2 = volMat1.Color;
            m_fvolOpacity = 1.0f;
            // Set same properties for both materials
            m_Material_2 = volMat1;
            m_volColor_2 = volMat1.Color;
            BIsDisplayed = bIsDisplayed;
            FTime = fTime;
        }
    }
}
