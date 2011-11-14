using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_1Din3D
{
    public class CCrSc
    {
        // Load data of cross section, there could be different values in each section of member / segment (tapered member)
        // For members/ segment with constant size can be used parameters directly of database profile or parametric cross-section
        // For Fem calculation it is not necessary to know all properties

        // Gross- corss-section area Ag
        // Shear effective area A_y_v (A_2_v, A_u_v) - optional
        // Shear effective area A_z_v (A_3_v, A_v_v) - optional
        // Moment of inertia - major principal axis Iy (I2, Iu)
        // Moment of inertia - minor principal axis Iz (I3, Iv)
        // Torsional inertia constant I_T
        // Section warping constant I_w  - optional (7th degree of freedom)

        public float m_fAg;
        public float m_fIy;
        public float m_fIz;
        public float m_fh; // not needed
        public float m_fb; // not needed
        public float m_fI_T;

        public CCrSc()
        {}



        // Temporary !!!!!

        public void CalcProp_RecSol(ECrScShType1 eCrScType)
        {
            switch (eCrScType)
            {
                case ECrScShType1.eCrScType_I:
                    {
                        // Temp
                        m_fb = 0.125f;
                        m_fh = 0.3f;

                        // Gross-cross section area
                        m_fAg =0.0069f; // Unit [m2]

                        // Second moment of Area / Moment of inertia
                        m_fIy = 9.79e-5f;  // Unit [m4]
                        m_fIz = 4.49e-6f;  // Unit [m4]

                        // Torsional constant (St. Venant Section Modulus)

                        m_fI_T = (m_fb * m_fb * m_fb * m_fh * m_fh * m_fh) / ((3.645f - (0.06f * m_fh / m_fb)) * (m_fb * m_fb + m_fh * m_fh));  // Unit [m4]


                        // Temporary
                        m_fI_T = 5.69e-7f;
                        break;
                    }
                case ECrScShType1.eCrScType_BOX:
                    {
                        // Gross-cross section area
                        m_fAg = m_fb * m_fh; // Unit [m2]

                        // Second moment of Area / Moment of inertia
                        m_fIy = 1f / 12f * m_fb * m_fh * m_fh * m_fh;  // Unit [m4]
                        m_fIz = 1f / 12f * m_fb * m_fb * m_fb * m_fh;  // Unit [m4]

                        break;
                    }
                default:
                    {



                        break;
                    }
            }
        }
    }
}
