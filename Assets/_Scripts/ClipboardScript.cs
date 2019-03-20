using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardScript : Collectable
{
    public override void Use(GameObject target = null)
    {
        base.Use(target);

        Destroy(this.gameObject);
    }
}
