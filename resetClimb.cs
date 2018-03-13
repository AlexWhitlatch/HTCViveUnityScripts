using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetClimb : MonoBehaviour {
    bool reset;
    public Rigidbody body;
    Vector3 resetPos;

	// Use this for initialization
	void Start () {
        reset = false;
        resetPos.Set(-5, 0, 2);
        //this.gameObject.transform.position = resetPos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(reset)
        {
            body.transform.position = resetPos;
        }
        else
        {
            reset = false;
        }
	}

     void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision Detected");
        if (collision.gameObject.tag.Equals("ResetOnCol"))
        {
            reset = true;
            Debug.Log("Collision was detected with proper tag");
        }
    }

    private void OnTriggerExit()
    {
        reset = false;
    }
}
