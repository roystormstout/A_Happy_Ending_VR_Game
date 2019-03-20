using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] string eventName;
    [SerializeField] AudioClip optionalClip;

    [SerializeField] GameObject demon;

    private AudioSource audioPlayer;
    private bool triggered = false;

    private void Update()
    {
        audioPlayer = GetComponent<AudioSource>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.gameObject.tag == "Player")
        {
            triggered = true;
            ConditionManager.instance.UpdateConditions(eventName);
            if (optionalClip != null)
                audioPlayer.PlayOneShot(optionalClip);

            demon.SetActive(true);
        }
    }
}
