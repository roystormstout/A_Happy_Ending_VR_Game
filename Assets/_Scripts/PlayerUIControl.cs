using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIControl : MonoBehaviour
{
    [SerializeField] Text titleText;

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
}
