﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // we store our current camera's position into the variable temp
        Vector3 temp = transform.position;

        // we set the camera's position x to be equal to the player's position x
        // temp.x = playerTransform.position.x;
        // temp.y = playerTransform.position.y;

        if (playerTransform)
        {
            temp.x = Mathf.Clamp(playerTransform.position.x, minPosition.x, maxPosition.x);
            temp.y = Mathf.Clamp(playerTransform.position.y, minPosition.y, maxPosition.y);

            // we set back the camera's temp position to the camera's position
            transform.position = temp;
        }
    }
}
