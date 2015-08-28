using UnityEngine;
using System.Collections;

public class _CharacterPanelManager : MonoBehaviour {

    //enum for collaspe and expanded state of the character window
    private enum enPanelState
    {
        collapsed,
        expanded
    };

    //current state of the character panel
    private enPanelState currentPanelState;
    //game object of collapsed panel
    private GameObject collapsedPanel;
    //game object of the expanded panel
    private GameObject expandedPanel;

	// Use this for initialization
	void Start () {
        currentPanelState = enPanelState.collapsed;

        //get the game objects of the collapsed and expanded panels
        collapsedPanel = GameObject.FindGameObjectWithTag("collapsed");
        expandedPanel =  GameObject.FindGameObjectWithTag("expanded");
        collapsedPanel.SetActive(true);
        expandedPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //toggles the panel state depending on the current state of the panel
    public void changePanelState(){

        if(currentPanelState == enPanelState.collapsed)
        {
            currentPanelState = enPanelState.expanded;
            collapsedPanel.SetActive(false);
            expandedPanel.SetActive(true);
        }
        else if (currentPanelState == enPanelState.expanded)
        {
            currentPanelState = enPanelState.collapsed;
            expandedPanel.SetActive(false);
            collapsedPanel.SetActive(true);
        }
    }
}
