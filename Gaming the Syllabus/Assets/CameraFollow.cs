using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform playerPos;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(playerPos.position.x,playerPos.position.y,-10f);
        transform.position = Vector3.Slerp(transform.position,newPos,followSpeed*Time.deltaTime);
    }
}
