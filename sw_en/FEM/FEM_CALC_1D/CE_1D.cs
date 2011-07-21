using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;

namespace FEM_CALC_1D
{
    public struct SMemberDisp
    {
        //SNodeDisp s_f_St;  
        //SNodeDisp s_f_En;

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

    public class CE_1D : CMember
    {
        Constants c = new Constants();
        public CMatrix CM;

        public int m_iSuppType;
        // Geometrical properties of Element
        public float m_flength_X, m_flength_Y, m_flength_Z, m_frotation_angle = 0f;
        public float m_flength_XY, m_flength_YZ, m_flength_XZ;
        public float m_flength;

        public float[] m_fC_GCS_Coord = new float[3]; // Relative oordinate AC of auxiliary point C which define local member z-Axis orientation in global coordinate system of model
        
        public float[,] m_fkLocMatr;

        static int iNodeDOFNo = 6; // int ot static int !!!!

        // Vector of member displacement
        public float[] m_ArrDisp = new float[2*iNodeDOFNo];
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




        // 3D
        public float[,] m_fAMatr3D;
        public float[,] m_fBMatr3D;

        // 2D
        //float[,] m_fATRMatr2D;
        //float[,] m_fBTTMatr2D;
        public float[,][,]m_fKGlobM;

        public CFemNode m_NodeStart;
        public CFemNode m_NodeEnd;
        public CLoad m_ELoad;

        public CMaterial m_Mat;
        public CCrSc m_CrSc;

        float m_GCS_X = 0f;
        float m_GCS_Y = 0f;
        float m_GCS_Z = 0f;

        float m_fAlpha;
        float m_fSinAlpha, m_fCosAlpha;

        // Constructor

        public CE_1D() 
        {
            // Create and fill elements base data
            FillBasic1();

            // Fill Arrays / Initialize
            Fill_EDisp_Init();
            Fill_EEndsLoad_Init();
        }
        public CE_1D(CFemNode NStart, CFemNode NEnd, int iSuppType, CMaterial EMat, CCrSc ECrSc)
        {
            // Create and fill elements base data
            FillBasic1();

            // Nodes
            m_NodeStart = NStart;
            m_NodeEnd   = NEnd;
            // Support type
            m_iSuppType = iSuppType;

            // Material 
            m_Mat = EMat;
            // Cross-section
            m_CrSc = ECrSc;

            FillBasic2();

        } // End of constructor

        private void FillBasic1()
        {
            CM = new CMatrix();
        }

