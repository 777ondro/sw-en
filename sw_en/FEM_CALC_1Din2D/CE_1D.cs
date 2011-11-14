using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;
using CENEX;

namespace FEM_CALC_1Din2D
{
    public class CE_1D : CMember
    {
        static int iNodeDOFNo = 3;

        Constants c = new Constants();

        public int m_iSuppType;
        // Geometrical properties of Element
        public float m_flength_X,
            m_flength_Y,
            m_frotation_angle = 0f;

        public float m_flength;

        public float[] m_fC_GCS_Coord = new float[3]; // Relative coordinate AC of auxiliary point C which define local member z-Axis orientation in global coordinate system of model

        // Vector of member displacement
        CMatrix m_Disp = new CMatrix(iNodeDOFNo, 1);
        // Array of global codes numbers of element desgress of freedom (Start node 0-5, and node 6-11)
        public int[] m_ArrCodeNo = new int[2*iNodeDOFNo]; 
        
        // Primary End Forces Vectors
        // Vector of member nodes (ends) primary forces in Local Coordinate System (LCS) due to the transverse member load
        public float[] m_ArrElemPEF_LCS_StNode = new float[iNodeDOFNo];  // Start Node
        public float[] m_ArrElemPEF_LCS_EnNode = new float[iNodeDOFNo];  // End Node

        // Vector of member ends primary forces in Local Coordinate System (LCS) due to the transverse member load
        public float[,] m_ArrElemPEF_LCS = new float[2, iNodeDOFNo];

        // Vector of member nodes (ends) primary forces in global Coordinate System due to the transverse member load
        public float[] m_ArrElemPEF_GCS_StNode = new float[iNodeDOFNo];  // Start Node
        public float[] m_ArrElemPEF_GCS_EnNode = new float[iNodeDOFNo];  // End Node

        // Vector of member ends primary forces in Global Coordinate System (GCS) due to the transverse member load
        public float[,] m_ArrElemPEF_GCS = new float[2, iNodeDOFNo];

        // Results End Forces Vectors
        // Vector of member nodes (ends) forces in GCS
        public float[] m_ArrElemEF_GCS_StNode = new float[iNodeDOFNo];  // Start Node
        public float[] m_ArrElemEF_GCS_EnNode = new float[iNodeDOFNo];  // End Node

        // Vector of member nodes (ends) forces in LCS
        public float[] m_ArrElemEF_LCS_StNode = new float[iNodeDOFNo];  // Start Node
        public float[] m_ArrElemEF_LCS_EnNode = new float[iNodeDOFNo];  // End Node

        // Vector of member nodes (ends) internal forces in LCS
        public float[] m_ArrElemIF_LCS_StNode = new float[iNodeDOFNo];  // Start Node
        public float[] m_ArrElemIF_LCS_EnNode = new float[iNodeDOFNo];  // End Node


        // 2D
        CMatrix m_fkLocMatr;
        CMatrix m_fATRMatr2D;
        CMatrix m_fBTTMatr2D;
        CMatrix m_fKGlobM;


        public CFemNode m_NodeStart = new CFemNode();
        public CFemNode m_NodeEnd = new CFemNode();
        public CMLoad m_ELoad;

        float m_GCS_X = 0f;
        float m_GCS_Y = 0f;

        float m_fAlpha = 0f;
        float m_fSinAlpha, m_fCosAlpha;

        CMember m_Member = new CMember();

        // Constructor

        public CE_1D() 
        {
            // Fill Arrays / Initialize
            //Fill_EDisp_Init();
            Fill_EEndsLoad_Init();
        }
        public CE_1D(CMember mMember, CFemNode NStart, CFemNode NEnd, int iSuppType)
        {
            // Main member or segment
            m_Member = mMember; 

            // Nodes
            m_NodeStart = NStart;
            m_NodeEnd   = NEnd;
            // Support type
            m_iSuppType = iSuppType;

            FillBasic2();

        } // End of constructor

        public CE_1D(CGenex FemModel, int iMemberID, int iSuppType)
        {
            // Main member or segment
            m_Member = FemModel.m_arrFemMembers[iMemberID];

            // Nodes
            m_NodeStart = FemModel.m_arrFemMembers[iMemberID].m_NodeStart;
            m_NodeEnd = FemModel.m_arrFemMembers[iMemberID].m_NodeEnd;
            // Support type
            m_iSuppType = iSuppType;

            FillBasic2();

        } // End of constructor

