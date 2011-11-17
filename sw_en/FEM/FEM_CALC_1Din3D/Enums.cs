using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEM_CALC_1Din3D
{
    public enum ESLN
    {
        e1D_1D,  // 1D members - simple member - colum or beam / continuous beam
        e2DD_1D, // 1D members - 2D truss, 2D frame
        e3DD_1D, // 1D members - 2D truss, 2D frame
        e4DD_1D  // 1D members - 3D + time
    }

    // Cartesian coordinate system, point in n-dimensional Euclidean space

    // In two dimensions

    /*Fixing or choosing the x-axis determines the y-axis up to direction. Namely, the y-axis is necessarily the perpendicular 
     * to the x-axis through the point marked 0 on the x-axis. But there is a choice of which of the two half lines on the perpendicular
     * to designate as positive and which as negative. Each of these two choices determines a different orientation (also called handedness) of the Cartesian plane.
    The usual way of orienting the axes, with the positive x-axis pointing right and the positive y-axis pointing up (and the x-axis being
     * the "first" and the y-axis the "second" axis) is considered the positive or standard orientation, also called the right-handed orientation.
    A commonly used mnemonic for defining the positive orientation is the right hand rule. Placing a somewhat closed right hand on the plane with the thumb pointing up,
     * the fingers point from the x-axis to the y-axis, in a positively oriented coordinate system.
    The other way of orienting the axes is following the left hand rule, placing the left hand on the plane with the thumb pointing up.
    When pointing the thumb away from the origin along an axis, the curvature of the fingers indicates a positive rotation along that axis.
    Regardless of the rule used to orient the axes, rotating the coordinate system will preserve the orientation. Switching any two axes will reverse the orientation.*/

    // In three dimensions

    /*
     Once the x- and y-axes are specified, they determine the line along which the z-axis should lie,
     but there are two possible directions on this line. The two possible coordinate systems which result are called 'right-handed' and 'left-handed'.
     The standard orientation, where the xy-plane is horizontal and the z-axis points up (and the x- and the y-axis form a positively oriented two-dimensional coordinate system
     in the xy-plane if observed from above the xy-plane) is called right-handed or positive.
     The name derives from the right-hand rule. If the index finger of the right hand is pointed forward, the middle finger bent inward at a right angle to it,
     and the thumb placed at a right angle to both, the three fingers indicate the relative directions of the x-, y-, and z-axes in a right-handed system.
     The thumb indicates the x-axis, the index finger the y-axis and the middle finger the z-axis. Conversely, if the same is done with the left hand, a left-handed system results.
     Because a three-dimensional object is represented on the two-dimensional screen, distortion and ambiguity result. The axis pointing downward (and to the right)
     is also meant to point towards the observer, whereas the "middle" axis is meant to point away from the observer.
     The red circle is parallel to the horizontal xy-plane and indicates rotation from the x-axis to the y-axis (in both cases).
     Hence the red arrow passes in front of the z-axis. */
    
    public enum EGCS
    {
        eGCSRightHanded,
        eGCSLeftHanded
    }

    //  DOF Vector Enum
    // Degrees of freedom 0-5 (6 - warping)
    public enum e3D_DOF
    {
        eUX = 0, // Displacement in X-Direction
        eUY = 1, // Displacement in Y-Direction
        eUZ = 2, // Displacement in Z-Direction
        eRX = 3, // Rotation around X-Axis
        eRY = 4, // Rotation around Y-Axis
        eRZ = 5, // Rotation around Z-Axis
        eW = 6   // Warping (not implemented yet)
    }

    //  Nodal Load Array Constants
    public enum e3D_E_F
    {
        eFX = 0,
        eFY = 1,
        eFZ = 2,

        eMX = 3,
        eMY = 4,
        eMZ = 5
    }
    
    // Internal Forces Enum
    public enum e3D_I_F
    {
      eNx = 0,
      eVy = 1,
      eVz = 2,

      eMx = 3,
      eMy = 4,
      eMz = 5
    }

    // Element SupportType
    public enum EElemSuppType
    {
        e3DEl_000000_000000 = 0, // Start Node - restrained DOF,                                                    End Node - restrained DOF
        e3DEl_000000_______ = 1, // Start Node - restrained DOF,                                                    End Node - free DOF
        e3DEl________000000 = 2, // Start Node - free DOF,                                                          End Node - restrained DOF
        e3DEl_000000_000___ = 3, // Start Node - restrained DOF,                                                    End Node - rotation hinge
        e3DEl_000____000000 = 4, // Start Node - rotation hinge,                                                    End Node - restrained DOF
        e3DEl_000000_0_00_0 = 5, // Start Node - restrained DOF,                                                    End Node - free displacement in y-Axis nad rotation about y-Axis
        e3DEl_0_00_0_000000 = 6, // Start Node - free displacement in y-Axis nad rotation about y-Axis,             End Node - restrained DOF
        e3DEl_000____000___ = 7  // Start Node - rotation hinge,                                                    End Node - rotation hinge
    }
}
