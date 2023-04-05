using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeLevel : MonoBehaviour
{
    public UnityEvent OnEnter;

    private void Start()
    {
        if (OnEnter == null)
        {
            OnEnter = new UnityEvent();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            OnEnter.Invoke();
        }
    }
}
