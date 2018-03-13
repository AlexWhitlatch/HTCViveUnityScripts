using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour {
    
    public Transform SetPosition()
    {
        Vector3 position;
        Quaternion rotation;
        Transform retVal = this.transform;
        Debug.Log("Entered SetPosition Function through Touched Object");

        switch (this.transform.tag)
        {
            case "Flashlight":
                position = new Vector3(0.0012f, 0.0008f, -0.1831f);
                rotation = new Quaternion(0.0012f, 0.0008f, 0.1831f, 2f);
                retVal.position = position;
                retVal.rotation = rotation;
                Debug.Log("Found Flashlight");
                break;
            case "Axe":
                position = new Vector3(0.0012f, 0.0008f, -0.1831f);
                rotation = new Quaternion(0.0012f, 0.0008f, 0.1831f, 2f);
                retVal.position = position;
                retVal.rotation = rotation;
                Debug.Log("Found Axe");
                break;
        }
        if(retVal == null)
        {
            Debug.Log("RetVal is NULL");
        }
        return retVal;
    }

}
