using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteraction : Collectable
{
    private AudioSource audioPlayer;
    
    private bool isTurnedOn;
    private bool isDying;

    private float lifeTime;

    [SerializeField] Light spotLight;
    [SerializeField] AudioClip flashlightSwitchClip;
    [SerializeField] AudioClip batteryPlaceClip;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        audioPlayer = GetComponent<AudioSource>();
        lifeTime = 100f;
        isTurnedOn = true;
        isDying = false;
    }

    void Update()
    {
        if (isDying) return;

        if (lifeTime <= 0.0f) {
            StartCoroutine("DieOut");
        }

        if (isTurnedOn)
        {
            lifeTime -= Time.deltaTime;
        }
    }

    protected override void OnTriggerEnter(Collider collider) {
        base.OnTriggerEnter(collider);

        if (collider.gameObject.name == "battery") {
            lifeTime = 100.0f;
            spotLight.intensity = 1.0f;

            Destroy(collider.gameObject);

            audioPlayer.Stop();
            audioPlayer.clip = batteryPlaceClip;
            audioPlayer.Play();
        }
    }

    public override void Use(GameObject target = null) {
        isTurnedOn = !isTurnedOn;
        spotLight.enabled = !spotLight.enabled;
        audioPlayer.Stop();
        audioPlayer.clip = flashlightSwitchClip;
        audioPlayer.Play();
    }

    protected override void OnFirstTimeCollect() {
        StartCoroutine("DieOut", 2f);
    }

    IEnumerator DieOut() {

        isDying = true;

        float timeRemaining = 2.0f;
        while (timeRemaining > 0.0f)
        {
            spotLight.intensity -= 0.04f;
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        // flicker for 1 second
        timeRemaining = 0.4f;
        while (timeRemaining > 0.0f)
        {
            spotLight.enabled = !spotLight.enabled;
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        isTurnedOn = false;
        spotLight.enabled = false;
        spotLight.intensity = 0.0f;
        lifeTime = 0.0f;
    }
}
