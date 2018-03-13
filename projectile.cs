using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {
    GameObject prefab;
    public GameObject launchSite;
    public GameObject explosionParticles;
    public GameObject turretDeadFire;
	// Use this for initialization
	void Start () {
        prefab = Resources.Load("projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            Debug.Log("Hit space button");
            GameObject projectile = Instantiate(prefab) as GameObject;
            gameObject.GetComponent<AudioSource>().Play();
            projectile.transform.position = launchSite.transform.position + launchSite.transform.forward; //set correct diretion to shoot
            Rigidbody rb = projectile.GetComponent<Rigidbody>();//sets speed
            rb.velocity = launchSite.transform.forward * 40;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit something!");
        if(collision.collider.tag == "Projectile")
        {
            Debug.Log("We were hit!");
            if (!explosionParticles.GetComponent<ParticleSystem>().isPlaying)
            {
                Debug.Log("Playing effect!");
                explosionParticles.GetComponent<ParticleSystem>().Play();
                StartCoroutine(waiting());
                Debug.Log("6 seconds has passed!");

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

}
