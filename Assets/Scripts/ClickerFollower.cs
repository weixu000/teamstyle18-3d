using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerFollower : MonoBehaviour {

    public Clicker clicker;
    public float distance = 10;
    public float height = 10;
    public float followHeight = 3;
    bool followed;
 
	// Use this for initialization
	void Start () {
        clicker = FindObjectOfType<Clicker>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (followed)
            {
                followed = false;
            }
            else
            {
                followed = true;
            }
        }

        if (followed == true)
        {
            if (clicker.selectObject == null)
            {
                followed = false;
            }
            else
            {
                Vector3 pos = new Vector3(
                    clicker.selectObject.transform.position.x - distance * clicker.selectObject.transform.forward.x,
                    height,
                    clicker.selectObject.transform.position.z - distance * clicker.selectObject.transform.forward.z
                    );
                Vector3 uppos = new Vector3(0, followHeight, 0);
                transform.position = pos;

                transform.LookAt(clicker.selectObject.transform.position + uppos);
            }
        }

	}
}
