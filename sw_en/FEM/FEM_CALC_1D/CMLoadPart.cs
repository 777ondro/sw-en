using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;

namespace FEM_CALC_1D
{
    public class CMLoadPart:CMLoad
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
                case EMLoadType1.eMLT_TMP_UNQ_Wz:
                    {
                        GetEIF_51z(Element);
                        break;
                    }
                case EMLoadType1.eMLT_TMP_UNQ_Wy:
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
        void GetEIF_51z(CE_1D Element)
        {


        }
        void GetEIF_51y(CE_1D Element)
        {


        }









        ///////////////////////////////////////////////////
        // Particular EIF not depending of direction -         // Pozri: Sobota Jan, Statika Stavebnych konstrukcii 2, Tab. 2.1

        //----------------------------------------------------------------------------------------------------------------------------
        // Obojstranne votknutie
        //----------------------------------------------------------------------------------------------------------------------------

        #region End Moments and Reactions of Both Sides Restrained member / Koncove momentz a rekacie votknuteho nosnika
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
        // Trapezoidal
        void GetEIF_00_41_UV(CMLoad_41 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - 2f * Load.Fa;
            fA = fB = (Load.Fq / 2f) * (fL - Load.Fa);
            fMa = -(Load.Fq / (12f * fL)) * (MathF.Pow3(fL) + MathF.Pow3(Load.Fa) - 2f * MathF.Pow2(Load.Fa) * fL);
            fMb = -fMa;
        }
        // Temperature
        void GetEIF_00_51_UV(CMLoad_51z Load, CMaterial objMat, CCrSc objCrSc, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = fB = 0f;
            fMa = -objMat.m_fE * objCrSc.m_fIy * objMat.m_fAlpha_T * (Load.Ft_0_b - Load.Ft_0_u) / objCrSc.m_fh;
            fMb = -fMa;
        }
        void GetEIF_00_51_UV(CMLoad_51y Load, CMaterial objMat, CCrSc objCrSc, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = fB = 0f;
            fMa = -objMat.m_fE * objCrSc.m_fIz * objMat.m_fAlpha_T * (Load.Ft_0_l - Load.Ft_0_r) / objCrSc.m_fb;
            fMb = -fMa;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------------------
        // Jedna strana nosnika jednoducho podopreta a druha votknuta
        //----------------------------------------------------------------------------------------------------------------------------

        // OTOCENIE PODOPRETIA DOPRACOVAT !!!!

        #region End Moments and Reactions of One Side Simply Supported and the Other Side Restraint Member
        // Singular Load
        void GetEIF__0_11_UV(CMLoad_11 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = ((Load.FF * MathF.Pow2(fb)) / (2f*MathF.Pow3(fL)))* (2f*fL + Load.Fa);
            fB = Load.FF * Load.Fa / (2 * MathF.Pow3(fL)) * (3f * MathF.Pow2(fL) - MathF.Pow2(Load.Fa));
            fMa = 0f;
            fMb = (Load.FF * Load.Fa * fb / (2 * MathF.Pow2(fL))) * (Load.Fa + fL);

        }
        void GetEIF__0_12_UV(CMLoad_12 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA  = 5f/16f*Load.FF;
            fB = 11f / 16f * Load.FF;
            fMa = 0f;
            fMb = 3f/16f*Load.FF*fL;
        }
        void GetEIF__0_13_UV(CMLoad_13 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = -3f/2f * ((MathF.Pow2(fL) - MathF.Pow2(Load.Fa)) / MathF.Pow3(fL)) * Load.FM;
            fB = - fA;
            fMa =0f;
            fMb = (Load.FM/(2*MathF.Pow2(fL))) * (MathF.Pow2(fL) - 3*MathF.Pow2(Load.Fa));
        }
        // Uniform Load
        void GetEIF__0_21_UV(CMLoad_21 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 3/8f * Load.Fq * fL;
            fB = 5/8f*Load.Fq * fL;
            fMa = 0f;
            fMb =-Load.Fq * fL * fL / 8f;
        }
        void GetEIF__0_22_UV(CMLoad_22 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * Load.Fa) / (8f * MathF.Pow3(fL)) * (2f * MathF.Pow2(fL) * (Load.Fa + 4f * fB) + MathF.Pow3(Load.Fa));
            fB = (Load.Fq * MathF.Pow3(Load.Fa)) / (8f * MathF.Pow3(fL)) * (6f * MathF.Pow2(fL) -MathF.Pow2(Load.Fa));
            fMa = 0f;;
            fMb = (Load.Fq * MathF.Pow2(Load.Fa)) / (8f * MathF.Pow2(fL)) * (2f*MathF.Pow2(fL) - MathF.Pow2(Load.Fa));
        }
        void GetEIF__0_23_UV(CMLoad_23 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * MathF.Pow3(fb) / (8f * MathF.Pow3(fL))) * (3f * fL + Load.Fa);
            fB = ((Load.Fq * fb) / (8f * MathF.Pow3(fL))) * (14f * (2f * MathF.Pow2(fL) - MathF.Pow2(fb)) + MathF.Pow3(fb));
            fMa = 0f;
            fMb = (Load.Fq * MathF.Pow2(fb) / 8f * MathF.Pow2(fL)) * (4f * Load.Fa * fL + MathF.Pow2(fb));
        }
        void GetEIF__0_24_UV(CMLoad_24 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * Load.Fs / (8f*MathF.Pow3(fL))) * (4f * MathF.Pow2(fb) * (2f*fL+Load.Fa) + Load.Fa * MathF.Pow2(Load.Fs));
            fB = (Load.Fq * Load.Fs * Load.Fa / (8f * MathF.Pow3(fL))) * (8f * MathF.Pow2(fL) + 4f * fb * (fL + Load.Fa) - MathF.Pow2(Load.Fs));
            fMa = 0f;
            fMb = (Load.Fq * Load.Fs * Load.Fa / (8f * MathF.Pow2(fL))) * (4f * fb * (fL + Load.Fa) - MathF.Pow2(Load.Fs));
        }
        // Triangular Load
        void GetEIF__0_31_UV(CMLoad_31 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 11f / 64f * Load.Fq * fL;
            fB = 21f/64f * Load.Fq * fL;
            fMa = 0f;
            fMb = 5f/64f*Load.Fq*MathF.Pow2(fL);
        }
        void GetEIF__0_32_UV(CMLoad_32 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 11f/40f * Load.Fq * fL;
            fB = 9f / 40f * Load.Fq * fL;
            fMa = 0f;
            fMb = 7f /120f * Load.Fq * MathF.Pow2(fL);
        }
        void GetEIF__0_33_UV(CMLoad_33 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 1f / 10f * Load.Fq * fL;
            fB = 2f / 5f * Load.Fq * fL;
            fMa = 0f;
            fMb = 1f / 15f * Load.Fq * MathF.Pow2(fL);
        }
        void GetEIF__0_34_UV(CMLoad_34 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa - Load.Fs;
            fA = (Load.Fq * Load.Fs / (4f*MathF.Pow3(fL))) * (MathF.Pow2(fB) * (3f*Load.Fa + 2*fB + 5*Load.Fs) + MathF.Pow2(Load.Fs) * (1.5f * Load.Fa + 4f*fb + 1.1f * Load.Fs) + 4f* Load.Fa*fB*Load.Fs);
            fB = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f* MathF.Pow2(Load.Fa) * (Load.Fa + 3 * fB + 3 * Load.Fs) + MathF.Pow2(fb) * (3*Load.Fa + Load.Fs) + MathF.Pow2(Load.Fs)*(4.5f * Load.Fa + 2f * fb + 0.9f * Load.Fs) + 8f * Load.Fa * fB * Load.Fs);
            fMa = 0f;
            fMb = Load.Fq * Load.Fs / (12f * MathF.Pow2(fL)) * (MathF.Pow2(Load.Fa) * (6f * fb + 4f * Load.Fs) + MathF.Pow2(fb)*(3f * Load.Fa + Load.Fs) + MathF.Pow2(Load.Fs) * (3.5f * Load.Fa + 2f * fb + 0.7f * Load.Fs) + 8f * Load.Fa * fb * Load.Fs);
        }
        void GetEIF__0_35_UV(CMLoad_35 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa - Load.Fs;
            fA = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (MathF.Pow2(fB) * (3f * Load.Fa + 2 * fB + 4 * Load.Fs) + MathF.Pow2(Load.Fs) * (0.5f * Load.Fa + 2f * fb + 0.4f * Load.Fs) + 2f * Load.Fa * fB * Load.Fs);
            fB = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(Load.Fa) * (Load.Fa + 3 * fB + 3 * Load.Fs) + MathF.Pow2(fb) * (3 * Load.Fa + 2f*Load.Fs) + MathF.Pow2(Load.Fs) * (5.5f * Load.Fa + 4f * fb + 1.6f * Load.Fs) + 10f * Load.Fa * fB * Load.Fs);
            fMa = 0f;
            fMb = Load.Fq * Load.Fs / (12f * MathF.Pow2(fL)) * (MathF.Pow2(Load.Fa) * (6f * fb + 2f * Load.Fs) + MathF.Pow2(fb) * (3f * Load.Fa + 2f*Load.Fs) + MathF.Pow2(Load.Fs) * (2.5f * Load.Fa + 4f * fb + 0.8f * Load.Fs) + 10f * Load.Fa * fb * Load.Fs);
        }
        void GetEIF__0_36_UV(CMLoad_36 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(fb) *(2f*fL + Load.Fa) + Load.Fa*MathF.Pow2(Load.Fs));
            fB = (Load.Fq * Load.Fs * Load.Fa / (4f * MathF.Pow3(fL))) * (4f * Load.Fa * fL + 2f*MathF.Pow2(fb) * (3f*fL + Load.Fa) - MathF.Pow2(Load.Fs));
            fMa = 0f;
            fMb = (Load.Fq * Load.Fa * Load.Fs / (4f * MathF.Pow2(fL))) * (2f * fb * (fL + Load.Fa) -MathF.Pow2(Load.Fs));
        }
        // Trapezoidal
        void GetEIF__0_41_UV(CMLoad_41 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            // Not implemented yet
        }
        // Temperature
        void GetEIF__0_51_UV(CMLoad_51z Load, CMaterial objMat, CCrSc objCrSc, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = -3f / 2f * objMat.m_fE * objCrSc.m_fIy / fL * objMat.m_fAlpha_T * (Load.Ft_0_b - Load.Ft_0_u) / objCrSc.m_fh; 
            fB = -fA;
            fMa = 0f;
            fMb = 3f / 2f * ((objMat.m_fE * objCrSc.m_fIy * objMat.m_fAlpha_T * (Load.Ft_0_b - Load.Ft_0_u)) / objCrSc.m_fh);
        }
        void GetEIF__0_51_UV(CMLoad_51y Load, CMaterial objMat, CCrSc objCrSc, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = -3f / 2f * objMat.m_fE * objCrSc.m_fIy / fL * objMat.m_fAlpha_T * (Load.Ft_0_l - Load.Ft_0_r) / objCrSc.m_fh; 
            fB = -fA;
            fMa = 0f;
            fMb = 3f / 2f * ((objMat.m_fE * objCrSc.m_fIy * objMat.m_fAlpha_T * (Load.Ft_0_l - Load.Ft_0_r)) / objCrSc.m_fh);
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------------------
        // Objostranne jednoducho podoprety prut
        //----------------------------------------------------------------------------------------------------------------------------

        #region Reactions of Both Sides Simply Supported Member
        // Singular Load
        void GetEIF____11_UV(CMLoad_11 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = Load.FF * fb / fL;
            fB = Load.FF * Load.Fa / fL;
            fMa = fMb = 0f;
        }
        void GetEIF____12_UV(CMLoad_12 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = Load.FF / 2f;
            fB = fA;
            fMa = fMb = 0f;
        }
        void GetEIF____13_UV(CMLoad_13 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = -Load.FM / fL;
            fB = -fA;
            fMa = fMb = 0f;
        }
        // Uniform Load
        void GetEIF____21_UV(CMLoad_21 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 0.5f * Load.Fq * fL;
            fB = fA;
            fMa = fMb = 0f;
        }
        void GetEIF____22_UV(CMLoad_22 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = ((Load.Fq * Load.Fa) / (2f*fL)) * (2f *fL - Load.Fa);
            fB = Load.Fq * MathF.Pow2(Load.Fa) / (2f * fL);
            fMa = fMb = 0f;
        }
        void GetEIF____23_UV(CMLoad_23 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * MathF.Pow3(fb) / (8f * MathF.Pow3(fL))) * (3f * fL + Load.Fa);
            fB = ((Load.Fq * fb) / (8f * MathF.Pow3(fL))) * (14f * (2f * MathF.Pow2(fL) - MathF.Pow2(fb)) + MathF.Pow3(fb));
            fMa = fMb = 0f;
        }
        void GetEIF____24_UV(CMLoad_24 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * Load.Fs / (8f * MathF.Pow3(fL))) * (4f * MathF.Pow2(fb) * (2f * fL + Load.Fa) + Load.Fa * MathF.Pow2(Load.Fs));
            fB = (Load.Fq * Load.Fs * Load.Fa / (8f * MathF.Pow3(fL))) * (8f * MathF.Pow2(fL) + 4f * fb * (fL + Load.Fa) - MathF.Pow2(Load.Fs));
            fMa = fMb = 0f;
        }
        // Triangular Load
        void GetEIF____31_UV(CMLoad_31 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 11f / 64f * Load.Fq * fL;
            fB = 21f / 64f * Load.Fq * fL;
            fMa = fMb = 0f;
        }
        void GetEIF____32_UV(CMLoad_32 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 11f / 40f * Load.Fq * fL;
            fB = 9f / 40f * Load.Fq * fL;
            fMa = fMb = 0f;
        }
        void GetEIF____33_UV(CMLoad_33 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = 1f / 10f * Load.Fq * fL;
            fB = 2f / 5f * Load.Fq * fL;
            fMa = fMb = 0f;
        }
        void GetEIF____34_UV(CMLoad_34 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa - Load.Fs;
            fA = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (MathF.Pow2(fB) * (3f * Load.Fa + 2 * fB + 5 * Load.Fs) + MathF.Pow2(Load.Fs) * (1.5f * Load.Fa + 4f * fb + 1.1f * Load.Fs) + 4f * Load.Fa * fB * Load.Fs);
            fB = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(Load.Fa) * (Load.Fa + 3 * fB + 3 * Load.Fs) + MathF.Pow2(fb) * (3 * Load.Fa + Load.Fs) + MathF.Pow2(Load.Fs) * (4.5f * Load.Fa + 2f * fb + 0.9f * Load.Fs) + 8f * Load.Fa * fB * Load.Fs);
            fMa = fMb = 0f;
        }
        void GetEIF____35_UV(CMLoad_35 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa - Load.Fs;
            fA = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (MathF.Pow2(fB) * (3f * Load.Fa + 2 * fB + 4 * Load.Fs) + MathF.Pow2(Load.Fs) * (0.5f * Load.Fa + 2f * fb + 0.4f * Load.Fs) + 2f * Load.Fa * fB * Load.Fs);
            fB = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(Load.Fa) * (Load.Fa + 3 * fB + 3 * Load.Fs) + MathF.Pow2(fb) * (3 * Load.Fa + 2f * Load.Fs) + MathF.Pow2(Load.Fs) * (5.5f * Load.Fa + 4f * fb + 1.6f * Load.Fs) + 10f * Load.Fa * fB * Load.Fs);
            fMa = fMb = 0f;
        }
        void GetEIF____36_UV(CMLoad_36 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            float fb = fL - Load.Fa;
            fA = (Load.Fq * Load.Fs / (4f * MathF.Pow3(fL))) * (2f * MathF.Pow2(fb) * (2f * fL + Load.Fa) + Load.Fa * MathF.Pow2(Load.Fs));
            fB = (Load.Fq * Load.Fs * Load.Fa / (4f * MathF.Pow3(fL))) * (4f * Load.Fa * fL + 2f * MathF.Pow2(fb) * (3f * fL + Load.Fa) - MathF.Pow2(Load.Fs));
            fMa = fMb = 0f;
        }
        // Trapezoidal
        void GetEIF____41_UV(CMLoad_41 Load, float fL, float fA, float fB, float fMa, float fMb)
        {
            // Not implemented yet
            fMa = fMb = 0f;
        }
        // Temperature
        void GetEIF____51_UV(CMLoad_51z Load, CMaterial objMat, CCrSc objCrSc, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = -3f / 2f * objMat.m_fE * objCrSc.m_fIy / fL * objMat.m_fAlpha_T * (Load.Ft_0_b - Load.Ft_0_u) / objCrSc.m_fh;
            fB = -fA;
            fMa = fMb = 0f;
        }
        void GetEIF____51_UV(CMLoad_51y Load, CMaterial objMat, CCrSc objCrSc, float fL, float fA, float fB, float fMa, float fMb)
        {
            fA = -3f / 2f * objMat.m_fE * objCrSc.m_fIy / fL * objMat.m_fAlpha_T * (Load.Ft_0_l - Load.Ft_0_r) / objCrSc.m_fh;
            fB = -fA;
            fMa = fMb = 0f;
        }

        #endregion

       // Este chyba konzola !!!!!!





















    }
}
