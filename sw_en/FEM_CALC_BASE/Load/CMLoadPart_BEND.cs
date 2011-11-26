﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;

namespace FEM_CALC_BASE
{
    public class CMLoadPart_BEND : CMLoad
    {
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoadPart_BEND()
        { }

        public CMLoadPart_BEND(CMLoad Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            if (Load is CMLoad_11)
            {
                GetMLoadPart_11((CMLoad_11)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_12)
            {
                GetMLoadPart_12((CMLoad_12)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_21)
            {
                GetMLoadPart_21((CMLoad_21)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_22)
            {
                GetMLoadPart_22((CMLoad_22)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_23)
            {
                GetMLoadPart_23((CMLoad_23)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_24)
            {
                GetMLoadPart_24((CMLoad_24)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_31)
            {
                GetMLoadPart_31((CMLoad_31)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_32)
            {
                GetMLoadPart_32((CMLoad_32)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_33)
            {
                GetMLoadPart_33((CMLoad_33)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_34)
            {
                GetMLoadPart_34((CMLoad_34)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_35)
            {
                GetMLoadPart_35((CMLoad_35)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_36)
            {
                GetMLoadPart_36((CMLoad_36)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else if (Load is CMLoad_41)
            {
                GetMLoadPart_41((CMLoad_41)Load, Member, eMType, out fA, out fB, out fMa, out fMb);
            }
            else // Exception
            {
                fA = fB = fMa = fMb = float.MaxValue; // Temporary !!!!
            }
        }

         void GetMLoadPart_11(CMLoad_11 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            switch (eMType)
            {
                case Enums.EElemSuppType.eEl_00_00: // Both Side restrained against axial direction displacement
                case Enums.EElemSuppType.eEl_00_0_:
                case Enums.EElemSuppType.eEl_0__00:
                case Enums.EElemSuppType.eEl_0__0_:
                    {
                        GetEIF_00_00_11_UV(Load, Member.FLength, out fA, out fB, out fMa, out fMb);
                        return;
                    }
                case Enums.EElemSuppType.eEl_00___:
                    {
                        GetEIF_00____11_UV(Load, Member.FLength, out fA, out fB, out fMa, out fMb);
                        return;
                    }
                case Enums.EElemSuppType.eEl____00:
                    {
                        GetEIF____00_11_UV(Load, Member.FLength, out fA, out fB, out fMa, out fMb);
                        return;
                    }
                default:
                    {
                        // Exception
                        fA = fB = fMa = fMb = 0.0f;
                        return;
                    }
            }
        }

        void GetMLoadPart_12(CMLoad_12 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
            fMa = fMb = 0.0f;
        }

        void GetMLoadPart_21(CMLoad_21 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_22(CMLoad_22 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_23(CMLoad_23 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_24(CMLoad_24 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_31(CMLoad_31 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_32(CMLoad_32 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_33(CMLoad_33 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_34(CMLoad_34 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_35(CMLoad_35 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_36(CMLoad_36 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }

        void GetMLoadPart_41(CMLoad_41 Load, CE_1D_BASE Member, FEM_CALC_BASE.Enums.EElemSuppType eMType, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }






        #region End Moments and Reactions of Both Sides Restrained member / Koncove momenty a reakcie obojstranne votknuteho nosnika

        void GetEIF_00_00_11_UV(CMLoad_11 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            fA = -6 * Load.FF / (float)Math.Pow(fL, 3) * Load.Fa * (fL - Load.Fa);
            fB = -fA; /*6 * Load.FM / (float)Math.Pow(fL, 3) * Load.Fa * (fL - Load.Fa);*/
            fMa = Load.FF * (fL - Load.Fa) / (float)Math.Pow(fL, 2) * (2 * fL - 3 * (fL - Load.Fa));
            fMb = Load.FF * Load.Fa / (float)Math.Pow(fL, 2) * (2 * fL - 3 * Load.Fa);
        }
        void GetEIF_00_00_12_UV(CMLoad_11 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
            fMa = fMb = 0.0f;
        }






        #endregion

        #region End Moments and Reactions of One Side Restrained and Other Side Simply Supported Member / Koncove momenty a reakcie nosnika na jednej strane votknuteho a na opacnej strane jednoducho podporeteho
        void GetEIF_00_0__11_UV(CMLoad_11 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            float fb = fL - Load.Fa;
            fA = -3f / 2f * ((MathF.Pow2(fL) - MathF.Pow2(fb)) / MathF.Pow3(fL)) * Load.FF;
            fB = -fA;
            fMa = -(Load.FF / (2 * MathF.Pow2(fL))) * (MathF.Pow2(fL) - 3 * MathF.Pow2(fb));
            fMb = 0.0f;
        }

        #endregion

        #region End Moments and Reactions of One Side Simply Supported and Other Side  Restrained Member / Koncove momenty a reakcie nosnnika na jednej strane jednoducho podporeteho a na opacnej strane votknuteho
        
        void GetEIF_0__00_11_UV(CMLoad_11 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            fA = -3f / 2f * ((MathF.Pow2(fL) - MathF.Pow2(Load.Fa)) / MathF.Pow3(fL)) * Load.FF;
            fB = -fA;
            fMa = 0f;
            fMb = (Load.FF / (2 * MathF.Pow2(fL))) * (MathF.Pow2(fL) - 3 * MathF.Pow2(Load.Fa));
        }






        #endregion

        #region End Moments and Reactions of Simply Supported Member / Koncove momenty a reakcie prosteho nosnika
        
        void GetEIF_0__0__11_UV(CMLoad_11 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            fA = -Load.FF / fL;
            fB = -fA;
            fMa = fMb = 0f;
        }



        #endregion

        #region Reactions of One Side Restrained and Other Side Unsupported Member

        void GetEIF_00____11_UV(CMLoad_11 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
            fMa = fMb = 0.0f;
        }
        void GetEIF_00____12_UV(CMLoad_12 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
            fMa = fMb = 0.0f;
        }







        #endregion


        #region Reactions of One Side Unsupported and Other Side Restrained Member

        void GetEIF____00_11_UV(CMLoad_11 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }
        void GetEIF____00_12_UV(CMLoad_12 Load, float fL, out float fA, out float fB, out float fMa, out float fMb)
        {
            // Temporary
            fA = fB = 0.0f;
                        fMa = fMb = 0.0f;
        }


        #endregion













    }
}
