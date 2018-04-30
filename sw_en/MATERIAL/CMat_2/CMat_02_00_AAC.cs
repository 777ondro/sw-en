using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MATERIAL
{
    public class CMat_02_00_AAC : CMat_02_00
    {

        double m_Rho_m;

        public double Rho_m
        {
            get => m_Rho_m;
            set => m_Rho_m = value;
        }

        double fctk_005;

        public double Fctk_005
        {
            get { return fctk_005; }
            set { fctk_005 = value; }
        }

        double fctk_095;

        public double Fctk_095
        {
            get { return fctk_095; }
            set { fctk_095 = value; }
        }

        double fcflk_005;

        public double Fcflk_005
        {
            get { return fcflk_005; }
            set { fcflk_005 = value; }
        }

        double fcflk_095;

        public double Fcflk_095
        {
            get { return fcflk_095; }
            set { fcflk_095 = value; }
        }

        double fE_cm;

        public double E_cm
        {
            get { return fE_cm; }
            set { fE_cm = value; }
        }

        public CMat_02_00_AAC()
        {

        }

        public CMat_02_00_AAC(int id_database, float frho_m_density)
        {
            FillData(id_database, frho_m_density);
        }

        public void FillData(int id_database, double rho_m_density)
        {
            Rho_m = rho_m_density; //kg/m^3
            Fck = AAC_fck_array[id_database] * 1.0e+6; // Pa

            Fctk_005 = 0.1f * Fck;
            Fctk_095 = 0.24f * Fck;
            Fcflk_005 = 0.18f * Fck;
            Fcflk_095 = 0.36f * Fck;
            E_cm = 5 * (Rho_m - 150) * 1e+6f; //Pa EN 12602 - 4.2.7(6)
        }

        // Autoclaved Aered Concrete

        private float[] AAC_fck_array = new float[] {1.5f, 2, 2.5f, 3, 3.5f, 4, 4.5f, 5, 5.5f, 6, 7, 8, 9, 10}; // MPa
    }
}
