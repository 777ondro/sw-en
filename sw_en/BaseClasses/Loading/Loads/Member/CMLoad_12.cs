using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BaseClasses.GraphObj.Objects_3D;

namespace BaseClasses
{
    public class CMLoad_12 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fF; // Force Value

        //----------------------------------------------------------------------------
        public float FF
        {
            get { return m_fF; }
            set { m_fF = value; }
        }

        public float m_fOpacity;
        public Color m_Color = new Color(); // Default
        public DiffuseMaterial m_Material = new DiffuseMaterial();
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_12(float fF)
        {
            FF = fF;
        }

        public CMLoad_12(float fF, int iMemberID)
        {
            FF = fF;

            // Dokoncit
            //IMemberCollection. add member ID
        }

        public CMLoad_12(float fF,
            CMember member_aux,
            EMLoadTypeDistr mLoadTypeDistr,
            EMLoadType mLoadType,
            EMLoadDirPCC1 eDirPPC,
            bool bIsDislayed,
            int fTime)
        {
            FF = fF;
            Member = member_aux;
            MLoadTypeDistr = mLoadTypeDistr;
            MLoadType = mLoadType;
            EDirPPC = eDirPPC;
            BIsDisplayed = bIsDislayed;
            FTime = fTime;
        }

        public override Model3DGroup CreateM_3D_G_Load()
        {
            Model3DGroup model_gr = new Model3DGroup();

            ENLoadType nLoadType = ENLoadType.eNLT_OTHER; // Auxliary

            if (MLoadType == EMLoadType.eMLT_F) // Force
            {
                if (EDirPPC == EMLoadDirPCC1.eMLD_PCC_FXX_MXX)
                {
                    nLoadType = ENLoadType.eNLT_Fx;
                }
                else if (EDirPPC == EMLoadDirPCC1.eMLD_PCC_FYU_MZV)
                {
                    nLoadType = ENLoadType.eNLT_Fy;
                }
                else if (EDirPPC == EMLoadDirPCC1.eMLD_PCC_FZV_MYU)
                {
                    nLoadType = ENLoadType.eNLT_Fz;
                }
            }
            else if (MLoadType == EMLoadType.eMLT_M) // Moment
            {
                if (EDirPPC == EMLoadDirPCC1.eMLD_PCC_FXX_MXX)
                {
                    nLoadType = ENLoadType.eNLT_Mx;
                }
                else if (EDirPPC == EMLoadDirPCC1.eMLD_PCC_FYU_MZV)
                {
                    nLoadType = ENLoadType.eNLT_Mz;
                }
                else if (EDirPPC == EMLoadDirPCC1.eMLD_PCC_FZV_MYU)
                {
                    nLoadType = ENLoadType.eNLT_My;
                }
            }
            else
            {
                nLoadType = ENLoadType.eNLT_OTHER;
            } //Temperature

            return model_gr = CreateM_3D_G_SimpleLoad(new Point3D(0.5f * Member.FLength, 0, 0), nLoadType, m_Color, FF, m_fOpacity, m_Material);
        }
    }
}
