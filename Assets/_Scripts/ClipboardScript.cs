using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardScript : Collectable
{
    [SerializeField] bool isEventTrigger = false;
    [SerializeField] string eventName = "photoChange";

    public override void Use(GameObject target = null)
    {
        base.Use(target);

        ConditionManager.instance.UpdateConditions(eventName);
        
        Destroy(this.gameObject);
    }
}
