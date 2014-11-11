using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BaseClasses
{
    // Class CNode
    [Serializable]
    public class CNode : CEntity
    {
        public float X;
        public float Y;
        public float Z;

        // Konstruktor1 CNode
        public CNode()
        {

        }
        // Konstruktor2 CNode (2D)
        public CNode(
            int iNode_ID,
            float fCoord_X,
            float fCoord_Y,
            int fTime
            )
        {
            ID = iNode_ID;
            X = fCoord_X;
            Y = fCoord_Y;
            FTime = fTime;
        }

        // Konstruktor3 CNode (3D)
        public CNode(
            int iNode_ID,
            float fCoord_X,
            float fCoord_Y,
            float fCoord_Z,
            int fTime
            )
        {
            ID = iNode_ID;
            X = fCoord_X;
            Y = fCoord_Y;
            Z = fCoord_Z;
            FTime = fTime;
        }


        #region IComparer Members

        public int Compare(object x, object y)
        {
            return ((CNode)x).ID - ((CNode)y).ID;
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.ID - ((CNode)obj).ID;
        }

        #endregion
    } // End of Class CNode

    public class CCompare_NodeID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CNode)x).ID - ((CNode)y).ID;
        }
    }

}
