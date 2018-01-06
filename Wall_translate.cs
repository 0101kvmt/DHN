using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_translate : MonoBehaviour
{
    public GameObject megadoor;
    public float speed;
    private bool isFalling = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(isFalling)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
           
        
    }
    public void Fallen()
    {
        isFalling = true;
    }

}