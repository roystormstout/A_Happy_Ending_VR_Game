using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDoll : MonoBehaviour
{

    public void Disappear()
    {
        GetComponent<AudioSource>().Play();
        transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
    }
}
