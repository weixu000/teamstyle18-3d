using UnityEngine;

public class ShellDstroy : MonoBehaviour
{
    NetCom com;
    public int started;

	void Awake () {
        com = GameObject.Find("GameController").GetComponent<NetCom>();
        started = com.round;
	}
	
	// Update is called once per frame
	void Update () {
		if(com.round - started >= 10)
        {
            Destroy(gameObject);
        }
	}
}
