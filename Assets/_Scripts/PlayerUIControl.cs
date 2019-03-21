using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerUIControl : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Image blackoutImage;
    [SerializeField] GameObject mainscreenPanel;
    [SerializeField] Button difficulty;
    [SerializeField] Button quit;
    [SerializeField] Button easy;
    [SerializeField] Button hard;

    private bool isChanging = false;

    public bool isMenu = false;
    private float menuCD = 3.0f;
    private float menuTimer = 0.0f;
    private int difficultyNum = 0;

    [SerializeField] Button selected;

    public static PlayerUIControl instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
            instance = this;

        mainscreenPanel.SetActive(false);

    }

    private void Update()
    {
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (input.x != 0 || input.y != 0)
        {

            mainscreenPanel.SetActive(true);

            menuTimer = menuCD;


            if (!isMenu)
            {
                difficulty.gameObject.SetActive(true);
                quit.gameObject.SetActive(true);

                easy.gameObject.SetActive(false);
                hard.gameObject.SetActive(false);
            }

            isMenu = true;
        }


        if (menuTimer > 0.0f)
        {
            menuTimer -= Time.deltaTime;
        }
        else {
            mainscreenPanel.SetActive(false);
            isMenu = false;
        }


        if (isMenu && OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) {
            Debug.Log(selected);
            if (selected == quit) Quit();

            if (selected == difficulty) Difficulty();

            if (selected == easy) Easy();

            if (selected == hard) Hard();
        }
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
        isChanging = true;
        float totalTime = 2.0f;
        float timeRemaining = totalTime;

        while (timeRemaining > 0.0f)
        {
            var tempColor = blackoutImage.color;
            tempColor.a = Mathf.Min(1.0f, tempColor.a + 1.0f / totalTime * Time.deltaTime);
            blackoutImage.color = tempColor;
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        isChanging = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitSelect(BaseEventData eventData)
    {
        selected = quit;
    }

    public void OnQuitDeselect(BaseEventData eventData)
    {
        selected = null;
    }


    public void OnDifficultSelect(BaseEventData eventData)
    {
        selected = difficulty;
    }

    public void OnDifficultDeSelect(BaseEventData eventData)
    {
        selected = null;
    }

    public void OnEasySelect(BaseEventData eventData)
    {
        selected = easy;
    }

    public void OnEasyDeselect(BaseEventData eventData)
    {
        selected = null;
    }


    public void OnHardSelect(BaseEventData eventData)
    {
        selected = hard;
    }

    public void OnHardDeSelect(BaseEventData eventData)
    {
        selected = null;
    }

    public void Quit() {
        Application.Quit();
    }


    public void Difficulty()
    {
        difficulty.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);

        easy.gameObject.SetActive(true);
        hard.gameObject.SetActive(true);
    }

    public void Easy() {
        GameObject.FindGameObjectWithTag("difficulty").GetComponent<Light>().intensity = 1.0f;
    }

    public void Hard() {
        GameObject.FindGameObjectWithTag("difficulty").GetComponent<Light>().intensity = 0.5f;
    }
}
