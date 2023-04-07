using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(transform.up, rotationSpeed);
    }
}
