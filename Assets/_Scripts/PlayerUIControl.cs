using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIControl : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Image blackoutImage;

    private bool isChanging = false;

    public static PlayerUIControl instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
            instance = this;

    }

    public void SetTitleText(string titleString)
    {
        titleText.text = titleString;
    }

    public void Blackout() {
        if (!isChanging) {
            StartCoroutine("CloseScreen");
        }
    }

    IEnumerator CloseScreen()
    {
        float totalTime = 2.0f;
        float timeRemaining = totalTime;

        while (timeRemaining > 0.0f)
        {
        }
    }
}
