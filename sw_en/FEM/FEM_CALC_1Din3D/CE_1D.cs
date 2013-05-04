﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using FEM_CALC_BASE;
using MATH;
using CRSC;

namespace FEM_CALC_1Din3D
{
    public struct SMemberDisp
    {
        public float s_fUX_St;
        public float s_fUY_St;
        public float s_fUZ_St;
        public float s_fRX_St;
        public float s_fRY_St;
        public float s_fRZ_St;

        public float s_fUX_En;
        public float s_fUY_En;
        public float s_fUZ_En;
        public float s_fRX_En;
        public float s_fRY_En;
        public float s_fRZ_En;
    };

    public struct SGCSAngles
    {
        public float fAngleXx0;
        public float fAngleXy0;
        public float fAngleXz0;

        public float fAngleYx0;
        public float fAngleYy0;
        public float fAngleYz0;

        public float fAngleZx0;
        public float fAngleZy0;
        public float fAngleZz0;
    };

    public class CE_1D:CE_1D_BASE // :CE_1D_BASE
    {
        public int m_iSuppType;
        // Geometrical properties of Element
        public float m_flength_X, m_flength_Y, m_flength_Z, m_frotation_angle = 0f;
        public float m_flength_XY, m_flength_YZ, m_flength_XZ;

        public float[] m_fC_GCS_Coord = new float[3]; // Relative oordinate AC of auxiliary point C which define local member z-Axis orientation in global coordinate system of model
        
        public CMatrix m_fkLocMatr;

        static int iNodeDOFNo = 6; // int ot static int !!!!

        // Vector of member displacement
        public CVector m_ArrDisp = new CVector(2*Constants.iNodeDOFNo);
        // Array of global codes numbers of element desgress of freedom (Start node 0-5, and node 6-11)
        public int[] m_ArrCodeNo = new int[2 * Constants.iNodeDOFNo]; 
        
        
        // Primary End Forces Vectors
        // Vector of member nodes (ends) primary forces in Local Coordinate System (LCS) due to the transverse member load
        public CVector m_ArrElemPEF_LCS_StNode = new CVector(Constants.iNodeDOFNo);  // Start Node
        public CVector m_ArrElemPEF_LCS_EnNode = new CVector(Constants.iNodeDOFNo);  // End Node

        // Vector of member ends primary forces in Local Coordinate System (LCS) due to the transverse member load
        public CMatrix m_ArrElemPEF_LCS = new CMatrix(2, Constants.iNodeDOFNo);

        // Vector of member nodes (ends) primary forces in global Coordinate System due to the transverse member load
        public CVector m_ArrElemPEF_GCS_StNode = new CVector(Constants.iNodeDOFNo);  // Start Node
        public CVector m_ArrElemPEF_GCS_EnNode = new CVector(Constants.iNodeDOFNo);  // End Node

        // Vector of member ends primary forces in Global Coordinate System (GCS) due to the transverse member load
        public CMatrix m_ArrElemPEF_GCS = new CMatrix(2, Constants.iNodeDOFNo);

        // Results End Forces Vectors
        // Vector of member nodes (ends) forces in GCS
        public CVector m_ArrElemEF_GCS_StNode = new CVector(Constants.iNodeDOFNo);  // Start Node
        public CVector m_ArrElemEF_GCS_EnNode = new CVector(Constants.iNodeDOFNo);  // End Node

        // Vector of member nodes (ends) forces in LCS
        public CVector m_ArrElemEF_LCS_StNode = new CVector(Constants.iNodeDOFNo);  // Start Node
        public CVector m_ArrElemEF_LCS_EnNode = new CVector(Constants.iNodeDOFNo);  // End Node

        // Vector of member nodes (ends) internal forces in LCS
        public CVector m_ArrElemIF_LCS_StNode = new CVector(Constants.iNodeDOFNo);  // Start Node
        public CVector m_ArrElemIF_LCS_EnNode = new CVector(Constants.iNodeDOFNo);  // End Node

        // 3D
        public CMatrix m_fAMatr3D = new CMatrix(Constants.iNodeDOFNo);
        public CMatrix m_fBMatr3D = new CMatrix(Constants.iNodeDOFNo);

        public CMatrix m_fKGlobM;  // (2x6)*(2x6)

        public CLoad m_ELoad;

