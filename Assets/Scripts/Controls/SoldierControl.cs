using System.Collections;
using UnityEngine;

public class SoldierControl : InvasiveControl
{
    public Animator body, weapon;
    public GameObject fire1;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Die()
    {
        body.SetTrigger("Die");
        if (weapon != null)
        {
            weapon.SetTrigger("Die");
        }
        base.Die();
    }

    public override void Skill1(int target_id)
    {
        base.Skill1(target_id);

        transform.rotation = Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - transform.position);

        if(fire1 != null)
        {
            fire1.GetComponent<AbstractLine>().Fire(GameObject.Find(target_id.ToString()));
            fireDone = false;
            StartCoroutine(WaitForFireDone(fire1));
        }

        body.SetTrigger("Shoot");
        if (weapon != null)
        {
            weapon.SetTrigger("Shoot");
        }
    }

    protected override void Walk()
    {
        body.SetBool("IsWalking", true);
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
        body.SetBool("IsWalking", false);
    }
}