        public CE_1D(CMember mMember, int iSuppType)
        {
            // Main member or segment
            m_Member = mMember;

            // Nodes
            m_NodeStart.INode_ID = m_Member.INode1.INode_ID;
            m_NodeEnd.INode_ID = m_Member.INode1.INode_ID;
            // Support type
            m_iSuppType = iSuppType;

            FillBasic2();

        } // End of constructor

        // Constructor - FEM Member is copy of topological member or segment
        public CE_1D(CMember TopoMember)
        {
            IMember_ID = TopoMember.IMember_ID;
            // m_CrSc = (CCrSc)TopoMember.CrSc;
            m_NodeStart.INode_ID = TopoMember.INode1.INode_ID;
            m_NodeEnd.INode_ID = TopoMember.INode2.INode_ID;
        }

        public void FillBasic2()
        {

            // Displacement
            // doplnit vektor premiestneni pruta - sklada sa z vektorov pre zac a konc. uzol
            // SMemberDisp sElemDisp;

            //// Fill Element Nodes Displacement
            //for (int i = 0; i < 12; i++)
            //{
            //    if (i < 6)
            //        m_ArrDisp[i] = m_NodeStart.m_ArrDisp[i];   // Fill with Start Node
            //    else
            //        m_ArrDisp[i] = m_NodeEnd.m_ArrDisp[i - 6]; // Fill with End Node
            //}

            // Element / Member load








            // Lengths in Global Coordinates
            m_flength_X = GetGCSLengh(0);
            m_flength_Y = GetGCSLengh(1);

            // FEM Element Length
            m_flength = MathF.Sqrt(MathF.Pow2(m_flength_X) + MathF.Pow2(m_flength_Y));

            //m_fAlpha = GetGCSAlpha(1);
            m_fSinAlpha = (float)Math.Sin(m_fAlpha);
            m_fCosAlpha = (float)Math.Cos(m_fAlpha);

            // Get auxialiary point relative coordinates
            Get_C_GCS_coord();


            //// 2D
            //// Transformation Matrix of Element Rotation - 2D
            //m_fATRMatr2D = new float[3, 3]
            //{
            //{  m_fCosAlpha,     m_fSinAlpha,    0f },
            //{ -m_fSinAlpha,     m_fCosAlpha,    0f },
            //{           0f,              0f,    1f }
            //};


            //// Transformation Transfer Matrix - 2D
            //m_fBTTMatr2D = new float[3, 3]
            //{
            //{           -1f,              0f,    0f },
            //{            0f,             -1f,    0f },
            //{  -m_flength_Y,     m_flength_X,   -1f }
            //};


            // Get local matrix acc. to end support/restraint of element

            //// 2D
            //switch (iSuppType)
            //{
            //    case 0:
            //        m_fkLocMatr = GetLocMatrix_2D0();
            //        break;
            //    case 1:
            //        m_fkLocMatr = GetLocMatrix_2D1();
            //        break;
            //    case 2:
            //        m_fkLocMatr = GetLocMatrix_2D2();
            //        break;
            //    case 3:
            //        m_fkLocMatr = GetLocMatrix_2D3();
            //        break;

            //    default:
            //        // Error
            //        break;
            //}

            // Check of partial matrices members

            // Partial matrices of global matrix of member 6 x 6
            //Console.WriteLine(CM.Print2DMatrix(GetPartM_k11(m_fkLocMatr, m_fAMatr2D), 6));

            // Return partial matrixes and global matrix of FEM element

            // 2D

        //    m_fKGlobM = fGetGlobM(
        //    GetPartM_k11(m_fkLocMatr, m_fATRMatr2D),
        //    GetPartM_k12(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D),
        //    GetPartM_k21(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D),
        //    GetPartM_k22(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D)
        //    );
        }

        //public void Fill_EDisp_Init()
        //{
        //    m_ArrDisp[c.UX] = float.PositiveInfinity;
        //    m_ArrDisp[c.UY] = float.PositiveInfinity;
        //    m_ArrDisp[c.RZ] = float.PositiveInfinity;

