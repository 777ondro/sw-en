using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using CRSC;
using MATH;
using CENEX;

namespace FEM_CALC_1Din2D
{
    public class CE_1D : CMember
    {
        public int m_iSuppType;
        // Geometrical properties of Element
        public float m_fLength_X, m_fLength_Y, m_frotation_angle = 0f;

        public float m_fLength;

        // Primary End Forces Vectors
        // Vector of member nodes (ends) primary forces in Local Coordinate System (LCS) due to the transverse member load
        CVector m_VElemPEF_LCS_StNode = new CVector(Constants.i2D_DOFNo);  // Start Node
        CVector m_VElemPEF_LCS_EnNode = new CVector(Constants.i2D_DOFNo);  // End Node

        // Vector of member nodes (ends) primary forces in Global Coordinate System due to the transverse member load
        CVector m_VElemPEF_GCS_StNode = new CVector(Constants.i2D_DOFNo);  // Start Node
        CVector m_VElemPEF_GCS_EnNode = new CVector(Constants.i2D_DOFNo);  // End Node

        // Results End Forces Vectors
        // Vector of member nodes (ends) forces in GCS
        CVector m_VElemEF_GCS_StNode = new CVector(Constants.i2D_DOFNo);  // Start Node
        CVector m_VElemEF_GCS_EnNode = new CVector(Constants.i2D_DOFNo);  // End Node

        // Vector of member nodes (ends) forces in LCS
        CVector m_VElemEF_LCS_StNode = new CVector(Constants.i2D_DOFNo);  // Start Node
        CVector m_VElemEF_LCS_EnNode = new CVector(Constants.i2D_DOFNo);  // End Node

        // Vector of member nodes (ends) internal forces in LCS
        CVector m_VElemIF_LCS_StNode = new CVector(Constants.i2D_DOFNo);  // Start Node
        CVector m_VElemIF_LCS_EnNode = new CVector(Constants.i2D_DOFNo);  // End Node

        // 2D Matrices 
        CMatrix m_fkLocMatr = new CMatrix(Constants.i2D_DOFNo);   // 3x3
        CMatrix m_fATRMatr2D = new CMatrix(Constants.i2D_DOFNo);  // 3x3
        CMatrix m_fBTTMatr2D = new CMatrix(Constants.i2D_DOFNo);  // 3x3
        CMatrix m_fKGlobM;     // (2x3)*(2x3)

        public CFemNode m_NodeStart =  new CFemNode();
        public CFemNode m_NodeEnd = new CFemNode();
        public CMember m_Member = new CMember();
        public CCrSc m_CrSc = new CCrSc();
        public CMLoad m_ELoad;
        EElemSuppType m_eSuppType;

        float m_GCS_X = 0f;
        float m_GCS_Y = 0f;

        float m_fAlpha = 0f;
        float m_fSinAlpha, m_fCosAlpha;

        // Constructor

        public CE_1D() 
        {
            // Fill Arrays / Initialize
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

            // Cross-section
            m_CrSc = m_Member.CrSc;

            FillBasic2();

        } // End of constructor

        public CE_1D(CGenex FemModel, int iMemberID, int iSuppType)
        {
            // Main member or segment
            m_Member = FemModel.m_arrFemMembers[iMemberID];

            // Nodes
            m_NodeStart.CopyTopoNodetoFemNode(FemModel.m_arrFemMembers[iMemberID].m_NodeStart);
            m_NodeEnd.CopyTopoNodetoFemNode(FemModel.m_arrFemMembers[iMemberID].m_NodeEnd);
            // Support type
            m_iSuppType = iSuppType;

            // Cross-section
            m_CrSc = m_Member.CrSc;

            FillBasic2();

        } // End of constructor

        public CE_1D(CMember mMember, int iSuppType)
        {
            // Main member or segment
            m_Member = mMember;

            // Nodes
            m_NodeStart.CopyTopoNodetoFemNode(m_Member.INode1);
            m_NodeEnd.CopyTopoNodetoFemNode(m_Member.INode2);
            // Support type
            m_iSuppType = iSuppType;

            // Cross-section
            m_CrSc = m_Member.CrSc;

            FillBasic2();
        } // End of constructor

        // Constructor - FEM Member is copy of topological member or segment
        public CE_1D(CMember TopoMember)
        {
            // Main member or segment
            m_Member = TopoMember;
            // Nodes
            m_NodeStart.CopyTopoNodetoFemNode(m_Member.INode1);
            m_NodeEnd.CopyTopoNodetoFemNode(m_Member.INode2);

            // Cross-section
            m_CrSc = m_Member.CrSc;

            FillBasic2();
        }

