using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] Transform leftHandAnchor;
    [SerializeField] Transform rightHandAnchor;

    private Transform leftHandTransform;
    private Transform rightHandTransform;

    private bool isCollected = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        GameObject[] hands = GameObject.FindGameObjectsWithTag("hands");
        if (hands.Length != 2)
            Debug.LogError("Missing hand transforms for collectables to work properly");

        if (hands[0].name == "hand_left")
        {
            leftHandTransform = hands[0].transform;
            rightHandTransform = hands[1].transform;
        }
        else
        {
            leftHandTransform = hands[1].transform;
            rightHandTransform = hands[0].transform;
        }
    }

    public virtual void Use(GameObject target = null) {}

    public override void Interact(InteractionType interactionType)
    {
        base.Interact(interactionType);

        // collect the object

        if (!isCollected)
        {
            OnFirstTimeCollect();
            isCollected = true;
            SetIsInteractable(false);
        }
        else
        {
            return;
        }

        if (interactionType == InteractionType.None) return;

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (interactionType == InteractionType.LeftHand)
        {
            transform.parent = leftHandTransform;
            transform.localPosition = leftHandAnchor.localPosition;
            transform.localRotation = leftHandAnchor.localRotation;
            PlayerControl.instance.leftHandItem = this;
            PlayerControl.instance.leftHandItemInReach = null;
            if (PlayerControl.instance.rightHandItemInReach == this)
                PlayerControl.instance.rightHandItemInReach = null;
        }
        if (interactionType == InteractionType.RightHand)
        {
            transform.parent = rightHandTransform;
            transform.localPosition = rightHandAnchor.localPosition;
            transform.localRotation = rightHandAnchor.localRotation;
            PlayerControl.instance.rightHandItem = this;
            PlayerControl.instance.rightHandItemInReach = null;
            if (PlayerControl.instance.leftHandItemInReach == this)
                PlayerControl.instance.leftHandItemInReach = null;
        }
    }

    public virtual void Release(InteractionType interactionType)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }

    protected virtual void OnFirstTimeCollect() { }
}