        //    // Temporary
        //    m_ArrDisp[c.RZ + 1] = float.PositiveInfinity;
        //    m_ArrDisp[c.RZ + 2] = float.PositiveInfinity;
        //    m_ArrDisp[c.RZ + 3] = float.PositiveInfinity;
        //}

        public void Fill_ECode_Init()
        {
            m_ArrCodeNo[c.UX] = int.MaxValue;
            m_ArrCodeNo[c.UY] = int.MaxValue;
            m_ArrCodeNo[c.RZ] = int.MaxValue;

            // Temporary
            m_ArrCodeNo[c.RZ + 1] = int.MaxValue;
            m_ArrCodeNo[c.RZ + 2] = int.MaxValue;
            m_ArrCodeNo[c.RZ + 3] = int.MaxValue;
        }

        public void Fill_EEndsLoad_Init()
        {
            // Start Node
            m_ArrElemPEF_LCS[0, c.FX] = 0f;
            m_ArrElemPEF_LCS[0, c.FY] = 0f;
            m_ArrElemPEF_LCS[0, c.MZ] = 0f;

            // End Node
            m_ArrElemPEF_LCS[1, c.FX] = 0f;
            m_ArrElemPEF_LCS[1, c.FY] = 0f;
            m_ArrElemPEF_LCS[1, c.MZ] = 0f;
        }


        public float GetGCSLengh(float fCoordStart, float fCoordEnd, float fGCSCoord)
        {
            // Priebezne
            if (
                ((fCoordEnd >= fGCSCoord) && (fCoordStart >= fGCSCoord)) || // Both positive
                 (fCoordEnd <= fGCSCoord && fCoordStart <= fGCSCoord) // Both negative
               )
            {
                return fCoordEnd - fCoordStart;  // if (fCoordEnd > fCoordStart) Positive length / if (fCoordStart > fCoordEnd) Negative length
            }
            else if (fCoordEnd <= fGCSCoord && fCoordStart >= fGCSCoord) // Start positive / End negative
            {
                return fCoordEnd - fCoordStart; // Negative length
            }
            else if (fCoordStart <= fGCSCoord && fCoordEnd >= fGCSCoord) // Start negative / End positive
            {
                return fCoordEnd - fCoordStart; // Positive length
            }
            else // Exception
            { 
                return 0.0f;
            }
        }

        public float GetGCSLengh(int i)
        {
            // global coordinate system direction
            // 0 - global x-axis
            // 1 - global y-axis
            switch (i)
            {
                case 0:
                    return GetGCSLengh(m_NodeStart.FCoord_X, m_NodeEnd.FCoord_X, m_GCS_X);
                case 1:
                    return GetGCSLengh(m_NodeStart.FCoord_Y, m_NodeEnd.FCoord_Y, m_GCS_Y);
                default:
                    return 0f;
            }
        }

        public float GetGCSAlpha(float fCoordStart1, float fCoordEnd1, float fCoordStart2, float fCoordEnd2, float fGCsCoord1, float fGCsCoord2)
        {
            ///////////////////////////////////////////////////////////////////
            // len rozpracovane , nutne skontrolovat znamienka a vylepsit 
            ///////////////////////////////////////////////////////////////////

            if ((fCoordEnd1 >= fCoordStart1) && (fCoordEnd2 >= fCoordStart2))
            {
               // 1st Quadrant (0-90 degrees / resp. 0 - 0.5PI)
                return (float)Math.Atan((fCoordEnd2 - fCoordStart2) / (fCoordEnd1 - fCoordStart1));
            }
            else if ((fCoordEnd1 >= fCoordStart1) && (fCoordEnd2 <= fCoordStart2))
            {
               // 2nd Quadrant (90-180 degrees / resp. 0.5PI - PI)
                return (float)Math.PI/2 + (float)Math.Atan((fCoordEnd1 - fCoordStart1) / (fCoordEnd2 - fCoordStart2));
            }
            else if ((fCoordEnd1 <= fCoordStart1) && (fCoordEnd2 <= fCoordStart2))
            {
                // 3rd Quadrant (180-270 degrees / resp. PI - 1.5PI)
                return (float)Math.PI + (float)Math.Atan((fCoordEnd2 - fCoordStart2) / (fCoordEnd1 - fCoordStart1));
            }
            else /*((fCoordEnd1 <= fCoordStart1) && (fCoordEnd2 >= fCoordStart2))*/
            {
                // 4th Quadrant (270-360 degrees / resp. 1.5PI - 2PI)
                return (1.5f * (float)Math.PI) + (float)Math.Atan((fCoordEnd2 - fCoordStart1) / (fCoordEnd2 - fCoordStart2));
            }
        }

