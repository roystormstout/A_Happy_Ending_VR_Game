using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject moveIndicator;
    public GameObject player;
    public GameObject door;
    public Text helperText;
    private float waitTime = 5.0f;
    private float timer = 0.0f;
    private int step = 0;
    private float distance = 0.0f;
    private float target_distance = 2.0f;
    private Vector3 prev_trans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
        if (timer > waitTime && step < 1)
        {
            step = 1;
            timer = 0;
        } else if (timer > waitTime && distance > target_distance && step == 1)
        {
            step = 2;
            timer = 0;
        }
        
        if(step == 1)
        {
            
            helperText.text = "Move by simulating running with your arms. Now, look at where you want to go and wave your arms as if you are actually running.";
            distance += Vector3.Distance(prev_trans, player.transform.position);
            prev_trans = player.transform.position;
        } else if (step == 2)
        {
            if (!moveIndicator.activeSelf)
            {
                moveIndicator.SetActive(true);
                helperText.text = "Follow the arrow to go the next location.";

            }


        } else if (step == 3)
        {
            helperText.text = "You can press the index finger trigger to push the door open.";

        } else if (step == 4)
        {
            helperText.text = "The item which you can collect will be highlighted when you put your hand on it. Press hand finger trigger to collect it.";
        }
        else if (step == 5)
        {
            helperText.text = "Press index finger trigger to use the item.";
        } else if (step == 6)
        {
            // play scary music and trasition plot
            // switching scene to chapter 1.
            helperText.text = "WELCOME TO THE NIGHTMARE";
        }
    }
}
