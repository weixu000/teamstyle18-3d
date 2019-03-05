using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{

    object[] gameObjects;
    NetCom netcom;

    public UILabel[] redLabel;
    public UILabel[] blueLabel;
    public int[] redNum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] blueNum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    int round;

    void Start()
    {
        netcom = GameObject.FindWithTag("GameController").GetComponent<NetCom>();
        round = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (netcom.round != round)
        {
            round = netcom.round;
            gameObjects = GameObject.FindObjectsOfType(typeof(GameObject));
            redNum = new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            blueNum = new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (GameObject go in gameObjects)
            {
                UnitControl uc;
                uc = go.GetComponent<UnitControl>();
                if (uc != null)
                {
                    if (uc.flag == 0)
                        redNum[(int)uc.unit_name]++;
                    else if (uc.flag == 1)
                        blueNum[(int)uc.unit_name]++;
                }
            }

            for (int i = 1; i <= 21; i++)
            {
                redLabel[i].text = "" + redNum[i];
                blueLabel[i].text = "" + blueNum[i];
            }
        }
    }
}
