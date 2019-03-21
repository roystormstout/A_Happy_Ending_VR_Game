using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        LeftHand,
        RightHand,
        Both,
        None
    }

    private Transform silhouette;
    protected bool isInteractable = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // find silhouette for highlight use
        silhouette = transform.Find("Silhouette");

        if (silhouette == null)
            Debug.Log("Interactable " + this.gameObject.name + " does not have a silhouette!");
    }

    public virtual void Interact(InteractionType interactionType) { }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!isInteractable)
            return;

        if (other.gameObject.name == "hand_left")
        {
            SetHighlight(true);
            PlayerControl.instance.leftHandItemInReach = this;
        }
        if (other.gameObject.name == "hand_right")
        {
            SetHighlight(true);
            PlayerControl.instance.rightHandItemInReach = this;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!isInteractable)
            return;

        if (other.gameObject.name == "hand_left" && PlayerControl.instance.leftHandItemInReach == this)
        {
            PlayerControl.instance.leftHandItemInReach = null;
            SetHighlight(false);
        }
        if (other.gameObject.name == "hand_right" && PlayerControl.instance.rightHandItemInReach == this)
        {
            PlayerControl.instance.rightHandItemInReach = null;
            SetHighlight(false);
        }
    }

    protected void SetHighlight(bool toggle)
    {
        if (silhouette == null)
            return;

        if (isInteractable)
            silhouette.gameObject.SetActive(toggle);
        else
            silhouette.gameObject.SetActive(false);
    }

    protected void SetIsInteractable(bool interactable) {
        isInteractable = interactable;
        if (!isInteractable)
            SetHighlight(false);
    }
}