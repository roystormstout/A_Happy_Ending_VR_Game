using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyScript : Collectable
{

    public override void Use(GameObject target = null)
    {
        base.Use(target);

        if (target == null) return;

        DoorScript doorScript = target.GetComponent<DoorScript>();
        if (doorScript != null) {
            // if successfully unlocked the door, destroy key for now
            if (doorScript.UnlockDoor())
                Destroy(this.gameObject);
        }
    }
}
