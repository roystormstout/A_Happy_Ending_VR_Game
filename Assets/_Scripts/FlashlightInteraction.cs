using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteraction : Collectable
{
    private bool isTurnedOn;
    private bool isDying;

    private float lifeTime;

    [SerializeField] Light spotLight;
    [SerializeField] AudioClip flashlightSwitchClip;
    [SerializeField] AudioClip batteryPlaceClip;
    [SerializeField] AudioClip lightFlickerClip;

    private Transform forwardDirection;
    private Condition raycastCondition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        lifeTime = 100f;
        isTurnedOn = true;
        isDying = false;
        raycastCondition = new Condition("hallwaylightoff");
        raycastCondition = ConditionManager.instance.AddCondition(raycastCondition);
        forwardDirection = transform.Find("ForwardDirection");
    }

    void Update()
    {
        if (isDying) { Debug.Log("died"); }

        //if (lifeTime <= 0.0f) {
        //    StartCoroutine("DieOut");
        //}
        else
        {
            if (isTurnedOn)
            {
                lifeTime -= Time.deltaTime;

                // raycast
                int layerMask = 1 << 8 + 1 << 11;
                layerMask = ~layerMask;

                if (raycastCondition.IsCompleted())
                {
                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(forwardDirection.position, forwardDirection.position - transform.position, out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.collider.gameObject.name == "Demon")
                        {
                            GameObject.FindGameObjectWithTag("Demon").GetComponent<Animator>().enabled = true;
                            StartCoroutine("DieOut");
                        }
                    }
                }
            }
        }
    }

    protected override void OnTriggerEnter(Collider collider) {
        base.OnTriggerEnter(collider);

        if (collider.gameObject.name == "battery") {
            isDying = false;
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
        //StartCoroutine("DieOut", 2f);
    }

    IEnumerator DieOut() {

        isDying = true;


        float timeRemaining = 1.5f;
        while (timeRemaining > 0.0f)
        {
            spotLight.intensity -= 0.04f;
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        audioPlayer.PlayOneShot(lightFlickerClip);
        // flicker for 1 second
        timeRemaining = 1.0f;
        while (timeRemaining > 0.0f)
        {
            spotLight.enabled = !spotLight.enabled;
            timeRemaining -= 0.12f;
            yield return new WaitForSeconds(0.12f);
        }

        audioPlayer.Stop();

        isTurnedOn = false;
        spotLight.enabled = false;
        spotLight.intensity = 0.0f;
        lifeTime = 0.0f;
    }
}
