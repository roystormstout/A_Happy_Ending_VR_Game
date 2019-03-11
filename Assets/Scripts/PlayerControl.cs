using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Collectable leftHandItemInReach;
    public Collectable rightHandItemInReach;

    private Collectable leftHandItem;
    private Collectable rightHandItem;

    public static PlayerControl instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Only 1 PlayerControl is allowed");

        // find left hand and right hand transform


        leftHandItemInReach = rightHandItemInReach = null;
        leftHandItem = rightHandItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            if (leftHandItem != null)
            {
                leftHandItem.Release(Collectable.InteractionType.LeftHand);
                leftHandItem = null;
            }

            else if (leftHandItemInReach != null)
            {
                leftHandItemInReach.Collect(Collectable.InteractionType.LeftHand);
                leftHandItem = leftHandItemInReach;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            if (rightHandItem != null)
            {
                rightHandItem.Release(Collectable.InteractionType.RightHand);
                rightHandItem = null;
            }

            else if (rightHandItemInReach != null)
            {
                rightHandItemInReach.Collect(Collectable.InteractionType.RightHand);
                rightHandItem = rightHandItemInReach;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && leftHandItem != null) {
            Interactable leftHandInteract = leftHandItem.gameObject.GetComponent<Interactable>();
            if (leftHandInteract != null)
                leftHandInteract.Interact();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && rightHandItem  != null)
        {
            Interactable rightHandInteract = rightHandItem.gameObject.GetComponent<Interactable>();
            if (rightHandInteract != null)
                rightHandInteract.Interact();
        }
    }
}