        public float GetGCSAlpha()
        {
            // GLOBAL COORDINATE SYSTEM direction
            // Rotation about Global Z-Axis

            return GetGCSAlpha(m_NodeStart.FCoord_X, m_NodeEnd.FCoord_X, m_NodeStart.FCoord_Y, m_NodeEnd.FCoord_Y, m_GCS_X, m_GCS_Y);
        }

        // Transformation Matrix of Element Rotation - 3D
        // 2x2 - 3x3
        public float[,][,] Get_AMatr3D0()
        {
            /*
            // Angles
            SGCSAngles sAngles = Get_Angles();

            // Matrix Members
            float flambda1 = (float)Math.Cos(sAngles.fAngleXx0);
            float flambda2 = (float)Math.Cos(sAngles.fAngleXy0);
            float flambda3 = (float)Math.Cos(sAngles.fAngleXz0);

            float fmu1 = (float)Math.Cos(sAngles.fAngleYx0);
            float fmu2 = (float)Math.Cos(sAngles.fAngleYy0);
            float fmu3 = (float)Math.Cos(sAngles.fAngleYz0);

            float fv1 = (float)Math.Cos(sAngles.fAngleZx0);
            float fv2 = (float)Math.Cos(sAngles.fAngleZy0);
            float fv3 = (float)Math.Cos(sAngles.fAngleZz0);

             */

            float[,] RPart = new float[3, 3];
            //RPart = CM.fTransMatrix(m_flength_X, m_flength_Y, m_flength, m_frotation_angle, m_fC_GCS_Coord);

            /*
            float[,] RPart = new float[3, 3]
            {
            {  flambda1,   fmu1,   fv1 },
            {  flambda2,   fmu2,   fv2 },
            {  flambda3,   fmu3,   fv3 }
            };
            */

            float[,] MZero = new float[3, 3]
            {
            {  0f,   0f,   0f },
            {  0f,   0f,   0f },
            {  0f,   0f,   0f }
            };

            // Local Stiffeness Matrix
            return new float[2, 2][,] 
            {
            {  RPart, MZero },
            {  MZero, RPart }
            };
        }

        private float Get_Angle1(float fl_inGCSAxis, float fl_GCSAxis, float fl_inGCSPlane)
        {
            if (fl_inGCSPlane == 0f) // Paralel to the axis // Perpendicular to the plane
                return (float)Math.Acos(fl_inGCSAxis / m_flength);
            else if (fl_inGCSAxis == 0f)  // Perpendicular to the axis // Paralel to the axis
            {
                if (fl_GCSAxis > 0f)
                    return 0f;
                else
                    return -(float)Math.PI; 
            }
            else // General
                return (float)Math.Acos(fl_inGCSPlane / m_flength);
        }

        private float Get_Angle2(float fl_inGCSAxis, float fl_GCSAxis, float fl_inGCSPlane)
        {
            if (fl_inGCSPlane == 0f) // Paralel to the axis // Perpendicular to the plane
            {
                if(fl_inGCSAxis > 0f)
                return -(float)Math.PI / 2f;
                else
                 return -(float)Math.PI * 3f / 2f;
            }
            else if (fl_inGCSAxis == 0f)  // Perpendicular to the axis // Paralel to the axis
            {
                if (fl_GCSAxis > 0f)
                    return -(float)Math.PI;
                else
                    return -((float)Math.PI * 3f / 2f);
            }
            else // General
                return (float)Math.Acos(fl_inGCSPlane / m_flength);
        }

        private float Get_Angle3(float fl_inGCSAxis, float fl_inGCSPlane)
        {
            if (fl_inGCSPlane == 0f || fl_inGCSAxis == 0f) // Paralel to the axis // Perpendicular to the plane
                return -(float)Math.PI / 2f;
            else
                return -(float)Math.Acos((fl_inGCSAxis / m_flength) + (float)Math.PI / 2f);

        }

