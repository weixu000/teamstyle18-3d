using System.Collections;
using UnityEngine;

public class PlaneControl : HackedControl
{
    public GameObject fire1, fire2;

    public override void Skill1(int target_id)
    {
        base.Skill1(target_id);
        fire1.GetComponent<AbstractLine>().Fire(GameObject.Find(target_id.ToString()));
        fireDone = false;
        StartCoroutine(WaitForFireDone(fire1));
    }

    public override void Skill2(Position pos1, Position pos2)
    {
        base.Skill2(pos1, pos2);
        if (fire2 != null)
        {
            fire2.GetComponent<AbstractLine>().Fire(pos1);
            fire2.GetComponent<AbstractLine>().Fire(pos2);
            fireDone = false;
            StartCoroutine(WaitForFireDone(fire2));
        }
    }
}