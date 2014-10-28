using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace BaseClasses
{
	//[Serializable]
    // Base class of all topological model entities
    abstract public class CEntity
    {
        int m_ID; // Unique entity ID

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        float m_fTime;

        public float FTime
        {
            get { return m_fTime; }
            set { m_fTime = value; }
        }

        bool m_bIsDisplayed;

        public bool BIsDisplayed
        {
            get { return m_bIsDisplayed; }
            set { m_bIsDisplayed = value; }
        }

        private bool m_bIsDebugging;

        public bool BIsDebugging
        {
            get { return m_bIsDebugging; }
            set { m_bIsDebugging = value; }
        }



        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        public CEntity() { }


        //----------------------------------------------------------------------------------------------------------------
        public void Create()
        { }

        public void Delete()
        { }

        public void Edit()
        { }

        public void Draw()
        { }

        // Refresh, modify, update, redraw, copy, changeID .....


    }
}
