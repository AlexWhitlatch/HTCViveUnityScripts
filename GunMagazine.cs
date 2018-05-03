using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMagazine : MonoBehaviour {

    public int MAG_SIZE;
    public int CURRENT_MAG_SIZE;
    public GameObject cartridgeHeight;

	// Use this for initialization
	void Start () {
        CURRENT_MAG_SIZE = MAG_SIZE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool AmmunitionCheck(int CURRENT_MAG_SIZE)
    {
        bool ammoLeft = false;
        if(CURRENT_MAG_SIZE > 0)
        { 
            ammoLeft = true;
           // Debug.Log("Ammo left: " + CURRENT_MAG_SIZE);
            return ammoLeft;
        }
        else
        {
            Debug.Log("No ammunition Left");
            return ammoLeft;
        }
    }
}