        public void FillBasic2()
        {

            // Displacement
            // doplnit vektor premiestneni pruta - sklada sa z vektorov pre zac a konc. uzol
            // SMemberDisp sElemDisp;

            // Fill Element Nodes Displacement
            for (int i = 0; i < 12; i++)
            {
                if (i < 6)
                    m_ArrDisp[i] = m_NodeStart.m_ArrDisp[i];   // Fill with Start Node
                else
                    m_ArrDisp[i] = m_NodeEnd.m_ArrDisp[i - 6]; // Fill with End Node
            }

            /*
            // Element start
            sElemDisp.s_fUX_St = NStart.m_sDisp.s_fUX;
            sElemDisp.s_fUY_St = NStart.m_sDisp.s_fUY;
            sElemDisp.s_fUZ_St = NStart.m_sDisp.s_fUZ;

            sElemDisp.s_fRX_St = NStart.m_sDisp.s_fRX;
            sElemDisp.s_fRY_St = NStart.m_sDisp.s_fRY;
            sElemDisp.s_fRZ_St = NStart.m_sDisp.s_fRZ;

            // Element end
            sElemDisp.s_fUX_En = NEnd.m_sDisp.s_fUX;
            sElemDisp.s_fUY_En = NEnd.m_sDisp.s_fUY;
            sElemDisp.s_fUZ_En = NEnd.m_sDisp.s_fUZ;

            sElemDisp.s_fRX_En = NEnd.m_sDisp.s_fRX;
            sElemDisp.s_fRY_En = NEnd.m_sDisp.s_fRY;
            sElemDisp.s_fRZ_En = NEnd.m_sDisp.s_fRZ;
             */



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
            m_flength = (float)Math.Sqrt((float)Math.Pow(m_flength_X, 2f) + (float)Math.Pow(m_flength_Y, 2f) + (float)Math.Pow(m_flength_Z, 2f));

            m_fAlpha = GetGCSAlpha(1);
            m_fSinAlpha = (float)Math.Sin(m_fAlpha);
            m_fCosAlpha = (float)Math.Cos(m_fAlpha);

            // Get auxialiary point relative coordinates
            Get_C_GCS_coord();


            // 2D
            // Transformation Matrix of Element Rotation - 2D
            /*m_fATRMatr2D = new float[3, 3]
            {
            {  m_fCosAlpha,     m_fSinAlpha,    0f },
            { -m_fSinAlpha,     m_fCosAlpha,    0f },
            {           0f,              0f,    1f }
            };*/


            // Transformation Transfer Matrix - 2D
            /*m_fBTTMatr2D = new float[3, 3]
            {
            {           -1f,              0f,    0f },
            {            0f,             -1f,    0f },
            {  -m_flength_Y,     m_flength_X,   -1f }
            };*/

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

            // 2D
            /*switch (iSuppType)
            {
                case 0:
                    m_fkLocMatr = GetLocMatrix_2D0();
                    break;
                case 1:
                    m_fkLocMatr = GetLocMatrix_2D1();
                    break;
                case 2:
                    m_fkLocMatr = GetLocMatrix_2D2();
                    break;
                case 3:
                    m_fkLocMatr = GetLocMatrix_2D3();
                    break;

                default:
                    // Error
                    break;
            }*/

            // Check of partial matrices members

            // Partial matrices of global matrix of member 6 x 6
            Console.WriteLine(CM.Print2DMatrix(GetPartM_k11(m_fkLocMatr, m_fAMatr3D), 6));





            // Return partial matrixes and global matrix of FEM element

            // 3D
            m_fKGlobM = fGetGlobM(
            GetPartM_k11(m_fkLocMatr, m_fAMatr3D),
            GetPartM_k12(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D),
            GetPartM_k21(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D),
            GetPartM_k22(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D)
            );




            // 2D
            /*
            m_fKGlobM = fGetGlobM(
            GetPartM_k11(m_fkLocMatr, m_fATRMatr2D),
            GetPartM_k12(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D),
            GetPartM_k21(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D),
            GetPartM_k22(m_fkLocMatr, m_fATRMatr2D, m_fBTTMatr2D)
            ); */


            

        }

        public void Fill_EDisp_Init()
        {
            m_ArrDisp[c.UX] = float.PositiveInfinity;
            m_ArrDisp[c.UY] = float.PositiveInfinity;
            m_ArrDisp[c.UZ] = float.PositiveInfinity;
            m_ArrDisp[c.RX] = float.PositiveInfinity;
            m_ArrDisp[c.RY] = float.PositiveInfinity;
            m_ArrDisp[c.RZ] = float.PositiveInfinity;

            // Tempoerary
            m_ArrDisp[c.RZ + 1] = float.PositiveInfinity;
            m_ArrDisp[c.RZ + 2] = float.PositiveInfinity;
            m_ArrDisp[c.RZ + 3] = float.PositiveInfinity;
            m_ArrDisp[c.RZ + 4] = float.PositiveInfinity;
            m_ArrDisp[c.RZ + 5] = float.PositiveInfinity;
            m_ArrDisp[c.RZ + 6] = float.PositiveInfinity;
        }

        public void Fill_ECode_Init()
        {
            m_ArrCodeNo[c.UX] = int.MaxValue;
            m_ArrCodeNo[c.UY] = int.MaxValue;
            m_ArrCodeNo[c.UZ] = int.MaxValue;
            m_ArrCodeNo[c.RX] = int.MaxValue;
            m_ArrCodeNo[c.RY] = int.MaxValue;
            m_ArrCodeNo[c.RZ] = int.MaxValue;

            // Temporary
            m_ArrCodeNo[c.RZ + 1] = int.MaxValue;
            m_ArrCodeNo[c.RZ + 2] = int.MaxValue;
            m_ArrCodeNo[c.RZ + 3] = int.MaxValue;
            m_ArrCodeNo[c.RZ + 4] = int.MaxValue;
            m_ArrCodeNo[c.RZ + 5] = int.MaxValue;
            m_ArrCodeNo[c.RZ + 6] = int.MaxValue;
        }

