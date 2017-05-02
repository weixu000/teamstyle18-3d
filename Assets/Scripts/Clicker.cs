using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour {

    RaycastHit hit;
    Ray ray;
    public GameObject hitObject;
    public GameObject selectObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            if (hit.collider.tag == "selectable")
            {
                hitObject = hit.collider.gameObject;
                if (Input.GetMouseButtonDown(0))
                    selectObject = hit.collider.gameObject;
            }
            else
            {
                hitObject = null;
            }
    }
}
