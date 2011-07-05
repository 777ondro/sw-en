using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_1D
{
    public class CCrSc
    {

        public float m_fAg;
        public float m_fIy;
        public float m_fIz;
        public float m_fh;
        public float m_fb;
        public float m_fI_T;

        public CCrSc()
        {}

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
