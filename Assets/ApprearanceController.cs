using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprearanceController : MonoBehaviour {

    public Clicker clicker;
    public GameObject []items;
    public GameObject selectedEffect;

	// Use this for initialization
	void Start () {
        clicker = FindObjectOfType<Clicker>();
	}
	
	// Update is called once per frame
	void Update () {
        if (clicker.selectObject == gameObject)
            foreach(GameObject a in items)
            {
                a.SetActive(true);
                selectedEffect.SetActive(true);
            }
        else if (clicker.hitObject == gameObject)
            foreach (GameObject a in items)
            {
                a.SetActive(true);       
            }
        else
            foreach (GameObject a in items)
            {
                a.SetActive(false);
                selectedEffect.SetActive(false);
            }
    }
}
