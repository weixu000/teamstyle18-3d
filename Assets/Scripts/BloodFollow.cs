using UnityEngine;

public class BloodFollow : MonoBehaviour {
	void Update () {
        transform.rotation = Camera.main.transform.rotation;
	}
}
