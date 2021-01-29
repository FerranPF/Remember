using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject playerPosition;
    private Vector3 initPosition;

    public float smoothSpeed = 0.125f;

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        initPosition = this.transform.position;
    }


    void FixedUpdate()
    {
        Vector3 desiredPosition = playerPosition.transform.position + initPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
