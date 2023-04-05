using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleLerp : MonoBehaviour
{
    public void OnCollect()
    {
        transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("CollectibleUI").transform.position, 2f);
        if (transform.position == GameObject.FindGameObjectWithTag("CollectibleUI").transform.position)
        {
            Destroy(gameObject.GetComponentInParent<GameObject>());
        }
    }
}
