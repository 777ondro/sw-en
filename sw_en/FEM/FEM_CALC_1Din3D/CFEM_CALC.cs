using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;

/*
 Todo
  
 INPUT / DATA
  
 1. Member Relases - Klby na prutoch (kompletne dopracovat)
 2. Stiffeness matrices for various restraints conditions
 3. Add loading vectors for various loading types and restraints conditions
 4. Geometry / various axial systems / correct direction cosine and length for member 
    if node are in varous quadrant 
    (opravit vypocet dlzok a smerovych kosinusov 
    ak su uzly elementu v roznych kvadrantoch)
 5. Definicia pootocenia pruta - suradnice bodu C urcujuceho rotaciu - momentalne velmi zjednodusene
 6. Otestovat znamienka zatazeni !!! podla orientacie pruta a podobne

 



 FEM
 1. Rozhodnut ci budu stvorcove matice vsetky typu matrix, co ma ostat float, podobne vektory
    upravit rozmery poli, asi bude najlepsie vychadzat z nasobkov "6" - pocet stupnov volnosti uzla v priestore
    zjednotit triedy a funkcie MATRIX a VECTOR, vyrobit triedy pre matematiku 
 
 2. Da sa u matic vyuzit symetria - trojuholnikova matica - redukcia poctu prvkov
 3. Generovanie lokalnych a globalnych matic pruta previest na paralelne  - moze sa pocitat pre viacero prutov naraz
 4. Vylepsit generovanie globalnej tuhostnej matice konstrukcie podla kodovych cisel
    Pokial mozno vymysliet zrychlenie tvorby inverznej matice a paralelne riesenie sustavy linearnych rovnic
 5. Implementovat do vypoctu zatazenie ktore je zadane priamo v uzloch
 6. Ukladanie vysledkov do databazy ???? Priemezne ak su uz spocitane vsetky potrebne DOF ???



  Rozsirit na obecnu metodu - mozna zmena dlzky pruta (uplna tuhostna matica)
  Rozsirit o 7 stupen volnosti pruta - deplanaciu
  Jednotny system hlaviciek funkcii , nazvoslovia, nazvov trieda a premenych a dalsej upravy kodu
 

  Vela ukonov sa da robit paralelne, napriklad tvorba jednotlivych matic prutov
  Tvorba tuhostnej matice a zatazovacej matice
  Tvorba matic s priebehmi vyslednych vnutornych sil, neyavisle pre jednotlive elementy a pruty 


  Asi by bolo rozumnejsie nealokovat cele matice ale na zaklade kodovych cisel vytvarat 
  len konretne prvky (resp. riadky / stlpce) matic s ktorymi sa naozaj pracuje, aby nebolo v pamati mnozstvo poli, ktore sa pouziju ovela neskor - napriklad urcenie vyslednych priebehov 
  vnutornych sil IF v GCS aj LCS 





*/

namespace FEM_CALC_1Din3D
{
    public class CFEM_CALC
    {
        // Settings
        static int iNodeDOFNo = 6; // No warping effect (bimoment)

        EGCS eGCS = EGCS.eGCSLeftHanded; // Global coordinate system
        ESLN eSLN = ESLN.e3DD_1D;        // Solution type - define n-dimesional space and members

        static int iNNoTot = 4;
        static int iElemNoTot = 3;
        int m_iCodeNo; // Size of structure global matrix / without zero rows 

        CFemNode[] m_NodeArray = new CFemNode[iNNoTot];
        CE_1D[] m_ELemArray = new CE_1D[iElemNoTot];
        CLoad[] m_ELoadArray = new CLoad[iElemNoTot];


        // Material
        CMaterial m_Mat = new CMaterial();

        // Cross-section
        CCrSc m_CrSc = new CCrSc();

        public int[,] m_fDisp_Vector_CN;

        CMatrix m_M_K_Structure;
        public CVector  m_V_Load;
        public CVector  m_V_Displ;

        
        
        
        
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // TEMPORARY EXAMPLE DATA

        // Use basic SI units

        // Load
        float m_fF = 17000f;   // Unit [N]
        float m_fM = 20000f;   // Unit [Nm]
        float m_fq =  5000f;   // Unit [N/m]

