using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteraction : Interactable
{
    [SerializeField] Light spotLight;
    private AudioSource audio;
    [SerializeField] AudioClip flashlightSwitchClip;
    [SerializeField] AudioClip batteryPlaceClip;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "battery") {
            Destroy(collider.gameObject);

            audio.Stop();
            audio.clip = batteryPlaceClip;
            audio.Play();
        }
    }

    public override void Interact() {
        spotLight.enabled = !spotLight.enabled;
        audio.Stop();
        audio.clip = flashlightSwitchClip;
        audio.Play();
    }
}
