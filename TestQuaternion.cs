using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuaternion : MonoBehaviour {
    public float movespeed = 5;
    public float gravity = -20;

    public LayerMask mask;

    void Update()
    {
        

        GetAlignment();

    }

    void GetAlignment()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 500, mask);
        Vector3 newUp = hit.normal;

        transform.up = newUp;
    }


}
