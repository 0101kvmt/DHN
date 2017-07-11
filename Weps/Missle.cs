using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float speed;
    Transform player;
    int missleLife;
    float timer;

    // Use this for initialization
    void Start()
    {
        missleLife = 15;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > missleLife)
            Destroy(this.gameObject);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("EnemyHit", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
        
    }
}
