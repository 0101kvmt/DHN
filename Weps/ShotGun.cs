using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ShotGun : MonoBehaviour
{

    public GameObject BloodSplat;
    public Sprite idleShotGun;
    public Sprite shotShotGun;
    public float shotGunDamage;
    public float shotGunRange;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;

    public float shotGunCount = 10;
    public float fireRate = 0.5F;
    public float nextFire = 0.15F;
    public float spreadFactor = 15;
    public Text ammoText;

    public Animator anim;

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

        if (Input.GetButton("Fire1") && Time.time > nextFire && isReloading == false)
        {
            nextFire = Time.time + fireRate;
            isShot = true;

        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
    }

    void FixedUpdate()
    {
      
           
            RaycastHit hit;
            if (isShot == true && ammoClipLeft > 0 && isReloading == false)
            {
                
                for (int s = 0; s < shotGunCount; s++)
                {
                    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Vector2 bulletOffset = Random.insideUnitCircle * 10;
                    //  Vector2 bulletOffset = Random.insideUnitCircle * DynamicCrossHair.spread;

                    Vector3 randomTarget = new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0);

                    randomTarget.x += Random.Range(-spreadFactor, spreadFactor);
                    randomTarget.y += Random.Range(-spreadFactor, spreadFactor);
                    randomTarget.z += Random.Range(-spreadFactor, spreadFactor);


                    Ray ray = Camera.main.ScreenPointToRay(randomTarget);
                    
                    DynamicCrossHair.spread += DynamicCrossHair.SHOTGUN_SHOOTING_SPREAD;
                    ammoClipLeft--;
                    source.PlayOneShot(shotSound);
                    StartCoroutine("shot");
                if (Physics.Raycast(ray, out hit, shotGunRange))
                {
                    Debug.Log("Collided at " + hit.collider.gameObject.name);
                    if (hit.transform.CompareTag("Enemy"))
                    {

                        Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
                        hit.rigidbody.AddForce(hit.transform.forward * 100);
                        Instantiate(BloodSplat, hit.point, Quaternion.identity);
                        if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().patrolState || hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().alertState)
                        {
                            hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                        }

                        hit.collider.gameObject.SendMessage("addDamage", shotGunDamage, SendMessageOptions.DontRequireReceiver);
                    }


                }
            }

                    isShot = false;
               
               
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
        yield return new WaitForSeconds(0.1f);
        Debug.Log("yeet");



    }

}