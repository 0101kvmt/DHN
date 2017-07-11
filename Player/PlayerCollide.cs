using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    public float healthness;
    public float healthnesskit;
    public float speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("healthblue"))
        {
            gameObject.SendMessage("itemHit", healthness, SendMessageOptions.DontRequireReceiver);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("healthkit"))
        {
            gameObject.SendMessage("healthKit", healthnesskit, SendMessageOptions.DontRequireReceiver);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("wall"))
        {
            other.gameObject.SendMessage("Fallen", SendMessageOptions.DontRequireReceiver);
        }

    }
}
