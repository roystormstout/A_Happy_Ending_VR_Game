using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseDrawDoorBehavior : MonoBehaviour
{
    private Condition morgueKeyCondition;
    private bool rotated;
    // Start is called before the first frame update
    void Start()
    {
        morgueKeyCondition = new Condition("hinttomorgue");
        morgueKeyCondition = ConditionManager.instance.AddCondition(morgueKeyCondition);
        rotated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (morgueKeyCondition.IsCompleted()&&!rotated)
        {
            Debug.Log("I just flipped te switch");
            transform.Rotate(0, -90f, 0);
            rotated = true;
        }
    }
}
