using UnityEngine;

public class SpeedAdjust : MonoBehaviour
{
    public float speed = 1.0f;

    void Update()
    {
        Time.timeScale = speed;
    }
}
