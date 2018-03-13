using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClimb : MonoBehaviour
{

    public Rigidbody Body;

    public SteamVR_TrackedObject controller;

    [HideInInspector]
    public Vector3 prevPos; //references controllers previous position

    [HideInInspector]
    public bool canGrip;

    // Use this for initialization
    void Start()
    {
        prevPos = controller.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate() //Use FixedUpdate for Rigidbodies
    {
        var device = SteamVR_Controller.Input((int)controller.index); //magic (Gets controller ID for input use)
        if (canGrip && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) //If can grip and button pressed for input
        {
            Body.useGravity = false;
            Body.isKinematic = true;
            Body.transform.position += prevPos - controller.transform.localPosition; //Global position of body is equal to delta change of controller movement
        }
        else if(canGrip && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Body.useGravity = true;
            Body.isKinematic = false;
            Body.velocity = (prevPos - controller.transform.localPosition) / Time.deltaTime;
        }

        else
        {
            Body.useGravity = true;
            Body.isKinematic = false;
        }
        prevPos = controller.transform.localPosition;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Climbable"))
            canGrip = true;
    }

    private void OnTriggerExit()
    {
        canGrip = false;
    }


}
