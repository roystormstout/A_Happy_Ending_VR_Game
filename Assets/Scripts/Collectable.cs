using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] Transform leftHandAnchor;
    [SerializeField] Transform rightHandAnchor;

    private Transform leftHandTransform;
    private Transform rightHandTransform;

    private Transform silhouette;

    public enum InteractionType
    {
        LeftHand,
        RightHand,
        Both,
        None
    }

    // Start is called before the first frame update
    void Start()
    {

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

        // find silhouette for highlight use
        silhouette = transform.Find("Silhouette");

        if (silhouette == null)
            Debug.LogError("Collectable " + this.gameObject.name + " does not have a silhouette!");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "hand_left")
        {
            SetHighLight(true);
            PlayerControl.instance.leftHandItemInReach = this;
        }
        if (other.gameObject.name == "hand_right")
        {
            SetHighLight(true);
            PlayerControl.instance.rightHandItemInReach = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "hand_left" && PlayerControl.instance.leftHandItemInReach == this)
        {
            PlayerControl.instance.leftHandItemInReach = null;
            SetHighLight(false);
        }
        if (other.gameObject.name == "hand_right" && PlayerControl.instance.rightHandItemInReach == this)
        {
            PlayerControl.instance.rightHandItemInReach = null;
            SetHighLight(false);
        }
    }


    public virtual void Collect(InteractionType interactionType)
    {
        if (interactionType == InteractionType.None) return;

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (interactionType == InteractionType.LeftHand)
        {
            transform.parent = leftHandTransform;
            transform.localPosition = leftHandAnchor.localPosition;
            transform.localRotation = leftHandAnchor.localRotation;
        }
        if (interactionType == InteractionType.RightHand)
        {
            transform.parent = rightHandTransform;
            transform.localPosition = rightHandAnchor.localPosition;
            transform.localRotation = rightHandAnchor.localRotation;
        }

        // set highlight to false
        SetHighLight(false);
    }

    public virtual void Release(InteractionType interactionType)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }


    public void SetHighLight(bool isHighlight) {
        if (silhouette == null)
            return;

        silhouette.gameObject.SetActive(isHighlight);
    }
}
