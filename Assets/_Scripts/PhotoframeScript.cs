using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoframeScript : MonoBehaviour
{
    [SerializeField] Material ghostgirlMat;
    [SerializeField] string eventName = "photoChange";
    private Transform plane;
    private Condition triggerCondition;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        plane = transform.GetChild(0);
        triggerCondition = ConditionManager.instance.AddCondition(new Condition(eventName));
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerCondition.IsCompleted() && !triggered)
        {
            StartCoroutine("ChangePhoto");
        }
    }

    IEnumerator ChangePhoto()
    {
        triggered = true;

        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        plane.GetComponent<MeshRenderer>().material = ghostgirlMat;

        yield return new WaitForSeconds(2.5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().PlayHeartBeat();
        this.enabled = false;
    }
}
