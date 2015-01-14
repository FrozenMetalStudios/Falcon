using UnityEngine;
using System.Collections;
using FrozenMetal.MathUtil;

namespace FrozenMetal
{
namespace Camera
{
    /// <summary>
    /// Provides the Perpective Topdown Camera behavior.
    /// </summary>
    public class HackAndSlashCamera : MonoBehaviour
    {
        /// <summary>
        /// The position that that camera will be following.
        /// </summary>
        public Transform target;

        /// <summary>
        /// The speed with which the camera will be following.
        /// </summary>
        public float smoothing = 5f;

        /// <summary>
        /// The damping factor of the Look-at movement.
        /// </summary>
        public float smoothLookAtDamping = 6.0f;

        /// <summary>
        /// Specifies the Placement of the Camera relative to the
        /// Target in Spherical terms.
        /// </summary>
        public SphericalCoord cameraOffset;

        /// <summary>
        /// Specifies the Look-at offset from the Target.
        /// </summary>
        public Vector3 viewOffset = new Vector3(0, 0, 0);

        /// <summary>
        /// Specifies the Look-at rotational Offset.
        /// </summary>
        public Quaternion rotationOffset = Quaternion.Euler(0, 0, 0);

        /// <summary>
        /// The placement offset from the target.
        /// </summary>
        private Vector3 placementOffset;

        /// <summary>
        /// Determines the position from the Target for the
        /// Camera focus point.
        /// </summary>
        private Vector3 lookAtPosition;

        /// <summary>
        /// Sets the target runtime.
        /// </summary>
        public virtual void SetTarget(Transform transform)
        {
            this.target = transform;
        }

        /// <summary>
        /// Get the Target at Runtime.
        /// </summary>
        /// <returns>The Cameras Target Transform</returns>
        public Transform GetTarget()
        {
            return target;
        }

        /// <summary>
        /// When the Scene starts, set the camera to the
        /// correct location.
        /// </summary>
        void Start()
        {
            // Calculate the initial offset.
            placementOffset = target.position + cameraOffset.ToCartesian();

            // Calculate the Look-at offset from the target.
            lookAtPosition = target.position + viewOffset;

            // Set the Cameras Rotation for the Look-at Point
            transform.rotation = Quaternion.LookRotation(lookAtPosition - transform.position) * rotationOffset;
        }

        /// <summary>
        /// Every Frame, update the position of the Camera smoothly.
        /// </summary>
        void LateUpdate()
        {
            // Create a postion the Camera is heading toward for based on the offset from the target.
            Vector3 targetCamPos = target.position + placementOffset;

            // Update the Look-at target for the Camera
            lookAtPosition = target.position + viewOffset;

            // Smoothly interpolate between the Camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

            // Look at and dampen the rotation
            Quaternion rotation = Quaternion.LookRotation(lookAtPosition - transform.position) * rotationOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothLookAtDamping);
        }
    }
}
}