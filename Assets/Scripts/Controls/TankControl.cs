using System.Collections;
using UnityEngine;

public class TankControl : HackedControl
{
    public Transform turret;
    public GameObject fire1,fire2;

    public override void Skill1(int target_id)
    {
        base.Skill1(target_id);
        RotateTurret(GameObject.Find(target_id.ToString()).transform.position);
        fire1.GetComponent<AbstractLine>().Fire(GameObject.Find(target_id.ToString()));
        fireDone = false;
        StartCoroutine(WaitForFireDone(fire1));
    }

    public override void Skill2(Position pos1, Position pos2)
    {
        base.Skill2(pos1, pos2);
        RotateTurret(pos1.Random(turret.position.y));
        if (fire2)
        {
            fire2.GetComponent<AbstractLine>().Fire(pos1);
            fireDone = false;
            StartCoroutine(WaitForFireDone(fire2));
        }
    }
    void RotateTurret(Vector3 target)
    {
        var direction = target - turret.position;
        direction.y = 0;
        turret.rotation = Quaternion.LookRotation(direction);
    }
}