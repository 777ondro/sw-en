using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using MATERIAL;
using CENEX;

namespace CRSC
{
    [Serializable]
    public abstract class CCrSc
    {
        // Collection of indices for generation of member surface
        // Plati aj pre pruty s nabehom s rovnakym poctom bodov v reze - 2D
        // Use also for tapered members with same number of nodes (points) in section - 2D
        private Int32Collection m_TriangleIndices;

        public Int32Collection TriangleIndices
        {
            get { return m_TriangleIndices; }
            set { m_TriangleIndices = value; }
        }


        // New

        private Int32Collection m_TriangleIndicesFrontSide;

        public Int32Collection TriangleIndicesFrontSide
        {
            get { return m_TriangleIndicesFrontSide; }
            set { m_TriangleIndicesFrontSide = value; }
        }

        private Int32Collection m_TriangleIndicesShell;

        public Int32Collection TriangleIndicesShell
        {
            get { return m_TriangleIndicesShell; }
            set { m_TriangleIndicesShell = value; }
        }

        private Int32Collection m_TriangleIndicesBackSide;

        public Int32Collection TriangleIndicesBackSide
        {
            get { return m_TriangleIndicesBackSide; }
            set { m_TriangleIndicesBackSide = value; }
        }









        // Type of cross-section
        private bool m_bIsShapeSolid; // 0 (false) - Hollow section, 1 (true) - Solid section

        public bool IsShapeSolid
        {
            get { return m_bIsShapeSolid; }
            set { m_bIsShapeSolid = value; }
        }

        // Total number of points per section in 2D
        private short m_iTotNoPoints;

        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        // Number of points per inside surface of section in 2D - hollow sections
        private short m_iNoPointsIn;

        public short INoPointsIn
        {
            get { return m_iNoPointsIn; }
            set { m_iNoPointsIn = value; }
        }

        // Number of points per outside surface of section in 2D - hollow sections
        private short m_iNoPointsOut;

        public short INoPointsOut
        {
            get { return m_iNoPointsOut; }
            set { m_iNoPointsOut = value; }
        }

        // Use for Inside surface of hollow sections
        private float[,] m_CrScPointsIn;

        public float[,] CrScPointsIn
        {
            get { return m_CrScPointsIn; }
            set { m_CrScPointsIn = value; }
        }

        // Use for Outside surface of hollow sections and surface of solid sections
        private float[,] m_CrScPointsOut;

        public float[,] CrScPointsOut
        {
            get { return m_CrScPointsOut; }
            set { m_CrScPointsOut = value; }
        }

        private int m_iCrSc_ID;

        public int ICrSc_ID
        {
            get { return m_iCrSc_ID; }
            set { m_iCrSc_ID = value; }
        }


        // !!! Pre FEM vypocet nepotrebujeme vsetky charakteristiky, len Ag, Avy, Avz, Iy, Iz, It !!!!
        // !!! Ostatne charakteristiky postaci nacitavat az pri posudeni

        // Gross- corss-section area Ag
        // Shear effective area A_y_v (A_2_v, A_u_v) - optional
        // Shear effective area A_z_v (A_3_v, A_v_v) - optional
        // Moment of inertia - major principal axis Iy (I2, Iu)
        // Moment of inertia - minor principal axis Iz (I3, Iv)
        // Torsional inertia constant I_T
        // Section warping constant I_w  - optional (7th degree of freedom)


        // Gross-cross section area
        //m_fAg = m_fb * m_fh; // Unit [m2]
        // Second moment of Area / Moment of inertia
        //m_fIy = 1f / 12f * m_fb * m_fh * m_fh * m_fh;  // Unit [m4]
        //m_fIz = 1f / 12f * m_fb * m_fb * m_fb * m_fh;  // Unit [m4]
        // Torsional constant (St. Venant Section Modulus)
        //m_fI_t = (m_fb * m_fb * m_fb * m_fh * m_fh * m_fh) / ((3.645f - (0.06f * m_fh / m_fb)) * (m_fb * m_fb + m_fh * m_fh));  // Unit [m4]



        private float m_fh, m_fb; // Total depth and width of section (must be defined for all section shapes)

        private float m_fU,
        m_fA_g,
        m_fA_vy,
        m_fA_vz,
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
        m_fI_w,
        m_fEta_y_v,
        m_fA_y_v_el,
        m_fA_y_v_pl,
        m_ff_y_v_plel,
        m_fEta_z_v,
        m_fA_z_v_el,
        m_fA_z_v_pl,
        m_ff_z_v_plel;

