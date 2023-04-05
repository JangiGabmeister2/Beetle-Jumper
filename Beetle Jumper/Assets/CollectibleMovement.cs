using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
    public float rotationSpeed;
    private Vector3 upPosition, downPosition;

    private void Start()
    {
        upPosition = new Vector3(transform.position.x, transform.position.y + 2);
        downPosition = new Vector3(transform.position.x, transform.position.y - 2);
    }

    private void Bobbing()
    {
        Vector3 newPosition = Vector3.zero;

        if (transform.position == newPosition)
        {
            if (transform.position == upPosition)
            {
                newPosition = downPosition;                
            }
            else if (transform.position == downPosition)
            {
                newPosition = upPosition;
            }
        }

        transform.Translate(newPosition * Time.deltaTime, Space.World);
    }

    private void Rotate()
    {
        transform.Rotate(transform.up, rotationSpeed);
    }

    private void Update()
    {
        Rotate();
        Bobbing();
    }
}
