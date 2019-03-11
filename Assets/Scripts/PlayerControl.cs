using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Collectable leftHandItem;
    Collectable rightHandItem;

    // Start is called before the first frame update
    void Start()
    {
        leftHandItem = rightHandItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Index trigger pressed !");
        }

    }
}
