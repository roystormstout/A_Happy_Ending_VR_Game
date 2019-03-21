using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseDrawInteract : Interactable
{
    private AudioSource audioPlayer;

    private float smooth = 0.8f;
    private float DoorOpenAngle = -90.0f;

    private Vector3 defaultRot;
    private Vector3 openRot;

    private Condition morgueKeyCondition;


    protected override void Start()
    {
        base.Start();
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);

        audioPlayer = GetComponent<AudioSource>();

        morgueKeyCondition = new Condition("hinttomorgue");
        morgueKeyCondition = ConditionManager.instance.AddCondition(morgueKeyCondition);
    }


    public override void Interact(InteractionType interactionType)
    {
        base.Interact(interactionType);

        if (!morgueKeyCondition.IsCompleted())
            return;

        audioPlayer.Play();

        SetIsInteractable(false);

        StartCoroutine("DoorOperation");
    }

    IEnumerator DoorOperation()
    {
        float timeRemaining = 2f;
        while (timeRemaining > 0.0f)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
    }

}
