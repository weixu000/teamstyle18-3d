using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationFreezer : MonoBehaviour {

    Quaternion t;
	void Start () {
 
	}
	
	void Update () {
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
	}
}