        public CMaterial m_Mat = new CMaterial();
        public CCrSc m_CrSc /*= new CCrSc()*/; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        float m_GCS_X = 0f;
        float m_GCS_Y = 0f;
        float m_GCS_Z = 0f;

        float m_fAlpha;
        float m_fSinAlpha, m_fCosAlpha;

        // Constructor

        public CE_1D() 
        {
            // Create and fill elements base data

            // Fill Arrays / Initialize
            Fill_EDisp_Init();
            Fill_EEndsLoad_Init();
        }
        public CE_1D(CFemNode NStart, CFemNode NEnd, int iSuppType, CCrSc ECrSc)
        {
            // Create and fill elements base data

            // Nodes
            NodeStart = NStart;
            NodeEnd   = NEnd;
            // Support type
            m_iSuppType = iSuppType;

            // Cross-section
            m_CrSc = ECrSc;

            FillBasic2();

        } // End of constructor

        // Constructor 3
        public CE_1D(CMember TopoMember, CFemNode[] arrFemNodes)
        {
            // Main member or segment
            Member = TopoMember;
            ID = TopoMember.IMember_ID; // Temporary - TopoMember ID is same as FemMember
            // Nodes
            //m_NodeStart.CopyTopoNodetoFemNode(m_Member.INode1);
            //m_NodeEnd.CopyTopoNodetoFemNode(m_Member.INode2);

            // Nodes - temporary - nodes of Topo and Fem model are same
            // Search FEM nodes
            for (int i = 0; i < arrFemNodes.Length; i++)
            {
                if (Member.NodeStart.INode_ID == arrFemNodes[i].ID)
                    NodeStart = arrFemNodes[i];
            }

            for (int i = 0; i < arrFemNodes.Length; i++)
            {
                if (Member.NodeEnd.INode_ID == arrFemNodes[i].ID)
                    NodeEnd = arrFemNodes[i];
            }

            // Cross-section
            m_CrSc = TopoMember.CrSc;

            //FillBasic2_GeomMatrices();
            FillBasic2();
        } // End of constructor

        public void FillBasic2()
        {

            // Displacement
            // doplnit vektor premiestneni pruta - sklada sa z vektorov pre zac a konc. uzol
            // SMemberDisp sElemDisp;

            // Fill Element Nodes Displacement
            for (int i = 0; i < 12; i++)
            {
                if (i < 6)
                    m_ArrDisp.FVectorItems[i] = NodeStart.m_VDisp.FVectorItems[i];   // Fill with Start Node
                else
                    m_ArrDisp.FVectorItems[i] = NodeEnd.m_VDisp.FVectorItems[i - 6]; // Fill with End Node
            }

            // Element / Member load








            // Lengths in Global Coordinates
            m_flength_X = GetGCSLengh(0);
            m_flength_Y = GetGCSLengh(1);
            m_flength_Z = GetGCSLengh(2);

            // Lengths of member projection into GCS areas
            m_flength_XY = GetGCSProjLengh(m_flength_X, m_flength_Y);
            m_flength_YZ = GetGCSProjLengh(m_flength_Y, m_flength_Z);
            m_flength_XZ = GetGCSProjLengh(m_flength_X, m_flength_Z);

            // FEM Element Length
            FLength = (float)Math.Sqrt((float)Math.Pow(m_flength_X, 2f) + (float)Math.Pow(m_flength_Y, 2f) + (float)Math.Pow(m_flength_Z, 2f));

            // Temporary !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Member.FLength = FLength;

            m_fAlpha = GetGCSAlpha(1);
            m_fSinAlpha = (float)Math.Sin(m_fAlpha);
            m_fCosAlpha = (float)Math.Cos(m_fAlpha);

            // Get auxialiary point relative coordinates
            Get_C_GCS_coord();
            
            // 3D

            // Transformation Matrix of Element Rotation (12x12) - 3D
            m_fAMatr3D = Get_AMatr3D1();

            // Transformation Transfer Matrix - 3D

            m_fBMatr3D = Get_BMatr3D1();



            // Get local matrix acc. to end support/restraint of element

            // 3D
            switch (m_iSuppType)
            {
                case (int)EElemSuppType.e3DEl_000000_000000:
                    m_fkLocMatr = GetLocMatrix_3D_000000_000000();
                    break;
                case (int)EElemSuppType.e3DEl_000000_000___:
                case (int)EElemSuppType.e3DEl_000____000000:
                    m_fkLocMatr = GetLocMatrix_3D_000000_000___a(); // !!!!
                    break;
                case (int)EElemSuppType.e3DEl_000000_0_00_0:
                case (int)EElemSuppType.e3DEl_0_00_0_000000:
                    m_fkLocMatr = GetLocMatrix_3D_000000_0_00_0();
                    break;
                case (int)EElemSuppType.e3DEl_000000_______:
                case (int)EElemSuppType.e3DEl________000000:
                    m_fkLocMatr = GetLocMatrix_3D_000000_______(); // !!!!
                    break;

                default:
                    // Error
                    break;
            }

            // Check of partial matrices members

            // Partial matrices of global matrix of member 6 x 6
            // Console.WriteLine(GetPartM_k11(m_fkLocMatr,m_fAMatr3D).Print2DMatrix());

            // Return partial matrixes and global matrix of FEM element

            // 3D
            m_fKGlobM = fGetGlobM(
            GetPartM_k11(m_fkLocMatr, m_fAMatr3D),
            GetPartM_k12(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D),
            GetPartM_k21(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D),
            GetPartM_k22(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D)
            );
        }

