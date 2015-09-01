using UnityEngine;
using System.Collections;
using System.Xml.Linq;

namespace MacroViewUI
{
    //Generic Tab class
    public class Tab
    {
        private string tabContent;
        private GameObject tabObj;
        public GameObject TabObj
        {
            get { return tabObj; }
            set { tabObj = value; }
        }

        public Tab(string objTag)
        {
            tabObj = GameObject.FindGameObjectWithTag(objTag);
        }

        //tab functionality functions
        public void updateTabContent(GameObject container, string info) { }
        //https://support.microsoft.com/en-us/kb/307548
        //https://msdn.microsoft.com/en-us/library/cc189056(v=vs.95).aspx
        public string findElemet(string tag, XDocument xmlDoc) { return null; }
        public void updateXMLDoc(string info, string tag) { }
    }

    //Contains all the different types of tabs in the Management Panel 
    public class ManagementPanelTabs
    {
        //Holds all information regarding the army information
        public class ArmyTab : Tab 
        {
            public ArmyTab(string tag) : base(tag) { }
        }
        //Hold all information regarding the captains information
        public class CaptainsTab : Tab 
        {
            public CaptainsTab(string tag) : base(tag) {}
        }
        //Holds all information regarding the controlled areas information
        public class ControlledAreasTab : Tab 
        {
            public ControlledAreasTab(string tag) : base(tag) { }
        }
        //Holds all information regarding the resources information
        public class ResourcesTab : Tab
        {
            public ResourcesTab(string tag) : base(tag) { }
        }
        //Holds all information regarding the Technology information
        public class TechnologyTab : Tab
        {
            public TechnologyTab(string tag) : base(tag) { }
        }
        //Holds all information regarding the options tab
        public class OptionsTab : Tab 
        {
            public OptionsTab(string tag) : base(tag) { }
        }
    }

    //Contains all the necessary tabs for the management panel
    public class ManagementPanel
    {
        private ManagementPanelTabs.ArmyTab armyTab;
        private ManagementPanelTabs.CaptainsTab captainsTab;
        private ManagementPanelTabs.ControlledAreasTab controlledAreasTab;
        private ManagementPanelTabs.ResourcesTab resourcesTab;
        private ManagementPanelTabs.TechnologyTab technologyTab;
        private ManagementPanelTabs.OptionsTab optionsTab;

        //default constructor
        public ManagementPanel()
        {
            armyTab = new ManagementPanelTabs.ArmyTab("Army_small");
            captainsTab = new ManagementPanelTabs.CaptainsTab("Captains_small");
            controlledAreasTab = new ManagementPanelTabs.ControlledAreasTab("ControlledAreas_small");
            resourcesTab = new ManagementPanelTabs.ResourcesTab("Resources_small");
            technologyTab = new ManagementPanelTabs.TechnologyTab("Technology_small");
            optionsTab = new ManagementPanelTabs.OptionsTab("Options_small");
        }

        //Getters and setters for each panel tab
        public ManagementPanelTabs.ArmyTab ArmyTab
        {
            get { return armyTab; }
            set { armyTab = value; }
        }
        public ManagementPanelTabs.CaptainsTab CaptainsTab
        {
            get { return captainsTab; }
            set { captainsTab = value; }
        }
        public ManagementPanelTabs.ControlledAreasTab ControlledAreasTab
        {
            get { return controlledAreasTab; }
            set { controlledAreasTab = value; }
        }
        public ManagementPanelTabs.ResourcesTab ResourcesTab
        {
            get { return resourcesTab; }
            set { resourcesTab = value; }
        }
        public ManagementPanelTabs.TechnologyTab TechnologyTab
        {
            get { return technologyTab; }
            set { technologyTab = value; }
        }
        public ManagementPanelTabs.OptionsTab OptionsTab
        {
            get { return optionsTab; }
            set { optionsTab = value; }
        }

        //hides all the tab content
        public void contentHide()
        {
            armyTab.TabObj.SetActive(false);
            captainsTab.TabObj.SetActive(false);
            controlledAreasTab.TabObj.SetActive(false);
            resourcesTab.TabObj.SetActive(false);
            technologyTab.TabObj.SetActive(false);
            optionsTab.TabObj.SetActive(false);
        }
        //function returns the GameObject associated with the id
        public GameObject findTabObject(int id)
        {
            switch (id)
            {
                case 1:
                    return armyTab.TabObj;
                case 2:
                    return captainsTab.TabObj;
                case 3:
                    return controlledAreasTab.TabObj;
                case 4:
                    return resourcesTab.TabObj;
                case 5:
                    return technologyTab.TabObj;
                case 6:
                    return optionsTab.TabObj;
                default:
                    return null;
            }
        }    

    }

    //Class for the small management panel(mini window on the HUD)
    public class SmallManagementPanel : ManagementPanel
    {
        public SmallManagementPanel()
        {
            base.ArmyTab = new ManagementPanelTabs.ArmyTab("Army_small");
            base.CaptainsTab = new ManagementPanelTabs.CaptainsTab("Captains_small");
            base.ControlledAreasTab = new ManagementPanelTabs.ControlledAreasTab("ControlledAreas_small");
            base.ResourcesTab = new ManagementPanelTabs.ResourcesTab("Resources_small");
            base.TechnologyTab = new ManagementPanelTabs.TechnologyTab("Technology_small");
            base.OptionsTab = new ManagementPanelTabs.OptionsTab("Options_small");
        }
    }

    //class for the large management panel(expanded window opened by pressing M)
    public class LargeManagementPanel : ManagementPanel
    {
        private GameObject maximizedPanel;

        public LargeManagementPanel()
        {
            maximizedPanel = GameObject.FindGameObjectWithTag("maximizedManagementPanel");
            base.ArmyTab = new ManagementPanelTabs.ArmyTab("Army_large");
            base.CaptainsTab = new ManagementPanelTabs.CaptainsTab("Captains_large");
            base.ControlledAreasTab = new ManagementPanelTabs.ControlledAreasTab("ControlledAreas_large");
            base.ResourcesTab = new ManagementPanelTabs.ResourcesTab("Resources_large");
            base.TechnologyTab = new ManagementPanelTabs.TechnologyTab("Technology_large");
        }

        public GameObject MaximizedPanel
        {
            get { return maximizedPanel; }
            set { maximizedPanel = value; }
        }

        public void contentHide()
        {
            ArmyTab.TabObj.SetActive(false);
            CaptainsTab.TabObj.SetActive(false);
            ControlledAreasTab.TabObj.SetActive(false);
            ResourcesTab.TabObj.SetActive(false);
            TechnologyTab.TabObj.SetActive(false);
        }

    }

    //class for the character panel which contains all the information regarding the players hero
    public class CharacterPanel
    {
        //enum for collaspe and expanded state of the character window
        public enum enPanelState
        {
            collapsed,
            expanded
        };

        private enPanelState currentPanelState;
        private GameObject collapsedPanel;
        private GameObject expandedPanel;

        public GameObject CollapsedPanel
        {
            get { return collapsedPanel; }
            set { collapsedPanel = value; }
        }

        public GameObject ExpandedPanel
        {
            get { return expandedPanel; }
            set { expandedPanel = value; }
        }

        public enPanelState CurrentPanelState
        {
            get { return currentPanelState; }
            set { currentPanelState = value; }
        }

        public CharacterPanel()
        {
            //get the game objects of the collapsed and expanded panels
            collapsedPanel = GameObject.FindGameObjectWithTag("collapsed");
            expandedPanel = GameObject.FindGameObjectWithTag("expanded");

        }

    }

}