        // Geometry
        float m_fGeom_a = 4f,
              m_fGeom_b = 5f,
              m_fGeom_c = 3.5f;     // Unit [m]

        // Material properties
        /*
        float m_fE  = 2.1e+11f;          // Unit [Pa]
        float m_fnu = 0.3f;              // Unit [-]
        float m_fG  = 0.8076923e+11f;    // Unit [Pa]
        */

        // Cross-section properties / Geometrical data
        CRSC.Enums.ECrScShType1 m_eCrScType = CRSC.Enums.ECrScShType1.eCrScType_I;
 





        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        // Consructor
        public CFEM_CALC()
        {

            // Define Nodes Properties

            for (int i = 0; i < iNNoTot; i++)
            {
                // Create auxiliary Node object
                CFemNode CNode_i = new CFemNode();

                // Fill array object item with auxliary Node
                m_NodeArray[i] = CNode_i;
            }

            // Node 1
            m_NodeArray[0].ID = 1;
            m_NodeArray[0].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUX] = m_fGeom_a;
            m_NodeArray[0].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUY] = 0f;
            m_NodeArray[0].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUZ] = 0f;

            // Node 2
            m_NodeArray[1].ID = 2;
            m_NodeArray[1].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUX] = 0f;
            m_NodeArray[1].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUY] = 0f;
            m_NodeArray[1].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUZ] = 0f;

            // Node 3
            m_NodeArray[2].ID = 5;
            m_NodeArray[2].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUX] = m_fGeom_a;
            m_NodeArray[2].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUY] = 0f;
            m_NodeArray[2].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUZ] = -m_fGeom_c;

            // Node 4
            m_NodeArray[3].ID = 3;
            m_NodeArray[3].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUX] = m_fGeom_a;
            m_NodeArray[3].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUY] = -m_fGeom_b;
            m_NodeArray[3].m_fVNodeCoordinates.FVectorItems[(int) e3D_DOF.eUZ] = 0f;

            // Set Nodal Supports (for restraint set 0f)
            // Node 1

            // Node 2
            m_NodeArray[1].m_VDisp.FVectorItems[0] = 0f;
            m_NodeArray[1].m_VDisp.FVectorItems[1] = 0f;
            m_NodeArray[1].m_VDisp.FVectorItems[2] = 0f;
            m_NodeArray[1].m_VDisp.FVectorItems[3] = 0f;
            m_NodeArray[1].m_VDisp.FVectorItems[4] = 0f;
            m_NodeArray[1].m_VDisp.FVectorItems[5] = 0f;

            // Node 3
            m_NodeArray[2].m_VDisp.FVectorItems[0] = 0f;
            m_NodeArray[2].m_VDisp.FVectorItems[1] = 0f;
            m_NodeArray[2].m_VDisp.FVectorItems[2] = 0f;
            m_NodeArray[2].m_VDisp.FVectorItems[3] = 0f;
            m_NodeArray[2].m_VDisp.FVectorItems[4] = 0f;
            m_NodeArray[2].m_VDisp.FVectorItems[5] = 0f;

            // Node 4
            m_NodeArray[3].m_VDisp.FVectorItems[0] = 0f;
            m_NodeArray[3].m_VDisp.FVectorItems[1] = 0f;
            m_NodeArray[3].m_VDisp.FVectorItems[2] = 0f;
            m_NodeArray[3].m_VDisp.FVectorItems[3] = 0f;
            m_NodeArray[3].m_VDisp.FVectorItems[4] = 0f;
            m_NodeArray[3].m_VDisp.FVectorItems[5] = 0f;

            // Set Global Code Numbers

            int m_iCodeNo = 0; // Number of restrained degrees of freedom - finally gives size of structure global matrix

            foreach (CFemNode i_CNode in m_NodeArray) // Each Node
            {
                for (int i = 0; i < iNodeDOFNo; i++)     // Each DOF
                {
                    if (i_CNode.m_VDisp.FVectorItems[i] != 0)  // Perform for not restrained DOF
                    {
                        i_CNode.m_ArrNCodeNo[i] = m_iCodeNo; // Set global code number of degree of freedom (DOF)

                        m_iCodeNo++;
                    }
                }
            }

            // Fill members of structure global vector of displacement
            // Now we know number of not restrained DOF, so we can allocate array size
            m_fDisp_Vector_CN = new int[m_iCodeNo,3]; // 1st - global DOF code number, 2nd - Node index, 3rd - local code number of DOF in NODE

            FillGlobalDisplCodeNo();


            ////////////////////////////////////////////////////////////////////////////////////
            // Set Nodal Loads (acting directly in nodes)


            ////////////////////////////////////////////////////////////////////////////////////
            // !!!!!! No kind of these loads actually









            ///////////////////////////////////////////////////////////////////////////////////////
            // Define Cross-section
            ///////////////////////////////////////////////////////////////////////////////////////
            
            // Fill CrSc properties
            m_CrSc.CalcProp_RecSol(m_eCrScType);


            ///////////////////////////////////////////////////////////////////////////////////////
            // Define FEM 1D elements
            ///////////////////////////////////////////////////////////////////////////////////////



            for (int i = 0; i < iElemNoTot; i++)
            {
                // Create auxiliary Element object
                CE_1D CElement_i = new CE_1D();
                // Fill array object item
                m_ELemArray[i] = CElement_i;

                // Create auxiliary Element Load Object
                CLoad CLoad_i = new CLoad();
                // Fill array object item
                m_ELoadArray[i] = CLoad_i;
            }

            // Member 1 [0] Nodes 1 - 2 ([0] [1]) 
            m_ELemArray[0].NodeStart = m_NodeArray[0];
            m_ELemArray[0].NodeEnd = m_NodeArray[1];
            // Element  Type
            m_ELemArray[0].m_iSuppType = (int)EElemSuppType.e3DEl_000000_000000;
            // Element Material
            m_ELemArray[0].m_Mat = m_Mat;
            // Element Corss-section
            m_ELemArray[0].m_CrSc = m_CrSc;
            // Fill Basic Element Data
            m_ELemArray[0].FillBasic2();
            // Load of Element only due to Element Transversal Forces
            m_ELoadArray[0].GetEndLoad_g(m_ELemArray[0], m_fq);
            // Output
            // kij_0 - local stiffeness matrix       6 x  6
            m_ELemArray[0].m_fkLocMatr.Print2DMatrixFormated();
            // A  Tranformation Rotation Matrixes    6 x  6
            m_ELemArray[0].m_fAMatr3D.Print2DMatrixFormated();
            // B  Transfer Matrixes                  6 x  6
            m_ELemArray[0].m_fBMatr3D.Print2DMatrixFormated();
            // Kij - global matrix of member        12 x 12
            m_ELemArray[0].m_fKGlobM.Print2DMatrixFormated();
            // Element Load Vector                   2 x  6
            m_ELemArray[0].m_ArrElemPEF_LCS.Print2DMatrixFormated();

            
            #region MATRIX TEST
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEST
            /*
            float[,] farrk11 = new float[6, 6];
            float[,] farrk12 = new float[6, 6];
            float[,] farrk21 = new float[6, 6];
            float[,] farrk22 = new float[6, 6];

            // Fill array
            for (int i = 0; i < 6; i++)
            {
               for (int j = 0; j < 6; j++)
                {
                    farrk11[i, j] = ((i+1)*10)+1 + j;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    farrk12[i, j] = ((i + 1) * 10) + 1 + j;
                }
            }


            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    farrk21[i, j] = ((i + 1) * 10) + 1 + j;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    farrk22[i, j] = ((i + 1) * 10) + 1 + j;
                }
            }

            float[,][,] farrK = new float[2, 2][,]
                    {{farrk11, farrk12},
                    {farrk21, farrk22}};

            Console.WriteLine(m_ELemArray[0].CM.Print2DMatrix(farrK, 2, 6));
            */
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
             
             

            // Member 2 [1] Nodes 1 - 3 ([0] [2])
            m_ELemArray[1].NodeStart = m_NodeArray[0];
            m_ELemArray[1].NodeEnd = m_NodeArray[2];
            // Element  Type
            m_ELemArray[1].m_iSuppType = (int)EElemSuppType.e3DEl_000000_000000;
            // Element Material
            m_ELemArray[1].m_Mat = m_Mat;
            // Element Corss-section
            m_ELemArray[1].m_CrSc = m_CrSc;
            // Fill Basic Element Data
            m_ELemArray[1].FillBasic2();
            // Load of Element only due to Element Transversal Forces
            m_ELoadArray[1].GetEndLoad_F(m_ELemArray[1], 0f, 0f, m_fF);
            // Output
            // kij_0 - local stiffeness matrix       6 x  6
           m_ELemArray[1].m_fkLocMatr.Print2DMatrixFormated();
            // A  Tranformation Rotation Matrixes    6 x  6
            m_ELemArray[1].m_fAMatr3D.Print2DMatrixFormated();
            // B  Transfer Matrixes                  6 x  6
            m_ELemArray[1].m_fBMatr3D.Print2DMatrixFormated();
            // Kij - global matrix of member        12 x 12
            m_ELemArray[1].m_fKGlobM.Print2DMatrixFormated();
            // Element Load Vector                   2 x  6
            m_ELemArray[1].m_ArrElemPEF_LCS.Print2DMatrixFormated();


            // Member 3 [2] Nodes 1 - 4 ([0] [3])
            m_ELemArray[2].NodeStart = m_NodeArray[0];
            m_ELemArray[2].NodeEnd = m_NodeArray[3];
            // Element  Type
            m_ELemArray[2].m_iSuppType = (int)EElemSuppType.e3DEl_000000_000___;
            // Element Material
            m_ELemArray[2].m_Mat = m_Mat;
            // Element Corss-section
            m_ELemArray[2].m_CrSc = m_CrSc;
            // Fill Basic Element Data
            m_ELemArray[2].FillBasic2();
            // Load of Element only due to Element Transversal Forces
            m_ELoadArray[2].GetEndLoad_M(m_ELemArray[2], m_fM, 0f, 0f);
            // Output
            // kij_0 - local stiffeness matrix       6 x  6
            m_ELemArray[2].m_fkLocMatr.Print2DMatrixFormated();
            // A  Tranformation Rotation Matrixes    6 x  6
            m_ELemArray[2].m_fAMatr3D.Print2DMatrixFormated();
            // B  Transfer Matrixes                  6 x  6
            m_ELemArray[2].m_fBMatr3D.Print2DMatrixFormated();
            // Kij - global matrix of member        12 x 12
            m_ELemArray[2].m_fKGlobM.Print2DMatrixFormated();
            // Element Load Vector                   2 x  6
            m_ELemArray[2].m_ArrElemPEF_LCS.Print2DMatrixFormated();

            /*
            // Nodal loads (sum nodal loads and nodal loads due to element loads)
            m_NodeArray[2].m_sLoad.s_fFZ += -55000; // Add local nodal load to element ends loads due to intermediate load
            */








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



            MatrixF64 objMatrix = new MatrixF64(6, 6, m_M_K_dTemp1D);
            // Print Created Matrix of MatrixF64 Class
            objMatrix.WriteLine();
            // Get Inverse Global Stiffeness Matrix
            MatrixF64 objMatrixInv =  objMatrix.Inverse();
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
            
            // Display Global Displacemnt Vector - solution result
            m_V_Displ.Print1DVector();

            // Set displacements and rotations of DOF in GCS to appropriate node DOF acc. to global code numbers
            for (int i = 0; i < m_iCodeNo; i++)
            {
                // Check if DOF is default (free - ) or has some initial value (settlement; soil consolidation etc.)
                // See Fill_NDisp_InitStr() for default values - float.PositiveInfinity
                if (m_NodeArray[m_fDisp_Vector_CN[i, 1]].m_VDisp.FVectorItems[m_fDisp_Vector_CN[i, 2]] == float.PositiveInfinity)
                    m_NodeArray[m_fDisp_Vector_CN[i, 1]].m_VDisp.FVectorItems[m_fDisp_Vector_CN[i, 2]] = m_V_Displ.FVectorItems[i]; // set calculated
                else // some real initial value exists
                    m_NodeArray[m_fDisp_Vector_CN[i, 1]].m_VDisp.FVectorItems[m_fDisp_Vector_CN[i, 2]] += m_V_Displ.FVectorItems[i]; // add calculated (to sum)
            }


                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Get final end forces at element in global coordinate system GCS
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                for (int i = 0; i < iElemNoTot; i++)
                {
                    m_ELemArray[i].GetArrElemEF_GCS_StNode();
                    Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + m_ELemArray[i].NodeStart.ID + "; " + "Start Node End Forces in GCS");
                    m_ELemArray[i].m_ArrElemEF_GCS_StNode.Print1DVector();
                    m_ELemArray[i].GetArrElemEF_GCS_EnNode();
                    Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + m_ELemArray[i].NodeEnd.ID + "; " + "End Node End Forces in GCS");
                    m_ELemArray[i].m_ArrElemEF_GCS_EnNode.Print1DVector();
                    m_ELemArray[i].GetArrElemEF_LCS_StNode();
                    Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + m_ELemArray[i].NodeStart.ID + "; " + "Start Node End Forces in LCS");
                    m_ELemArray[i].m_ArrElemEF_LCS_StNode.Print1DVector();
                    m_ELemArray[i].GetArrElemEF_LCS_EnNode();
                    Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + m_ELemArray[i].NodeEnd.ID + "; " + "End Node End Forces in LCS");
                    m_ELemArray[i].m_ArrElemEF_LCS_EnNode.Print1DVector();
                    m_ELemArray[i].GetArrElemIF_LCS_StNode();
                    Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + m_ELemArray[i].NodeStart.ID + "; " + "Start Node Internal Forces in LCS");
                    m_ELemArray[i].m_ArrElemIF_LCS_StNode.Print1DVector();
                    m_ELemArray[i].GetArrElemIF_LCS_EnNode();
                    Console.WriteLine("Element Index No.: " + i + "; " + "Node No.: " + m_ELemArray[i].NodeEnd.ID + "; " + "End Node Internal Forces in LCS");
                    m_ELemArray[i].m_ArrElemIF_LCS_EnNode.Print1DVector();
                }
        } // End of Constructor









        /// <summary>
        ///  Functions and Methods
        /// </summary>
        

        void SetNodesGlobCodeNo()
        {
            // Set Global Code Number of Nodes / Nastavit globalne kodove cisla uzlov

            m_iCodeNo = 0;

            for (int i = 0; i < iNNoTot; i++)
            {
                if (m_NodeArray[i].m_VDisp.FVectorItems[(int)ENSupportType.eNST_Ux] != 0f)
                {
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Ux] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Ux] = 0;

                if (m_NodeArray[i].m_VDisp.FVectorItems[(int)ENSupportType.eNST_Uy] != 0f)
                {
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Uy] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Uy] = 0;

                if (m_NodeArray[i].m_VDisp.FVectorItems[(int)ENSupportType.eNST_Uz]!= 0f)
                {
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Uz] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Uz] = 0;

                if (m_NodeArray[i].m_VDisp.FVectorItems[(int)ENSupportType.eNST_Rx]!= 0f)
                {
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Rx] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Rx] = 0;

                if (m_NodeArray[i].m_VDisp.FVectorItems[(int)ENSupportType.eNST_Ry] != 0f)
                {
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Ry] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Ry] = 0;

                if (m_NodeArray[i].m_VDisp.FVectorItems[(int)ENSupportType.eNST_Rz] != 0f)
                {
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Rz] = m_iCodeNo;
                    m_iCodeNo++;
                }
                else
                    m_NodeArray[i].m_ArrNCodeNo[(int)ENSupportType.eNST_Rz] = 0;

            }
        }

        void FillGlobalDisplCodeNo()
        {
            m_iCodeNo = 0;

            foreach (CFemNode i_CNode in m_NodeArray) // Each Node
            {
                for (int i = 0; i < iNodeDOFNo; i++)     // Each DOF
                {
                    if (i_CNode.m_VDisp.FVectorItems[i] != 0f)       // Perform for not restrained DOF
                    {
                        m_fDisp_Vector_CN[m_iCodeNo, 0] = m_iCodeNo;                 // Add global code number index of degree of freedom (DOF)
                        m_fDisp_Vector_CN[m_iCodeNo, 1] = i_CNode.ID - 1;        // Add Node index !!! Node ID starts with 1
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
                ArrayList iElemTemp_Index = m_NodeArray[m_fDisp_Vector_CN[i, 1]].GetFoundedMembers_Index(m_NodeArray[m_fDisp_Vector_CN[i, 1]], m_ELemArray, iElemNoTot);

                for (int j = 0; j < m_iCodeNo; j++) // Fill each row of current DOF // number of column of global structure matrix into which we insert value
                {
                    float temp = 0f; // Temporary for sum of matrix values from all elements which transfer connected to the node for current DOF

                    for (int l = 0; l < iElemTemp_Index.Count; l++)  //  Sum all FEM Element Matrix members for given deggree of freedom of node
                    {
                        // Assign existing element from list to the temp element to get its global stifeness matrix members (12x12)
                        CE_1D El_Temp = m_ELemArray[iElemTemp_Index.IndexOf(l)];

                        if (m_fDisp_Vector_CN[i, 1] == El_Temp.NodeStart.ID - 1) // Current DOF-row is on Start Node
                        {
                            if (m_fDisp_Vector_CN[i, 1] == m_fDisp_Vector_CN[j, 1]) // Current DOF-row is in member of same Node as filled columns DOF - [0,0] - partial stiffeness matrix k_11 / k_aa 
                            {
                                temp += El_Temp.m_fKGlobM.m_fArrMembersABxCD[0,0].m_fArrMembers[m_fDisp_Vector_CN[i, 2], m_fDisp_Vector_CN[j, 2]];
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
                ArrayList iElemTemp_Index = m_NodeArray[m_fDisp_Vector_CN[i, 1]].GetFoundedMembers_Index(m_NodeArray[m_fDisp_Vector_CN[i, 1]], m_ELemArray, iElemNoTot);

                float tempEl = 0f; // Temporary for sum of values from all elements which transfer connected to the node for current DOF

                    for (int l = 0; l < iElemTemp_Index.Count; l++)  //  Sum all FEM Element Matrix members for given deggree of freedom of node
                    {
                        // Assign existing element from list to the temp element  
                        CE_1D El_Temp = m_ELemArray[iElemTemp_Index.IndexOf(l)];

                        if (m_fDisp_Vector_CN[i, 1] == El_Temp.NodeStart.ID - 1) // If DOF is on Start Node of Element
                        {
                            // Temporary transposed transformation matrix of element rotation multiplied by load vector
                            /*float[] fTempNodeVector = El_Temp.CM.fMultiplyMatr(El_Temp.CM.GetTransMatrix(El_Temp.m_fAMatr3D), El_Temp.m_ArrElemPEF_GCS_StNode);*/
                            float[] fTempNodeVector = El_Temp.m_ArrElemPEF_GCS_StNode.FVectorItems;
                            // Add Value
                            tempEl += fTempNodeVector[m_fDisp_Vector_CN[i, 2]];
                        }
                        else                                                     // If DOF is on End Node of Element
                        {
                            // Temporary transposed transformation matrix of element rotation multiplied by load vector
                            /*float[] fTempNodeVector = El_Temp.CM.fMultiplyMatr(El_Temp.CM.GetTransMatrix(El_Temp.m_fAMatr3D), El_Temp.m_ArrElemPEF_GCS_EnNode);*/
                            float[] fTempNodeVector = El_Temp.m_ArrElemPEF_GCS_EnNode.FVectorItems;
                            // Add Value
                            tempEl += fTempNodeVector[m_fDisp_Vector_CN[i, 2]];
                        }

                    }




                    // Node DOF Load due to direct node loading

                    float tempNode = m_NodeArray[m_fDisp_Vector_CN[i, 1]].m_DirNodeLoad.FVectorItems[m_fDisp_Vector_CN[i, 2]];


                    // Sum loads

                    m_V_Load.FVectorItems[i] = tempNode - tempEl;

            }
        }





        void GetFinalElemEndIF(CE_1D element, CFemNode node)
        {
        
          //node.m_sDisp = 
        
        
        }

        







    







    }

}
