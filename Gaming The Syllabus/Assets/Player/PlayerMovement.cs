using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 4.5f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (movementInput != Vector2.zero) {
            // check original move
            bool success = TryMove(movementInput);

            if (!success) {
                // check x movement
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success) {
                    // check y movement
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

        }
    }

    // this will check for collisions for when we have walls
    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }

    // OnMove is called everytime an input is given
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
}
