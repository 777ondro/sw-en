using System;
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
        CTest_1 TopoModelFile; // Create topological model file
        CGenex FEMModel;  // Create FEM model

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
            Console.WriteLine("Member ID: " + FEMModel.m_arrFemMembers[i].IMember_ID + "\n");
            // kij_0 - local stiffeness matrix       3 x  3
            Console.WriteLine("Local stiffeness matrix k" + FEMModel.m_arrFemMembers[i].m_NodeStart.INode_ID + FEMModel.m_arrFemMembers[i].m_NodeEnd.INode_ID + "0 - Dimensions: 3 x 3 \n");
            FEMModel.m_arrFemMembers[i].m_fkLocMatr.Print2DMatrixFormated();
            // A  Tranformation Rotation Matrixes    3 x  3
            Console.WriteLine("Tranformation rotation matrix A - Dimensions: 3 x 3 \n");
            FEMModel.m_arrFemMembers[i].m_fATRMatr2D.Print2DMatrixFormated();
            // B  Transfer Matrixes                  3 x  3
            Console.WriteLine("Tranfer matrix B - Dimensions: 3 x 3 \n");
            FEMModel.m_arrFemMembers[i].m_fBTTMatr2D.Print2DMatrixFormated();
            // Kij - global matrix of member         6 x 6
            Console.WriteLine("Global stiffeness matrix K" + FEMModel.m_arrFemMembers[i].m_NodeStart.INode_ID + FEMModel.m_arrFemMembers[i].m_NodeEnd.INode_ID + "0 - Dimensions: 6 x 6 \n");
            FEMModel.m_arrFemMembers[i].m_fKGlobM.Print2DMatrixFormated_ABxCD(FEMModel.m_arrFemMembers[i].m_fKGlobM.m_fArrMembersABxCD);
            // Element Load Vector                   2 x 3
            Console.WriteLine("Member load vector - start node ID: " + FEMModel.m_arrFemMembers[i].m_NodeStart.INode_ID + " - Dimensions: 3 x 1 \n");
            FEMModel.m_arrFemMembers[i].m_VElemPEF_LCS_StNode.Print1DVector();
            Console.WriteLine("Member load vector - end node ID: " + FEMModel.m_arrFemMembers[i].m_NodeEnd.INode_ID + " - Dimensions: 3 x 1 \n");
            FEMModel.m_arrFemMembers[i].m_VElemPEF_LCS_EnNode.Print1DVector();
            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////












        }
    }
}
