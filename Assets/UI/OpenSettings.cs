using UnityEngine;

public class OpenSettings : MonoBehaviour {
    public GameObject settings;

    public void open()
    {
        if (settings.activeSelf)
        {
            settings.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
        }
    }
}
