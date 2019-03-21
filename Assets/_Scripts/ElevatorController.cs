using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public GameObject trickwall;
    [SerializeField] AudioClip mockingbirdClip;
    [SerializeField] AudioClip arriveClip;
    [SerializeField] AudioClip openClip;
    [SerializeField] AudioClip closeClip;
    [SerializeField] AudioClip movingClip;
    [SerializeField] AudioClip stopClip;
    [SerializeField] AudioSource sourceplayer;
    private Condition endofroadcondition;
    private Condition insideelevatorcondition;
    private Animation animator;
    private bool endofroadFinished;
    private bool endingFinished;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animation>();
        endofroadcondition = new Condition("hallwaylightoff");
        endofroadcondition = ConditionManager.instance.AddCondition(endofroadcondition);
        endofroadFinished = false;
        endingFinished = false;
        insideelevatorcondition = new Condition("theend");
        insideelevatorcondition = ConditionManager.instance.AddCondition(insideelevatorcondition);
    }

    // Update is called once per frame
    void Update()
    {

        if (endofroadcondition.IsCompleted()&&!endofroadFinished)
        {
            StartCoroutine("FirstCondtionMet");
            endofroadFinished = true;
        }

        if (insideelevatorcondition.IsCompleted()&& !endingFinished)
        {
            StartCoroutine("SecondCondtionMet");
            endingFinished = true;
        }
    }

    IEnumerator FirstCondtionMet()
    {
        yield return new WaitForSeconds(8f);
        animator.Play("OpenDoors");
        sourceplayer.clip = arriveClip;
        sourceplayer.Play();
        trickwall.SetActive(false);
        yield return new WaitForSeconds(sourceplayer.clip.length);
        sourceplayer.clip = openClip;
        sourceplayer.Play();
        yield return new WaitForSeconds(sourceplayer.clip.length);
       
    }


    IEnumerator SecondCondtionMet()
    {
        animator.Play("CloseDoors");
        sourceplayer.clip = closeClip;
        sourceplayer.Play();
        yield return new WaitForSeconds(sourceplayer.clip.length);
        sourceplayer.clip = movingClip;
        sourceplayer.Play();
        yield return new WaitForSeconds(5f);
        sourceplayer.clip = stopClip;
        sourceplayer.Play();
        yield return new WaitForSeconds(sourceplayer.clip.length);
        PlayerUIControl.instance.Blackout();
        sourceplayer.clip = mockingbirdClip;
        sourceplayer.Play();
    }
}
