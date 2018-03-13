using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchetBlade : MonoBehaviour
{
    FixedJoint joint;
    public GameObject parent;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hatchet Blade Hit something");
        Debug.Log("Attempting to add Fixed Joint");
        if (collision.GetComponent<Rigidbody>())
        {
            joint = parent.AddComponent<FixedJoint>();
            joint.connectedBody = collision.GetComponent<Rigidbody>();
            joint.breakForce = 80000;
            joint.breakTorque = 80000;
        }
        else
        {
            Debug.Log("No rigidbody found for FixedJoint!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Removing Joint!");
        Destroy(joint);
    }

}
