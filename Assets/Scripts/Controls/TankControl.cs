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

    public override void Skill2(Position pos1, Position pos2)
    {
        turret.rotation = Quaternion.LookRotation(pos1.Random(turret.position.y) - turret.position);
        if (fire2)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var pos = new Position(pos1.x + i, pos1.y + j);
                    fire2.GetComponent<FireLine>().Fire(pos);
                }
            }
        }

        base.Skill2(pos1, pos2);
    }
}