        public void Fill_EDisp_Init()
        {
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eUX] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eUY] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eUZ] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRX] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRY] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ] = float.PositiveInfinity;

            // Tempoerary
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ + 1] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ + 2] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ + 3] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ + 4] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ + 5] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ + 6] = float.PositiveInfinity;
        }

        public void Fill_ECode_Init()
        {
            m_ArrCodeNo[(int)e3D_DOF.eUX] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eUY] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eUZ] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRX] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRY] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRZ] = int.MaxValue;

            // Temporary
            m_ArrCodeNo[(int)e3D_DOF.eRZ + 1] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRZ + 2] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRZ + 3] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRZ + 4] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRZ + 5] = int.MaxValue;
            m_ArrCodeNo[(int)e3D_DOF.eRZ + 6] = int.MaxValue;
        }

        public void Fill_EEndsLoad_Init()
        {
            // Start Node
            m_ArrElemPEF_LCS.m_fArrMembers[0, (int)e3D_E_F.eFX] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[0, (int)e3D_E_F.eFY] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[0, (int)e3D_E_F.eFZ] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[0, (int)e3D_E_F.eMX] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[0, (int)e3D_E_F.eMY] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[0, (int)e3D_E_F.eMZ] = 0f;

            // End Node
            m_ArrElemPEF_LCS.m_fArrMembers[1, (int)e3D_E_F.eFX] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[1, (int)e3D_E_F.eFY] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[1, (int)e3D_E_F.eFZ] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[1, (int)e3D_E_F.eMX] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[1, (int)e3D_E_F.eMY] = 0f;
            m_ArrElemPEF_LCS.m_fArrMembers[1, (int)e3D_E_F.eMZ] = 0f;
        }


        public float GetGCSLengh(float fCoordStart, float fCoordEnd, float fGCsCoord)
        {
            // Prebezne, neuvazuje s tym ze GCS Coord mozu byt zaporne alebo kladne
            if (
                ((fCoordEnd >= fGCsCoord) && (fCoordStart >= fGCsCoord)) || // Both positive
                 (fCoordEnd <= fGCsCoord && fCoordStart <= fGCsCoord) // Both negative
                )
            {
                return fCoordEnd - fCoordStart;  // if (fCoordEnd > fCoordStart) Positive length / if (fCoordStart > fCoordEnd) Negative length
            }
            else if (fCoordEnd <= fGCsCoord && fCoordStart >= fGCsCoord) // Start positive / End negative
            {
                return fCoordEnd - fCoordStart; // Negative length
            }
            else if (fCoordStart <= fGCsCoord && fCoordEnd >= fGCsCoord) // Start negative / End positive
            {
                return fCoordEnd - fCoordStart; // Poaitive length
            }
            else
            {
                // ?????????????
                return 0f;
            }
        
        }


        // Length of member in plane of projection into Global Coordinate System (GCS) 
        // Dlzka priemetu pruta do hlavneho suranicoveho systemu 

        public float GetGCSProjLengh(float fNStartCoord_i, float fNStartCoord_j, float fNEndCoord_i, float fNEndCoord_j, float fGCSLength)
        {
                return 0f;
        }
        public float GetGCSProjLengh(float fGCSlength_i, float fGCSlength_j)
        {
            // FEM Element Length in projection
            return (float)Math.Sqrt((float)Math.Pow(fGCSlength_i, 2f) + (float)Math.Pow(fGCSlength_j, 2f));
        }



        public float GetGCSLengh(int i)
        {
           // GLOBAL COORDINATE SYSTEM direction
            // 0 - Global X-Axis
            // 1 - Global Y-Axis
            // 2 - Global Z-Axis
            switch (i)
            {
                case 0:
                    return GetGCSLengh(NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX], m_GCS_X);
                case 1:
                    return GetGCSLengh(NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY], m_GCS_Y);
                case 2:
                    return GetGCSLengh(NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ], m_GCS_Z);
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

        public float GetGCSAlpha(int i)
        {
            // GLOBAL COORDINATE SYSTEM direction
            // 0 - Rotation about Global X-Axis
            // 1 - Rotation about Global Y-Axis
            // 2 - Rotation about Global Z-Axis
            switch (i)
            {
                case 0:
                    return GetGCSAlpha(NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY], NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ], m_GCS_Y, m_GCS_Z);
                case 1:
                    return GetGCSAlpha(NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX], NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ], m_GCS_X, m_GCS_Z);
                case 2:
                    return GetGCSAlpha(NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX], NodeStart.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY], NodeEnd.m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY], m_GCS_X, m_GCS_Y);
                default:
                    return 0f;
            }
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

            CMatrix RPart = new CMatrix (3);
            RPart.m_fArrMembers = RPart.fTransMatrix(m_flength_X, m_flength_Y, m_flength_Z, FLength, m_frotation_angle, m_fC_GCS_Coord);

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
            {  RPart.m_fArrMembers, MZero },
            {  MZero, RPart.m_fArrMembers }
            };
        }

        // Transformation Matrix of Element Rotation - 3D
        // 6x6
        private CMatrix Get_AMatr3D1()
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
 
            
            
            CMatrix RPart = new CMatrix(3,3);
            RPart.m_fArrMembers = RPart.fTransMatrix(m_flength_X, m_flength_Y, m_flength_Z, FLength, m_frotation_angle, m_fC_GCS_Coord);

            float flambda1 = RPart.m_fArrMembers[0, 0];
            float flambda2 = RPart.m_fArrMembers[1, 0];
            float flambda3 = RPart.m_fArrMembers[2, 0];

            float fmu1 = RPart.m_fArrMembers[0, 1];
            float fmu2 = RPart.m_fArrMembers[1, 1];
            float fmu3 = RPart.m_fArrMembers[2, 1];

            float fv1 = RPart.m_fArrMembers[0, 2];
            float fv2 = RPart.m_fArrMembers[1, 2];
            float fv3 = RPart.m_fArrMembers[2, 2];
            

            /*
             float[,] RPart = new float[3, 3]
                 {
                 {  flambda1,   fmu1,   fv1 },
                 {  flambda2,   fmu2,   fv2 },
                 {  flambda3,   fmu3,   fv3 }
                 };
             */

            CMatrix fM_output = new CMatrix(6);
            fM_output.m_fArrMembers = new float[6, 6]
                {
                {    flambda1,   fmu1,   fv1,       0f,     0f,    0f },
                {    flambda2,   fmu2,   fv2,       0f,     0f,    0f },
                {    flambda3,   fmu3,   fv3,       0f,     0f,    0f },
                {          0f,     0f,    0f, flambda1,   fmu1,   fv1 },
                {          0f,     0f,    0f, flambda2,   fmu2,   fv2 },
                {          0f,     0f,    0f, flambda3,   fmu3,   fv3 },
                };

            return fM_output;
        }





        // Transformation Transfer Matrix - 3D
        // 2x2 - 3x3
        private CMatrix Get_BMatr3D0()
        {
            // SubMatrix
            // Ksi (Xi) 
            CMatrix RXi = new CMatrix(3, 3);

            RXi.m_fArrMembers = new float[3,3]
            {
            {            0f,   -m_flength_Z,   -m_flength_Y },
            {   m_flength_Z,             0f,   -m_flength_X },
            {  -m_flength_Y,    m_flength_X,             0f }
            };

            CMatrix MZero = new CMatrix(3, 3);

            MZero.m_fArrMembers = new float[3, 3]
            {
            {  0f,   0f,   0f },
            {  0f,   0f,   0f },
            {  0f,   0f,   0f }
            };

            CMatrix EPart = new CMatrix(3, 3);

            EPart.m_fArrMembers = new float[3, 3]
            {
            {  1f,   0f,   0f },
            {  0f,   1f,   0f },
            {  0f,   0f,   1f }
            };

            // Local Stiffeness Matrix

            // Matrix 2x2 * 3x3
            CMatrix fM = new CMatrix(MatrixF.fChangeSignMatr(EPart), MZero, RXi, MatrixF.fChangeSignMatr(EPart));
            return fM;
        }


        // Transformation Transfer Matrix - 3D
        // 6x6
        private CMatrix Get_BMatr3D1()
        {
            CMatrix fM = new CMatrix(6);
            fM.m_fArrMembers = new float[6, 6]  
            {
            {           -1f,           0f,           0f,    0f,   0f,   0f },
            {            0f,          -1f,           0f,    0f,   0f,   0f },
            {            0f,           0f,          -1f,    0f,   0f,   0f },
            {            0f, -m_flength_Z,  m_flength_Y,   -1f,   0f,   0f },
            {   m_flength_Z,           0f, -m_flength_X,    0f,  -1f,   0f },
            {  -m_flength_Y,  m_flength_X,           0f,    0f,   0f,  -1f }
            };

            return fM;
        }

        private SGCSAngles Get_Angles()
        {
            SGCSAngles sAngles;

            // Angle between x0 and X
            // Angle between y0 and X
            // Angle between z0 and X
            sAngles.fAngleXx0 = Get_Angle1(m_flength_X, m_flength_X, m_flength_YZ);
            sAngles.fAngleXy0 = Get_Angle2(m_flength_X, m_flength_Y, m_flength_YZ);
            sAngles.fAngleXz0 = Get_Angle3(m_flength_X, m_flength_YZ);
            // Angle between x0 and Y
            // Angle between y0 and Y
            // Angle between z0 and Y
            sAngles.fAngleYx0 = Get_Angle3(m_flength_Y, m_flength_XZ);
            sAngles.fAngleYy0 = Get_Angle1(m_flength_Y, m_flength_X, m_flength_XZ);
            sAngles.fAngleYz0 = Get_Angle2(m_flength_Y, m_flength_Z, m_flength_XZ);
            // Angle between x0 and Z
            // Angle between y0 and Z
            // Angle between z0 and Z
            sAngles.fAngleZx0 = Get_Angle2(m_flength_Z, m_flength_X, m_flength_XY);
            sAngles.fAngleZy0 = Get_Angle3(m_flength_Z, m_flength_XY);
            sAngles.fAngleZz0 = Get_Angle1(m_flength_Z, m_flength_X, m_flength_XY);

            return sAngles; 
        }

        private float Get_Angle1(float fl_inGCSAxis, float fl_GCSAxis, float fl_inGCSPlane)
        {
            if (fl_inGCSPlane == 0f) // Paralel to the axis // Perpendicular to the plane
                return (float)Math.Acos(fl_inGCSAxis / FLength);
            else if (fl_inGCSAxis == 0f)  // Perpendicular to the axis // Paralel to the axis
            {
                if (fl_GCSAxis > 0f)
                    return 0f;
                else
                    return -(float)Math.PI; 
            }
            else // General
                return (float)Math.Acos(fl_inGCSPlane / FLength);
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
                return (float)Math.Acos(fl_inGCSPlane / FLength);
        }

        private float Get_Angle3(float fl_inGCSAxis, float fl_inGCSPlane)
        {
            if (fl_inGCSPlane == 0f || fl_inGCSAxis == 0f) // Paralel to the axis // Perpendicular to the plane
                return -(float)Math.PI / 2f;
            else
                return -(float)Math.Acos((fl_inGCSAxis / FLength) + (float)Math.PI / 2f);

        }

        // Get relative coordinates of node C(auxialiary point to specify orientation of local z-Axis) to node A (start node of member)

        void Get_C_GCS_coord()
        {
            // Default - parallel to the global Z-axis positive (upwards) 
            m_fC_GCS_Coord[0] = 0f;
            m_fC_GCS_Coord[1] = 0f;
            m_fC_GCS_Coord[2] = 1f; // possitive global and local z-Axis - upwards


            // Local z axis is paralel to global - member is in XY - plane
            if (m_flength_Z == 0f)
            {
                m_fC_GCS_Coord[0] = 0f;
                m_fC_GCS_Coord[1] = 0f;
                m_fC_GCS_Coord[2] = 1f; // possitive global and local z-Axis - upwards
            }
            // Local z axis is paralel to global XY - plane  member is in Z-Axis
            else if (m_flength_XY == 0f)
            {
                //Local z axis is in global X direction! // it could be also in Y or general
                if (m_flength_Z > 0f)   // local x axis - upwards
                {
                    m_fC_GCS_Coord[0] = -1f;
                    m_fC_GCS_Coord[1] = 0f;
                    m_fC_GCS_Coord[2] = 0f; 
                }
                else   // local x axis - downwards
                {
                    m_fC_GCS_Coord[0] = 1f;
                    m_fC_GCS_Coord[1] = 0f;
                    m_fC_GCS_Coord[2] = 0f;
                }
            }
        }








        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Definition of local stiffeness matrixes depending on loading and restraints
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region 3D_000000_000000
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - votknutie - 3D - spojite zatazenie
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private CMatrix GetLocMatrix_3D_000000_000000()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.FA_g / FLength;

            float f_EIy = m_Mat.m_fE * m_CrSc.FI_y;
            float f12EIy_len3 = (12f * f_EIy) / (float)Math.Pow(FLength, 3f);
            float f06EIy_len2 = (6f * f_EIy) / (float)Math.Pow(FLength, 2f);
            float f04EIy_len1 = (4f * f_EIy) / FLength;

            float f_EIz = m_Mat.m_fE * m_CrSc.FI_z;
            float f12EIz_len3 = (12f * f_EIz) / (float)Math.Pow(FLength, 3f);
            float f06EIz_len2 = (6f * f_EIz) / (float)Math.Pow(FLength, 2f);
            float f04EIz_len1 = (4f * f_EIz) / FLength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.FI_t / FLength;

            // Local Stiffeness Matrix
            CMatrix fM = new CMatrix(6);
            fM.m_fArrMembers= new float[6, 6]  
            {
            {  fEA_len,          0f,            0f,         0f,            0f,            0f },
            {       0f, f12EIz_len3,            0f,         0f,            0f,   f06EIz_len2 },
            {       0f,          0f,   f12EIy_len3,         0f,  -f06EIy_len2,            0f },
            {       0f,          0f,            0f,  fGIT_len1,            0f,            0f },
            {       0f,          0f,  -f06EIy_len2,         0f,   f04EIy_len1,            0f },
            {       0f, f06EIz_len2,            0f,         0f,            0f,   f04EIz_len1 }
            };

            return fM;
        }
        #endregion

        #region 3D_000000_000___a
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - valcovy klb / osamele bremeno - 3D / osamely krutiaci moment - 3D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private CMatrix GetLocMatrix_3D_000000_000___a()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.FA_g / FLength;
            float f_EIy = m_Mat.m_fE * m_CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(FLength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(FLength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / FLength;

            float f_EIz = m_Mat.m_fE * m_CrSc.FI_z;
            float f3EIz_len3 = (3f * f_EIz) / (float)Math.Pow(FLength, 3f);
            float f3EIz_len2 = (3f * f_EIz) / (float)Math.Pow(FLength, 2f);
            float f3EIz_len1 = (3f * f_EIz) / FLength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.FI_t / FLength;

            // Local Stiffeness Matrix
            CMatrix fM = new CMatrix(6);
            fM.m_fArrMembers = new float[6, 6]  
            {
            {  fEA_len,              0f,           0f,           0f,             0f,           0f },
            {       0f,      f3EIz_len3,           0f,           0f,             0f,   f3EIz_len2 },
            {       0f,              0f,   f3EIy_len3,           0f,    -f3EIy_len2,           0f },
            {       0f,              0f,           0f,     fGIT_len1,            0f,           0f },
            {       0f,              0f,  -f3EIy_len2,           0f,     f3EIy_len1,           0f },
            {       0f,      f3EIz_len2,           0f,           0f,             0f,   f3EIz_len1 }
            };

            return fM;
        }
        #endregion

        #region 3D_000000_000___b
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - valcovy klb / ohyb moment - 3D / skripta (bey tuhosti v kruteni)!!!!
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private CMatrix GetLocMatrix_3D_000000_000___b()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.FA_g / FLength;

            float f_EIy = m_Mat.m_fE * m_CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(FLength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(FLength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / FLength;

            float f_EIz = m_Mat.m_fE * m_CrSc.FI_z;
            float f3EIz_len3 = (3f * f_EIz) / (float)Math.Pow(FLength, 3f);
            float f3EIz_len2 = (3f * f_EIz) / (float)Math.Pow(FLength, 2f);
            float f3EIz_len1 = (3f * f_EIz) / FLength;

            // Local Stiffeness Matrix
            CMatrix fM = new CMatrix(6);
            fM.m_fArrMembers = new float[6, 6]  
            {
            {  fEA_len,             0f,           0f,     0f,         0f,           0f },
            {       0f,     f3EIz_len3,           0f,     0f,         0f,   f3EIz_len2 },
            {       0f,             0f,   f3EIy_len3,     0f,-f3EIy_len2,           0f },
            {       0f,             0f,           0f,     0f,         0f,           0f },
            {       0f,             0f,  -f3EIy_len2,     0f, f3EIy_len1,           0f },
            {       0f,     f3EIz_len2,           0f,     0f,         0f,   f3EIz_len1 }
            };

            return fM;
        }
        #endregion

        #region 3D_000000_0_00_0
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - vidlicove ulozenie - 3D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private CMatrix GetLocMatrix_3D_000000_0_00_0()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.FA_g / FLength;

            float f_EIy = m_Mat.m_fE * m_CrSc.FI_y;
            float f03EIy_len3 = (3f * f_EIy) / (float)Math.Pow(FLength, 3f);
            float f03EIy_len2 = (3f * f_EIy) / (float)Math.Pow(FLength, 2f);
            float f03EIy_len1 = (3f * f_EIy) / FLength;

            float f_EIz = m_Mat.m_fE * m_CrSc.FI_z;
            float f12EIz_len3 = (12f * f_EIz) / (float)Math.Pow(FLength, 3f);
            float f06EIz_len2 = (6f * f_EIz) / (float)Math.Pow(FLength, 2f);
            float f04EIz_len1 = (4f * f_EIz) / FLength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.FI_t / FLength;

            // Local Stiffeness Matrix
            CMatrix fM = new CMatrix(6);
            fM.m_fArrMembers = new float[6, 6]  
            {
            {  fEA_len,          0f,            0f,         0f,            0f,            0f },
            {       0f, f12EIz_len3,            0f,         0f,            0f,   f06EIz_len2 },
            {       0f,          0f,   f03EIy_len3,         0f,  -f03EIy_len2,            0f },
            {       0f,          0f,            0f,  fGIT_len1,            0f,            0f },
            {       0f,          0f,  -f03EIy_len2,         0f,   f03EIy_len1,            0f },
            {       0f, f06EIz_len2,            0f,         0f,            0f,   f04EIz_len1 }
            };

            return fM;
        }
        #endregion

        #region 3D_000000_______
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - volny koniec - konzola - 3D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private CMatrix GetLocMatrix_3D_000000_______()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.FA_g / FLength;
            float f_EIy = m_Mat.m_fE * m_CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(FLength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(FLength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / FLength;

            float f_EIz = m_Mat.m_fE * m_CrSc.FI_z;
            float f3EIz_len3 = (3f * f_EIz) / (float)Math.Pow(FLength, 3f);
            float f3EIz_len2 = (3f * f_EIz) / (float)Math.Pow(FLength, 2f);
            float f3EIz_len1 = (3f * f_EIz) / FLength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.FI_t / FLength;

            // Local Stiffeness Matrix
            CMatrix fM = new CMatrix(6);
            fM.m_fArrMembers= new float[6, 6]  
            {
            {  fEA_len,              0f,           0f,            0f,             0f,           0f },
            {       0f,      f3EIz_len3,           0f,            0f,             0f,   f3EIz_len2 },
            {       0f,              0f,   f3EIy_len3,            0f,    -f3EIy_len2,           0f },
            {       0f,              0f,           0f,     fGIT_len1,             0f,           0f },
            {       0f,              0f,  -f3EIy_len2,            0f,     f3EIy_len1,           0f },
            {       0f,      f3EIz_len2,           0f,            0f,             0f,   f3EIz_len1 }
            };

            return fM;
        }
        #endregion

        #region 3D_000____00___
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // posuvne ulozenie - valcovy klb / spojite rovnomerne zatazenie - 3D
        // najst podklady pre priecne zatazenie !!!! ????????????????????????????????????????????????????????????????
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private CMatrix GetLocMatrix_3D_000____00___()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.FA_g / FLength;
            float f_EIy = m_Mat.m_fE * m_CrSc.FI_y;
            float f3EIy_len3 = (3f * f_EIy * m_CrSc.FI_y) / (float)Math.Pow(FLength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(FLength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / FLength;

            float f_EIz = m_Mat.m_fE * m_CrSc.FI_z;
            float f3EIz_len3 = (3f * f_EIz * m_CrSc.FI_z) / (float)Math.Pow(FLength, 3f);

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.FI_t / FLength;

            // Local Stiffeness Matrix
            CMatrix fM = new CMatrix(6);
            fM.m_fArrMembers = new float[6, 6]  
            {
            {fEA_len,           0f,         0f,         0f,          0f,      0f },
            {       0f, f3EIz_len3,         0f,         0f,          0f,      0f },
            {       0f,         0f, f3EIy_len3,         0f, -f3EIy_len2,      0f }, 
            {       0f,         0f,         0f,  fGIT_len1,          0f,      0f },
            {       0f,         0f,-f3EIy_len2,         0f,  f3EIy_len1,      0f },
            {       0f,         0f,         0f,         0f,          0f,      0f }
            };

            return fM;
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
            return new CMatrix(fMk11, fMk12,fMk21, fMk22);
        }







        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Results
        // Get internal forces in global and local coordinate system
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Element Final End Forces GCS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Start Node Vector - 1 x 6
        // [EF_GCS i] = [ELoad i LCS] + [Kii] * [delta i] + [Kij] * [delta j]
        public void GetArrElemEF_GCS_StNode()
        {
            m_ArrElemEF_GCS_StNode =
                VectorF.fGetSum(
                m_ArrElemPEF_GCS_StNode,
                VectorF.fGetSum(
                VectorF.fMultiplyMatrVectr(GetPartM_k11(m_fkLocMatr, m_fAMatr3D), NodeStart.m_VDisp),
                VectorF.fMultiplyMatrVectr(GetPartM_k12(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D), NodeEnd.m_VDisp)
                )
                );
        }

        // End Node Vector - 1 x 6
        // [EF_GCS j] = [ELoad j LCS] + [Kji] * [delta i] + [Kjj] * [delta j]
        public void GetArrElemEF_GCS_EnNode()
        {
            m_ArrElemEF_GCS_EnNode =
                VectorF.fGetSum(
                m_ArrElemPEF_GCS_EnNode,
                VectorF.fGetSum(
                VectorF.fMultiplyMatrVectr(GetPartM_k21(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D), NodeStart.m_VDisp),
                VectorF.fMultiplyMatrVectr(GetPartM_k22(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D), NodeEnd.m_VDisp)
                )
                );
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Element Final End forces LCS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Start Node Vector - 1 x 6
        //  [EF_LCS i] = [A0] * [EF_GCS i]
        public void GetArrElemEF_LCS_StNode()
        {
            m_ArrElemEF_LCS_StNode = VectorF.fMultiplyMatrVectr(m_fAMatr3D, m_ArrElemEF_GCS_StNode);
        }

        // End Node Vector - 1 x 6
        // [EF_LCS j] = [A0] * [EF_GCS j]
        public void GetArrElemEF_LCS_EnNode()
        {
            m_ArrElemEF_LCS_EnNode = VectorF.fMultiplyMatrVectr(m_fAMatr3D, m_ArrElemEF_GCS_EnNode);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Element final internal forces in LCS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Start Node Vector - 1 x 6
        //  [IF_LCS i] = [-1,-1,-1,-1,-1,1] * [EF_LCS i]
        public void GetArrElemIF_LCS_StNode()
        {
            CVector fTempSignTransf = new CVector(6, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, 1.0f );
            m_ArrElemIF_LCS_StNode = VectorF.fMultiplyVectors(fTempSignTransf, m_ArrElemEF_LCS_StNode);
        }

        // End Node Vector - 1 x 6
        // [IF_LCS j]  = [ 1, 1, 1, 1, 1,-1] * [EF_LCS j]
        public void GetArrElemIF_LCS_EnNode()
        {
            CVector fTempSignTransf = new CVector(6, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, -1.0f);
            m_ArrElemIF_LCS_EnNode = VectorF.fMultiplyVectors(fTempSignTransf, m_ArrElemEF_LCS_EnNode);
        }



    }
}
