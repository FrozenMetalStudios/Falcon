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
 * Filename: CombatManager.cs
 * 
 * Description:
 * 
 *******************************************************************/
using UnityEngine;
using System;
using System.Diagnostics;
using Assets.Scripts.Utility;
using Assets.Scripts.CustomEditor;

namespace Assets.Scripts
{
    public class CombatManager : MonoBehaviour
    {
        // --------------------------------------------------------------------
        static CombatManager _Singleton = null;

        // --------------------------------------------------------------------
        public static CombatManager Singleton
        {
            get { return _Singleton; }
        }

        // --------------------------------------------------------------------
        void Awake()
        {
            // Ensure only 1 singleton
            if (null != _Singleton)
            {
                Logger.LogMessage(eLogCategory.General,
                                  eLogLevel.Assert,
                                  "CombatManager: Multiple CombatManagers violate Singleton pattern.");
                return;
            }
            _Singleton = this;

            // Make sure this object persists between scene loads.
            DontDestroyOnLoad(gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}