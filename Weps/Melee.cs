using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Melee : MonoBehaviour {
    
    public GameObject BloodSplat;
    public GameObject MeleeHand;

    // Delay Between Attaks
    public float MeleeTimer = 2f;  

    public float MeleeRange;
    public float MeleeDamage;

    bool meleeAttack = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
            meleeAttack = true;
	}
    void FixedUpdate ()
    {
        if(meleeAttack = true)
        {
//            RaycastHit hit;
//            if (hit.transform.CompareTag("Enemy"))
//            {
//                Deug.Log("Melee hit at" + hit.collider.gameObject.name);
//              if (hit.transform.CompareTag("Enemy"))
//                {
//                    Instantiate(BloodSplat, hit.point.Quaternion.identity);

//                }
//            }
        }
    }
}
