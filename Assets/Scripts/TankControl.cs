using UnityEngine;

public class TankControl : InvasiveControl
{
    public Transform turret;
    public GameObject fire;

    public override void Fire(int target_id)
    {
        turret.rotation = Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - transform.position);
        fire.SetActive(true);
        base.Fire(target_id);
    }
}
