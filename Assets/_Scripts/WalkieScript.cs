using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieScript : Collectable
{
    [SerializeField] AudioClip walkieSound;

    public override void Use(GameObject target = null) {
        audioPlayer.PlayOneShot(walkieSound);
        ConditionManager.instance.UpdateConditions("t_item_interaction");
    }

    public override void Interact(InteractionType interactionType)
    {
        base.Interact(interactionType);

        ConditionManager.instance.UpdateConditions("t_item_pickup");
    }
}
