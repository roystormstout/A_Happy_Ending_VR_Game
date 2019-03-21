using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneFlowControl : MonoBehaviour
{
    [SerializeField] float startSceneFreezeTime = 8.0f;

    private void Start()
    {
        SetPlayerControlActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > startSceneFreezeTime) {
            SetPlayerControlActive(true);
            this.enabled = false;
        }    
    }

    private void SetPlayerControlActive(bool active) {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = active;
        GameObject.FindGameObjectWithTag("Player").GetComponent<OVRPlayerController>().EnableRotation = active;
    }
}
