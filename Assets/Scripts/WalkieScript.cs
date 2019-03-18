using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieScript : Collectable
{
    public override void Use(GameObject target = null) {
        ConditionManager.instance.UpdateConditions("t_item_interaction");
    }
}
