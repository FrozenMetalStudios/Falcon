using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FrozenMetal
{
namespace Perspective
{
    /// <summary>
    /// Responsible for triggering a Camera change upon Camera Collision.
    /// </summary>
    [RequireComponent (typeof (Collider))]
    public class PerspectiveCollider : MonoBehavior
    {
        /// <summary>
        /// Stores a Reference to the PerspectiveController.
        /// </summary>
        public PerspectiveController perspectiveController;

        /*
        public int newCamera = 0;
        private static List<int> cameraStack = new List<int>();
        public float interpolationTime = 1;
        void Start(){
        if (!collider.isTrigger){
        Debug.LogWarning("CameraChangeCollider should be set to trigger");
        }
        }
        void OnTriggerEnter(Collider other) {
        if (other.transform==compositeCamera.GetTarget()){
        int oldCamera = compositeCamera.currentCamera;
        cameraStack.Add(oldCamera);
        compositeCamera.SetCamera(newCamera, interpolationTime);
        }
        }
        void OnTriggerExit(Collider other) {
        if (other.transform==compositeCamera.GetTarget()){
        if (compositeCamera.currentCamera == newCamera){
        // if current camera, then pop
        compositeCamera.SetCamera(cameraStack[cameraStack.Count-1], interpolationTime);
        cameraStack.RemoveAt(cameraStack.Count-1);
        } else {
        int lastIndex = cameraStack.LastIndexOf(newCamera);
        if (lastIndex>-1){
        cameraStack.RemoveAt(lastIndex);
        } else {
        Debug.LogWarning("Error in CameraChangeCollider. Cannot find camera "+newCamera+" in the camera stack");
        }
        }
        }
        }
         */
    }
}
}