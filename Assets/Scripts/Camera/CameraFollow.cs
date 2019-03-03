using UnityEngine;

namespace Camera
{
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