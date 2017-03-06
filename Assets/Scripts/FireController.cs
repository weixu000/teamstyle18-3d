using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject fire4;
    UISlider uislider;

	// Use this for initialization
	void Start () {
        uislider = GetComponent<UISlider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (uislider.value < 0.75 && fire1.activeSelf == false)
            fire1.SetActive(true);
        if (uislider.value < 0.5 && fire2.activeSelf == false)
            fire2.SetActive(true);
        if (uislider.value < 0.25 && fire3.activeSelf == false)
        {
            fire3.SetActive(true);
            fire4.SetActive(true);
        }
	}
}