        public void Fill_EEndsLoad_Init()
        {
            // Start Node
            m_ArrElemPEF_LCS[0, c.FX] = 0f;
            m_ArrElemPEF_LCS[0, c.FY] = 0f;
            m_ArrElemPEF_LCS[0, c.FZ] = 0f;
            m_ArrElemPEF_LCS[0, c.MX] = 0f;
            m_ArrElemPEF_LCS[0, c.MY] = 0f;
            m_ArrElemPEF_LCS[0, c.MZ] = 0f;

            // End Node
            m_ArrElemPEF_LCS[1, c.FX] = 0f;
            m_ArrElemPEF_LCS[1, c.FY] = 0f;
            m_ArrElemPEF_LCS[1, c.FZ] = 0f;
            m_ArrElemPEF_LCS[1, c.MX] = 0f;
            m_ArrElemPEF_LCS[1, c.MY] = 0f;
            m_ArrElemPEF_LCS[1, c.MZ] = 0f;
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
                    return GetGCSLengh(m_NodeStart.FCoord_X, m_NodeEnd.FCoord_X, m_GCS_X);
                case 1:
                    return GetGCSLengh(m_NodeStart.FCoord_Y, m_NodeEnd.FCoord_Y, m_GCS_Y);
                case 2:
                    return GetGCSLengh(m_NodeStart.FCoord_Z, m_NodeEnd.FCoord_Z, m_GCS_Z);
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
                    return GetGCSAlpha(m_NodeStart.FCoord_Y, m_NodeEnd.FCoord_Y, m_NodeStart.FCoord_Z, m_NodeEnd.FCoord_Z, m_GCS_Y, m_GCS_Z);
                case 1:
                    return GetGCSAlpha(m_NodeStart.FCoord_X, m_NodeEnd.FCoord_X, m_NodeStart.FCoord_Z, m_NodeEnd.FCoord_Z, m_GCS_X, m_GCS_Z);
                case 2:
                    return GetGCSAlpha(m_NodeStart.FCoord_X, m_NodeEnd.FCoord_X, m_NodeStart.FCoord_Y, m_NodeEnd.FCoord_Y, m_GCS_X, m_GCS_Y);
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

            float[,] RPart = new float[3, 3];
            RPart = CM.fTransMatrix(m_flength_X, m_flength_Y, m_flength_Z, m_flength, m_frotation_angle, m_fC_GCS_Coord);
            

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

        // Transformation Matrix of Element Rotation - 3D
        // 6x6
        private float[,] Get_AMatr3D1()
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
            RPart = CM.fTransMatrix(m_flength_X, m_flength_Y, m_flength_Z, m_flength, m_frotation_angle, m_fC_GCS_Coord);

            float flambda1 = RPart[0, 0];
            float flambda2 = RPart[1, 0];
            float flambda3 = RPart[2, 0];

            float fmu1 = RPart[0, 1];
            float fmu2 = RPart[1, 1];
            float fmu3 = RPart[2, 1];

            float fv1 = RPart[0, 2];
            float fv2 = RPart[1, 2];
            float fv3 = RPart[2, 2];
            

            /*
             float[,] RPart = new float[3, 3]
                 {
                 {  flambda1,   fmu1,   fv1 },
                 {  flambda2,   fmu2,   fv2 },
                 {  flambda3,   fmu3,   fv3 }
                 };
             */

            return new float[6, 6]
                {
                {    flambda1,   fmu1,   fv1,       0f,     0f,    0f },
                {    flambda2,   fmu2,   fv2,       0f,     0f,    0f },
                {    flambda3,   fmu3,   fv3,       0f,     0f,    0f },
                {          0f,     0f,    0f, flambda1,   fmu1,   fv1 },
                {          0f,     0f,    0f, flambda2,   fmu2,   fv2 },
                {          0f,     0f,    0f, flambda3,   fmu3,   fv3 },
                };
        }





        // Transformation Transfer Matrix - 3D
        // 2x2 - 3x3
        private float[,][,] Get_BMatr3D0()
        {
            // SubMatrix
            // Ksi (Xi) 
            float[,] RXi = new float[3, 3]
            {
            {            0f,   -m_flength_Z,   -m_flength_Y },
            {   m_flength_Z,             0f,   -m_flength_X },
            {  -m_flength_Y,    m_flength_X,             0f }
            };

            float[,] MZero = new float[3, 3]
            {
            {  0f,   0f,   0f },
            {  0f,   0f,   0f },
            {  0f,   0f,   0f }
            };

            float[,] EPart = new float[3, 3]
            {
            {  1f,   0f,   0f },
            {  0f,   1f,   0f },
            {  0f,   0f,   1f }
            };

            // Local Stiffeness Matrix
            return new float[2, 2][,] 
            {
            {  CM.fChangeSignMatr(EPart),                    MZero },
            {                       RXi, CM.fChangeSignMatr(EPart) }
            };
        }


