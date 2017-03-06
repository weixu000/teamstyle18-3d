using UnityEngine;

public class OpenSettings : MonoBehaviour {
    public GameObject settings;

    public void open()
    {
        if (settings.activeSelf)
        {
            settings.SetActive(false);
            Camera.main.GetComponent<FreeCamera>().enabled = true;
        }
        else
        {
            settings.SetActive(true);
            Camera.main.GetComponent<FreeCamera>().enabled = false;
        }
    }
}
