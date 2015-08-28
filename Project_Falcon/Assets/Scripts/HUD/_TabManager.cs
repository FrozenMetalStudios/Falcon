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
                resourceContent[0].SetActive(true);
                break;
            case 2:
                resourceContent[1].SetActive(true);
                break;
            case 3:
                resourceContent[2].SetActive(true);
                break;
            case 4:
                resourceContent[3].SetActive(true);
                break;
            case 5:
                resourceContent[4].SetActive(true);
                break;
            case 6:
                resourceContent[5].SetActive(true);
                break;
            default:
                break;
        }
    }

    private void changeTabs(enResourceTabs newTab)
    {
        //turn off the current tab
        //change to the next tab
    }
}
