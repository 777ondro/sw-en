using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using CRSC;

namespace CENEX
{
    // Main model class

    // List of model objects is included

    [Serializable]
    public class CModel
    {
        // General project data

        public string m_sProjectName;
        public string m_sConstObjectName;
        public string m_sFileName;
        public int m_eNDOF;

        // Physical Model / Structural data
        // Collection of references to objects

        // Materials used/defined in current model
        public CMat_00[] m_arrMat;
        // Cross-sections used/ defined in current model
        public CCrSc[] m_arrCrSc;

        // Topological nodes (not FEM)
        // Note !!!
        // Type of object collections - some dynamically allocated which can be resized - stack, queue, vector ????

        public CNode[] m_arrNodes;
        // 1D Elements (not FEM)
        public CMember [] m_arrMembers;
        // Nodal Supports
        public CNSupport[] m_arrNSupports;

        // Loading
        // Nodal Loads
        public CNLoad[] m_arrNLoads;
        // Member Loads
        public CMLoad[] m_arrMLoads;
        // Load Cases
        public CLoadCase[] m_arrLoadCases;
        // Load Combinations
        public CLoadCombination[] m_arrLoadCombs;


        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CModel() { }
        public CModel(string sFileName)
        {
            m_sFileName = sFileName;
        }
        // Alokuje velkost poli zoznamov, malo by to byt dymamicke
        public CModel(string sFileName, int eNDOF,
            int iMatNum, int iCrScNum, int iNodeNum,
            int iMemNum, int iNSupNum, int iNLoadNum,
            int iMLoadNum, int iLoadCaseNum, int iLoadComNum)
        {
            m_eNDOF = eNDOF;
            m_arrMat = new CMat_00[iMatNum];
            m_arrCrSc = new CCrSc[iCrScNum];
            m_arrNodes = new CNode[iNodeNum];
            m_arrMembers = new CMember[iMemNum];
            m_arrNSupports = new CNSupport[iNSupNum];
            m_arrNLoads = new CNLoad[iNLoadNum];
            m_arrMLoads = new CMLoad[iMLoadNum];
            m_arrLoadCases = new CLoadCase[iLoadCaseNum];
            m_arrLoadCombs = new CLoadCombination[iLoadComNum];
        }
        public CModel(string sProjectName, string sConstObjectName, string sFileName)
        {
            m_sProjectName = sProjectName;
            m_sConstObjectName = sConstObjectName;
            m_sFileName = sFileName;
        }

    }
}
