using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoPresenter : MonoBehaviour {

    Clicker clicker;
    UnitControl uControl;
    UILabel uType;
    UILabel uName;

	// Use this for initialization
	void Start () {
        clicker = FindObjectOfType<Clicker>();
	}
	
	// Update is called once per frame
	void Update () {
        if (clicker.selectObject != null)
        {
            uControl = clicker.selectObject.GetComponent<UnitControl>();
            // uType = uControl.
        }
	}
}
