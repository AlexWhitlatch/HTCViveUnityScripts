using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

    private GameObject heldObj;

    public SteamVR_TrackedObject controller;

    private Vector3 prevPos;
    private Transform setGrabPoint;
    private GrabPoint gp;
    private Flashlight flashlight;

    [HideInInspector]
    public bool canGrab;
    public bool isGrabbing;

    // Use this for initialization
    void Start()
    {
        heldObj = null;
        prevPos = controller.transform.localPosition;
        isGrabbing = false;
    }

    // Update is called once per frame
    void FixedUpdate() //Use FixedUpdate for Rigidbodies
    {
        var device = SteamVR_Controller.Input((int)controller.index); //magic (Gets controller ID for input use)
        if (canGrab && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) //If can grip and button pressed for input
        {
            isGrabbing = true;
            if (gp != null)
            {
                Debug.Log("gp not null");
                Debug.Log("Going in method");
                setGrabPoint = gp.SetPosition();
                if(setGrabPoint == null)
                {
                    Debug.Log("Shouldn't see this");
                }
                else
                {
                    Debug.Log("It worked");

                }
                heldObj.transform.parent = this.transform;
                heldObj.GetComponent<Transform>().localPosition = setGrabPoint.position;
                heldObj.GetComponent<Transform>().localRotation = setGrabPoint.rotation;
                heldObj.GetComponent<Rigidbody>().isKinematic = true;
                heldObj.GetComponent<Rigidbody>().useGravity = false;
                //controller.GetComponent<MeshRenderer>().enabled = false;
                if (flashlight != null && device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
                {
                    Debug.Log("Turning on flashlight!");
                    flashlight.turnOnLight();
                }
            }
            else
            {
                Debug.Log("gp was null");
                heldObj.transform.parent = this.transform;
                heldObj.GetComponent<Rigidbody>().isKinematic = true;
                heldObj.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
            heldObj.GetComponent<Rigidbody>().useGravity = true;
            heldObj.GetComponent<Rigidbody>().velocity = (controller.transform.localPosition - prevPos) * 1.3f / Time.deltaTime;
            heldObj.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity * 1f;
            heldObj.transform.parent = null;
            isGrabbing = false;
            heldObj = null;
            this.GetComponentInChildren<MeshRenderer>().enabled = true;
        }

        prevPos = controller.transform.localPosition;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.tag.Equals("NonGrabable") && isGrabbing == false)
        {
            canGrab = true;
            heldObj = collision.gameObject;
            gp = heldObj.GetComponent<GrabPoint>();
            flashlight = heldObj.GetComponent<Flashlight>();
            if(gp != null)
            {
                Debug.Log("Found Grab Point script");
            }
            if (flashlight != null)
            {
                Debug.Log("Found Flashlight script");
            }
        }
    }

    private void OnTriggerExit()
    {
        canGrab = false;
    }
}
