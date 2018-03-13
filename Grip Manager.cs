using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripManager : MonoBehaviour {

    public Rigidbody Body;

    public LeftClimb left;
    public RightClimb right;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate() //Use FixedUpdate for Rigidbodies
    {
        var lDevice = SteamVR_Controller.Input((int)left.controller.index); //magic (Gets controller ID for input use)
        var rDevice = SteamVR_Controller.Input((int)right.controller.index);

        bool isGripped = left.canGrip || right.canGrip;

        if (isGripped)
        {
            if (left.canGrip && lDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) //If can grip and button pressed for input
            {
                Body.useGravity = false;
                Body.isKinematic = true;
                Body.transform.position += left.prevPos - left.transform.localPosition; //Global position of body is equal to delta change of controller movement
            }
            else if (left.canGrip && lDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Body.useGravity = true;
                Body.isKinematic = false;
                Body.velocity = (left.prevPos - left.transform.localPosition) / Time.deltaTime;
            }
            if (right.canGrip && rDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) //If can grip and button pressed for input
            {
                Body.useGravity = false;
                Body.isKinematic = true;
                Body.transform.position += right.prevPos - right.transform.localPosition; //Global position of body is equal to delta change of controller movement
            }
            else if (right.canGrip && rDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Body.useGravity = true;
                Body.isKinematic = false;
                Body.velocity = (right.prevPos - right.transform.localPosition) / Time.deltaTime;
            }
        }
        else
        {
            Body.useGravity = true;
            Body.isKinematic = false;
        }
        left.prevPos = left.transform.localPosition;
        right.prevPos = right.transform.localPosition;
    }
}
