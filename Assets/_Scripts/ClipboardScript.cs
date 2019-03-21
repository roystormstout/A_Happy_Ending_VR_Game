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

        Destroy(this.gameObject);

    }

    protected override void OnFirstTimeCollect()
    {
        base.OnFirstTimeCollect();

        if (isEventTrigger)
        {
            ConditionManager.instance.UpdateConditions(eventName);
            Debug.Log("triggered");
        }
    }
}
