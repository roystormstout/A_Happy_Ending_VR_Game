using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteraction : MonoBehaviour, Interactable
{
    private AudioSource audioPlayer;
    
    private bool isTurnedOn;
    private bool isDying;

    private float lifeTime;

    [SerializeField] Light spotLight;
    [SerializeField] AudioClip flashlightSwitchClip;
    [SerializeField] AudioClip batteryPlaceClip;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        lifeTime = 5f;
        isTurnedOn = true;
        isDying = false;
    }

    void Update()
    {
        if (isDying) return;

        if (lifeTime < 0.0f) {
            StartCoroutine("DieOut");
        }

        if (isTurnedOn)
        {
            lifeTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "battery") {
            lifeTime = 100.0f;
            spotLight.intensity = 1.0f;

            Destroy(collider.gameObject);

            audioPlayer.Stop();
            audioPlayer.clip = batteryPlaceClip;
            audioPlayer.Play();
        }
    }

    public void Interact() {
        isTurnedOn = !isTurnedOn;
        spotLight.enabled = !spotLight.enabled;
        audioPlayer.Stop();
        audioPlayer.clip = flashlightSwitchClip;
        audioPlayer.Play();
    }

    IEnumerator DieOut() {

        isDying = true;

        float timeRemaining = 5.0f;
        while (timeRemaining > 0.0f)
        {
            spotLight.intensity = spotLight.intensity * 0.9f;
            timeRemaining -= 1f;
            yield return new WaitForSeconds(1f);
        }

        
    }
}
