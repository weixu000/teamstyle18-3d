using UnityEngine;

public class TankControl : InvasiveControl
{
    public Transform turret;
    public GameObject fire;

    public override void Skill1(int target_id)
    {
        turret.rotation = Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - transform.position);
        fire.GetComponent<XLine>().target = GameObject.Find(target_id.ToString());
        fire.SetActive(true);
        base.Skill1(target_id);
    }
}