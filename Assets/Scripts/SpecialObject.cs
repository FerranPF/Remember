using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialObject : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    public Vector3 offset = new Vector3(0,0,0);

    bool placed = false;
    Vector3 initialPosition;
    Vector3 initialRotation;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (placed)
        {
            this.transform.position = GameObject.FindGameObjectWithTag("ViewPosition").transform.position + offset;
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(v, 0, -h, Space.Self);

            if (Input.GetKeyDown(KeyCode.E))
            {
                ResetObject();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetMovement();
            }
        }
        
    }

    public void ResetObject()
    {
        placed = false;
        this.transform.position = initialPosition;
        this.transform.eulerAngles = initialRotation;
    }

    public void ShowObject()
    {
        if (!placed)
        {
            placed = true;
        }
    }

}
