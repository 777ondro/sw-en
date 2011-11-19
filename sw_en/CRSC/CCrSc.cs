using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATERIAL;
using CENEX;

namespace CRSC
{
    [Serializable]
    public class CCrSc
    {
        private int m_iCrSc_ID;

        public int ICrSc_ID
        {
            get { return m_iCrSc_ID; }
            set { m_iCrSc_ID = value; }
        }

        private float m_fh, m_fb; // Total depth and width of section (must be defined for all section shapes)

        private float m_fU,
        m_fA_g,
        m_fS_y,
        m_fI_y,
        m_fW_y_el,
        m_fW_y_pl,
        m_ff_y_plel,
        m_fS_z,
        m_fI_z,
        m_fW_z_el,
        m_fW_z_pl,
        m_ff_z_plel,
        m_fW_t_el,
        m_fI_t,
        m_fi_t,
        m_fq_t,
        m_fW_t_pl,
        m_ff_t_plel,
        m_fEta_y_v,
        m_fA_y_v_el,
        m_fA_y_v_pl,
        m_ff_y_v_plel,
        m_fEta_z_v,
        m_fA_z_v_el,
        m_fA_z_v_pl,
        m_ff_z_v_plel;

        public float Fh
        {
            get { return m_fh; }
            set { m_fh = value; }
        }

        public float Fb
        {
            get { return m_fb; }
            set { m_fb = value; }
        }

        public float FI_y
        {
            get { return m_fI_y; }
            set { m_fI_y = value; }
        }

        public float FI_z
        {
            get { return m_fI_z; }
            set { m_fI_z = value; }
        }

        public float FI_t
        {
            get { return m_fI_t; }
            set { m_fI_t = value; }
        }

        public float FS_y
        {
            get { return m_fS_y; }
            set { m_fS_y = value; }
        }

        public float FS_z
        {
            get { return m_fS_z; }
            set { m_fS_z = value; }
        }

        public float FA_g
        {
            get { return m_fA_g; }
            set { m_fA_g = value; }
        }

        public float FU
        {
            get { return m_fU; }
            set { m_fU = value; }
        }
















        public CMat_00 m_Mat = new CMat_00();

        // Constructor 1
        public CCrSc()
        { 
        
        }
        // Constructor 2
        public CCrSc(CMat_00 objMat)
        {
            m_Mat = objMat; // !!! Nevytvarat lokalne kopie !!!
        }

    }
}
