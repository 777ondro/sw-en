using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using MATH;

namespace CRSC
{
    public class CCrSc_2_00_AAC_Roof_Panel : CCrSc_2_00
    {
        // Solid AAC roof panel - grooved

        public CCrSc_2_00_AAC_Roof_Panel()
        {
        }
        public CCrSc_2_00_AAC_Roof_Panel(float fh, float fb)
        {
            IsShapeSolid = true;

            //ITotNoPoints = 12;
            ITotNoPoints = 12;
            Fh = fh;
            Fb = fb;

            // Create Array - allocate memory
            CrScPointsOut = new float[ITotNoPoints, 2];

            // Fill Array Data
            CalcCrSc_Coord();

            // Particular indices Rozpracovane pre vykreslovanie cela prutu inou farbou
            loadCrScIndicesFrontSide();
            loadCrScIndicesShell();
            loadCrScIndicesBackSide();

            // All indices together
            //loadCrScIndices();
        }

        public new void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Outside Points Coordinates

            float xc = 0.03f; // 30 mm ???

            float ya = 0.3f * Fh;
            float yb = 0.4f * Fh;

            float h = (float)Fh;
            float b = (float)Fb;

            CrScPointsOut[0, 0] = 0.0f;
            CrScPointsOut[0, 1] = h;
            CrScPointsOut[1, 0] = b;
            CrScPointsOut[1, 1] = h;
            CrScPointsOut[2, 0] = b;
            CrScPointsOut[2, 1] = h - ya;
            CrScPointsOut[3, 0] = b + xc;
            CrScPointsOut[3, 1] = h - yb;
            CrScPointsOut[4, 0] = b + xc;
            CrScPointsOut[4, 1] = yb;
            CrScPointsOut[5, 0] = b;
            CrScPointsOut[5, 1] = ya;
            CrScPointsOut[6, 0] = b;
            CrScPointsOut[6, 1] = 0;
            CrScPointsOut[7, 0] = 0;
            CrScPointsOut[7, 1] = 0;
            CrScPointsOut[8, 0] = 0;
            CrScPointsOut[8, 1] = ya;
            CrScPointsOut[9, 0] = xc;
            CrScPointsOut[9, 1] = yb;
            CrScPointsOut[10, 0] = xc;
            CrScPointsOut[10, 1] = h - yb;
            CrScPointsOut[11, 0] = 0;
            CrScPointsOut[11, 1] = h - ya;
        }

        protected override void loadCrScIndicesFrontSide()
        {
            TriangleIndicesFrontSide = new Int32Collection();

            AddTriangleIndices(TriangleIndicesFrontSide, 0, 2, 1);
            AddTriangleIndices(TriangleIndicesFrontSide, 0, 11, 2);
            AddTriangleIndices(TriangleIndicesFrontSide, 11, 10, 3);
            AddTriangleIndices(TriangleIndicesFrontSide, 11, 3, 2);
            AddTriangleIndices(TriangleIndicesFrontSide, 10, 4, 3);
            AddTriangleIndices(TriangleIndicesFrontSide, 10, 9, 4);
            AddTriangleIndices(TriangleIndicesFrontSide, 9, 5, 4);
            AddTriangleIndices(TriangleIndicesFrontSide, 9, 8, 5);
            AddTriangleIndices(TriangleIndicesFrontSide, 8, 6, 5);
            AddTriangleIndices(TriangleIndicesFrontSide, 8, 7, 6);
        }

        protected override void loadCrScIndicesShell()
        {
            TriangleIndicesShell = new Int32Collection();

            // Shell Surface OutSide
            for (int i = 0; i < ITotNoPoints - 1; i++)
            {
                if (i < ITotNoPoints - 2)
                    AddRectangleIndices_CW_1234(TriangleIndicesShell, i, ITotNoPoints + i, ITotNoPoints + i + 1, i + 1);
                else
                    AddRectangleIndices_CW_1234(TriangleIndicesShell, i, ITotNoPoints + i, ITotNoPoints, 0); // Last Element

            }
        }

        protected override void loadCrScIndicesBackSide()
        {
            TriangleIndicesBackSide = new Int32Collection();

            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints, ITotNoPoints + 2, ITotNoPoints + 1);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints, ITotNoPoints + 11, ITotNoPoints + 2);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 11, ITotNoPoints + 10, ITotNoPoints + 3);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 11, ITotNoPoints + 3, ITotNoPoints + 2);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 10, ITotNoPoints + 4, ITotNoPoints + 3);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 10, ITotNoPoints + 9, ITotNoPoints + 4);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 9, ITotNoPoints + 5, ITotNoPoints + 4);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 9, ITotNoPoints + 8, ITotNoPoints + 5);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 8, ITotNoPoints + 6, ITotNoPoints + 5);
            AddTriangleIndices(TriangleIndicesBackSide, ITotNoPoints + 8, ITotNoPoints + 7, ITotNoPoints + 6);
        }

        private void AddTriangleIndices(Int32Collection collection, int a, int b, int c)
        {
            collection.Add(a);
            collection.Add(b);
            collection.Add(c);
        }


}
}
