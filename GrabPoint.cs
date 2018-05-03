using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour {
    
    public Transform SetPosition()
    {
        Vector3 position;
        Quaternion rotation;
        Transform retVal = gameObject.transform;
        Debug.Log("Entered SetPosition Function through Touched Object");

        switch (this.transform.tag)
        {
            case "Flashlight":
                position = new Vector3(.004f, -0.086f, .056f);
                rotation =  Quaternion.Euler(-4.924f, 179.928f, 174.627f);
                retVal.position = position;
                retVal.rotation = rotation;
                Debug.Log("Found Flashlight");
                break;
            case "Axe":
                position = new Vector3(0.0012f, 0.0008f, -0.1831f);
                rotation = new Quaternion(49.946f, 6.623f, 4.964f, 0f);
                retVal.position = position;
                retVal.rotation = rotation;
                Debug.Log("Found Axe");
                break;
            case "Gun":
                position = new Vector3(-0.9656595f, -0.647679f, 0.2662789f);
                rotation = Quaternion.Euler(57.507f, 7.926001f, 10.002f);
                retVal.position = position;
                retVal.rotation = rotation;
                Debug.Log("Found Pistol");
                break;
        }
        if(retVal == null)
        {
            Debug.Log("RetVal is NULL");
        }
        return retVal;
    }

}