        public float FI_w
        {
            get { return m_fI_w; }
            set { m_fI_w = value; }
        }

        public float FEta_z_v
        {
            get { return m_fEta_z_v; }
            set { m_fEta_z_v = value; }
        }

        public float FA_z_v_pl
        {
            get { return m_fA_z_v_pl; }
            set { m_fA_z_v_pl = value; }
        }

        public float FA_z_v_el
        {
            get { return m_fA_z_v_el; }
            set { m_fA_z_v_el = value; }
        }

        public float FA_y_v_pl
        {
            get { return m_fA_y_v_pl; }
            set { m_fA_y_v_pl = value; }
        }

        public float FA_y_v_el
        {
            get { return m_fA_y_v_el; }
            set { m_fA_y_v_el = value; }
        }

        public float FEta_y_v
        {
            get { return m_fEta_y_v; }
            set { m_fEta_y_v = value; }
        }

        public float Ff_t_plel
        {
            get { return m_ff_t_plel; }
            set { m_ff_t_plel = value; }
        }

        public float FW_t_pl
        {
            get { return m_fW_t_pl; }
            set { m_fW_t_pl = value; }
        }

        public float FW_t_el
        {
            get { return m_fW_t_el; }
            set { m_fW_t_el = value; }
        }

        public float Fi_t
        {
            get { return m_fi_t; }
            set { m_fi_t = value; }
        }

        public float Ff_z_v_plel
        {
            get { return m_ff_z_v_plel; }
            set { m_ff_z_v_plel = value; }
        }

        public float Ff_z_plel
        {
            get { return m_ff_z_plel; }
            set { m_ff_z_plel = value; }
        }

        public float FW_z_pl
        {
            get { return m_fW_z_pl; }
            set { m_fW_z_pl = value; }
        }

        public float FW_z_el
        {
            get { return m_fW_z_el; }
            set { m_fW_z_el = value; }
        }

        public float Ff_y_plel
        {
            get { return m_ff_y_plel; }
            set { m_ff_y_plel = value; }
        }

        public float Ff_y_v_plel
        {
            get { return m_ff_y_v_plel; }
            set { m_ff_y_v_plel = value; }
        }

        public float FW_y_pl
        {
            get { return m_fW_y_pl; }
            set { m_fW_y_pl = value; }
        }

        public float FW_y_el
        {
            get { return m_fW_y_el; }
            set { m_fW_y_el = value; }
        }

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

        public float FA_vy
        {
            get { return m_fA_vy; }
            set { m_fA_vy = value; }
        }

        public float FA_vz
        {
            get { return m_fA_vz; }
            set { m_fA_vz = value; }
        }

        public float FU
        {
            get { return m_fU; }
            set { m_fU = value; }
        }
















        public CMat_00 m_Mat = new CMat_00();

		//// Constructor 1
		//public CCrSc()
		//{ 
        
		//}
		//// Constructor 2
		//public CCrSc(CMat_00 objMat)
		//{
		//    m_Mat = objMat; // !!! Nevytvarat lokalne kopie !!!
		//}


		// Draw Rectangle / Add rectangle indices - clockwise CW numbering of input points 1,2,3,4 (see scheme)
		// Add in order 1,2,3,4
		protected void AddRectangleIndices_CW_1234(Int32Collection Indices,
			  int point1, int point2,
			  int point3, int point4)
		{
			// Main numbering is clockwise

			// 1  _______  2
			//   |_______| 
			// 4           3

			// Triangles Numbering is Counterclockwise
			// Top Right
			Indices.Add(point1);
			Indices.Add(point3);
			Indices.Add(point2);

			// Bottom Left
			Indices.Add(point1);
			Indices.Add(point4);
			Indices.Add(point3);
		}

		// Draw Rectangle / Add rectangle indices - countrer-clockwise CCW numbering of input points 1,2,3,4 (see scheme)
		// Add in order 1,4,3,2
		protected void AddRectangleIndices_CCW_1234(Int32Collection Indices,
			  int point1, int point2,
			  int point3, int point4)
		{
			// Main input numbering is clockwise, add indices counter-clockwise

			// 1  _______  2
			//   |_______| 
			// 4           3

			// Triangles Numbering is Clockwise
			// Top Right
			Indices.Add(point1);
			Indices.Add(point2);
			Indices.Add(point3);

			// Bottom Left
			Indices.Add(point1);
			Indices.Add(point3);
			Indices.Add(point4);
		}

