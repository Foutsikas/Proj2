using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        float moveCameraHorizontal = Input.GetAxis("camHorizontal");
        float moveCameraVertical = Input.GetAxis("camVertical");
        float moveUpward = 0.0f;

        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
        {
            moveUpward = 1.0f;
        }
        else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            moveUpward = -1.0f;
        }

        Vector3 movement = new Vector3(moveCameraHorizontal, moveUpward, moveCameraVertical);
        transform.position = transform.position + movement * speed * Time.deltaTime;
    }
}