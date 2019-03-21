using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Condition raycastStart;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        lifeTime = 100f;
        isTurnedOn = true;
        isDying = false;
        raycastStart = new Condition("hallwaylightoff");
        raycastStart = ConditionManager.instance.AddCondition(raycastStart);
        forwardDirection = transform.Find("ForwardDirection");


        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            this.Interact(Interactable.InteractionType.RightHand);

        }
    }

    void Update()
    {
        if (isDying)
            return;

        if (isTurnedOn)
        {
            lifeTime -= Time.deltaTime;

            // raycast
            int layerMask = 1 << 8 + 1 << 11;
            layerMask = ~layerMask;

            if (raycastStart.IsCompleted())
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(forwardDirection.position, forwardDirection.position - transform.position, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.gameObject.tag == "Demon")
                    {
                        StartCoroutine("DieOut");
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


        float timeRemaining = 1f;
        while (timeRemaining > 0.0f)
        {
            spotLight.intensity -= 0.04f;
            timeRemaining -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        audioPlayer.PlayOneShot(lightFlickerClip);

        timeRemaining = 0.4f;
        spotLight.enabled = false;
        yield return new WaitForSeconds(0.4f);
        if (timeRemaining < 1.0f && !spotLight.enabled)
        {
            GameObject demonDoll = GameObject.FindGameObjectWithTag("Demon");
            if (demonDoll)
                demonDoll.GetComponent<DemonDoll>().Disappear();
        }

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
