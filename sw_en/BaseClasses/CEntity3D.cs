using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BaseClasses.GraphObj;

namespace BaseClasses
{
	//[Serializable]
    // Base class of all topological model entities
    abstract public class CEntity3D : CEntity
    {
        /*
        Model3DGroup mObject3DModel = new Model3DGroup();

        public Model3DGroup MObject3DModel
        {
            get { return mObject3DModel; }
            set { mObject3DModel = value; }
        }
        */

        public  CPoint m_pControlPoint = new CPoint();

        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        public CEntity3D() { }

        public void Transform3D_OnMemberEntity_fromLCStoGCS(Model3DGroup modelgroup, CMember member)
        {
            Transform3DGroup Trans3DGroup = new Transform3DGroup();
            RotateTransform3D RotateTrans3D_AUX_Y = new RotateTransform3D();
            RotateTransform3D RotateTrans3D_AUX_Z = new RotateTransform3D();

            double dAlphaX = 0;
            double dBetaY = 0;
            double dGammaZ = 0;
            double dBetaY_aux = 0;
            double dGammaZ_aux = 0;

            member.GetRotationAngles(out dAlphaX, out dBetaY, out dGammaZ, out dBetaY_aux, out dGammaZ_aux);

            RotateTrans3D_AUX_Y.Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), dBetaY_aux * 180 / Math.PI);
            RotateTrans3D_AUX_Y.CenterX = 0;
            RotateTrans3D_AUX_Y.CenterY = 0;
            RotateTrans3D_AUX_Y.CenterZ = 0;

            RotateTrans3D_AUX_Z.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), dGammaZ_aux * 180 / Math.PI);
            RotateTrans3D_AUX_Z.CenterX = 0;
            RotateTrans3D_AUX_Z.CenterY = 0;
            RotateTrans3D_AUX_Z.CenterZ = 0;

            // Klb na konci pruta - Otoci sa v podstate spravne, ale umiestni sa na zaciatku pruta ?!
            // Oba klby na zaciatku aj na konci sa teda vykresluju na zaciatku

            TranslateTransform3D Translate3D_AUX = new TranslateTransform3D(member.NodeStart.X, member.NodeStart.Y, member.NodeStart.Z);

            Trans3DGroup.Children.Add(RotateTrans3D_AUX_Y);
            Trans3DGroup.Children.Add(RotateTrans3D_AUX_Z);
            Trans3DGroup.Children.Add(Translate3D_AUX);

            // Uhly su nula, suradnice posunu su nula a predsa sa bounds v model3Dgroup zmenia ?!
            Model3DGroup mg = new Model3DGroup();
            mg = modelgroup;
            mg.Transform = Trans3DGroup;

            modelgroup.Transform = Trans3DGroup;
        }
    }
}
