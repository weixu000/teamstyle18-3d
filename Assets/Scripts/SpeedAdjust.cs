using UnityEngine;

public class SpeedAdjust : MonoBehaviour
{
    public float maxSpeed = 2;
    UISlider speedSlider;
    UILabel speedLabel;

    void Awake()
    {
        speedSlider = GetComponent<UISlider>();
        speedLabel = GetComponentInChildren<UILabel>();
    }

    void Update()
    {
        Time.timeScale = speedSlider.value * maxSpeed;
        speedLabel.text = ((int)(speedSlider.value * 100)).ToString() + "%";
    }
}
