using UnityEngine;
using System;

namespace Assets.Scripts.Utility
{
    [Serializable]
    public class DebugScene : MonoBehaviour
    {
        public GameObject LoggerPrefab;
        public GameObject SceneManagerPrefab;

        void Awake()
        {
            // Is the controlling App already in existence?
            if (null == GameObject.Find("Logger")
             && null == GameObject.Find("Logger(Clone)"))
            {
                Instantiate(LoggerPrefab);
            }
            if (null == GameObject.Find("SceneManager")
             && null == GameObject.Find("SceneManager(Clone)"))
            {
                Instantiate(SceneManagerPrefab);
            }
        }
    }
}
