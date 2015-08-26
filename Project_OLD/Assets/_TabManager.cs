using UnityEngine;
using System.Collections;

public class _TabManager : MonoBehaviour {

    private enum enResourceTabs
    {
        _,
        tab1,
        tab2,
        tab3,
        tab4,
        tab5,
        tab6,
        END
    };
    private GameObject[] resourceContent = new GameObject[6];

    void Start()
    {
        print("Start");
        //make sure that all tabs except for the intial one is disabled
        resourceContent[0] = GameObject.FindGameObjectWithTag("Tab1");
        resourceContent[1] = GameObject.FindGameObjectWithTag("Tab2");
        resourceContent[2] = GameObject.FindGameObjectWithTag("Tab3");
        resourceContent[3] = GameObject.FindGameObjectWithTag("Tab4");
        resourceContent[4] = GameObject.FindGameObjectWithTag("Tab5");
        resourceContent[5] = GameObject.FindGameObjectWithTag("Tab6");

        contentHide();
        resourceContent[0].SetActive(true);

    }
    private void contentHide()
    {
        print("hide");
        foreach (GameObject panel in resourceContent)
        {
            panel.SetActive(false);
        }
    }

    public void selectedTab(int id)
    {
        contentHide();

        switch (id)
        {

            case 1:
                print("tab 1 selected");
                resourceContent[0].SetActive(true);
                break;
            case 2:
                print("tab 2 selected");
                resourceContent[1].SetActive(true);
                break;
            case 3:
                print("tab 3 selected");
                resourceContent[2].SetActive(true);
                break;
            case 4:
                print("tab 4 selected");
                resourceContent[3].SetActive(true);
                break;
            case 5:
                print("tab 5 selected");
                resourceContent[4].SetActive(true);
                break;
            case 6:
                print("tab 6 selected");
                resourceContent[5].SetActive(true);
                break;
            default:
                print("incorrect tab");
                break;
        }
    }

    private void changeTabs(enResourceTabs newTab)
    {
        //turn off the current tab
        //change to the next tab
    }
}
