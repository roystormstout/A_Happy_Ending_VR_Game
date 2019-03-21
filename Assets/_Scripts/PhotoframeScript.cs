using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoframeScript : MonoBehaviour
{
    [SerializeField] Material ghostgirlMat;
    Transform plane;

    // Start is called before the first frame update
    void Start()
    {
        plane = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("ChangePhoto");
        }
    }

    IEnumerator ChangePhoto()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        plane.GetComponent<MeshRenderer>().material = ghostgirlMat;
    }
}
