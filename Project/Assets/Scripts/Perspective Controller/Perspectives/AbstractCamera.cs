using UnityEngine;
using System.Collections;

namespace FrozenMetal
{
namespace Perspective
{
    public abstract class AbstractCamera : MonoBehaviour
    {
        /// <summary>
        /// The position that that camera will be following.
        /// </summary>
        public Transform target;

        /// <summary>
        /// Ensure the Look-at occurs smoothly.
        /// </summary>
        public bool smoothLookAt = true;

        /// <summary>
        /// The damping factor of the Look-at movement.
        /// </summary>
        public float smoothLookAtDamping = 6.0f;

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
        /// Controls the Camera actions every Frame.
        /// </summary>
        public void LateUpdate()
        {
            // Update the Cameras Position
            UpdatePosition();

            // Update the Cameras Lookat
            UpdateLookAt();
        }

        /// <summary>
        /// Ensures the Cameras position is updated every Frame if required.
        /// </summary>
        public virtual void UpdatePosition()
        {
            // Abstract Implementation does not move the Camera.
        }

        /// <summary>
        /// Updates the Cameras Lookat every Frame if required.
        /// </summary>
        public virtual void UpateLookAt()
        {
            if (smoothLookAt)
            {
                // Look at and dampen the rotation
                Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothLookAtDamping);
            }
            else
            {
                // Set the Cameras Rotation for the Look-at Point
                transform.rotation = Quaternion.LookRotation(target.position - transform.position);
            }
        }
    }
}
}