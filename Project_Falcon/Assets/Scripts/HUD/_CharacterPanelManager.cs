using UnityEngine;
using System.Collections;

public class _CharacterPanelManager : MonoBehaviour {

    private enum enPanelState
    {
        collapsed,
        expanded
    };

    private enPanelState characterPanelState;

	// Use this for initialization
	void Start () {
        characterPanelState = enPanelState.collapsed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changePanelState(){

        if(characterPanelState == enPanelState.collapsed)
        {
            print("collapsed -> expanded");
            characterPanelState = enPanelState.expanded;
        }
        else if (characterPanelState == enPanelState.expanded)
        {
            print("expanded -> collapsed");
            characterPanelState = enPanelState.collapsed;
        }
    }
}
