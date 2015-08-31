using UnityEngine;
using System.Collections;
using MacroViewUI;

public class _MacroViewManager : MonoBehaviour {

    private static double CLOCK_TIC = 0.2;
    private enum enMouseClick
    {
        SingleClick,
        DoubleClick,
        None
    }

    private GameObject maximizedPanel;
    private float lastClick = 0;
    private SmallManagementPanel smallMP;
    private LargeManagementPanel largeMP;
    private CharacterPanel characterPanel;

    void Start()
    {
        smallMP = new SmallManagementPanel();
        largeMP = new LargeManagementPanel();
        characterPanel = new CharacterPanel();

        smallMP.contentHide();
        smallMP.ArmyTab.SetActive(true);
        largeMP.contentHide();
        largeMP.MaximizedPanel.SetActive(false);
        characterPanel.CollapsedPanel.SetActive(true);
        characterPanel.ExpandedPanel.SetActive(false);
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

    /*************************************************************************************************
     *                                          Onclick Functions
     *************************************************************************************************/
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

    public void changePanelState()
    {

        if (characterPanel.CurrentPanelState == MacroViewUI.CharacterPanel.enPanelState.collapsed)
        {
            characterPanel.CurrentPanelState = MacroViewUI.CharacterPanel.enPanelState.expanded;
            characterPanel.CollapsedPanel.SetActive(false);
            characterPanel.ExpandedPanel.SetActive(true);
        }
        else if (characterPanel.CurrentPanelState == MacroViewUI.CharacterPanel.enPanelState.expanded)
        {
            characterPanel.CurrentPanelState = MacroViewUI.CharacterPanel.enPanelState.collapsed;
            characterPanel.ExpandedPanel.SetActive(false);
            characterPanel.CollapsedPanel.SetActive(true);
        }
    }

    private GameObject findTabObject(int id, ManagementPanel panel)
    {
        switch (id)
        {
            case 1:
                return panel.ArmyTab;
            case 2:
                return panel.CaptainsTab;
            case 3:
                return panel.ControlledAreasTab;
            case 4:
                return panel.ResourcesTab;
            case 5:
                return panel.TechnologyTab;
            case 6:
                return panel.OptionsTab;
            default:
                return null;
        }
    }
    private enMouseClick tabClick()
    {

        enMouseClick mouseClick;
        if (Time.time - lastClick < CLOCK_TIC)
        {
            mouseClick = enMouseClick.DoubleClick;
        }
        else
        {
            mouseClick = enMouseClick.SingleClick;
        }
        lastClick = Time.time;
        return mouseClick;
    }
}
