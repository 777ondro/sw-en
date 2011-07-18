using System;
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
      float [] m_fEIF_A = new float [6];
      float [] m_fEIF_B = new float [6];

      //----------------------------------------------------------------------------
      //----------------------------------------------------------------------------
      //----------------------------------------------------------------------------
      public CMLoadPart(EMLoadType1 eLoadType, EMLoadDirPCC1 eLDirPCC, CMLoad_11 Load, CE_1D Element)
      {
          //  Fill with zero
          for (int i=0; i < 6; i++)
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
          fA = Load.FFValue * (float)Math.Pow(fL - Load.Fa, 2) / (float)Math.Pow(fL, 3) * (fL + 2f * Load.Fa);
          fB = Load.FFValue * (float)Math.Pow(Load.Fa, 2) / (float)Math.Pow(fL, 3) * (fL + 2f * (fL - Load.Fa));
          fMa = -Load.FFValue * Load.Fa * (float)Math.Pow(fL - Load.Fa, 2) / (float)Math.Pow(fL, 2);
          fMb = Load.FFValue * (float)Math.Pow(Load.Fa, 2) * (fL - Load.Fa) / (float)Math.Pow(fL, 2);
      }
      void GetEIF_00_12_UV(CMLoad_12 Load, float fL, float fA, float fB, float fMa, float fMb)
      {
          fA = Load.FFValue / 2f;
          fB = Load.FFValue / 2f;
          fMa = -Load.FFValue * fL / 8f;
          fMb = Load.FFValue * fL/8f;
      }
      void GetEIF_00_13_UV(CMLoad_13 Load, float fL, float fA, float fB, float fMa, float fMb)
      {
          fA = -6 * Load.FMValue / (float)Math.Pow(fL, 3) * Load.Fa * (fL - Load.Fa);
          fB = 6 * Load.FMValue / (float)Math.Pow(fL, 3) * Load.Fa * (fL - Load.Fa);
          fMa = Load.FMValue * (fL-Load.Fa)/ (float)Math.Pow(fL, 2) * (2*fL - 3* (fL - Load.Fa));
          fMb = Load.FMValue * Load.Fa/  (float)Math.Pow(fL, 2) * (2*fL - 3* Load.Fa);
      }
      // Uniform Load
      void GetEIF_00_21_UV(CMLoad_21 Load, float fL, float fA, float fB, float fMa, float fMb)
      {
          fA = Load.FqValue * fL / 2f;
          fB = Load.FqValue * fL / 2f;
          fMa = -Load.FqValue * fL * fL / 12f;
          fMb = Load.FqValue * fL * fL / 12f;
      }
      void GetEIF_00_22_UV(CMLoad_22 Load, float fL, float fA, float fB, float fMa, float fMb)
      {
          fA = (Load.FqValue *Load.FFa) / (2f * fL)  * ((2f*fL*(fL*fL - Load.FFa * Load.FFa)) +(float) Math.Pow(Load.FFa,3));
          fB = (Load.FqValue * (float)Math.Pow(Load.FFa,3) / (2f * (float)Math.Pow(fL,3))) * (fL + (fL-Load.FFa));
          fMa = -(Load.FqValue * Load.FFa * Load.FFa) / 12f*(float)Math.Pow(fL,2) * (Load.FFa*Load.FFa + 4f*Load.FFa*(fL-Load.FFa) + 6f*(float)Math.Pow(fL-Load.FFa,2));
          fMb = (Load.FqValue * (float)Math.Pow(Load.FFa, 3) / (12f*fL*fL)) * (Load.FFa * + 4f*(fL - Load.FFa));
      }
      void GetEIF_00_23_UV(CMLoad_23 Load, float fL, float fA, float fB, float fMa, float fMb)
      {

      
      }
      void GetEIF_00_24_UV(CMLoad_24 Load, float fL, float fA, float fB, float fMa, float fMb)
      {
          //!!!!!! Plati len ak je fa < fb !!!!
          // doriesit otocenie ak reakcii ak to neplati
          fA = ((Load.FqValue * Load.Fs) / (4f*(float)Math.Pow(fL,3))) * (4f*((float)Math.Pow(fL-Load.Fa,2)*(3f*Load.Fa + (fL-Load.Fa)) + (float)Math.Pow(Load.Fs,2)*(Load.Fa-(fL-Load.Fa))));



      }
      void GetEIF_00_31_UV(CMLoad_31 Load, float fL, float fA, float fB, float fMa, float fMb)
      {

          MathF.Pow3(3f);


      }



























    }
}
