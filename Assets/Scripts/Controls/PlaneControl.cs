using UnityEngine;

public class PlaneControl : HackedControl
{
    public GameObject fire1, fire2;

    public override void Skill1(int target_id)
    {
        var xline = fire1.GetComponent<XLine>();
        if (xline)
        {
            fire1.transform.rotation = Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - fire1.transform.position);
            xline.target = GameObject.Find(target_id.ToString());
            fire1.SetActive(true);
        }
        else
        {
            var bullet = fire1.GetComponent<FireLine>();
            if (bullet)
            {
                bullet.Fire(GameObject.Find(target_id.ToString()));
            }
        }

        base.Skill1(target_id);
    }

    public override void Skill2(Position pos1, Position pos2)
    {
        if (fire2 != null)
        {
            fire2.GetComponent<FireLine>().Fire(pos1);
            fire2.GetComponent<FireLine>().Fire(pos2);
        }

        base.Skill2(pos1, pos2);
    }
}