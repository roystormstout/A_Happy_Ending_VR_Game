using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGirlBehavior : MonoBehaviour
{
    [SerializeField] Transform nextPos;
    [SerializeField] GameObject body;
    private float CurrTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrTime += Time.deltaTime;
        if (5.0f> CurrTime && CurrTime > 4.0f)
        {
            body.SetActive(false);
        } else if(CurrTime > 5.0f)
        {

            body.SetActive(true);
            transform.position = nextPos.localPosition;
        }
    }
}
