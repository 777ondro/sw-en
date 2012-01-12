using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
	[Serializable]
	public class CLoadCase
	{
		//----------------------------------------------------------------------------
		private int m_iLoadCase_ID;

		public int ILoadCase_ID
		{
			get { return m_iLoadCase_ID; }
			set { m_iLoadCase_ID = value; }
		}
		//----------------------------------------------------------------------------
		//----------------------------------------------------------------------------
		//----------------------------------------------------------------------------
		public CLoadCase()
		{

		}
	}
}
