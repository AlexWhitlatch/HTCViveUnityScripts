using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 100f;
    public float range = 100f;
    public GameObject bulletExit;
    public GunMagazine gun_magazine;
    public GameObject impactEffect;
    public ParticleSystem muzzleFlashEffect;
    public GameObject gunMag; //this might need to be removed more testing needed
    public GunMagazine gunMagVar;
    public GameObject casing_EjectPoint;
    public GameObject EjectedbulletCasings;
    private GameObject bulletCasings;
    public AudioSource reloadSfx;
    public AudioSource magEjectSfx;
    //public AudioClip fireWeaponSfx;
    public AudioSource fireWeaponSfxtest;
   // private AudioSource fireWeapon;
    private bool disableBulletHeightRenderer = false;
    private Transform cartridgeHeight;
    private bool chamberedRound = false;

    void Start()
    {
        //fireWeapon = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void Shoot()
    {
        Debug.Log("SHOOTING GUNS YAY!");
        RaycastHit hit;
        fireWeaponSfxtest.PlayOneShot(fireWeaponSfxtest.clip, 1f);
        //fireWeapon.PlayOneShot(fireWeaponSfx, 1f);
        bulletCasings = Instantiate(EjectedbulletCasings, casing_EjectPoint.transform.position, casing_EjectPoint.transform.rotation);
        bulletCasings.GetComponent<Rigidbody>().AddForce(casing_EjectPoint.transform.forward * 250);
        bulletCasings = null;
        //Instantiate(muzzleFlashEffect, bulletExit.transform.position, bulletExit.transform.rotation);
        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.Play();
        }
        else
        {
            Debug.Log("Muzzle Fire Particle Flash is null");
        }
           if (Physics.Raycast(bulletExit.transform.position, bulletExit.transform.forward, out hit))
            {
            Debug.Log("We've shot the " + hit.transform.name);

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            if(hit.transform.GetComponent<Health>() != null)
            {
                Debug.Log("Found HP on target - Target Taking Damage");
                hit.transform.GetComponent<Health>().TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition(transform.forward * 5000, hit.point); //adds force and pushes objects back when hit
            }
        }
    }

    public void CanShoot()
    {
        if (chamberedRound == true)
        {
            Shoot();
            chamberedRound = false;
            if (gun_magazine != null && gun_magazine.AmmunitionCheck(gun_magazine.CURRENT_MAG_SIZE))
            { 
            gun_magazine.CURRENT_MAG_SIZE = gun_magazine.CURRENT_MAG_SIZE - 1;
            gun_magazine.cartridgeHeight.gameObject.SetActive(true);
                chamberedRound = true;
            }
            
            if (gun_magazine.CURRENT_MAG_SIZE == 0)
            {
                gun_magazine.cartridgeHeight.gameObject.SetActive(false);

            }
            disableBulletHeightRenderer = false;
        }
        else
        {
            Debug.Log("Ammunition Check Failed - OUT OF AMMO!");
            //disableBulletHeightRenderer = true;
            chamberedRound = false;
            if(gun_magazine != null)
            {
                magEjectSfx.Play();
            }
            EjectMagazine();
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("PistolMagazine") && gun_magazine == null){
            //could add in code to prevent certain mags from certain guns
            other.transform.parent = null;
            AttachMagazine(other);
        }
    }

    public void AttachMagazine(Collider magazine)
    {
        gunMag = magazine.gameObject;
        //gunMagVar = magazine.GetComponent<GunMagazine>();
        gunMag.gameObject.tag = "NonGrabable";
        gunMag.GetComponent<Transform>().parent = null; // setting null to detach from controller
        gun_magazine = magazine.GetComponent<GunMagazine>();
        gunMag.GetComponent<Transform>().parent = GameObject.FindGameObjectWithTag("Gun").transform; //this needs to change to have mag go to correct gun if multiple guns exist
        gunMag.GetComponent<Transform>().GetComponent<Transform>().localPosition = new Vector3(10.193f,-0.404f, -6.474f);
        gunMag.GetComponent<Transform>().GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
        gunMag.GetComponent<BoxCollider>().enabled = false;
        gunMag.GetComponent<Rigidbody>().isKinematic = true;
        reloadSfx.Play();
        if(gun_magazine.AmmunitionCheck(gun_magazine.CURRENT_MAG_SIZE) && chamberedRound == false)
        {
            chamberedRound = true;
            gun_magazine.CURRENT_MAG_SIZE = gun_magazine.CURRENT_MAG_SIZE - 1;
            if(gun_magazine.CURRENT_MAG_SIZE > 0)
            {
                gun_magazine.cartridgeHeight.gameObject.SetActive(true);
            }
            else
            {
                gun_magazine.cartridgeHeight.gameObject.SetActive(false);
            }
                
        }

    }

    public void EjectMagazine()
    {
        if (disableBulletHeightRenderer)
        {
            Debug.Log("Removing Child Render");
            gun_magazine.cartridgeHeight.gameObject.SetActive(false);
        }
        if(gun_magazine != null)
        {
            magEjectSfx.Play();
        }
        //gunMagVar = null;
        gunMag.gameObject.tag = "Grabable";
        gunMag.GetComponent<Transform>().parent = null; // setting null to detach from Gun
        gun_magazine = null;
        gunMag.GetComponent<BoxCollider>().enabled = true;
        gunMag.GetComponent<Rigidbody>().isKinematic = false;
        gunMag.GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine(waiting(gunMag)); // waiting to set tag back to normal for use

    }

    private IEnumerator waiting(GameObject gunMag)
    {
        yield return new WaitForSeconds(1);
        gunMag.gameObject.tag = "PistolMagazine";
      
    }
}
