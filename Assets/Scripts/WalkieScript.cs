using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieScript : Collectable, Interactable
{
    public override void Collect(InteractionType interactionType)
    {
        base.Collect(interactionType);
        ConditionManager.instance.UpdateConditions("t_item_pickup");
    }

    public void Interact() {
        ConditionManager.instance.UpdateConditions("t_item_interaction");
    }
}
