using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, Interactable
{
    public void Interact()
    {
        ConditionManager.instance.UpdateConditions("t_door_interaction");
    }
}
