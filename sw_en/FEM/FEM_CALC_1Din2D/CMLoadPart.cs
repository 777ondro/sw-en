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
        // Temporary array of EIF for particular loading
        float[] m_fEIF_A = new float[6];
        float[] m_fEIF_B = new float[6];

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoadPart(EMLoadType1 eLoadType, EMLoadDirPCC1 eLDirPCC, CMLoad Load, CE_1D Element)
        {
            //  Fill with zero
            for (int i = 0; i < 6; i++)
            {
                m_fEIF_A[i] = m_fEIF_B[i] = 0f;
            }

            // Find appropriate case for load and  support conditions (acc. to member nodes DOF)
            switch (eLoadType)
            {
                case EMLoadType1.eMLT_FS_G_11:
                    {
                        GetEIF_11(Element, eLDirPCC);
                        break;
                    }
                case EMLoadType1.eMLT_FS_H_12:
                    {
                        GetEIF_12(Element);
                        break;
                    }
                case EMLoadType1.eMLT_MS_G_13:
                    {
                        GetEIF_13(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_W_21:
                    {
                        GetEIF_21(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_PA_22:
                    {
                        GetEIF_22(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_PB_23:
                    {
                        GetEIF_23(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_PG_24:
                    {
                        GetEIF_24(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_SW_31:
                    {
                        GetEIF_31(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MA_W_32:
                    {
                        GetEIF_32(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MB_W_33:
                    {
                        GetEIF_33(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MA_P_34:
                    {
                        GetEIF_34(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MB_P_35:
                    {
                        GetEIF_35(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_SP_36:
                    {
                        GetEIF_36(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTPF_SW_41:
                    {
                        GetEIF_41(Element);
                        break;
                    }
                case EMLoadType1.eMLT_TMP_UNQ_Wz_51:
                    {
                        GetEIF_51z(Element);
                        break;
                    }
                case EMLoadType1.eMLT_TMP_UNQ_Wy_52:
                    {
                        GetEIF_51y(Element);
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }

        //------------------------------------------------------------------------
        // Particular calculation of end IF
        void GetEIF_11(CE_1D Element, EMLoadDirPCC1 eLDirPCC)
        {
            switch (Element.m_eSuppType)
            {
                case EElemSuppType.e2DEl_000____:
                    {
                        switch (eLDirPCC)
                        {
                            case EMLoadDirPCC1.eMLD_PCC_XX:
                                {
                                    // m_fEIF_A[];
                                    break;
                                }
                            case EMLoadDirPCC1.eMLD_PCC_YU:
                                {
                                    // Potrebujem vztvorit objekt triedy ktory bude mat rovnaky typ ale vzdy ine clenske premenne ????
                                    // GetEIF_11_UV(Load, Element.m_flength, m_fEIF_A[(int)EM_IF.eFy], m_fEIF_B[(int)EM_IF.eFy], m_fEIF_A[(int)EM_IF.eMz], m_fEIF_B[(int)EM_IF.eMz]);
                                    break;
                                }
                            case EMLoadDirPCC1.eMLD_PCC_ZV:
                                {
                                    // GetEIF_11_UV(Load, Element.m_flength, m_fEIF_A[(int)EM_IF.eFz], m_fEIF_B[(int)EM_IF.eFz], m_fEIF_A[(int)EM_IF.eMy], m_fEIF_B[(int)EM_IF.eMy]);
                                    break;
                                }
                            default:
                                { break; }
                        }
                        break;
                    }
                case EElemSuppType.e2DEl_000_000:
                    {

                        break;
                    }
                case EElemSuppType.e2DEl_000_00_:
                    {

                        break;
                    }
                case EElemSuppType.e2DEl_000_0_0:
                    {

                        break;
                    }
                case EElemSuppType.e2DEl_____000:
                    {

                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }
        void GetEIF_12(CE_1D Element)
        {


        }
        void GetEIF_13(CE_1D Element)
        {

        }
        void GetEIF_21(CE_1D Element)
        {


        }
        void GetEIF_22(CE_1D Element)
        {

        }
        void GetEIF_23(CE_1D Element)
        {


        }
        void GetEIF_24(CE_1D Element)
        {


        }
        void GetEIF_31(CE_1D Element)
        {


        }
        void GetEIF_32(CE_1D Element)
        {

        }
        void GetEIF_33(CE_1D Element)
        {


        }
        void GetEIF_34(CE_1D Element)
        {


        }
        void GetEIF_35(CE_1D Element)
        {


        }
        void GetEIF_36(CE_1D Element)
        {


        }
        void GetEIF_41(CE_1D Element)
        {


        }
        void GetEIF_51z(CE_1D Element)
        {


        }
        void GetEIF_51y(CE_1D Element)
        {


        }
    }
}
