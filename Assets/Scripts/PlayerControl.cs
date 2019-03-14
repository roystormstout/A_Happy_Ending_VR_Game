using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private AudioSource audioPlayer;
    private CharacterController controller;

    public Collectable leftHandItemInReach;
    public Collectable rightHandItemInReach;

    private Collectable leftHandItem;
    private Collectable rightHandItem;

    private Transform leftHandTransform;
    private Transform rightHandTransform;
    private Vector3 lastFrameLeftHandPosition;
    private Vector3 lastFrameRightHandPosition;

    private float midpointZ = 0.3f;

    // player movement variables
    private float nextTimeToRefreshSpeed = 0.0f;
    private float speedRefreshInterval = 0.2f;
    private float playerSpeed = 0.0f;
    private float playerWalkSpeed = 1.5f;
    private float walkDetactThreshold = 0.025f;
    private float deltaMovement = 0.0f;

    // variables used to determine foot steps
    [SerializeField] AudioClip footStepClip;
    private float walkGestureDirection = 0.0f;
    private float lastRefreshedPosition = 0.0f;

    public static PlayerControl instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Only 1 PlayerControl is allowed");

        controller = GetComponent<CharacterController>();
        audioPlayer = GetComponent<AudioSource>();

        leftHandItemInReach = rightHandItemInReach = null;
        leftHandItem = rightHandItem = null;


        // find two hands in the world
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

    // Update is called once per frame
    void Update()
    {

        HandleMovement();


        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
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

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
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

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick) && leftHandItem != null) {
            Interactable leftHandInteract = leftHandItem.gameObject.GetComponent<Interactable>();
            if (leftHandInteract != null)
                leftHandInteract.Interact();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) && rightHandItem  != null)
        {
            Interactable rightHandInteract = rightHandItem.gameObject.GetComponent<Interactable>();
            if (rightHandInteract != null)
                rightHandInteract.Interact();
        }
    }


    private void HandleMovement() {

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            // initiate movement
            playerSpeed = playerWalkSpeed;
            nextTimeToRefreshSpeed = speedRefreshInterval;
            deltaMovement = 0.0f;
            walkGestureDirection = 0.0f;
            lastRefreshedPosition = rightHandTransform.localPosition.z;
        }

        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if (nextTimeToRefreshSpeed < 0.0f)
            {
                float currentRefreshedPosition = rightHandTransform.localPosition.z;

                if (deltaMovement > walkDetactThreshold)
                {
                    playerSpeed = playerWalkSpeed;

                    if ((currentRefreshedPosition - lastRefreshedPosition) * walkGestureDirection < 0.0f)
                    {
                        audioPlayer.clip = footStepClip;
                        audioPlayer.Stop();
                        audioPlayer.Play();
                    }

                    walkGestureDirection = currentRefreshedPosition - lastRefreshedPosition;
                }
                else
                {
                    playerSpeed = 0.0f;
                    walkGestureDirection = 0.0f;
                }

                lastRefreshedPosition = currentRefreshedPosition;

                nextTimeToRefreshSpeed = speedRefreshInterval;
                deltaMovement = 0.0f;
            }
            else
            {
                nextTimeToRefreshSpeed -= Time.deltaTime;
                deltaMovement += Mathf.Abs(rightHandTransform.localPosition.z - lastFrameRightHandPosition.z);
            }

            float deltaZ = rightHandTransform.localPosition.z - lastFrameRightHandPosition.z;
            controller.Move(transform.forward * Mathf.Abs(playerSpeed) * Time.deltaTime);
        }

        else
        {
            // reset movement
            playerSpeed = 0.0f;
            nextTimeToRefreshSpeed = speedRefreshInterval;
            deltaMovement = 0.0f;
        }

        lastFrameLeftHandPosition = leftHandTransform.localPosition;
        lastFrameRightHandPosition = rightHandTransform.localPosition;
    }


    public bool isHolding()
    {
        return leftHandItem != null || rightHandItem != null;
    }
}