        // Transformation Transfer Matrix - 3D
        // 6x6
        private float[,] Get_BMatr3D1()
        {
            return new float[6, 6]  
            {
            {           -1f,           0f,           0f,    0f,   0f,   0f },
            {            0f,          -1f,           0f,    0f,   0f,   0f },
            {            0f,           0f,          -1f,    0f,   0f,   0f },
            {            0f, -m_flength_Z,  m_flength_Y,   -1f,   0f,   0f },
            {   m_flength_Z,           0f, -m_flength_X,    0f,  -1f,   0f },
            {  -m_flength_Y,  m_flength_X,           0f,    0f,   0f,  -1f }
            };

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

        #region 2D_000_000
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - votknutie 2D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_2D_000_000()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
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
        #region 3D_000000_000000
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - votknutie - 3D - spojite zatazenie
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_3D_000000_000000()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;

            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
            float f12EIy_len3 = (12f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f06EIy_len2 = (6f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f04EIy_len1 = (4f * f_EIy) / m_flength;

            float f_EIz = m_Mat.m_fE * m_CrSc.m_fIz;
            float f12EIz_len3 = (12f * f_EIz) / (float)Math.Pow(m_flength, 3f);
            float f06EIz_len2 = (6f * f_EIz) / (float)Math.Pow(m_flength, 2f);
            float f04EIz_len1 = (4f * f_EIz) / m_flength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.m_fI_T / m_flength;

            // Local Stiffeness Matrix
            return new float[6, 6]  
            {
            {  fEA_len,          0f,            0f,         0f,            0f,            0f },
            {       0f, f12EIz_len3,            0f,         0f,            0f,   f06EIz_len2 },
            {       0f,          0f,   f12EIy_len3,         0f,  -f06EIy_len2,            0f },
            {       0f,          0f,            0f,  fGIT_len1,            0f,            0f },
            {       0f,          0f,  -f06EIy_len2,         0f,   f04EIy_len1,            0f },
            {       0f, f06EIz_len2,            0f,         0f,            0f,   f04EIz_len1 }
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
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
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
        #region 3D_000000_000___a
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - valcovy klb / osamele bremeno - 3D / osamely krutiaci moment - 3D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_3D_000000_000___a()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_flength;

            float f_EIz = m_Mat.m_fE * m_CrSc.m_fIz;
            float f3EIz_len3 = (3f * f_EIz) / (float)Math.Pow(m_flength, 3f);
            float f3EIz_len2 = (3f * f_EIz) / (float)Math.Pow(m_flength, 2f);
            float f3EIz_len1 = (3f * f_EIz) / m_flength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.m_fI_T / m_flength;

            // Local Stiffeness Matrix
            return new float[6, 6]  
            {
            {  fEA_len,              0f,           0f,           0f,             0f,           0f },
            {       0f,      f3EIz_len3,           0f,           0f,             0f,   f3EIz_len2 },
            {       0f,              0f,   f3EIy_len3,           0f,    -f3EIy_len2,           0f },
            {       0f,              0f,           0f,     fGIT_len1,            0f,           0f },
            {       0f,              0f,  -f3EIy_len2,           0f,     f3EIy_len1,           0f },
            {       0f,      f3EIz_len2,           0f,           0f,             0f,   f3EIz_len1 }
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
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
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
        #region 3D_000000_000___b
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - valcovy klb / ohyb moment - 3D / skripta (bey tuhosti v kruteni)!!!!
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_3D_000000_000___b()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;

            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_flength;

            float f_EIz = m_Mat.m_fE * m_CrSc.m_fIz;
            float f3EIz_len3 = (3f * f_EIz) / (float)Math.Pow(m_flength, 3f);
            float f3EIz_len2 = (3f * f_EIz) / (float)Math.Pow(m_flength, 2f);
            float f3EIz_len1 = (3f * f_EIz) / m_flength;

            // Local Stiffeness Matrix
            return new float[6, 6]  
            {
            {  fEA_len,             0f,           0f,     0f,         0f,           0f },
            {       0f,     f3EIz_len3,           0f,     0f,         0f,   f3EIz_len2 },
            {       0f,             0f,   f3EIy_len3,     0f,-f3EIy_len2,           0f },
            {       0f,             0f,           0f,     0f,         0f,           0f },
            {       0f,             0f,  -f3EIy_len2,     0f, f3EIy_len1,           0f },
            {       0f,     f3EIz_len2,           0f,     0f,         0f,   f3EIz_len1 }
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
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
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
        #region 3D_000000_0_00_0
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - vidlicove ulozenie - 3D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_3D_000000_0_00_0()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;

            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
            float f03EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f03EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f03EIy_len1 = (3f * f_EIy) / m_flength;

            float f_EIz = m_Mat.m_fE * m_CrSc.m_fIz;
            float f12EIz_len3 = (12f * f_EIz) / (float)Math.Pow(m_flength, 3f);
            float f06EIz_len2 = (6f * f_EIz) / (float)Math.Pow(m_flength, 2f);
            float f04EIz_len1 = (4f * f_EIz) / m_flength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.m_fI_T / m_flength;

            // Local Stiffeness Matrix
            return new float[6, 6]  
            {
            {  fEA_len,          0f,            0f,         0f,            0f,            0f },
            {       0f, f12EIz_len3,            0f,         0f,            0f,   f06EIz_len2 },
            {       0f,          0f,   f03EIy_len3,         0f,  -f03EIy_len2,            0f },
            {       0f,          0f,            0f,  fGIT_len1,            0f,            0f },
            {       0f,          0f,  -f03EIy_len2,         0f,   f03EIy_len1,            0f },
            {       0f, f06EIz_len2,            0f,         0f,            0f,   f04EIz_len1 }
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
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
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
        #region 3D_000000_______
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // votknutie - volny koniec - konzola - 3D
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_3D_000000_______()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
            float f3EIy_len3 = (3f * f_EIy) / (float)Math.Pow(m_flength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_flength;

            float f_EIz = m_Mat.m_fE * m_CrSc.m_fIz;
            float f3EIz_len3 = (3f * f_EIz) / (float)Math.Pow(m_flength, 3f);
            float f3EIz_len2 = (3f * f_EIz) / (float)Math.Pow(m_flength, 2f);
            float f3EIz_len1 = (3f * f_EIz) / m_flength;

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.m_fI_T / m_flength;

            // Local Stiffeness Matrix
            return new float[6, 6]  
            {
            {  fEA_len,              0f,           0f,            0f,             0f,           0f },
            {       0f,      f3EIz_len3,           0f,            0f,             0f,   f3EIz_len2 },
            {       0f,              0f,   f3EIy_len3,            0f,    -f3EIy_len2,           0f },
            {       0f,              0f,           0f,     fGIT_len1,             0f,           0f },
            {       0f,              0f,  -f3EIy_len2,            0f,     f3EIy_len1,           0f },
            {       0f,      f3EIz_len2,           0f,            0f,             0f,   f3EIz_len1 }
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
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f3EIy_len3 = (3f * m_Mat.m_fE * m_CrSc.m_fIy) / (float)Math.Pow(m_flength, 3f);

            // Local Stiffeness Matrix
            return new float[3, 3]  
            {
            {fEA_len,          0f,    0f },
            {       0f, f3EIy_len3,   0f },
            {       0f,        0f,    0f }
            };
        }
        #endregion
        #region 3D_000____00___
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // posuvne ulozenie - valcovy klb / spojite rovnomerne zatazenie - 3D
        // najst podklady pre priecne zatazenie !!!! ????????????????????????????????????????????????????????????????
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private float[,] GetLocMatrix_3D_000____00___()
        {
            // Local Stiffeness Matrix Members
            float fEA_len = m_Mat.m_fE * m_CrSc.m_fAg / m_flength;
            float f_EIy = m_Mat.m_fE * m_CrSc.m_fIy;
            float f3EIy_len3 = (3f * f_EIy * m_CrSc.m_fIy) / (float)Math.Pow(m_flength, 3f);
            float f3EIy_len2 = (3f * f_EIy) / (float)Math.Pow(m_flength, 2f);
            float f3EIy_len1 = (3f * f_EIy) / m_flength;

            float f_EIz = m_Mat.m_fE * m_CrSc.m_fIz;
            float f3EIz_len3 = (3f * f_EIz * m_CrSc.m_fIz) / (float)Math.Pow(m_flength, 3f);

            float fGIT_len1 = m_Mat.m_fG * m_CrSc.m_fI_T / m_flength;

            // Local Stiffeness Matrix
            return new float[6, 6]  
            {
            {fEA_len,           0f,         0f,         0f,          0f,      0f },
            {       0f, f3EIz_len3,         0f,         0f,          0f,      0f },
            {       0f,         0f, f3EIy_len3,         0f, -f3EIy_len2,      0f }, 
            {       0f,         0f,         0f,  fGIT_len1,          0f,      0f },
            {       0f,         0f,-f3EIy_len2,         0f,  f3EIy_len1,      0f },
            {       0f,         0f,         0f,         0f,          0f,      0f }
            };
        }
        #endregion



















































        // GENERAL FEM OPERATIONS

        // Return partial matrix k11 of global matrix of FEM 1D element
        float[,] GetPartM_k11(float[,] fMk_0, float[,] fMA)
        {
            // [fMA]T * [fMk_0] * [fMA] 

            // Output Matrix
            return CM.fMultiplyMatr(CM.fMultiplyMatr(CM.GetTransMatrix(fMA), fMk_0), fMA);
        }

        // Return partial matrix k12 of global matrix of FEM 1D element
        float[,] GetPartM_k12(float[,] fMk_0, float[,] fMA, float[,] fMB)
        {
            // Output Matrix
            return CM.GetTransMatrix(CM.fMultiplyMatr(CM.fMultiplyMatr(fMB, CM.GetTransMatrix(fMA)), CM.fMultiplyMatr(fMk_0, fMA)));
        }

        // Return partial matrix k21 of global matrix of FEM 1D element
        float[,] GetPartM_k21(float[,] fMk_0, float[,] fMA, float[,] fMB)
        {
            // Output Matrix
            return CM.fMultiplyMatr(CM.fMultiplyMatr(fMB, CM.GetTransMatrix(fMA)), CM.fMultiplyMatr(fMk_0, fMA));
        }

        // Return partial matrix k22 of global matrix of FEM 1D element
        float[,] GetPartM_k22(float[,] fMk_0, float[,] fMA, float[,] fMB)
        {
            // Output Matrix
            return CM.fMultiplyMatr(fMB, CM.GetTransMatrix(CM.fMultiplyMatr(CM.fMultiplyMatr(fMB, CM.GetTransMatrix(fMA)), CM.fMultiplyMatr(fMk_0, fMA))));
        }

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
                CM.fGetSum(
                m_ArrElemPEF_GCS_StNode,
                CM.fGetSum(
                CM.fMultiplyMatr(GetPartM_k11(m_fkLocMatr, m_fAMatr3D), m_NodeStart.m_ArrDisp),
                CM.fMultiplyMatr(GetPartM_k12(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D), m_NodeEnd.m_ArrDisp)
                )
                );
        }

        // End Node Vector - 1 x 6
        // [EF_GCS j] = [ELoad j LCS] + [Kji] * [delta i] + [Kjj] * [delta j]
        public void GetArrElemEF_GCS_EnNode()
        {
            m_ArrElemEF_GCS_EnNode =
                CM.fGetSum(
                m_ArrElemPEF_GCS_EnNode,
                CM.fGetSum(
                CM.fMultiplyMatr(GetPartM_k21(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D), m_NodeStart.m_ArrDisp),
                CM.fMultiplyMatr(GetPartM_k22(m_fkLocMatr, m_fAMatr3D, m_fBMatr3D), m_NodeEnd.m_ArrDisp)
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
            m_ArrElemEF_LCS_StNode = CM.fMultiplyMatr(m_fAMatr3D, m_ArrElemEF_GCS_StNode);
        }

        // End Node Vector - 1 x 6
        // [EF_LCS j] = [A0] * [EF_GCS j]
        public void GetArrElemEF_LCS_EnNode()
        {
            m_ArrElemEF_LCS_EnNode = CM.fMultiplyMatr(m_fAMatr3D, m_ArrElemEF_GCS_EnNode);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Element final internal forces in LCS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Start Node Vector - 1 x 6
        //  [IF_LCS i] = [-1,-1,-1,-1,-1,1] * [EF_LCS i]
        public void GetArrElemIF_LCS_StNode()
        {
            int [] fTempSignTransf = new int[6] { -1, -1, -1, -1, -1, 1 };
            m_ArrElemIF_LCS_StNode = CM.fMultiplyMatr(fTempSignTransf, m_ArrElemEF_LCS_StNode);
        }

        // End Node Vector - 1 x 6
        // [IF_LCS j]  = [ 1, 1, 1, 1, 1,-1] * [EF_LCS j]
        public void GetArrElemIF_LCS_EnNode()
        {
            int[] fTempSignTransf = new int[6] { 1, 1, 1, 1, 1, -1 };
            m_ArrElemIF_LCS_EnNode = CM.fMultiplyMatr(fTempSignTransf, m_ArrElemEF_LCS_EnNode);
        }



    }
}
