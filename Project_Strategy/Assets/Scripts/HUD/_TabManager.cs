using UnityEngine;
using System.Collections;

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

    public void contentHide(){
        armyTab.SetActive(false);
        captainsTab.SetActive(false);
        controlledAreasTab.SetActive(false);
        resourcesTab.SetActive(false);
        technologyTab.SetActive(false);
        optionsTab.SetActive(false);
    }
    public void armySetActive(bool state) { armyTab.SetActive(state); }
    public void captainsSetActive(bool state) { captainsTab.SetActive(state); }
    public void controlledAreasSetActive(bool state) { controlledAreasTab.SetActive(state); }
    public void resourcesSetActive(bool state) { resourcesTab.SetActive(state); }
    public void technologySetActive(bool state) { technologyTab.SetActive(state); }
    public void optionsSetActive(bool state) { optionsTab.SetActive(state); }


}

public class smallManagementPanel : ManagementPanel
{
    public smallManagementPanel()
    {
        base.ArmyTab = GameObject.FindGameObjectWithTag("Army_small");
        base.CaptainsTab = GameObject.FindGameObjectWithTag("Captains_small");
        base.ControlledAreasTab = GameObject.FindGameObjectWithTag("ControlledAreas_small");
        base.ResourcesTab = GameObject.FindGameObjectWithTag("Resources_small");
        base.TechnologyTab = GameObject.FindGameObjectWithTag("Technology_small");
        base.OptionsTab = GameObject.FindGameObjectWithTag("Options_small");
    }
}

public class largeManagementPanel : ManagementPanel
{
    private GameObject maximizedPanel;

    public largeManagementPanel()
    {
        maximizedPanel = GameObject.FindGameObjectWithTag("maximizedManagementPanel");
        base.ArmyTab = GameObject.FindGameObjectWithTag("Army_large");
        base.CaptainsTab = GameObject.FindGameObjectWithTag("Captains_large");
        base.ControlledAreasTab = GameObject.FindGameObjectWithTag("ControlledAreas_large");
        base.ResourcesTab = GameObject.FindGameObjectWithTag("Resources_large");
        base.TechnologyTab = GameObject.FindGameObjectWithTag("Technology_large");
    }

    public GameObject MaximizedPanel { 
        get { return maximizedPanel; } 
        set { maximizedPanel = value; } 
    }

    public void contentHide(){
        ArmyTab.SetActive(false);
        CaptainsTab.SetActive(false);
        ControlledAreasTab.SetActive(false);
        ResourcesTab.SetActive(false);
        TechnologyTab.SetActive(false);
    }

}

public class _TabManager : MonoBehaviour {
    private enum enMouseClick
    {
        SingleClick,
        DoubleClick,
        None
    }

    private GameObject maximizedPanel;
    private float lastClick = 0;
    private smallManagementPanel smallMP;
    private largeManagementPanel largeMP;

    void Start()
    {
        smallMP = new smallManagementPanel();
        largeMP = new largeManagementPanel();
        smallMP.contentHide();
        smallMP.ArmyTab.SetActive(true);
        largeMP.contentHide();
        largeMP.MaximizedPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if(largeMP.MaximizedPanel != null)
            {
                largeMP.contentHide();
                if (largeMP.MaximizedPanel.activeSelf == true)
                {
                    largeMP.MaximizedPanel.SetActive(false);
                }
                else {
                    largeMP.MaximizedPanel.SetActive(true);
                    largeMP.ArmyTab.SetActive(true);
                }
            }
        }
    }

    public void smallPanelTabChange(int id)
    {
        smallMP.contentHide();
        enMouseClick mouseClick = tabClick();

        if (largeMP.MaximizedPanel != null)
        {
            if (mouseClick == enMouseClick.DoubleClick && largeMP.MaximizedPanel.activeSelf == false)
            {
                //open big window
                largeMP.MaximizedPanel.SetActive(true);
                largeMP.contentHide();
                findTabObject(id, largeMP).SetActive(true);
            }
            else if (mouseClick == enMouseClick.DoubleClick && largeMP.MaximizedPanel.activeSelf == true)
            {
                //close big window
                largeMP.MaximizedPanel.SetActive(false);
            }
        }

        findTabObject(id, smallMP).SetActive(true);
    }
    public void largePanelTabChange(int id)
    {
        largeMP.contentHide();
        enMouseClick mouseClick = tabClick();

        if (largeMP.MaximizedPanel != null)
        {
            if (mouseClick == enMouseClick.DoubleClick && largeMP.MaximizedPanel.activeSelf == false)
            {
                //open big window
                largeMP.MaximizedPanel.SetActive(true);
            }
            else if (mouseClick == enMouseClick.DoubleClick && largeMP.MaximizedPanel.activeSelf == true)
            {
                //close big window
                largeMP.MaximizedPanel.SetActive(false);
            }
        }

        findTabObject(id, largeMP).SetActive(true);
    }

    private GameObject findTabObject(int id, ManagementPanel panel)
    {
        switch (id)
        {
            case 1:
                //resourceContent[0].SetActive(true);
                return panel.ArmyTab;
            case 2:
                //resourceContent[1].SetActive(true);
                return panel.CaptainsTab;
            case 3:
                //resourceContent[2].SetActive(true);
                return panel.ControlledAreasTab;
            case 4:
                //resourceContent[3].SetActive(true);
                return panel.ResourcesTab;
            case 5:
                //resourceContent[4].SetActive(true);
                return panel.TechnologyTab;
            case 6:
                //resourceContent[5].SetActive(true);
                return panel.OptionsTab;
            default:
                return null;
        }
    }
    private enMouseClick tabClick(){

        enMouseClick mouseClick;
        if (Time.time - lastClick < 0.2)
        {
            mouseClick =  enMouseClick.DoubleClick;
        }
        else
        {
            mouseClick =  enMouseClick.SingleClick;
        }
        lastClick = Time.time;
        return mouseClick;
    }
}
