using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{

    private GameObject heldObj;

    public SteamVR_TrackedObject controller;

    private Vector3 prevPos;
    private Transform setGrabPoint;
    private GrabPoint gp;
    private Flashlight flashlight;
    private Gun gun;

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
        if (canGrab && device.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) //If can grip and button pressed for input
        {
            canGrab = false;
            isGrabbing = true;
            Debug.Log("gp is " + gp);
            if (gp != null)
            {
                Debug.Log("GRAB POINT HAS BEEN DETECTED AND ATTEMPTING TO GRAB AT SPECIFIC LOCATION ON SPECIFIC OBJECT");
                SettingGrabPoint();
            }
            else
            {
                //Debug.Log("gp was null");
                Debug.Log("Standard Grab");
                heldObj.transform.parent = this.transform;
                heldObj.GetComponent<Rigidbody>().isKinematic = true;
                heldObj.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else if (!canGrab && device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("TRYING TO DROP THE OBJECT!!!");
            isGrabbing = false;
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
            heldObj.GetComponent<Rigidbody>().useGravity = true;
            heldObj.GetComponent<Rigidbody>().velocity = (controller.transform.localPosition - prevPos) * 1.3f / Time.deltaTime;
            heldObj.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity * 1f;
            heldObj.transform.parent = null;
            heldObj = null;
            this.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else
        {
            // isGrabbing = false;  - do nothing for now
            Debug.Log("Nothing happening in here");
            Debug.Log("Is Grabbing is " + isGrabbing);
            Debug.Log("CanGrab is " + canGrab);


            if (flashlight != null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("Turning on flashlight!");
                flashlight.turnOnLight();
            }
            else if (gun != null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("Shot the gun!");
                StartCoroutine(LongVibration(.3f, 100, device));
                gun.CanShoot();
            }
            else if (gun != null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Debug.Log("Shot the gun!");
                gun.EjectMagazine();
            }


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
            gun = heldObj.GetComponent<Gun>();
            if (gp != null)
            {
                Debug.Log("Found Grab Point script");
            }
            if (flashlight != null)
            {
                Debug.Log("Found Flashlight script");
            }
            if (gun != null)
            {
                Debug.Log("Found Gun script");
            }
        }
    }

    private void OnTriggerExit()
    {
        canGrab = false;
    }

    private void SettingGrabPoint()
    {
        Debug.Log("gp not null");
        Debug.Log("Going in method");
        setGrabPoint = gp.SetPosition();
        if (setGrabPoint == null)
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
        //heldObj.GetComponent<Transform>().localPosition = new Vector3(-0.9656595f, -0.647679f, 0.2662789f);  //this is for the pistol
        //heldObj.GetComponent<Transform>().localRotation = Quaternion.Euler(57.507f, 7.926001f, 10.002f); // this is for the pistol


        //heldObj.GetComponent<Transform>().position = controller.GetComponent<Transform>().position;   old code
        // heldObj.GetComponent<Transform>().rotation = controller.GetComponent<Transform>().rotation; old code
        heldObj.GetComponent<Rigidbody>().isKinematic = true;
        heldObj.GetComponent<Rigidbody>().useGravity = false;
        //controller.GetComponent<MeshRenderer>().enabled = false;

    }


    IEnumerator LongVibration(float length, float strength, SteamVR_Controller.Device device)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }


    }
}
