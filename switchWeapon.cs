using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchWeapon : MonoBehaviour {

    public GameObject weapon01;
    public GameObject weapon02;

	// Use this for initialization
	void Start () {
        weapon02.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switchWeaponsPlease();
        }
    }

    void switchWeaponsPlease()
    {
        if(weapon01.activeSelf)
        {
            weapon01.SetActive(false);
            weapon02.SetActive(true);
        }
        else
        {
            weapon01.SetActive(true);
            weapon02.SetActive(false);
        }
    }
}
