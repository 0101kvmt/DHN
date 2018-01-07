using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Text Health;
    public int maxHealth;
    public AudioClip Hit;
    public FlashScreen flash;
    AudioSource source;
    float health;
	// Use this for initialization
	void Start () {
        health = maxHealth;
        source = GetComponent<AudioSource>();
	}
	
    void EnemyHit (float damage) {
        flash.TookDamage();
        health -= damage;
        source.PlayOneShot(Hit);
    }

    void itemHit(float healthness)
    {
        flash.TookHP();
        health += healthness;
        //source.PlayOneShot(healthUp);
    }
    void healthKit(float healthnesskit)
    {
        flash.TookHP();
        health += healthnesskit;
        //source.PlayOneShot(healthUp);
    }
    // Update is called once per frame
    void Update () {
        Health.text = health + "GR";
        if(health == 0)
        {
            Debug.Log("dead");
        }
    }
}
