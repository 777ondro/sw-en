﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;

namespace FEM_CALC_1D
{
    public class CMLoadPart
    {
        // Temporary array of EIF for particular loading
        float[] m_fEIF_A = new float[6];
        float[] m_fEIF_B = new float[6];

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoadPart(EMLoadType1 eLoadType, EMLoadDirPCC1 eLDirPCC, CMLoad_11 Load, CE_1D Element)
        {
            //  Fill with zero
            for (int i = 0; i < 6; i++)
            {
                m_fEIF_A[i] = m_fEIF_B[i] = 0f;
            }




            // Find appropriate case for load and  support conditions (acc. to member nodes DOF)
            switch (eLoadType)
            {
                case EMLoadType1.eMLT_FS_G:
                    {
                        GetEIF_11(Element, eLDirPCC);
                        break;
                    }
                case EMLoadType1.eMLT_FS_H:
                    {
                        GetEIF_12(Element);
                        break;
                    }
                case EMLoadType1.eMLT_MS_G:
                    {
                        GetEIF_13(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_W:
                    {
                        GetEIF_21(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_PA:
                    {
                        GetEIF_22(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_PB:
                    {
                        GetEIF_23(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QUF_PG:
                    {
                        GetEIF_24(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_SW:
                    {
                        GetEIF_31(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MA_W:
                    {
                        GetEIF_32(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MB_W:
                    {
                        GetEIF_33(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MA_P:
                    {
                        GetEIF_34(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_MB_P:
                    {
                        GetEIF_35(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTNF_SP:
                    {
                        GetEIF_36(Element);
                        break;
                    }
                case EMLoadType1.eMLT_QTPF_SW:
                    {
                        GetEIF_41(Element);
                        break;
                    }
                case EMLoadType1.eMLT_TMP_UNQ_W:
                    {
                        GetEIF_51(Element);
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
            switch (Element.m_iSuppType)
            {
                case (int)EElemSuppType.e3DEl_000000_______:
                    {
                        switch (eLDirPCC)
                        {
                            case EMLoadDirPCC1.eMLD_PCC_X:
                                {
                                    // m_fEIF_A[];
                                    break;
                                }
                            case EMLoadDirPCC1.eMLD_PCC_U:
                                {
                                    // Potrebujem vztvorit objekt triedy ktory bude mat rovnaky typ ale vzdy ine clenske premenne ????
                                    // GetEIF_11_UV(Load, Element.m_flength, m_fEIF_A[(int)EM_IF.eFy], m_fEIF_B[(int)EM_IF.eFy], m_fEIF_A[(int)EM_IF.eMz], m_fEIF_B[(int)EM_IF.eMz]);
                                    break;
                                }
                            case EMLoadDirPCC1.eMLD_PCC_V:
                                {
                                    // GetEIF_11_UV(Load, Element.m_flength, m_fEIF_A[(int)EM_IF.eFz], m_fEIF_B[(int)EM_IF.eFz], m_fEIF_A[(int)EM_IF.eMy], m_fEIF_B[(int)EM_IF.eMy]);
                                    break;
                                }
                            default:
                                { break; }
                        }
                        break;
                    }
                case (int)EElemSuppType.e3DEl_000000_000000:
                    {

                        break;
                    }
                case (int)EElemSuppType.e3DEl_000000_000___:
                    {

                        break;
                    }
                case (int)EElemSuppType.e3DEl_000000_0_00_0:
                    {

                        break;
                    }
                case (int)EElemSuppType.e3DEl________000000:
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
        void GetEIF_51(CE_1D Element)
        {


        }










        ///////////////////////////////////////////////////
        // Particular EIF not depending of direction -         // Pozri: Sobota Jan, Statika Stavebnych konstrukcii 2, Tab. 2.1

        //----------------------------------------------------------------------------------------------------------------------------
        // Obojstranne votknutie
        //----------------------------------------------------------------------------------------------------------------------------

        // Singular Load
        void GetEIF_00_11_UV(CMLoad_11 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = Load.FF * (float)Math.Pow(fL - Load.Fa, 2) / (float)Math.Pow(fL, 3) * (fL + 2f * Load.Fa);
            fB = Load.FF * (float)Math.Pow(Load.Fa, 2) / (float)Math.Pow(fL, 3) * (fL + 2f * (fL - Load.Fa));
            fMa = -Load.FF * Load.Fa * (float)Math.Pow(fL - Load.Fa, 2) / (float)Math.Pow(fL, 2);
            fMb = Load.FF * (float)Math.Pow(Load.Fa, 2) * (fL - Load.Fa) / (float)Math.Pow(fL, 2);
        }
        void GetEIF_00_12_UV(CMLoad_12 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = fB = Load.FF / 2f;

            fMa = -Load.FF * fL / 8f;
            fMb = -fMa; /*Load.FF * fL/8f;*/
        }
        void GetEIF_00_13_UV(CMLoad_13 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = -6 * Load.FM / (float)Math.Pow(fL, 3) * Load.Fa * (fL - Load.Fa);
            fB = -fA; /*6 * Load.FM / (float)Math.Pow(fL, 3) * Load.Fa * (fL - Load.Fa);*/
            fMa = Load.FM * (fL - Load.Fa) / (float)Math.Pow(fL, 2) * (2 * fL - 3 * (fL - Load.Fa));
            fMb = Load.FM * Load.Fa / (float)Math.Pow(fL, 2) * (2 * fL - 3 * Load.Fa);
        }
        // Uniform Load
        void GetEIF_00_21_UV(CMLoad_21 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = fB = Load.Fq * fL / 2f;
            fMa = -Load.Fq * fL * fL / 12f;
            fMb = -fMa; /*Load.Fq * fL * fL / 12f;*/
        }
        void GetEIF_00_22_UV(CMLoad_22 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = (Load.Fq * Load.Fa) / (2f * fL) * ((2f * fL * (fL * fL - Load.Fa * Load.Fa)) + (float)Math.Pow(Load.Fa, 3));
            fB = (Load.Fq * (float)Math.Pow(Load.Fa, 3) / (2f * (float)Math.Pow(fL, 3))) * (fL + (fL - Load.Fa));
            fMa = -(Load.Fq * Load.Fa * Load.Fa) / 12f * (float)Math.Pow(fL, 2) * (Load.Fa * Load.Fa + 4f * Load.Fa * (fL - Load.Fa) + 6f * (float)Math.Pow(fL - Load.Fa, 2));
            fMb = (Load.Fq * (float)Math.Pow(Load.Fa, 3) / (12f * fL * fL)) * (Load.Fa * +4f * (fL - Load.Fa));
        }
        void GetEIF_00_23_UV(CMLoad_23 Load, float fL, float fA, float fB, float fMa, float fMb)
        {

            // Otocenie


        }
        void GetEIF_00_24_UV(CMLoad_24 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = ((Load.Fq * Load.Fs) / (4f * (float)Math.Pow(fL, 3))) * (4f * ((float)Math.Pow(fL - Load.Fa, 2) * (3f * Load.Fa + (fL - Load.Fa)) + (float)Math.Pow(Load.Fs, 2) * (Load.Fa - (fL - Load.Fa))));
            fB = ((Load.Fq * Load.Fs) / (4f * (float)Math.Pow(fL, 3))) * (4f * (float)Math.Pow(Load.Fa, 2) * (Load.Fa + 3f * (fL - Load.Fa)) + (float)Math.Pow(Load.Fs, 2) * ((fL - Load.Fa) - Load.Fa));
            fMa = -(Load.Fq * Load.Fs / (12f * fL * fL)) * (12f * Load.Fa * (float)Math.Pow(fL - Load.Fa, 2) + (float)Math.Pow(Load.Fs, 2) * (Load.Fa - 2 * (fL - Load.Fa)));
            fMa = (Load.Fq * Load.Fs / (12f * fL * fL)) * (12f * (float)Math.Pow(Load.Fa, 2) * (fL - Load.Fa) + (float)Math.Pow(Load.Fs, 2) * ((fL - Load.Fa) - 2 * Load.Fa));
        }
        // Own mathematical class MathF used in functions written below
        // Triangular Load
        void GetEIF_00_31_UV(CMLoad_31 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = fB = Load.Fq * fL / 4f;
            fMa = -5f * Load.Fq * MathF.Pow2(fL) / 96f;
            fMb = -fMa;
        }
        void GetEIF_00_32_UV(CMLoad_32 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 7f / 20f * Load.Fq * fL;
            fB = 3f / 20f * Load.Fq * fL;
            fMa = -Load.Fq * MathF.Pow2(fL) / 20f;
            fMb = Load.Fq * MathF.Pow2(fL) / 30f;
        }
        void GetEIF_00_33_UV(CMLoad_33 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 3f / 20f * Load.Fq * fL;
            fB = 7f / 20f * Load.Fq * fL;
            fMa = -Load.Fq * MathF.Pow2(fL) / 30f;
            fMb = Load.Fq * MathF.Pow2(fL) / 20f;
        }
        void GetEIF_00_34_UV(CMLoad_34 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa - Load.Fs;
            fA = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(fb) * (3f * fL - 2f * fb) + 8f * Load.Fa * fb * Load.Fs + MathF.Pow2(Load.Fs) * (3f * Load.Fa + 5f * fb + 1.4f * Load.Fs));
            fB = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(Load.Fa) * (3f * fL - 2f * Load.Fa) + 4f * Load.Fa * fb * Load.Fs + MathF.Pow2(Load.Fs) * (3f * Load.Fa + fb + 0.6f * Load.Fs));
            fMa = -(Load.Fq * Load.Fs / (60f * MathF.Pow2(fL))) * (10f * MathF.Pow2(fb) * (3f * Load.Fa + Load.Fs) + MathF.Pow2(Load.Fs) * (15f * Load.Fa + 10f * fb + 3f * Load.Fs) + 40f * Load.Fa * fb * Load.Fs);
            fMb = (Load.Fq * Load.Fs / (60f * MathF.Pow2(fL))) * (10f * MathF.Pow2(Load.Fa) * (3f * fb + 2f * Load.Fs) + MathF.Pow2(Load.Fs) * (10f * Load.Fa + 5f * fb + 2f * Load.Fs) + 20f * Load.Fa * fb * Load.Fs);
        }
        void GetEIF_00_35_UV(CMLoad_35 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa - Load.Fs;
            fA = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(fb) * (3f * fL - 2f * fb) + 4f * Load.Fa * fb * Load.Fs + MathF.Pow2(Load.Fs) * (3f * fb + Load.Fa + 0.6f * Load.Fs));
            fB = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(Load.Fa) * (3f * fL - 2f * Load.Fa) + 8f * Load.Fa * fb * Load.Fs + MathF.Pow2(Load.Fs) * (3f * fb + 5f * Load.Fa + 1.4f * Load.Fs));
            fMa = (Load.Fq * Load.Fs / (60f * MathF.Pow2(fL))) * (10f * MathF.Pow2(fb) * (3f * Load.Fa + 2f * Load.Fs) + MathF.Pow2(Load.Fs) * (10f * fb + 5f * Load.Fa + 2f * Load.Fs) + 20f * Load.Fa * fb * Load.Fs);
            fMb = -(Load.Fq * Load.Fs / (60f * MathF.Pow2(fL))) * (10f * MathF.Pow2(Load.Fa) * (3f * fb + Load.Fs) + MathF.Pow2(Load.Fs) * (15f * fb + 10f * Load.Fa + 3f * Load.Fs) + 40f * Load.Fa * fb * Load.Fs);
        }
        void GetEIF_00_36_UV(CMLoad_36 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * Load.Fs / (2f * MathF.Pow3(fL))) * (2f * MathF.Pow2(fb) * (fL + 2f * Load.Fa) + MathF.Pow2(Load.Fs) * (Load.Fa - fb));
            fB = (Load.Fq * Load.Fs / (2f * MathF.Pow3(fL))) * (2f * MathF.Pow2(Load.Fa) * (fL + 2f * fb) + MathF.Pow2(Load.Fs) * (fb - Load.Fa));
            fMa = -(Load.Fq * Load.Fs / (6f * MathF.Pow2(fL))) * (6f * Load.Fa * MathF.Pow2(fb) + MathF.Pow2(Load.Fs) * (Load.Fa - 2f * fb));
            fMb = (Load.Fq * Load.Fs / (6f * MathF.Pow2(fL))) * (6f * MathF.Pow2(Load.Fa) * fb + MathF.Pow2(Load.Fs) * (fb - 2f * Load.Fa));
        }



























    }
}
