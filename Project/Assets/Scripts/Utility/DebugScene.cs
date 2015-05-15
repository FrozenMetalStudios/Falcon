using UnityEngine;
using System;

namespace Assets.Scripts.Utility
{
    [Serializable]
    public class DebugScene : MonoBehaviour
    {
        public GameObject ManagerPrefab;

        void Awake()
        {
            // Is the controlling App already in existence?
            if (null == GameObject.Find("Managers")
             && null == GameObject.Find("Managers(Clone)"))
            {
                Instantiate(ManagerPrefab);
            }
        }
    }
}
