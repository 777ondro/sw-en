using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace BaseClasses
{
	[Serializable]
	public class CMLoad : CEntity
	{
		//----------------------------------------------------------------------------
		private int[] m_iMemberCollection; // List / Collection of members where load is defined
		private EMLoadTypeDistr m_mLoadTypeDistr; // Type of external force distribution
		private EMLoadType m_mLoadType; // Type of external force
		private EMLoadDirPCC1 m_eDirPPC; // External Force Direction in Principal Coordinate System of Member
		//----------------------------------------------------------------------------
		public int[] IMemberCollection
		{
			get { return m_iMemberCollection; }
			set { m_iMemberCollection = value; }
		}
		public EMLoadTypeDistr MLoadTypeDistr
		{
			get { return m_mLoadTypeDistr; }
			set { m_mLoadTypeDistr = value; }
		}
		public EMLoadType MLoadType
		{
			get { return m_mLoadType; }
			set { m_mLoadType = value; }
		}
		public EMLoadDirPCC1 EDirPPC
		{
			get { return m_eDirPPC; }
			set { m_eDirPPC = value; }
		}

		//----------------------------------------------------------------------------
		//----------------------------------------------------------------------------
		//----------------------------------------------------------------------------


		public CMLoad()
		{

		}


	}
}