        public void FillBasic2()
        {
            // Element / Member load



            // Lengths in Global Coordinates
            m_fLength_X = GetGCSLengh(0);
            m_fLength_Y = GetGCSLengh(1);

            // FEM Element Length
            m_fLength = MathF.Sqrt(MathF.Pow2(m_fLength_X) + MathF.Pow2(m_fLength_Y));

            //m_fAlpha = GetGCSAlpha(1);
            m_fSinAlpha = (float)Math.Sin(m_fAlpha);
            m_fCosAlpha = (float)Math.Cos(m_fAlpha);

            // 2D
            // Transformation Matrix of Element Rotation - 2D
            m_fATRMatr2D.m_fArrMembers = new float[Constants.i2D_DOFNo, Constants.i2D_DOFNo]
            {
            {  m_fCosAlpha,     m_fSinAlpha,    0f },
            { -m_fSinAlpha,     m_fCosAlpha,    0f },
            {           0f,              0f,    1f }
            };


            // Transformation Transfer Matrix - 2D
            m_fBTTMatr2D.m_fArrMembers = new float[Constants.i2D_DOFNo, Constants.i2D_DOFNo]
            {
            {           -1f,              0f,    0f },
            {            0f,             -1f,    0f },
            {  -m_fLength_Y,     m_fLength_X,   -1f }
            };

            // Get Element support type
            // Depends on nodal support and element releases
            m_eSuppType = Get_iElemSuppType();
            
            // Get local matrix acc. to end support/restraint of element
            GetLocMatrix_2D();

            // Check of partial matrices members

            // Partial matrices of global matrix of member 3 x 3
            Console.WriteLine(m_fkLocMatr.Print2DMatrix());

            // Return partial matrixes and global matrix of FEM element 6 x 6 (2*3x2*3) 2D

            m_fKGlobM = new CMatrix(
            GetPartM_k11(m_fkLocMatr, m_fATRMatr2D),
            GetPartM_k12(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D),
            GetPartM_k21(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D),
            GetPartM_k22(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D)
            );
        }

        public void Fill_ECode_Init()
        {
           // Start Node
           m_NodeStart.m_ArrNCodeNo[(int)e2D_DOF.eUX] = int.MaxValue;
           m_NodeStart.m_ArrNCodeNo[(int)e2D_DOF.eUY] = int.MaxValue;
           m_NodeStart.m_ArrNCodeNo[(int)e2D_DOF.eRZ] = int.MaxValue;

           // End Node
           m_NodeEnd.m_ArrNCodeNo[(int)e2D_DOF.eUX] = int.MaxValue;
           m_NodeEnd.m_ArrNCodeNo[(int)e2D_DOF.eUY] = int.MaxValue;
           m_NodeEnd.m_ArrNCodeNo[(int)e2D_DOF.eRZ] = int.MaxValue;
        }

