/*******************************************************************
 * 
 * Copyright (C) 2015 Frozen Metal Studios - All Rights Reserved
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Frozen Metal Studios. The intellectual and 
 * technical concepts contained herein are proprietary to 
 * Frozen Metal Studios are protected by copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from Frozen Metal Studios.
 * 
 * *****************************************************************
 * 
 * Filename: Entry.cs
 * 
 * Description: Controls Initial Game Entry and Scene Management.
 * 
 *******************************************************************/
using UnityEngine;
using System;
using System.Diagnostics;
using Assets.Scripts.Utility;
using Assets.Scripts.CustomEditor;

namespace Assets.Scripts
{
    class SceneManager : MonoBehaviour
    {
        // --------------------------------------------------------------------
        public string GameTitle = "Project Falcon";
        [ReadOnly]
        public bool DisplayedLogo = false;

        // --------------------------------------------------------------------
        private string _CurrentScene;

        // --------------------------------------------------------------------
#if !FINAL
        private float _FPS;
        private float _FPSTime;
        private int _FPSFrames;
#endif

        // --------------------------------------------------------------------
        static SceneManager _Singleton = null;

        // --------------------------------------------------------------------
        public static SceneManager Singleton
        {
            get { return _Singleton; }
        }

        // --------------------------------------------------------------------
        void Awake()
        {
            // Log Startup - Cannot use Logger due to uncertain initialization order
            UnityEngine.Debug.Log("SceneManager: Awake.");

            if (null != _Singleton)
            {
                UnityEngine.Debug.LogError("SceneManager: Multiple SceneManagers violate Singleton pattern.");
            }
            _Singleton = this;

            // Init the FPS Tracker
            InitFPS();

            // Are we in the Application Scene?
            if (Application.loadedLevelName == "Entry")
            {
                // Make sure this object persists between scene loads.
                DontDestroyOnLoad(gameObject);

#if !FINAL
                // Load the Debug Scene Selector
                UnityEngine.Debug.Log("SceneManager: Loading SceneSelector");
                _CurrentScene = "SceneSelector";
                Application.LoadLevel(_CurrentScene);
#endif
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------
        void Start()
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public void LoadLevel(string level)
        {
            Logger.LogMessage(ELogCategory.Control,
                              ELogLevel.Trace,
                              "SceneManager: Loading Level, " + level);

            _CurrentScene = level;
            Application.LoadLevel(_CurrentScene);
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public void ResetLevel()
        {
            Logger.LogMessage(ELogCategory.Control,
                              ELogLevel.Trace,
                              "SceneManager: Restarting Level, " + _CurrentScene);

            Application.LoadLevel(_CurrentScene);
        }

        //-------------------------------------------------------------------------------------------------------------------------
#if !FINAL
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            }

            UpdateFPS();
        }
#endif

        //-------------------------------------------------------------------------------------------------------------------------
        public void Quit()
        {
            UnityEngine.Debug.Log("SceneManager: Terminating.");
            Application.Quit();
        }

        //-------------------------------------------------------------------------------------------------------------------------
#if !FINAL
        void OnGUI()
        {
            RenderFPS();
        }
#endif

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        void InitFPS()
        {
#if !FINAL
            // Clear the fps counters.
            _FPS = 0.0f;
            _FPSTime = 0.0f;
            _FPSFrames = 0;
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        void UpdateFPS()
        {
#if !FINAL
            // Increase the number of frames.
            _FPSFrames++;

            // Accumulate time.
            _FPSTime += Time.deltaTime;

            // Have we reached 1 second.
            if (_FPSTime > 1.0f)
            {
                // Store number of frames per second.
                _FPS = _FPSFrames;

                // Reset for the next frame.
                _FPSTime -= 1.0f;
                _FPSFrames = 0;
            }
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------
        [Conditional("DEBUG"), Conditional("PROFILE")]
        void RenderFPS()
        {
#if !FINAL
            // Display on screen the current Frames Per Second.
            string fps_string = String.Format("FPS: {0:d2}", (int) _FPS);
            GUI.Label(new Rect(10, 10, 600, 30), fps_string);
#endif
        }

        public void OnDestroy()
        {
        }
    }
}
