using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    UIToggle uitoggle;
    public AudioListener listener;

	// Use this for initialization
	void Start () {
        uitoggle = GetComponent<UIToggle>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change()
    {
        if (uitoggle.value == false)
            listener.enabled = false;
        else
            listener.enabled = true;
    }
}
