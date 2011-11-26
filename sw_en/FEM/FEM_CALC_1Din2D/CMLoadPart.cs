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

namespace FEM_CALC_1Din2D
{
    public class CMLoadPart
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

            if (TopoModel.m_arrMLoads[iMLoadIndex].IMemberCollection.Contains(TopoModel.m_arrMembers[kMemberIndex].IMember_ID)) // If member ID is same as collection item
            {

                float fTemp_Ma_UXRX = 0.0f, fTemp_Mb_UXRX = 0.0f; // Temporary for output of Mx which is not used in 2D in-plane solution

                // Fill external forces temp values 

                switch (TopoModel.m_arrMLoads[iMLoadIndex].EDirPPC) // Load direction in principal coordinate system XX / YU / ZV
                {
                    case EMLoadDirPCC1.eMLD_PCC_FXX_MXX: // Axial load on member
                        {
                            // DOF RX can't be released - always rigid
                            switch (arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX])
                            {
                                // Type of supports is already defined  but I check it once more in body of function !!!

                                // XX - direction both sides supported displacement
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00_00:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00_0_:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0__00:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0__0_:
                                    {

                                        // Type ObjLoadType = typeof(TopoModel.m_arrMLoads[i]);

                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                            out fTemp_A_UXRX,
                                            out fTemp_B_UXRX,
                                            out fTemp_Ma_UXRX,
                                            out fTemp_Mb_UXRX);

                                        break;
                                    }
                                // XX - direction start supported, end free displacement
                                //case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00__0:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00___:
                                    //case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0____:
                                    {
                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                        (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                        arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                            out fTemp_A_UXRX,
                                            out fTemp_B_UXRX,
                                            out fTemp_Ma_UXRX,
                                            out fTemp_Mb_UXRX);

                                        break;
                                    }
                                // XX - direction start free displacement, end supported
                                //case FEM_CALC_BASE.Enums.EElemSuppType.eEl__0_00:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl____00:
                                    //case FEM_CALC_BASE.Enums.EElemSuppType.eEl____0_:
                                    {
                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                            out fTemp_A_UXRX,
                                            out fTemp_B_UXRX,
                                            out fTemp_Ma_UXRX,
                                            out fTemp_Mb_UXRX);

                                        break;
                                    }
                                // XX - direction start free displacement, end free displacement
                                //case FEM_CALC_BASE.Enums.EElemSuppType.eEl__0__0:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl______:
                                default:
                                    {
                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                            out fTemp_A_UXRX,
                                            out fTemp_B_UXRX,
                                            out fTemp_Ma_UXRX,
                                            out fTemp_Mb_UXRX);

                                        break;
                                    }
                            }
                            break;
                        }
                    case EMLoadDirPCC1.eMLD_PCC_YU:
                    // {
                    //    // DOF UY and RZ can't be released - always rigid
                    //     break;
                    // }
                    case EMLoadDirPCC1.eMLD_PCC_ZV:
                        {
                            switch (arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ])
                            {
                                // Type of supports is already defined  but I check it once more in body of function !!!

                                // UZRY - direction both sides supported displacement
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00_00:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00_0_:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0__00:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0__0_:
                                    {
                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            out fTemp_A_UYRZ,
                                            out fTemp_B_UYRZ,
                                            out fTemp_Ma_UYRZ,
                                            out fTemp_Mb_UYRZ);

                                        break;
                                    }
                                // UZRY - direction start supported, end free displacement
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00___:
                                    {
                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                        (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                        arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            out fTemp_A_UYRZ,
                                            out fTemp_B_UYRZ,
                                            out fTemp_Ma_UYRZ,
                                            out fTemp_Mb_UYRZ);

                                        break;
                                    }
                                // UZRY - direction start free displacement, end supported
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl____00:
                                    {
                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                        arrFemMembers[kMemberIndex],
                                        arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            out fTemp_A_UYRZ,
                                            out fTemp_B_UYRZ,
                                            out fTemp_Ma_UYRZ,
                                            out fTemp_Mb_UYRZ);

                                        break;
                                    }
                                // UZRY - direction start free displacement, end free displacement
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl______:
                                default:
                                    {
                                        FEM_CALC_BASE.CMLoadPart_TRANS_F objMLoadPart = new FEM_CALC_BASE.CMLoadPart_TRANS_F(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            out fTemp_A_UYRZ,
                                            out fTemp_B_UYRZ,
                                            out fTemp_Ma_UYRZ,
                                            out fTemp_Mb_UYRZ);

                                        break;
                                    }
                            }
                            break;
                        }
                    default: // Exception
                        {
                            fTemp_A_UXRX = float.MaxValue;
                            fTemp_B_UXRX = float.MaxValue;
                            fTemp_A_UYRZ = float.MaxValue;
                            fTemp_B_UYRZ = float.MaxValue;
                            fTemp_Ma_UYRZ = float.MaxValue;
                            fTemp_Mb_UYRZ = float.MaxValue;
                            break;
                        }
                }
            }
            //else
            //{
            //    // Default
            //    fTemp_A_UXRX = 0.0f;
            //    fTemp_B_UXRX = 0.0f;
            //    fTemp_A_UYRZ = 0.0f;
            //    fTemp_B_UYRZ = 0.0f;
            //    fTemp_Ma_UYRZ = 0.0f;
            //    fTemp_Mb_UYRZ = 0.0f;
            //}
        }
    }
}
