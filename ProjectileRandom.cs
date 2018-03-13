using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRandom : MonoBehaviour {
    GameObject prefab;
    public GameObject launchSite;
    public GameObject explosionParticles;
    public GameObject turretDeadFire;
    public GameObject warning;
    bool isDead = false;
    bool isFiring = false;
    int MAX_RAND = 20;
    int MIN_RAND = 6;
    int nextShotTime;
    int stopInt = 1;
	// Use this for initialization
	void Start () {
        prefab = Resources.Load("projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isFiring)
        {
            Debug.Log("NOT FIRING AND StopInt is " + stopInt);
            if (stopInt != 0)
            {
                isFiring = true;
                
                Debug.Log(isDead + "Is bool value");
                nextShotTime = Random.Range(MIN_RAND, MAX_RAND);
                Debug.Log(nextShotTime);
                StartCoroutine(shootProjectile(nextShotTime));
            }
           
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit something!");
        if(collision.collider.tag == "Projectile")
        {
            stopInt = 0;
            isDead = true;
            Debug.Log("We were hit!");
            if (!explosionParticles.GetComponent<ParticleSystem>().isPlaying)
            {
                Debug.Log("Playing effect!");
                explosionParticles.GetComponent<ParticleSystem>().Play();
                StartCoroutine(waiting());
                Debug.Log("6 seconds has passed!");
                Debug.Log("StopINT changed to 0!");
            }
            

        }
    }
    private IEnumerator waiting()
    {
        yield return new WaitForSeconds(1);
        if (!turretDeadFire.GetComponent<ParticleSystem>().isPlaying)
        {
            turretDeadFire.GetComponent<ParticleSystem>().Play();
        }
    }

    private IEnumerator shootProjectile(int nextShotTime)
    {
        yield return new WaitForSeconds(nextShotTime - 4);
        if (!warning.GetComponent<ParticleSystem>().isPlaying && !isDead)
        {
            warning.GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(4);
        if (!isDead) {
            Debug.Log(isDead + "Is dead value");
            GameObject projectile = Instantiate(prefab) as GameObject;
            gameObject.GetComponent<AudioSource>().Play();
            projectile.transform.position = launchSite.transform.position + launchSite.transform.forward; //set correct diretion to shoot
            Rigidbody rb = projectile.GetComponent<Rigidbody>();//sets speed
            rb.velocity = launchSite.transform.forward * 40;
            isFiring = false;
        }
    }

}
