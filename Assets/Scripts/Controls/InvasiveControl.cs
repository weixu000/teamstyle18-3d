using UnityEngine;

public class InvasiveControl : DestroyableControl
{
    public float moveSpeed = 50.0f, rotateSpeed = 150.0f;

    protected Vector3 targetPosition;
    protected Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public override void SetState(UnitState state)
    {
        Move(state.position);
        base.SetState(state);
    }

    protected virtual void FixedUpdate()
    {
        Vector3 cur = rb.position;
        cur.y = 0;
        if ((rb.position - targetPosition).sqrMagnitude >= 0.1)
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
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime));
        if (targetPosition - rb.position != Vector3.zero)
        {
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(targetPosition - rb.position), rotateSpeed * Time.deltaTime));
        }
    }

    protected virtual void Stop()
    {

    }
}
