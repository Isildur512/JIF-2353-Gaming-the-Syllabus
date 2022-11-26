using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 speed = new Vector2(5,5);

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed.x * xInput, speed.y * yInput, 0);

        movement *= Time.deltaTime;
        
        transform.Translate(movement);
    }
}
