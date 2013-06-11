using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;
using MATERIAL;
using CRSC;
using CENEX;
using FEM_CALC_BASE;

namespace FEM_CALC_1Din3D
{
    class CMLoadPart
    {
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoadPart(CModel TopoModel,
            CE_1D[] arrFemMembers,
            int iMLoadIndex,
            int kMemberIndex,
            out float fTemp_A_UXRX,
            out float fTemp_A_UYRZ,
            out float fTemp_Ma_UYRZ,
            out float fTemp_B_UXRX,
            out float fTemp_B_UYRZ,
            out float fTemp_Mb_UYRZ)
        {
            // Default
            fTemp_A_UXRX = 0.0f;
            fTemp_B_UXRX = 0.0f;
            fTemp_A_UYRZ = 0.0f;
            fTemp_B_UYRZ = 0.0f;
            fTemp_Ma_UYRZ = 0.0f;
            fTemp_Mb_UYRZ = 0.0f;


            ///////////////////
            // DOKONCIT, pozri 2D
            ///////////////////
        }
    }
}
