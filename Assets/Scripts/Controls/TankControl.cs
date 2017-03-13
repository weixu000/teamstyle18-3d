using UnityEngine;

public class TankControl : HackedControl
{
    public Transform turret;
    public GameObject fire1,fire2;

    public override void Skill1(int target_id)
    {
        turret.rotation = Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - turret.position);
        fire1.GetComponent<FireLine>().Fire(GameObject.Find(target_id.ToString()));

        base.Skill1(target_id);
    }

    public override void Skill2(Position pos)
    {
        turret.rotation = Quaternion.LookRotation(pos.Random(turret.position.y) - turret.position);
        if (fire2)
        {
            fire2.GetComponent<FireLine>().Fire(pos);
        }

        base.Skill2(pos);
    }
}