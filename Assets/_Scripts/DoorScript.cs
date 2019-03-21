using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactable
{
    private AudioSource audioPlayer;

    // rewrite using condition if possible
    [SerializeField] bool isLocked = false;

    [SerializeField] AudioClip DoorOpenClip;
    [SerializeField] AudioClip DoorCloseClip;
    [SerializeField] AudioClip DoorLockedClip;
    [SerializeField] AudioClip DoorUnlockClip;

    private bool AudioS;

    private float smooth = 2.0f;
    [SerializeField] float DoorOpenAngle = 90.0f;

    private Vector3 defaultRot;
    private Vector3 openRot;
    private bool isOpened = false;
    private bool playerInRange = false;
    private bool isRotating = false;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
        audioPlayer = GetComponent<AudioSource>();
    }

    public override void Interact(InteractionType interactionType)
    {
        if (isLocked)
            audioPlayer.PlayOneShot(DoorLockedClip);

        if (isRotating || isLocked)
            return;

        StartCoroutine("DoorOperation");
    }

    // return whether successfully unlocked the door
    public bool UnlockDoor() {
        if (isLocked == false)
            return false;

        Debug.Log(this.gameObject.name + " unlocked ");
        isLocked = false;
        audioPlayer.PlayOneShot(DoorUnlockClip);

        return true;
    }

    IEnumerator DoorOperation()
    {
        isRotating = true;

        if (isOpened)
            audioPlayer.PlayOneShot(DoorOpenClip);
        else
            audioPlayer.PlayOneShot(DoorCloseClip);

        float timeRemaining = 2f;
        while (timeRemaining > 0.0f) {
            if (!isOpened)
                transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
            else
                transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);

            timeRemaining -= Time.deltaTime;

            yield return null;
        }

        isOpened = !isOpened;
        isRotating = false;
    }

}






