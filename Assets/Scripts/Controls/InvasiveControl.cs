using UnityEngine;

public class InvasiveControl : DestroyableControl
{
    public float moveSpeed = 50.0f, rotateSpeed = 150.0f;
    [HideInInspector]
    public bool walking = false;

    protected Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    public override void SetState(UnitState state)
    {
        Move(state.position);
        base.SetState(state);
    }

    protected virtual void FixedUpdate()
    {
        var cur = transform.position;
        cur.y = 0;
        if ((transform.position - targetPosition).sqrMagnitude >= 0.1)
        {
            Walk();
        }else
        {
            Stop();
        }
    }

    public void Move(Position tar)
    {
        if (position == tar) return;
        targetPosition = tar.Random();
        position = tar;
    }

    public virtual void Skill1(int target_id)
    {
        Debug.Log(name + "attack" + target_id);
    }

    public virtual void Skill2(Position pos)
    {
        Debug.Log(name + "bombed");
    }

    protected virtual void Walk()
    {
        walking = true;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (targetPosition - transform.position != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), rotateSpeed * Time.deltaTime);
        }
    }

    protected virtual void Stop()
    {
        walking = false;
    }
}
