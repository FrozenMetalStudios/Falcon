using UnityEngine;
using System.Collections;
using Assets.Scripts;

namespace Assets.Scripts.Menu
{
    public class SceneSelector : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------------------
        enum EMainMenuState
        {
            Startup,
            DisplayLogo,
            Menu,
        };

        //-------------------------------------------------------------------------------------------------------------------------
        enum EMenuButtonId
        {
            None,

            Dev_Scene_Dan,
            Dev_Scene_Andy,
            Quit,
        };

        //-------------------------------------------------------------------------------------------------------------------------
        public GameObject AppPrefab;

        //-------------------------------------------------------------------------------------------------------------------------
        private EMainMenuState StateId;
        private EMenuButtonId ButtonId;
        private float LogoDisplayTimer;

        //-------------------------------------------------------------------------------------------------------------------------
        void Start()
        {
            Debug.Log("MainMenu: Starting.");

            // Is the controlling App already in existence?
            if (GameObject.Find("Managers") == null)
            {
                Debug.Log("Creating temporary App");

                Instantiate(AppPrefab);
            }

            StateId = EMainMenuState.Startup;
            ButtonId = EMenuButtonId.None;
        }

        //-------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            switch (StateId)
            {
                case EMainMenuState.Startup:
                    Debug.Log("MainMenu: State: Startup.");

                    // Here we would do any menu preparation work.

                    // Has the logo been displayed already?
                    if (SceneManager.Singleton.DisplayedLogo)
                        // Move to the next state.
                        StateId = EMainMenuState.Menu;
                    else
                    {
                        SceneManager.Singleton.DisplayedLogo = true;
                        StateId = EMainMenuState.DisplayLogo;
                        LogoDisplayTimer = 1.0f;
                    }
                    break;

                case EMainMenuState.DisplayLogo:
                    LogoDisplayTimer -= Time.deltaTime;

                    if (LogoDisplayTimer < 0.0f)
                        StateId = EMainMenuState.Menu;
                    break;

                case EMainMenuState.Menu:
                    if (ButtonId == EMenuButtonId.Dev_Scene_Dan)
                    {
                        Debug.Log("MainMenu: Dev_Scene_Dan Selected.");
                        SceneManager.Singleton.LoadLevel("Dev_Scene_Dan");
                    }
                    else if (ButtonId == EMenuButtonId.Dev_Scene_Andy)
                    {
                        Debug.Log("MainMenu: Dev_Scene_Andy Selected.");
                        SceneManager.Singleton.LoadLevel("Dev_Scene_Andy");
                    }
                    else if (ButtonId == EMenuButtonId.Quit)
                    {
                        Debug.Log("MainMenu: Quit Selected.");
                        SceneManager.Singleton.Quit();
                    }

                    ButtonId = EMenuButtonId.None;
                    break;

                default:
                    Debug.LogError("Really shouldn't be here... illegal state id set.");

                    // Auto recover.
                    StateId = EMainMenuState.Startup;
                    break;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------
        void OnGUI()
        {
            if (StateId == EMainMenuState.DisplayLogo)
            {
                GUI.Button(new Rect(100.0f, 100.0f, 500.0f, 500.0f), "BIG LOGO DISPLAYED HERE!");
            }
            else
            {
                if (GUI.Button(new Rect(200.0f, 100.0f, 300.0f, 100.0f), "Dev_Scene_Dan"))
                {
                    ButtonId = EMenuButtonId.Dev_Scene_Dan;
                }

                if (GUI.Button(new Rect(200.0f, 250.0f, 300.0f, 100.0f), "Dev_Scene_Andy"))
                {
                    ButtonId = EMenuButtonId.Dev_Scene_Andy;
                }

                if (GUI.Button(new Rect(200.0f, 350.0f, 300.0f, 100.0f), "Quit"))
                {
                    ButtonId = EMenuButtonId.Quit;
                }
            }
        }
    }
}