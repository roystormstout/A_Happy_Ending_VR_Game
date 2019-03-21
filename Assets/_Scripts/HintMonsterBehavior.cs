using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintMonsterBehavior : MonoBehaviour
{
    public float speed = 1.4f;

    private AudioSource audioPlayer;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed*Time.deltaTime);
    }



}
