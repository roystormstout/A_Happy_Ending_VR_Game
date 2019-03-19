using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardScript : Interactable
{
    [SerializeField] AudioClip noteflipClip;
    [SerializeField] Transform playerReadAnchor;

    private AudioSource audioPlayer;

    protected override void Start()
    {
        base.Start();

        audioPlayer = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
       
    }

    public override void Interact(InteractionType interactionType)
    {
        base.Interact(interactionType);
        // show up a board in front of player

        GetComponent<Rigidbody>().isKinematic = true;

        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localPosition = playerReadAnchor.localPosition;
        transform.localRotation = playerReadAnchor.localRotation;
    }
}
