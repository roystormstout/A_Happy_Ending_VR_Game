using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDoll : MonoBehaviour
{

    public void Disappear()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<MeshRenderer>().enabled = false;
    }
}
