using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    [HideInInspector]
    public AudioClip explosionSound;

    AudioSource source;

    // Duration
    float lifespan;

	private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

   void Start()
    {
        source.PlayOneShot(explosionSound);
    }

	// Update is called once per frame
	void Update () {
        lifespan += Time.deltaTime;
        if (lifespan > .5)
            Destroy(this.gameObject);
	}
}
