using UnityEngine;
using System.Collections;

namespace MacroViewUI
{

    public class ManagementPanel
    {
        private GameObject armyTab;
        private GameObject captainsTab;
        private GameObject controlledAreasTab;
        private GameObject resourcesTab;
        private GameObject technologyTab;
        private GameObject optionsTab;

        public ManagementPanel()
        {
            armyTab = GameObject.FindGameObjectWithTag("Army_small");
            captainsTab = GameObject.FindGameObjectWithTag("Captains_small");
            controlledAreasTab = GameObject.FindGameObjectWithTag("ControlledAreas_small");
            resourcesTab = GameObject.FindGameObjectWithTag("Resources_small");
            technologyTab = GameObject.FindGameObjectWithTag("Technology_small");
            optionsTab = GameObject.FindGameObjectWithTag("Options_small");
        }

        public GameObject ArmyTab
        {
            get { return armyTab; }
            set { armyTab = value; }
        }
        public GameObject CaptainsTab
        {
            get { return captainsTab; }
            set { captainsTab = value; }
        }
        public GameObject ControlledAreasTab
        {
            get { return controlledAreasTab; }
            set { controlledAreasTab = value; }
        }
        public GameObject ResourcesTab
        {
            get { return resourcesTab; }
            set { resourcesTab = value; }
        }
        public GameObject TechnologyTab
        {
            get { return technologyTab; }
            set { technologyTab = value; }
        }
        public GameObject OptionsTab
        {
            get { return optionsTab; }
            set { optionsTab = value; }
        }

        public void contentHide()
        {
            armyTab.SetActive(false);
            captainsTab.SetActive(false);
            controlledAreasTab.SetActive(false);
            resourcesTab.SetActive(false);
            technologyTab.SetActive(false);
            optionsTab.SetActive(false);
        }
        public GameObject findTabObject(int id)
        {
            switch (id)
            {
                case 1:
                    return armyTab;
                case 2:
                    return captainsTab;
                case 3:
                    return controlledAreasTab;
                case 4:
                    return resourcesTab;
                case 5:
                    return technologyTab;
                case 6:
                    return optionsTab;
                default:
                    return null;
            }
        }    

    }

    public class SmallManagementPanel : ManagementPanel
    {
        public SmallManagementPanel()
        {
            base.ArmyTab = GameObject.FindGameObjectWithTag("Army_small");
            base.CaptainsTab = GameObject.FindGameObjectWithTag("Captains_small");
            base.ControlledAreasTab = GameObject.FindGameObjectWithTag("ControlledAreas_small");
            base.ResourcesTab = GameObject.FindGameObjectWithTag("Resources_small");
            base.TechnologyTab = GameObject.FindGameObjectWithTag("Technology_small");
            base.OptionsTab = GameObject.FindGameObjectWithTag("Options_small");
        }
    }

    public class LargeManagementPanel : ManagementPanel
    {
        private GameObject maximizedPanel;

        public LargeManagementPanel()
        {
            maximizedPanel = GameObject.FindGameObjectWithTag("maximizedManagementPanel");
            base.ArmyTab = GameObject.FindGameObjectWithTag("Army_large");
            base.CaptainsTab = GameObject.FindGameObjectWithTag("Captains_large");
            base.ControlledAreasTab = GameObject.FindGameObjectWithTag("ControlledAreas_large");
            base.ResourcesTab = GameObject.FindGameObjectWithTag("Resources_large");
            base.TechnologyTab = GameObject.FindGameObjectWithTag("Technology_large");
        }

        public GameObject MaximizedPanel
        {
            get { return maximizedPanel; }
            set { maximizedPanel = value; }
        }

        public void contentHide()
        {
            ArmyTab.SetActive(false);
            CaptainsTab.SetActive(false);
            ControlledAreasTab.SetActive(false);
            ResourcesTab.SetActive(false);
            TechnologyTab.SetActive(false);
        }

    }

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