        public void Fill_EEndsLoad_Init()
        {
            // Start Node
            m_VElemPEF_LCS_StNode.FVectorItems[(int)e2D_E_F.eFX] = 0f;
            m_VElemPEF_LCS_StNode.FVectorItems[(int)e2D_E_F.eFY] = 0f;
            m_VElemPEF_LCS_StNode.FVectorItems[(int)e2D_E_F.eMZ] = 0f;

            // End Node
            m_VElemPEF_LCS_EnNode.FVectorItems[(int)e2D_E_F.eFX] = 0f;
            m_VElemPEF_LCS_EnNode.FVectorItems[(int)e2D_E_F.eFY] = 0f;
            m_VElemPEF_LCS_EnNode.FVectorItems[(int)e2D_E_F.eMZ] = 0f;
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

        private EElemSuppType Get_iElemSuppType()
        {
            // ROZPRACOVANE, zahrnut aj klby

            // Is DOF free?
            // true - yes, it is
            // false - no, it isnt

            if  (
                (m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == false) &&
                (m_Member.CnRelease1 == null && m_Member.CnRelease2 == null)
                )
                return EElemSuppType.e2DEl_000_000;
            else if
                (
                (m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUX] == true &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUY] == true &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true) ||
                 (m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUX] == true &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUY] == true &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eRZ] == true)       
                )
                return EElemSuppType.e2DEl_000____;
            else if
                (
                (m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUX] == true && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUY] == true && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == false) ||
                (m_Member.CnRelease1.m_bRestrain[(int)e2D_DOF.eUX] == true &&
                 m_Member.CnRelease1.m_bRestrain[(int)e2D_DOF.eUY] == true &&
                 m_Member.CnRelease1.m_bRestrain[(int)e2D_DOF.eRZ] == true)
                )
                return EElemSuppType.e2DEl_____000;
            else if
                (
                (m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUX] == true &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false &&
                 m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true) ||
                ((m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUX] == false &&
                 m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUY] == false &&
                 m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eRZ] == true) ||
                 (m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUX] == true &&
                 m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUY] == false &&
                 m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eRZ] == true))
                )
                return EElemSuppType.e2DEl_00___0_;
            else if
                (
                 (m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUX] == true && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false &&
                  m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false &&
                  m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true) ||
                 ((m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUX] == true &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUY] == false &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eRZ] == true) ||
                 (m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUX] == false &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUY] == false &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eRZ] == true))
                )
                return EElemSuppType.e2DEl__0__00_;
            else if
                (
                 (m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUX] == false &&
                  m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eUY] == false &&
                  m_NodeStart.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true && m_NodeEnd.m_ArrNodeDOF[(int)e2D_DOF.eRZ] == true) ||
                 ((m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUX] == false &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUY] == false &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eRZ] == true) ||
                 (m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUX] == false &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eUY] == false &&
                  m_Member.CnRelease2.m_bRestrain[(int)e2D_DOF.eRZ] == true))
                )
                return EElemSuppType.e2DEl_00__00_;
            else
                return EElemSuppType.e2DEl________; // Not implemented
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
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_fLength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f12EIy_len3 = (12f * f_EIy) / (float)Math.Pow(m_fLength, 3f);
            float f06EIy_len2 = (6f * f_EIy) / (float)Math.Pow(m_fLength, 2f);
            float f04EIy_len1 = (4f * f_EIy) / m_fLength;

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
            float fEA_len = (m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g) / m_fLength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_fLength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_fLength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_fLength;

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
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_fLength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_fLength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_fLength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_fLength;

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
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_fLength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f12EIy_len3 = (12f * f_EIy) / (float)Math.Pow(m_fLength, 3f);
            float f06EIy_len2 = (6f * f_EIy) / (float)Math.Pow(m_fLength, 2f);
            float f04EIy_len1 = (4f * f_EIy) / m_fLength;

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
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_fLength;
            float f_EIy = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_fLength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_fLength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_fLength;

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,                0f,           0f },
            {       0f,      f3EIy_len3,   f3EIy_len2 },
            {       0f,      f3EIy_len2,   f3EIy_len1 }
            };
        }
        #endregion

        #region 2D_00___0_
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // posuvne ulozenie - valcovy klb / spojite rovnomerne zatazenie - 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_00___0_()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FA_g / m_fLength;
            float f3EIy_len3 = (3f * m_Member.CrSc.m_Mat.m_fE * m_Member.CrSc.FI_y) / (float)Math.Pow(m_fLength, 3f);

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,          0f,    0f },
            {       0f, f3EIy_len3,   0f },
            {       0f,        0f,    0f }
            };
        }
        #endregion

        #region Local stiffeness matrix of member in 2D 
        private void GetLocMatrix_2D()
        {
            switch (m_eSuppType)
            {
                case EElemSuppType.e2DEl_000_000:
                    m_fkLocMatr.m_fArrMembers = GetLocMatrix_2D_000_000();
                    break;
                case EElemSuppType.e2DEl_000_00_:
                case EElemSuppType.e2DEl_00__000:
                    m_fkLocMatr.m_fArrMembers = GetLocMatrix_2D_000_00_a();
                    break;
                case EElemSuppType.e2DEl_000_0_0:
                case EElemSuppType.e2DEl_0_0_000:
                    m_fkLocMatr.m_fArrMembers = GetLocMatrix_2D_000_0_0();
                    break;
                case EElemSuppType.e2DEl_00___0_:
                case EElemSuppType.e2DEl__0__00_:
                    m_fkLocMatr.m_fArrMembers = GetLocMatrix_2D_00___0_();
                    break;
                case EElemSuppType.e2DEl_000____:
                case EElemSuppType.e2DEl_____000:
                    m_fkLocMatr.m_fArrMembers = GetLocMatrix_2D_000____();
                    break;
                default:
                    // Error or unsupported element - exception
                    m_fkLocMatr.m_fArrMembers = null;
                    break;
            }
        }
        #endregion



















































        // GENERAL FEM OPERATIONS

        // Return partial matrix k11 of global matrix of FEM 1D element
        CMatrix GetPartM_k11(CMatrix fMk_0, CMatrix fMA)
        {
            // [fMA]T * [fMk_0] * [fMA] 

            // Output Matrix
            return MatrixF.fMultiplyMatr(MatrixF.fMultiplyMatr(MatrixF.GetTransMatrix(fMA), fMk_0), fMA);
        }

        // Return partial matrix k12 of global matrix of FEM 1D element
        CMatrix GetPartM_k12(CMatrix fMk_0, CMatrix fMA, CMatrix fMB)
        {
            // Output Matrix
            return MatrixF.GetTransMatrix(MatrixF.fMultiplyMatr(MatrixF.fMultiplyMatr(fMB, MatrixF.GetTransMatrix(fMA)), MatrixF.fMultiplyMatr(fMk_0, fMA)));
        }

        // Return partial matrix k21 of global matrix of FEM 1D element
        CMatrix GetPartM_k21(CMatrix fMk_0, CMatrix fMA, CMatrix fMB)
        {
            // Output Matrix
            return MatrixF.fMultiplyMatr(MatrixF.fMultiplyMatr(fMB, MatrixF.GetTransMatrix(fMA)), MatrixF.fMultiplyMatr(fMk_0, fMA));
        }

        // Return partial matrix k22 of global matrix of FEM 1D element
        CMatrix GetPartM_k22(CMatrix fMk_0, CMatrix fMA, CMatrix fMB)
        {
            // Output Matrix
            return MatrixF.fMultiplyMatr(fMB, MatrixF.GetTransMatrix(MatrixF.fMultiplyMatr(MatrixF.fMultiplyMatr(fMB, MatrixF.GetTransMatrix(fMA)), MatrixF.fMultiplyMatr(fMk_0, fMA))));
        }

        CMatrix fGetGlobM(CMatrix fMk11, CMatrix fMk12, CMatrix fMk21, CMatrix fMk22)
        {
            // Number of Matrix M rows and columns
            // Output Matrix
            return new CMatrix( fMk11, fMk12, fMk21,fMk22);
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
        //        MatrixF.fGetSum(
        //        m_ArrElemPEF_GCS_StNode,
        //        MatrixF.fGetSum(
        //        MatrixF.fMultiplyMatr(GetPartM_k11(m_fkLocMatr, m_fAMatr2D), m_NodeStart.m_ArrDisp),
        //        MatrixF.fMultiplyMatr(GetPartM_k12(m_fkLocMatr, m_fAMatr2D, m_fBMatr2D), m_NodeEnd.m_ArrDisp)
        //        )
        //        );
        //}

        //// End Node Vector - 1 x 6
        //// [EF_GCS j] = [ELoad j LCS] + [Kji] * [delta i] + [Kjj] * [delta j]
        //public void GetArrElemEF_GCS_EnNode()
        //{
        //    m_ArrElemEF_GCS_EnNode =
        //        MatrixF.fGetSum(
        //        m_ArrElemPEF_GCS_EnNode,
        //        MatrixF.fGetSum(
        //        MatrixF.fMultiplyMatr(GetPartM_k21(m_fkLocMatr, m_fAMatr2D, m_fBMatr2D), m_NodeStart.m_ArrDisp),
        //        MatrixF.fMultiplyMatr(GetPartM_k22(m_fkLocMatr, m_fAMatr2D, m_fBMatr2D), m_NodeEnd.m_ArrDisp)
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
        //    m_ArrElemEF_LCS_StNode = MatrixF.fMultiplyMatr(m_fAMatr2D, m_ArrElemEF_GCS_StNode);
        //}

        //// End Node Vector - 1 x 6
        //// [EF_LCS j] = [A0] * [EF_GCS j]
        //public void GetArrElemEF_LCS_EnNode()
        //{
        //    m_ArrElemEF_LCS_EnNode = MatrixF.fMultiplyMatr(m_fAMatr2D, m_ArrElemEF_GCS_EnNode);
        //}


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// Element final internal forces in LCS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //// Start Node Vector - 1 x 6
        ////  [IF_LCS i] = [-1,-1,-1,-1,-1,1] * [EF_LCS i]
        //public void GetArrElemIF_LCS_StNode()
        //{
        //    int [] fTempSignTransf = new int[6] { -1, -1, -1, -1, -1, 1 };
        //    m_ArrElemIF_LCS_StNode = MatrixF.fMultiplyMatr(fTempSignTransf, m_ArrElemEF_LCS_StNode);
        //}

        //// End Node Vector - 1 x 6
        //// [IF_LCS j]  = [ 1, 1, 1, 1, 1,-1] * [EF_LCS j]
        //public void GetArrElemIF_LCS_EnNode()
        //{
        //    int[] fTempSignTransf = new int[6] { 1, 1, 1, 1, 1, -1 };
        //    m_ArrElemIF_LCS_EnNode = MatrixF.fMultiplyMatr(fTempSignTransf, m_ArrElemEF_LCS_EnNode);
        //}



    }
}
