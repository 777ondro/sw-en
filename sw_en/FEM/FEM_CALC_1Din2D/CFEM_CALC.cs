using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;
using BaseClasses;
using MATH;
using MATERIAL;
using CRSC;

namespace FEM_CALC_1Din2D
{
    public class CFEM_CALC
    {
        // Settings
        static int iNodeDOFNo = 3; // No warping effect (bimoment)

        CTest_1 TopoModelFile; // Create topological model file
        CGenex FEMModel;  // Create FEM model

        int m_iCodeNo; // Size of structure global matrix / without zero rows 
        public int[,] m_fDisp_Vector_CN;

        CMatrix m_M_K_Structure;
        public CVector m_V_Load;
        public CVector m_V_Displ;

        public CFEM_CALC()
        {
            // Load Topological model
            TopoModelFile = new CTest_1(); // Temporary
            
            // Generate FEM model data from Topological model
            // Prepare solver data
            // Fill local and global matrices of FEM elements

            FEMModel = new CGenex(TopoModelFile.TopoModel);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Temp - display matrices

            for(int i = 0; i < FEMModel.m_arrFemMembers.Length; i++)
            {
            // Member ID
            Console.WriteLine("Member ID: " + FEMModel.m_arrFemMembers[i].ID + "\n");
            // kij_0 - local stiffeness matrix       3 x  3
            Console.WriteLine("Local stiffeness matrix k" + FEMModel.m_arrFemMembers[i].NodeStart.ID + FEMModel.m_arrFemMembers[i].NodeEnd.ID + "0 - Dimensions: 3 x 3 \n");
            FEMModel.m_arrFemMembers[i].m_fkLocMatr.Print2DMatrixFormated();
            // A  Tranformation Rotation Matrixes    3 x  3
            Console.WriteLine("Tranformation rotation matrix A - Dimensions: 3 x 3 \n");
            FEMModel.m_arrFemMembers[i].m_fATRMatr2D.Print2DMatrixFormated();
            // B  Transfer Matrixes                  3 x  3
            Console.WriteLine("Transfer matrix B - Dimensions: 3 x 3 \n");
            FEMModel.m_arrFemMembers[i].m_fBTTMatr2D.Print2DMatrixFormated();
            // Kij - global matrix of member         6 x 6
            Console.WriteLine("Global stiffeness matrix K" + FEMModel.m_arrFemMembers[i].NodeStart.ID + FEMModel.m_arrFemMembers[i].NodeEnd.ID + "0 - Dimensions: 6 x 6 \n");
            FEMModel.m_arrFemMembers[i].m_fKGlobM.Print2DMatrixFormated_ABxCD(FEMModel.m_arrFemMembers[i].m_fKGlobM.m_fArrMembersABxCD);
            // Element Load Vector                   2 x 3
            Console.WriteLine("Member load vector - primary end forces in LCS at start node ID: " + FEMModel.m_arrFemMembers[i].NodeStart.ID + " - Dimensions: 3 x 1 \n");
            FEMModel.m_arrFemMembers[i].m_VElemPEF_LCS_StNode.Print1DVector();
            Console.WriteLine("Member load vector - primary end forces in LCS at end node ID: " + FEMModel.m_arrFemMembers[i].NodeEnd.ID + " - Dimensions: 3 x 1 \n");
            FEMModel.m_arrFemMembers[i].m_VElemPEF_LCS_EnNode.Print1DVector();
            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Set Global Code Numbers

            int m_iCodeNo = 0; // Number of unrestrained degrees of freedom - finally gives size of structure global matrix

            foreach (CFemNode i_CNode in FEMModel.m_arrFemNodes) // Each Node
            {
                for (int i = 0; i < iNodeDOFNo; i++)     // Each DOF
                {
                    if (i_CNode.m_ArrNodeDOF[i] != true)  // Perform for not restrained DOF
                    {
                        i_CNode.m_ArrNCodeNo[i] = m_iCodeNo; // Set global code number of degree of freedom (DOF)

                        m_iCodeNo++;
                    }
                }
            }

            // Fill members of structure global vector of displacement
            // Now we know number of not restrained DOF, so we can allocate array size
            m_fDisp_Vector_CN = new int[m_iCodeNo, 3]; // 1st - global DOF code number, 2nd - Node index, 3rd - local code number of DOF in NODE

            FillGlobalDisplCodeNo();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Set Global Code Number of Nodes / Nastavit globalne kodove cisla uzlov
            // Save indexes of nodes and DOF which are free and represent vector of uknown variables in solution 
            // Save it as array of arrays n x 2 (1st value is index - node index (0 - n-1) , 2nd value is DOF index (0-5)
            // n - total number of nodes in model
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SetNodesGlobCodeNo(); // Nastavi DOF v uzlov globalne kodove cisla ???

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Right side of Equation System
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Global Stiffeness Matrix of Structure - Allocate Memory (Matrix Size)
            m_M_K_Structure = new CMatrix(m_iCodeNo);

            // Fill Global Stiffeness Matrix
            FillGlobalMatrix();

            // Global Stiffeness Matrix    m_iCodeNo x  m_iCodeNo
            m_M_K_Structure.Print2DMatrix();






            // Auxialiary temporary transformation from 2D to 1D array / from float do double 
            // Pomocne prevody medzi jednorozmernym, dvojroymernym polom a triedom Matrix, 
            // bude nutne zladit a format v akom budeme pracovat s datami a potom zmazat 



            CArray objArray = new CArray();
            // Convert Size
            float[] m_M_K_fTemp1D = objArray.ArrTranf2Dto1D(m_M_K_Structure.m_fArrMembers);
            // Convert Type
            double[] m_M_K_dTemp1D = objArray.ArrConverFloatToDouble1D(m_M_K_fTemp1D);



            MatrixF64 objMatrix = new MatrixF64(4, 4, m_M_K_dTemp1D);
            // Print Created Matrix of MatrixF64 Class
            objMatrix.WriteLine();
            // Get Inverse Global Stiffeness Matrix
            MatrixF64 objMatrixInv = objMatrix.Inverse();
            // Print Inverse Matrix
            objMatrixInv.WriteLine();
            // Convert Type
            float[] m_M_K_Inv_fTemp1D = objArray.ArrConverMatrixF64ToFloat1D(objMatrixInv);
            // Inverse Global Stiffeness Matrix of Structure - Allocate Memory (Matrix Size)
            CMatrix m_M_K_Structure_Inv = new CMatrix(m_iCodeNo);
            m_M_K_Structure_Inv.m_fArrMembers = objArray.ArrTranf1Dto2D(m_M_K_Inv_fTemp1D);




            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Left side of Equation System
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Global Load Vector - Allocate Memory (Vector Size)
            m_V_Load = new CVector(m_iCodeNo);

            // Fill Global Load Vector
            FillGlobalLoadVector();

            // Display Global Load Vector
            m_V_Load.Print1DVector();





            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Solution - calculation of unknown displacement of nodes in GCS - system of linear equations
            // Start Solver
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Global Displacement Vector - Allocate Memory (Vector Size)
            m_V_Displ = new CVector(m_iCodeNo);

            // Fill Global Displacement Vector
            m_V_Displ = VectorF.fMultiplyMatrVectr(m_M_K_Structure_Inv, m_V_Load);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // End Solver
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Display Global Displacement Vector - solution result
            m_V_Displ.Print1DVector();

            // Set displacements and rotations of DOF in GCS to appropriate node DOF acc. to global code numbers
            for (int i = 0; i < m_iCodeNo; i++)
            {
                // Check if DOF is default (free - ) or has some initial value (settlement; soil consolidation etc.)
                // See default values - float.PositiveInfinity
                if (FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]].m_VDisp.FVectorItems[m_fDisp_Vector_CN[i, 2]] == float.PositiveInfinity)
                    FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]].m_VDisp.FVectorItems[m_fDisp_Vector_CN[i, 2]] = m_V_Displ.FVectorItems[i]; // set calculated
                else // some real initial value exists
                    FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]].m_VDisp.FVectorItems[m_fDisp_Vector_CN[i, 2]] += m_V_Displ.FVectorItems[i]; // add calculated (to sum)
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Get final end forces at element in global coordinate system GCS
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            for (int i = 0; i < FEMModel.m_arrFemMembers.Length; i++)
            {
                FEMModel.m_arrFemMembers[i].GetArrElemEF_GCS_StNode();
                Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + FEMModel.m_arrFemMembers[i].NodeStart.ID + "; " + "Start Node End Forces in GCS");
                FEMModel.m_arrFemMembers[i].m_VElemEF_GCS_StNode.Print1DVector();
                FEMModel.m_arrFemMembers[i].GetArrElemEF_GCS_EnNode();
                Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + FEMModel.m_arrFemMembers[i].NodeEnd.ID + "; " + "End Node End Forces in GCS");
                FEMModel.m_arrFemMembers[i].m_VElemEF_GCS_EnNode.Print1DVector();
                FEMModel.m_arrFemMembers[i].GetArrElemEF_LCS_StNode();
                Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + FEMModel.m_arrFemMembers[i].NodeStart.ID + "; " + "Start Node End Forces in LCS");
                FEMModel.m_arrFemMembers[i].m_VElemEF_LCS_StNode.Print1DVector();
                FEMModel.m_arrFemMembers[i].GetArrElemEF_LCS_EnNode();
                Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + FEMModel.m_arrFemMembers[i].NodeEnd.ID + "; " + "End Node End Forces in LCS");
                FEMModel.m_arrFemMembers[i].m_VElemEF_LCS_EnNode.Print1DVector();
                FEMModel.m_arrFemMembers[i].GetArrElemIF_LCS_StNode();
                Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + FEMModel.m_arrFemMembers[i].NodeStart.ID + "; " + "Start Node Internal Forces in LCS");
                FEMModel.m_arrFemMembers[i].m_VElemIF_LCS_StNode.Print1DVector();
                FEMModel.m_arrFemMembers[i].GetArrElemIF_LCS_EnNode();
                Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + FEMModel.m_arrFemMembers[i].NodeEnd.ID + "; " + "End Node Internal Forces in LCS");
                FEMModel.m_arrFemMembers[i].m_VElemIF_LCS_EnNode.Print1DVector();
            }
        } // End of Constructor




        /// <summary>
        ///  Functions and Methods
        /// </summary>


        void SetNodesGlobCodeNo()
        {
            // Set Global Code Number of Nodes / Nastavit globalne kodove cisla uzlov

            m_iCodeNo = 0;

            for (int i = 0; i < FEMModel.m_arrFemNodes.Length; i++)
            {
                if (FEMModel.m_arrFemNodes[i].m_ArrNodeDOF[(int)e2D_DOF.eUX] != true)
                {
                    FEMModel.m_arrFemNodes[i].m_ArrNCodeNo[(int)e2D_DOF.eUX] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    FEMModel.m_arrFemNodes[i].m_ArrNCodeNo[(int)e2D_DOF.eUX] = 0;

                if (FEMModel.m_arrFemNodes[i].m_ArrNodeDOF[(int)e2D_DOF.eUY] != true)
                {
                    FEMModel.m_arrFemNodes[i].m_ArrNCodeNo[(int)e2D_DOF.eUY] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    FEMModel.m_arrFemNodes[i].m_ArrNCodeNo[(int)e2D_DOF.eUY] = 0;

                if (FEMModel.m_arrFemNodes[i].m_ArrNodeDOF[(int)e2D_DOF.eRZ] != true)
                {
                    FEMModel.m_arrFemNodes[i].m_ArrNCodeNo[(int)e2D_DOF.eRZ] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    FEMModel.m_arrFemNodes[i].m_ArrNCodeNo[(int)e2D_DOF.eRZ] = 0;
            }
        }

        void FillGlobalDisplCodeNo()
        {
            m_iCodeNo = 0;

            foreach (CFemNode i_CNode in FEMModel.m_arrFemNodes) // Each Node
            {
                for (int i = 0; i < iNodeDOFNo; i++)     // Each DOF
                {
                    if (i_CNode.m_ArrNodeDOF[i] != true)       // Perform for not restrained DOF
                    {
                        m_fDisp_Vector_CN[m_iCodeNo, 0] = m_iCodeNo;                 // Add global code number index of degree of freedom (DOF)
                        m_fDisp_Vector_CN[m_iCodeNo, 1] = i_CNode.ID - 1;            // Add Node index !!! Node ID starts with 1
                        m_fDisp_Vector_CN[m_iCodeNo, 2] = i_CNode.m_ArrNCodeNo[i];   // Add local code number of degree of freedom (DOF) 0-5

                        m_iCodeNo++;

                        if (m_iCodeNo >= m_fDisp_Vector_CN.Length / 3)
                            continue;
                    }
                } 
            }
        }

        // Returns square stiffeness matrix of free DOF which are uknown in solution
        // Create matrix of code numbers or nodes (n - number of nodes) indexes and DOF indexes ((n-1) * 2) should be advantageous way for high-speed calculation

        void FillGlobalMatrix()
        {
            // i - Counter of global code number (which code number is actually filled)

            for (int i = 0; i < m_iCodeNo; i++) // For not restrained DOF of node (code number which is not empty) // number of row of global structure matrix into which we insert value
            {
                // Create temporary Arraylist of FEM elements which include node
                // List/collection which is defined in fem node class is used instead of next code line
                //ArrayList iElemTemp_Index = FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]].GetFoundedMembers_Index(FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]], FEMModel.m_arrFemMembers);

                for (int j = 0; j < m_iCodeNo; j++) // Fill each row of current DOF // number of column of global structure matrix into which we insert value
                {
                    float temp = 0f; // Temporary for sum of matrix values from all elements which transfer connected to the node for current DOF

                    for (int l = 0; l < FEMModel.m_arrFemNodes[i].m_iMemberCollection.Count; l++)  //  Sum all FEM Element Matrix members for given deggree of freedom of node
                    {
                        foreach (CE_1D El_Temp in FEMModel.m_arrFemMembers) // Search element - neefektivne prehladavat zase cele pole ked mame zoznam ID prvok ktore obsahuju uzol !!!!!
                        {
                            // Assign existing element from list to the temp element to get its global stifeness matrix members (6x6)
                            if (FEMModel.m_arrFemNodes[i].m_iMemberCollection.Contains(El_Temp.ID))
                            {
                                // CE_1D El_Temp = FEMModel.m_arrFemMembers[FEMModel.m_arrFemNodes[i].m_iMemberCollection.IndexOf(l)];

                                if (m_fDisp_Vector_CN[i, 1] == El_Temp.NodeStart.ID - 1) // Current DOF-row is on Start Node
                                {
                                    if (m_fDisp_Vector_CN[i, 1] == m_fDisp_Vector_CN[j, 1]) // Current DOF-row is in member of same Node as filled columns DOF - [0,0] - partial stiffeness matrix k_11 / k_aa 
                                    {
                                        temp += El_Temp.m_fKGlobM.m_fArrMembersABxCD[0, 0].m_fArrMembers[m_fDisp_Vector_CN[i, 2], m_fDisp_Vector_CN[j, 2]];
                                    }
                                    else  // [0,1] - partial stiffeness matrix k_12 / k_ab
                                    {
                                        temp += El_Temp.m_fKGlobM.m_fArrMembersABxCD[0, 1].m_fArrMembers[m_fDisp_Vector_CN[i, 2], m_fDisp_Vector_CN[j, 2]];
                                    }
                                }
                                else                                                     // Current DOF is on End Node
                                {
                                    if (m_fDisp_Vector_CN[i, 1] == m_fDisp_Vector_CN[j, 1]) // Current DOF-row is in member of same Node as filled columns DOF - [1,1] - partial stiffeness matrix k_22 / k_bb 
                                    {
                                        temp += El_Temp.m_fKGlobM.m_fArrMembersABxCD[1, 1].m_fArrMembers[m_fDisp_Vector_CN[i, 2], m_fDisp_Vector_CN[j, 2]];
                                    }
                                    else  // [1,0] - partial stiffeness matrix k_21 / k_ba
                                    {
                                        temp += El_Temp.m_fKGlobM.m_fArrMembersABxCD[1, 0].m_fArrMembers[m_fDisp_Vector_CN[i, 2], m_fDisp_Vector_CN[j, 2]];
                                    }
                                }
                            }
                        }
                    }

                    // Fill member of Global Stiffeness Matrix of Structure
                    m_M_K_Structure.m_fArrMembers[i, j] = temp;
                }
            }
        }





        void FillGlobalLoadVector()
        {
            for (int i = 0; i < m_iCodeNo; i++)
            {
                m_V_Load.FVectorItems[i] = 0f; // Set default value of variable

                // Fill Member of Global Load Vector of Structure
                // Node DOF load due to elements loads - sum of for array of elements

                // Create temporary Arraylist of FEM elements which include node
                ArrayList iElemTemp_Index = FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]].GetFoundedMembers_Index(FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]], FEMModel.m_arrFemMembers);

                float tempEl = 0f; // Temporary for sum of values from all elements which transfer connected to the node for current DOF

                for (int l = 0; l < iElemTemp_Index.Count; l++)  //  Sum all FEM Element Matrix members for given deggree of freedom of node
                {
                    // Assign existing element from list to the temp element  
                    CE_1D El_Temp = FEMModel.m_arrFemMembers[iElemTemp_Index.IndexOf(l)];

                    if (m_fDisp_Vector_CN[i, 1] == El_Temp.NodeStart.ID - 1) // If DOF is on Start Node of Element
                    {
                        // Temporary transposed transformation matrix of element rotation multiplied by load vector
                        /*float[] fTempNodeVector = El_Temp.CM.fMultiplyMatr(El_Temp.CM.GetTransMatrix(El_Temp.m_fAMatr3D), El_Temp.m_ArrElemPEF_GCS_StNode);*/
                        float[] fTempNodeVector = El_Temp.m_VElemPEF_GCS_StNode.FVectorItems;
                        // Add Value
                        tempEl += fTempNodeVector[m_fDisp_Vector_CN[i, 2]];
                    }
                    else                                                     // If DOF is on End Node of Element
                    {
                        // Temporary transposed transformation matrix of element rotation multiplied by load vector
                        /*float[] fTempNodeVector = El_Temp.CM.fMultiplyMatr(El_Temp.CM.GetTransMatrix(El_Temp.m_fAMatr3D), El_Temp.m_ArrElemPEF_GCS_EnNode);*/
                        float[] fTempNodeVector = El_Temp.m_VElemPEF_GCS_EnNode.FVectorItems;
                        // Add Value
                        tempEl += fTempNodeVector[m_fDisp_Vector_CN[i, 2]];
                    }

                }




                // Node DOF Load due to direct node loading

                float tempNode = FEMModel.m_arrFemNodes[m_fDisp_Vector_CN[i, 1]].m_VDirNodeLoad.FVectorItems[m_fDisp_Vector_CN[i, 2]];


                // Sum loads

                m_V_Load.FVectorItems[i] = tempNode - tempEl;

            }
        }

























    }
}
