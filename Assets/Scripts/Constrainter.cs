using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrainter : MonoBehaviour {

    public Transform cam;
    Vector3 rot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = cam.position;
        rot = cam.rotation.eulerAngles;
        rot.x = 90;
        rot.y -= 90;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
	}
}
