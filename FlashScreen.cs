using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlashScreen : MonoBehaviour {

    Image flashScreen;

    Text hp;
    Text ammo;
	
	void Start () {
        flashScreen = GetComponent<Image>();
        hp = GetComponent<Text>();
        ammo = GetComponent<Text>();
	}
	
	
	void Update () {
		if(flashScreen.color.a > 0)
        {
            Color invisible = new Color(flashScreen.color.r, flashScreen.color.g, flashScreen.color.b, 0);
            flashScreen.color = Color.Lerp(flashScreen.color, invisible, 5 * Time.deltaTime);
        }
	}

    public void TookDamage()
    {
        flashScreen.color = new Color(1, 0, 0, 0.8f);
    }
    public void TookHP()
    {
        flashScreen.color = new Color(0, 1, 0, 1);
        hp.text = "U GOT HEALTH";
    }
}
