using UnityEngine;

public class SoldierControl : InvasiveControl
{
    Animator anim, weapon;
    GameObject line;

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
            line = transform.FindChild("Line").gameObject;
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

    public override void Fire(int target_id)
    {
        rb.MoveRotation(Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - transform.position));

        if(line != null)
        {
            line.SetActive(true);
            line.GetComponent<XLine>().target = GameObject.Find(target_id.ToString());
        }

        anim.SetTrigger("Shoot");
        if (weapon != null)
        {
            weapon.SetTrigger("Shoot");
        }
        base.Fire(target_id);
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
