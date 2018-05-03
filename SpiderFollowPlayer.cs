using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFollowPlayer : MonoBehaviour {
    public GameObject VRPlayer;
    public float playerDistance;
    public float attackDistance = 1;
    public GameObject spider;
    public float travelSpeed;
    public float speed;
    public LayerMask mask;
    public RaycastHit shot;

    private void Start()
    {
        StartCoroutine(waiting()); //wait to set attack distance to avoid raycast calculation issues mid-way through terrain
    }

    // Update is called once per frame
    void Update () {
        if (!this.GetComponent<Health>().CheckHealth())
        {
            transform.LookAt(VRPlayer.transform);
            GetAlignment();
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
            {
                playerDistance = shot.distance;
                if (playerDistance >= attackDistance)
                {
                    travelSpeed = 0.02f;
                    spider.GetComponent<Animation>().Play("Walk");
                    transform.position = Vector3.MoveTowards(transform.position, VRPlayer.transform.position, travelSpeed);
                }
                else
                {
                    travelSpeed = 0;
                    spider.GetComponent<Animation>().Play("Attack");

                }
            }
        }
        else
        {
            spider.GetComponent<Animation>().Play("Death");
        }
	}


    void GetAlignmentNormal()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 10, mask);
        Vector3 newUp = hit.normal;
        transform.up = newUp;
        
    }



    void GetAlignment()
    {

        float distance = 100f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
        {
            /*
             * Set the target location to the location of the hit.
             */
            Vector3 targetLocation = hit.point;
            /*
             * Modify the target location so that the object is being perfectly aligned with the ground (if it's flat).
             */
            targetLocation += new Vector3(0, transform.localScale.y / 2, 0);
            /*
             * Move the object to the target location.
             */
            transform.position = targetLocation;
        }
    }


    private IEnumerator waiting()
    {
        yield return new WaitForSeconds(145);
        attackDistance = 33;

    }


}
