using UnityEngine;

public class RoundNumber : MonoBehaviour
{
    UILabel roundLabel;
    NetCom netcom;

    void Awake()
    {
        roundLabel = GetComponent<UILabel>();
        netcom = GameObject.FindWithTag("GameController").GetComponent<NetCom>();
    }

    void Update()
    {
        roundLabel.text = "Round:" + netcom.round.ToString();
    }
}