        // Get relative coordinates of node C(auxialiary point to specify orientation of local z-Axis) to node A (start node of member)

        void Get_C_GCS_coord()
        {
            // Default - parallel to the global Z-axis positive (upwards) 
            m_fC_GCS_Coord[0] = 0f;
            m_fC_GCS_Coord[1] = 0f;
            m_fC_GCS_Coord[2] = 1f; // positive global and local z-Axis - upwards

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Definition of local stiffeness matrixes depending on loading and restraints
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region 2D_000_000
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - votknutie 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_000_000()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_flength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f12EIy_len3 = (12f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f06EIy_len2 = (6f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f04EIy_len1 = (4f * f_EIy) / m_flength;

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,               0f,            0f },
            {       0f,      f12EIy_len3,   f06EIy_len2 },
            {       0f,      f06EIy_len2,   f04EIy_len1 }
            };
        }
        #endregion

        #region 2D_000_00_a
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - valcovy klb / osamele bremeno 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_000_00_a()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_flength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_flength;

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,                0f,           0f },
            {       0f,      f3EIy_len3,   f3EIy_len2 },
            {       0f,      f3EIy_len2,   f3EIy_len1 }
            };
        }
        #endregion

        #region 2D_000_00_b
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - valcovy klb / ohyb moment - 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_000_00_b()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_flength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_flength;

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {  fEA_len,             0f,           0f },
            {       0f,     f3EIy_len3,   f3EIy_len2 },
            {       0f,     f3EIy_len2,   f3EIy_len1 }
            };
        }
        #endregion

        #region 2D_000_0_0
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - vidlicove ulozenie 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_000_0_0()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_flength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f12EIy_len3 = (12f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f06EIy_len2 = (6f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f04EIy_len1 = (4f * f_EIy) / m_flength;

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,                 0f,            0f },
            {       0f,      f12EIy_len3,   f06EIy_len2 },
            {       0f,      f06EIy_len2,   f04EIy_len1 }
            };
        }
        #endregion

        #region 2D_000____
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - volny koniec konzola - 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_000____()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_flength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_flength;

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,                0f,           0f },
            {       0f,      f3EIy_len3,   f3EIy_len2 },
            {       0f,      f3EIy_len2,   f3EIy_len1 }
            };
        }
        #endregion

        #region 2D_00__0_
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // posuvne ulozenie - valcovy klb / spojite rovnomerne zatazenie - 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_00__0_()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_flength;
            float f3EIy_len3 = (3f * m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y) / (float)Math.Pow(m_flength, 3f);

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,          0f,    0f },
            {       0f, f3EIy_len3,   0f },
            {       0f,        0f,    0f }
            };
        }
        #endregion




















































        // GENERAL FEM OPERATIONS

        //// Return partial matrix k11 of global matrix of FEM 1D element
        //float[,] GetPartM_k11(float[,] fMk_0, float[,] fMA)
        //{
        //    // [fMA]T * [fMk_0] * [fMA] 

        //    // Output Matrix
        //    return CM.fMultiplyMatr(CM.fMultiplyMatr(CM.GetTransMatrix(fMA), fMk_0), fMA);
        //}

        //// Return partial matrix k12 of global matrix of FEM 1D element
        //float[,] GetPartM_k12(float[,] fMk_0, float[,] fMA, float[,] fMB)
        //{
        //    // Output Matrix
        //    return CM.GetTransMatrix(CM.fMultiplyMatr(CM.fMultiplyMatr(fMB, CM.GetTransMatrix(fMA)), CM.fMultiplyMatr(fMk_0, fMA)));
        //}

        //// Return partial matrix k21 of global matrix of FEM 1D element
        //float[,] GetPartM_k21(float[,] fMk_0, float[,] fMA, float[,] fMB)
        //{
        //    // Output Matrix
        //    return CM.fMultiplyMatr(CM.fMultiplyMatr(fMB, CM.GetTransMatrix(fMA)), CM.fMultiplyMatr(fMk_0, fMA));
        //}

        //// Return partial matrix k22 of global matrix of FEM 1D element
        //float[,] GetPartM_k22(float[,] fMk_0, float[,] fMA, float[,] fMB)
        //{
        //    // Output Matrix
        //    return CM.fMultiplyMatr(fMB, CM.GetTransMatrix(CM.fMultiplyMatr(CM.fMultiplyMatr(fMB, CM.GetTransMatrix(fMA)), CM.fMultiplyMatr(fMk_0, fMA))));
        //}

        float[,][,] fGetGlobM(float[,] fMk11, float[,] fMk12, float[,] fMk21, float[,] fMk22)
        {
            // Number of Matrix M rows and columns
            
            // 2D
            int iM_iRowsMax = 3;
            int iM_jColsMax = 3;

            // 3D
             iM_iRowsMax = 6;
             iM_jColsMax = 6;

            // Output Matrix
            return new float[2,2][,]
            {
                {fMk11, fMk12},    
                {fMk21, fMk22}
            };
        }







        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// Results
        //// Get internal forces in global and local coordinate system
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// Element Final End Forces GCS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Start Node Vector - 1 x 6
        //// [EF_GCS i] = [ELoad i LCS] + [Kii] * [delta i] + [Kij] * [delta j]
        //public void GetArrElemEF_GCS_StNode()
        //{
        //    m_ArrElemEF_GCS_StNode =
        //        CM.fGetSum(
        //        m_ArrElemPEF_GCS_StNode,
        //        CM.fGetSum(
        //        CM.fMultiplyMatr(GetPartM_k11(m_fkLocMatr, m_fAMatr2D), m_NodeStart.m_ArrDisp),
        //        CM.fMultiplyMatr(GetPartM_k12(m_fkLocMatr, m_fAMatr2D, m_fBMatr2D), m_NodeEnd.m_ArrDisp)
        //        )
        //        );
        //}

        //// End Node Vector - 1 x 6
        //// [EF_GCS j] = [ELoad j LCS] + [Kji] * [delta i] + [Kjj] * [delta j]
        //public void GetArrElemEF_GCS_EnNode()
        //{
        //    m_ArrElemEF_GCS_EnNode =
        //        CM.fGetSum(
        //        m_ArrElemPEF_GCS_EnNode,
        //        CM.fGetSum(
        //        CM.fMultiplyMatr(GetPartM_k21(m_fkLocMatr, m_fAMatr2D, m_fBMatr2D), m_NodeStart.m_ArrDisp),
        //        CM.fMultiplyMatr(GetPartM_k22(m_fkLocMatr, m_fAMatr2D, m_fBMatr2D), m_NodeEnd.m_ArrDisp)
        //        )
        //        );
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// Element Final End forces LCS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        //// Start Node Vector - 1 x 6
        ////  [EF_LCS i] = [A0] * [EF_GCS i]
        //public void GetArrElemEF_LCS_StNode()
        //{
        //    m_ArrElemEF_LCS_StNode = CM.fMultiplyMatr(m_fAMatr2D, m_ArrElemEF_GCS_StNode);
        //}

        //// End Node Vector - 1 x 6
        //// [EF_LCS j] = [A0] * [EF_GCS j]
        //public void GetArrElemEF_LCS_EnNode()
        //{
        //    m_ArrElemEF_LCS_EnNode = CM.fMultiplyMatr(m_fAMatr2D, m_ArrElemEF_GCS_EnNode);
        //}


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// Element final internal forces in LCS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Start Node Vector - 1 x 6
        ////  [IF_LCS i] = [-1,-1,-1,-1,-1,1] * [EF_LCS i]
        //public void GetArrElemIF_LCS_StNode()
        //{
        //    int [] fTempSignTransf = new int[6] { -1, -1, -1, -1, -1, 1 };
        //    m_ArrElemIF_LCS_StNode = CM.fMultiplyMatr(fTempSignTransf, m_ArrElemEF_LCS_StNode);
        //}

        //// End Node Vector - 1 x 6
        //// [IF_LCS j]  = [ 1, 1, 1, 1, 1,-1] * [EF_LCS j]
        //public void GetArrElemIF_LCS_EnNode()
        //{
        //    int[] fTempSignTransf = new int[6] { 1, 1, 1, 1, 1, -1 };
        //    m_ArrElemIF_LCS_EnNode = CM.fMultiplyMatr(fTempSignTransf, m_ArrElemEF_LCS_EnNode);
        //}



    }
}
