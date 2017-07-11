using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision2 : MonoBehaviour
{

    Transform player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtPosition = player.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
    }
}
