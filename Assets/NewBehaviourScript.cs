using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public GameObject bigmap;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M))
        {
            if (bigmap.activeSelf)
                bigmap.SetActive(false);
            else
                bigmap.SetActive(true);
        }
	}
}
