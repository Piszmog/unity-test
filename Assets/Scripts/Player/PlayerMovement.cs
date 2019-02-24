using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private const string Floor = "Floor";
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public float speed = 6f; // The speed that the player will move at.

        private Vector3 movement; // The vector to store the direction of the player's movement.
        private Animator animator; // Reference to the animator component.
        private Rigidbody playerRigidbody; // Reference to the player's rigidbody.
        private int floorMask; // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        private const float CamRayLength = 100f; // The length of the ray from the camera into the scene.

        private void Awake()
        {
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask(Floor);
            // Set up references.
            animator = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Store the input axes.
            var height = Input.GetAxisRaw(Horizontal);
            var vertical = Input.GetAxisRaw(Vertical);
            // Move the player around the scene.
            Move(height, vertical);
            // Turn the player to face the mouse cursor.
            Turning();
            // Animate the player.
            Animating(height, vertical);
        }

        private void Move(float height, float vertical)
        {
            // Set the movement vector based on the axis input.
            movement.Set(height, 0f, vertical);
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;
            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition(transform.position + movement);
        }

        private void Turning()
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Perform the raycast and if it hits something on the floor layer...
            // Create a RaycastHit variable to store information about what was hit by the ray.
            if (!Physics.Raycast(camRay, out var floorHit, CamRayLength, floorMask))
            {
                return;
            }

            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            var playerToMouse = floorHit.point - transform.position;
            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;
            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            var newRotation = Quaternion.LookRotation(playerToMouse);
            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }

        private void Animating(float height, float vertical)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            var walking = Math.Abs(height) > 0 || Math.Abs(vertical) > 0;
            // Tell the animator whether or not the player is walking.
            animator.SetBool(IsWalking, walking);
        }
    }
}