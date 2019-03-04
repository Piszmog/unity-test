using UnityEngine;

namespace Camera
{
    /// <inheritdoc />
    /// <summary>
    /// Class the controls the camera to follow the player around. This allows the the user to follow their player around.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothing = 5f;

        private Vector3 offset;

        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void FixedUpdate()
        {
            var targetCameraPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
        }
    }
}