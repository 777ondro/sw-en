using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{

    //----------------------------------------------------------------------------
    // Node DOF - number of degrees of freedom
    //----------------------------------------------------------------------------
    public enum ENDOF
    {
        e2DEnv = 3, // 3 for 2D environment
        e3DEnv = 6  // 6 for 3D environment
    }

    //----------------------------------------------------------------------------
    // Nodes
    //----------------------------------------------------------------------------
    public enum ENLoadType
    {
        eNLT_Fx,
        eNLT_Fy,
        eNLT_Fz,
        eNLT_Mx,
        eNLT_My,
        eNLT_Mz
    }
    //----------------------------------------------------------------------------
    public enum ENSupportType
    {
        eNST_Ux = 0,
        eNST_Uy = 1,
        eNST_Uz = 2,
        eNST_Rx = 3,
        eNST_Ry = 4,
        eNST_Rz = 5
    }
    // Nodes and Members
    //----------------------------------------------------------------------------
    // Define load direction or orientation in GCC (global coordinate system)
    //----------------------------------------------------------------------------
    public enum ELoadDirGCC1
    {
        eLD_GCC_X = 0,
        eLD_GCC_Y = 1,
        eLD_GCC_Z = 2
    }


    //----------------------------------------------------------------------------
    // Members
    //----------------------------------------------------------------------------
    public enum EM_IF
    {
        eFx,
        eFy,
        eFz,
        eMx,
        eMy,
        eMz
    }

    // Definition support conditions for members loading and local stiffeness matrix determination
    public enum EM_PCS_DIR1
    {
        eUXRX,
        eUYRZ,
        eUZRY
    }
    //----------------------------------------------------------------------------
    // Define load direction or orientation in LCC (local coordinate system) of members
    //----------------------------------------------------------------------------
    public enum EMLoadDirLCC1
    {
        eMLD_LCC_FX_MX = 0,
        eMLD_LCC_Y = 1,
        eMLD_LCC_Z = 2
    }
    //----------------------------------------------------------------------------
    // Define load direction or orientation in PCC (principal coordinate system) of members
    //----------------------------------------------------------------------------
    public enum EMLoadDirPCC1
    {
        eMLD_PCC_FXX_MXX = 0, 
        eMLD_PCC_FYU_MZV = 1,
        eMLD_PCC_FZV_MYU = 2
    }
    //----------------------------------------------------------------------------
    // Types of single member loading
    // Typy zatazenia jednoducheho pruta - podla druhu zatazenia, polohy a koncoveho podopretia prvku
    //----------------------------------------------------------------------------
    public enum EMLoadTypeDistr
    {
        // Pozri: Sobota Jan, Statika Stavebnych konstrukcii 2, Tab. 2.1

        // Singular loading
        eMLT_FS_G_11 = 11, // Singular Force - general position
        eMLT_FS_H_12 = 12, // Singular Force - in half of member length
        //eMLT_MS_G_13 = 13, // Singular Moment - general position
        //eMLT_M_SH_14 = 14, // Singular Moment - in half of member length

        // Uniform loading
        // Whole Member
        eMLT_QUF_W_21 = 21,  // Uniformly Distributed Force - whole length of member

        // Part of Member
        eMLT_QUF_PA_22 = 22, // Uniformly Distributed Force - partly from begining of member
        eMLT_QUF_PB_23 = 23, // Uniformly Distributed Force - partly from end of member
        eMLT_QUF_PG_24 = 24, // Uniformly Distributed Force - partly in general position on member

        // Triangular loading
        // Whole Member
        eMLT_QTNF_SW_31 = 31,   // Triangular load, symmetrical acc. to half of length of member
        eMLT_QTNF_MA_W_32 = 32, // Triangular load on whole member, maximum at start
        eMLT_QTNF_MB_W_33 = 33, // Triangular load on whole member, maximum at end

        // Part of Member
        eMLT_QTNF_MA_P_34 = 34, // Triangular load on part of member, maximum near to the start
        eMLT_QTNF_MB_P_35 = 35, // Triangular load on part of member, maximum near to the end
        eMLT_QTNF_SP_36 = 36,   // Triangular load, symmetrical acc. to transversal axis of member, in general position

        // Trapezoidal loading
        // Whole Member
        eMLT_QTPF_SW_41 = 41,   // Trapezoidal load, symmetrical acc. to half of member, apllied on whole member

        // Temperature
        // Whole Member
        eMLT_TMP_UNQ_Wz_51 = 51,   // Temperature loading on whole member, two temperatures (different for upper and bottom surface of member)
        eMLT_TMP_UNQ_Wy_52 = 52    // Temperature loading on whole member, two temperatures (different for upper and bottom surface of member)
    }
    //----------------------------------------------------------------------------
    // Define load type
    //----------------------------------------------------------------------------
    public enum EMLoadType
    {
        eMLT_F = 0, // Force
        eMLT_M = 1, // Moment
        eMLD_Temperature = 2 // Temperature
    }














}
