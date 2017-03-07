using UnityEngine;

public class SoldierControl : InvasiveControl
{
    Animator anim, weapon;
    public GameObject fire1, fire2;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        if (GetComponentsInChildren<Animator>().Length > 1)
        {
            weapon = GetComponentsInChildren<Animator>()[1];
        }

        if (transform.FindChild("Line") != null)
        {
            fire1 = transform.FindChild("Line").gameObject;
        }
    }

    protected override void Die()
    {
        anim.SetTrigger("Die");
        if (weapon != null)
        {
            weapon.SetTrigger("Die");
        }
        base.Die();
    }

    public override void Skill1(int target_id)
    {
        transform.rotation = Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - transform.position);
        //rb.MoveRotation(Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - transform.position));

        if(fire1 != null)
        {
            fire1.GetComponent<XLine>().target = GameObject.Find(target_id.ToString());
            fire1.SetActive(true);
        }

        anim.SetTrigger("Shoot");
        if (weapon != null)
        {
            weapon.SetTrigger("Shoot");
        }
        base.Skill1(target_id);
    }

    public override void Skill2(Position pos)
    {
        base.Skill2(pos);
    }

    protected override void Walk()
    {
        anim.SetBool("IsWalking", true);
        if (weapon != null)
        {
            weapon.SetBool("IsWalking", true);
        }

        base.Walk();
    }

    protected override void Stop()
    {
        base.Stop();
        if (weapon != null)
        {
            weapon.SetBool("IsWalking", false);
        }
        anim.SetBool("IsWalking", false);
    }
}
