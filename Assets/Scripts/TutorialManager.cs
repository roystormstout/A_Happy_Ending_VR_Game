using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject moveIndicator;

    public GameObject door;
    public Text helperText;
    private float waitTime = 5.0f;
    private float timer = 0.0f;
    private int step = 0;
    private float distance = 0.0f;
    private float target_distance = 3.0f;
    private bool triggered;
    private Vector3 prev_trans;
    // Start is called before the first frame update

    private Condition tutorialRootCondition;
    private List<string> tutorialHelperText;

    private int testIndex = 1;

    void Start()
    {
        SetPlayerMovementEnabled(false);

        List<Condition> steps = new List<Condition>();
        steps.Add(new Condition("t_welcome"));
        steps.Add(new Condition("t_movement_instruction"));
        steps.Add(new Condition("t_player_movement"));
        steps.Add(new Condition("t_item_pickup"));
        steps.Add(new Condition("t_item_interaction"));
        steps.Add(new Condition("t_door_interaction"));
        steps.Add(new Condition("t_trap_state"));
        steps.Add(new Condition("t_fallen_state"));

        tutorialRootCondition = new Condition("t_root", steps);
        tutorialRootCondition = ConditionManager.instance.AddCondition(tutorialRootCondition);

        // set up tutorial helper text
        tutorialHelperText = new List<string>();
        tutorialHelperText.Add("Welcome to the happiness resort!Let's get started with some basic operations.");
        tutorialHelperText.Add("Move by press down the right hand hand trigger and simulating running with your arms.Look at where you want to go and wave your arms.");
        tutorialHelperText.Add("Go to the highlighted location.");
        tutorialHelperText.Add("Use Index trigger to pick up an item.");
        tutorialHelperText.Add("Use joystick trigger to pick up an item.");
        tutorialHelperText.Add("Press the joystick button to push a door open.");
        tutorialHelperText.Add("Now, Go to the highlighted location.");
        tutorialHelperText.Add("WELCOME BACK TO UR LIFE.");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        helperText.text = tutorialHelperText[tutorialRootCondition.GetCurrentProgressIndex()];

        // welcome page trigger
        if (tutorialRootCondition.GetCurrentProgressIndex() == 0 && Time.timeSinceLevelLoad > 4.0f)
        {
            ConditionManager.instance.UpdateConditions("t_welcome");
        }
        
        // move instruction trigger
        if (tutorialRootCondition.GetCurrentProgressIndex() == 1 && Time.timeSinceLevelLoad > 8.0f)
        {
            ConditionManager.instance.UpdateConditions("t_movement_instruction");
            SetPlayerMovementEnabled(true);
            moveIndicator.SetActive(true);
        }
    }

    private void SetPlayerMovementEnabled(bool isEnabled) {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = isEnabled;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "moveIndicator")
        {
            ConditionManager.instance.UpdateConditions("t_player_movement");
            other.gameObject.SetActive(false);
        }
    }
}
