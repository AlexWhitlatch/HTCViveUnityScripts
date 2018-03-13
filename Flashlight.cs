using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
    bool lightIsOn = false;
    public Light light;
	// Use this for initialization
	void Start () {
        light.enabled = false;
	}

    public void turnOnLight()
    {
        {
            Debug.Log("Light Button Pressed!");
            if (!lightIsOn)
            {
                light.enabled = true;
                lightIsOn = true;
            }
            else if(lightIsOn)
            {
                light.enabled = false;
                lightIsOn = false;

            }
        }
    }

}
