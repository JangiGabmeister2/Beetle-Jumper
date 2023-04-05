using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnter : MonoBehaviour
{
    public UnityEvent enterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enterEvent.Invoke();
        }
    }
}
