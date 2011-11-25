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
        public CMLoadPart(CModel TopoModel, CE_1D[] arrFemMembers, int iMLoadIndex, int kMemberIndex)
        {
            if (TopoModel.m_arrMLoads[iMLoadIndex].IMemberCollection.Contains(TopoModel.m_arrMembers[kMemberIndex].IMember_ID)) // If member ID is same as collection item
            {
                float fTemp_1 = 0f, fTemp_2 = 0f; // Auxialiary for not used components

                // Fill external forces vectors 

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

                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                            ref fTemp_1,
                                            ref fTemp_2);

                                        break;
                                    }
                                // XX - direction start supported, end free displacement
                                //case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00__0:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00___:
                                    //case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0____:
                                    {
                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                        (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                        arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                        ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                        ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                        ref fTemp_1,
                                        ref fTemp_2);

                                        break;
                                    }
                                // XX - direction start free displacement, end supported
                                //case FEM_CALC_BASE.Enums.EElemSuppType.eEl__0_00:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl____00:
                                    //case FEM_CALC_BASE.Enums.EElemSuppType.eEl____0_:
                                    {
                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                ref fTemp_1,
                                ref fTemp_2);

                                        break;
                                    }
                                // XX - direction start free displacement, end free displacement
                                //case FEM_CALC_BASE.Enums.EElemSuppType.eEl__0__0:
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl______:
                                default:
                                    {
                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                            ref fTemp_1,
                                            ref fTemp_2);

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
                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eMZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eMZ]);

                                        break;
                                    }
                                // UZRY - direction start supported, end free displacement
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00___:
                                    {
                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                        (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                        arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eMZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eMZ]);

                                        break;
                                    }
                                // UZRY - direction start free displacement, end supported
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl____00:
                                    {
                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                        arrFemMembers[kMemberIndex],
                                        arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eMZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eMZ]);

                                        break;
                                    }
                                // UZRY - direction start free displacement, end free displacement
                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl______:
                                default:
                                    {
                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[iMLoadIndex],
                                            (CE_1D_BASE)arrFemMembers[kMemberIndex],
                                            arrFemMembers[kMemberIndex].m_eSuppType[(int)EM_PCS_DIR1.eUYRZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFY],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eMZ],
                                            ref TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eMZ]);

                                        break;
                                    }
                            }
                            break;
                        }
                    default: // Exception
                        {
                            break;
                        }
                }

                // Primary end forces due member loading in local coordinate system LCS

                // Start Node
                arrFemMembers[kMemberIndex].m_VElemPEF_GCS_StNode.FVectorItems[(int)e2D_E_F.eFX] += TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX];
                arrFemMembers[kMemberIndex].m_VElemPEF_GCS_StNode.FVectorItems[(int)e2D_E_F.eFY] += TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFY];
                arrFemMembers[kMemberIndex].m_VElemPEF_GCS_StNode.FVectorItems[(int)e2D_E_F.eMZ] += TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eMZ];

                // End Node
                arrFemMembers[kMemberIndex].m_VElemPEF_GCS_EnNode.FVectorItems[(int)e2D_E_F.eFX] += TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX];
                arrFemMembers[kMemberIndex].m_VElemPEF_GCS_EnNode.FVectorItems[(int)e2D_E_F.eFY] += TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFY];
                arrFemMembers[kMemberIndex].m_VElemPEF_GCS_EnNode.FVectorItems[(int)e2D_E_F.eMZ] += TopoModel.m_arrMLoads[iMLoadIndex].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eMZ];
            }
        }
    }
}
