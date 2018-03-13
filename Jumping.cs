using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{

    public Rigidbody Body;

    public SteamVR_TrackedObject rightController;
    public SteamVR_TrackedObject leftController;

    [HideInInspector]
    public Vector3 leftPrevPos; //references controllers previous position
    public Vector3 rightPrevPos; //references controllers previous position


    [HideInInspector]
    public bool onGround;

    // Use this for initialization
    void Start()
    {
        leftPrevPos = leftController.transform.localPosition;
        rightPrevPos = rightController.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate() //Use FixedUpdate for Rigidbodies
    {
        var leftDevice = SteamVR_Controller.Input((int)leftController.index); //magic (Gets controller ID for input use)
        var rightDevice = SteamVR_Controller.Input((int)rightController.index); //magic (Gets controller ID for input use)
        if (leftDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && rightDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && onGround)
        {
            Debug.Log("CAN JUMP");
                Body.useGravity = false;
                Body.isKinematic = true;
                Body.transform.position += leftPrevPos - leftController.transform.localPosition; //Global position of body is equal to delta change of controller movement
        }
        
        else if (leftDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) || rightDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Body.useGravity = true;
            Body.isKinematic = false;
            onGround = false;
            Body.velocity = (leftPrevPos - leftController.transform.localPosition) / Time.deltaTime;
        }

        else
        {
            Body.useGravity = true;
            Body.isKinematic = false;
            onGround = false;
        }
        leftPrevPos = leftController.transform.localPosition;
    }

    void OnCollisionEnter()
    {
        onGround = true;
    }

     void OnCollisionExit()
    {
       // onGround = false;
    }


}