        // Draw Triangle / Add triangle indices - clockwise CW numbering of input points 1,2,3 (see scheme)
        // Add in order 1,2,3,4
        protected void AddTriangleIndices_CW_123(Int32Collection Indices,
              int point1, int point2,
              int point3)
        {
            // Main numbering is clockwise

            // 1  _______  2
            //           | 
            //             3

            // Triangle Numbering is Counterclockwise
            Indices.Add(point1);
            Indices.Add(point3);
            Indices.Add(point2);
        }

        // Draw Triangle / Add triangle indices - countrer-clockwise CCW numbering of input points 1,2,3 (see scheme)
        // Add in order 1,3,2
        protected void AddTriangleIndices_CCW_123(Int32Collection Indices,
              int point1, int point2,
              int point3)
        {
            // Main input numbering is clockwise, add indices counter-clockwise

            // 1  _______  2
            //           | 
            //             3

            // Triangles Numbering is Clockwise
            Indices.Add(point1);
            Indices.Add(point2);
            Indices.Add(point3);
        }

        // Draw Prism CaraLaterals
        // Kresli plast hranola pre kontinualne pravidelne cislovanie bodov
        protected void DrawCaraLaterals(int secNum, Int32Collection TriangelsIndices)
		{
			// secNum - number of one base edges / - pocet rohov - hranicnych bodov jednej podstavy

			// Shell (Face)Surface
			// Cycle for regular numbering of section points

			for (int i = 0; i < secNum; i++)
			{
				if (i < secNum - 1)
					AddRectangleIndices_CW_1234(TriangelsIndices, i, secNum + i, secNum + i + 1, i + 1);
				else
					AddRectangleIndices_CW_1234(TriangelsIndices, i, secNum + i, secNum, 0); // Last Element
			}
		}

		// Draw Prism CaraLaterals
		// Kresli plast hranola pre pravidelne cislovanie bodov s vynechanim pociatocnych uzlov - pomocne 
		protected void DrawCaraLaterals(int iAuxNum, int secNum, Int32Collection TriangelsIndices)
		{
			// iAuxNum - number of auxiliary points - start ofset
			// secNum - number of one base edges / - pocet rohov - hranicnych bodov jednej podstavy (tento pocet neobsahuje pomocne body iAuxNum)

			// Shell (Face)Surface
			// Cycle for regular numbering of section points

			for (int i = 0; i < secNum; i++)
			{
				if (i < secNum - 1)
					AddRectangleIndices_CW_1234(TriangelsIndices, iAuxNum + i, 2 * iAuxNum + secNum + i, 2 * iAuxNum + secNum + i + 1, iAuxNum + i + 1);
				else
					AddRectangleIndices_CW_1234(TriangelsIndices, iAuxNum + i, 2 * iAuxNum + secNum + i, 2 * iAuxNum + secNum, iAuxNum + 0); // Last Element
			}
		}

		// Draw Sector of Solid Circle
		// Kresli vyrez kruhu,
		// Parametre:
		// pocet pomocnych uzlov;  ID stredu vyrezu; ID  prvy bod obluka; pocet segmentov (trojuholnikov); kolekcia, do ktorej sa zapisuju trojice, vzostupne cislovanie CW
		protected void AddSolidCircleSectorIndices(int iCentrePointID, int iArcFirstPointID, int iRadiusSegment, Int32Collection TriangelsIndices, bool bAscendingNumCW)
		{
			for (int i = 0; i < iRadiusSegment; i++)
			{
				TriangelsIndices.Add(iCentrePointID); // Centre point
				if (!bAscendingNumCW) // Clock-wise
				{
					TriangelsIndices.Add(iArcFirstPointID + 1 + i);
					TriangelsIndices.Add(iArcFirstPointID + i);
				}
				else // Counter Clock-wise
				{
					TriangelsIndices.Add(iArcFirstPointID + i);
					TriangelsIndices.Add(iArcFirstPointID + 1 + i);
				}
			}
		}

        protected abstract void loadCrScIndices();

        // New
        protected abstract void loadCrScIndicesFrontSide();
        protected abstract void loadCrScIndicesShell();
        protected abstract void loadCrScIndicesBackSide();
    }
}
