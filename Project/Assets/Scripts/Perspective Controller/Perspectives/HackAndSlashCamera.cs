using UnityEngine;
using System.Collections;
using FrozenMetal.MathUtil;

namespace FrozenMetal
{
namespace Perspective
{
    /// <summary>
    /// Provides the Perpective Topdown Camera behavior.
    /// </summary>
    [AddComponentMenu("Perspectives/Hack And Slash Camera")]
    [RequireComponent(typeof(Camera))]
    public class HackAndSlashCamera : AbstractCamera
    {
        /// <summary>
        /// Specifies the Placement of the Camera relative to the
        /// Target in Spherical terms.
        /// </summary>
        public SphericalCoord cameraOffset;

        /// <summary>
        /// The speed with which the camera will be following.
        /// </summary>
        public float smoothing = 5f;

        /// <summary>
        /// Specifies the Look-at offset from the Target.
        /// </summary>
        public Vector3 viewOffset = new Vector3(0, 0, 0);

        /// <summary>
        /// Specifies the Look-at rotational Offset, in Degrees.
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
        /// Ensures the Cameras position is updated every Frame if required.
        /// </summary>
        public override void UpdatePosition()
        {
            // Create a postion the Camera is heading toward for based on the offset from the target.
            Vector3 targetCamPos = target.position + placementOffset;

            // Update the Look-at target for the Camera
            lookAtPosition = target.position + viewOffset;

            // Smoothly interpolate between the Camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }

        /// <summary>
        /// Updates the Cameras Lookat every Frame if required.
        /// </summary>
        public override void UpateLookAt()
        {
            // Look at and dampen the rotation
            Quaternion rotation = Quaternion.LookRotation(lookAtPosition - transform.position) * rotationOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothLookAtDamping);
        }
    }
}
}