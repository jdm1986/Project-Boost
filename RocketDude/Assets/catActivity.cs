using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catActivity : MonoBehaviour {

    Vector3 velocity = new Vector3(-11.72f, 37.8f, 0.0f);
    float floorHeight = 17.8f;
    float sleepThreshold = 0.05f;
    float gravity = -9.8f;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(-11.72f, 17.8f, -12.63f);
	}
	
	// Update is called once per frame
	void Update () {

        

    }
}
