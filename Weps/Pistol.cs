﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour
{

    public GameObject BloodSplat;
    public Sprite idlePistol;
    public Sprite shotPistol;
    public float pistolDamage;
    public float pistolRange;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;

    public Text ammoText;

    public int ammoAmount;
    public int ammoClipSize;

    public GameObject bulletHole;

    int ammoLeft;
    int ammoClipLeft;

    bool isShot;
    bool isReloading;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount;
        ammoClipLeft = ammoClipSize;
    }

    void Update()
    {
        ammoText.text = ammoClipLeft + " / " + ammoLeft;

        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
    }

    void FixedUpdate()
    {
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector2 bulletOffset = Random.insideUnitCircle * 10;
        //  Vector2 bulletOffset = Random.insideUnitCircle * DynamicCrossHair.spread;
        Vector3 randomTarget = new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(randomTarget);
        RaycastHit hit;
        if (isShot == true && ammoClipLeft > 0 && isReloading == false)
        {
            isShot = false;
            DynamicCrossHair.spread += DynamicCrossHair.PISTOL_SHOOTING_SPREAD;
            ammoClipLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("shot");
            if (Physics.Raycast(ray, out hit, pistolRange))
            {
                Debug.Log("Collided at " + hit.collider.gameObject.name);
                if (hit.transform.CompareTag("Enemy"))
                {
                    Instantiate(BloodSplat, hit.point, Quaternion.identity);
                    if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().patrolState || hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().alertState)
                    {
                        hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                    }

                    hit.collider.gameObject.SendMessage("addDamage", pistolDamage, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    if (!hit.transform.CompareTag("Missle"))
                    {
                        Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
                    }
                }

            }
        }
        else if (isShot == true && ammoClipLeft <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }
    }

    void Reload()
    {
        int bulletsToReload = ammoClipSize - ammoClipLeft;
        if (ammoLeft >= bulletsToReload)
        {
            StartCoroutine("ReloadWeapon");
            ammoLeft -= bulletsToReload;
            ammoClipLeft = ammoClipSize;
        }
        else if (ammoLeft < bulletsToReload && ammoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            ammoClipLeft += ammoLeft;
            ammoLeft = 0;
        }
        else if (ammoLeft <= 0)
        {
            source.PlayOneShot(emptyGunSound);
        }
    }


    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2);
        isReloading = false;
    }

    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = shotPistol;
        yield return new WaitForSeconds(0.1f);
        Debug.Log("skr");
        GetComponent<SpriteRenderer>().sprite = idlePistol;

    }

}