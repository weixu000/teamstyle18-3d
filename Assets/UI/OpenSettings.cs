using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour {
    public GameObject prefab;

    bool opened = false;
    GameObject settings;

    void Awake()
    {
        settings = Instantiate(prefab, Camera.main.transform, false);
        settings.SetActive(false);
    }

    public void open()
    {
        if (opened)
        {
            settings.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
        }
        opened = !opened;
    }
}
