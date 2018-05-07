using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpiderFollowPlayer : MonoBehaviour
{
    public GameObject VRPlayer;
    public float playerDistance; // distance away from player
    public float attackDistance = -1; //how close to do attack animation
    public GameObject spider;
    public float travelSpeed = 0.05f;
    public LayerMask mask;
    public RaycastHit shot;
    public float waitTime;
    public GameObject sb;
     bool canDamage = false;
    

    private void Start()
    {
        attackDistance = -1;
        VRPlayer = GameObject.Find("[CameraRig]").transform.GetChild(2).gameObject; //attempts to find the "head" child of the VR Prefab
        spider.GetComponent<Animation>().Play("jump");
        StartCoroutine(waiting());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<Health>().CheckHealth())
        {
            transform.LookAt(VRPlayer.transform);
            GetAlignment();
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
            {
                playerDistance = shot.distance;
                if (playerDistance >= attackDistance)
                {
                    //travelSpeed = 0.05f;
                    spider.GetComponent<Animation>().Play("run");
                    if (!spider.GetComponent<AudioSource>().isPlaying)
                    {
                        spider.GetComponent<AudioSource>().Play();
                    }
                    transform.position = Vector3.MoveTowards(transform.position, VRPlayer.transform.position, travelSpeed);
                }
                else
                {
                    spider.GetComponent<Animation>().Stop("run");
                    travelSpeed = 0;
                    spider.GetComponent<AudioSource>().Stop();
                    spider.GetComponent<Animation>().Play("attack1");
                    if (canDamage)
                    {
                        
                        Debug.Log("Doing damage to player");
                    }

                }
            }
        }
        else
        {
            spider.GetComponent<AudioSource>().Stop();
            spider.GetComponent<Animation>().Play("death1");
            spider.AddComponent<Rigidbody>();
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
            targetLocation += new Vector3(0, transform.localScale.y / 5f, 0);
            /*
             * Move the object to the target location.
             */
            transform.position = targetLocation;
        }
    }


    private IEnumerator waiting()
    {
        yield return new WaitForSeconds(waitTime);
        attackDistance = 2;

    }




    private bool CheckHealthStatus()
    {

        return false;
    }

    public void setTravelSpeed(float number)
    {
        travelSpeed = number;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>().isPlayer)
        {
            canDamage = true;
        }
    }

}
