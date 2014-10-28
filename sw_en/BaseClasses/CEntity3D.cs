using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BaseClasses.GraphObj;

namespace BaseClasses
{
	[Serializable]
    // Base class of all topological model entities
    public class CEntity3D : CEntity
    {
        Model3DGroup mObject3DModel = new Model3DGroup();

        public Model3DGroup MObject3DModel
        {
            get { return mObject3DModel; }
            set { mObject3DModel = value; }
        }

        public CPoint m_pControlPoint = new CPoint();

        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        public CEntity3D() { }


    }
}
