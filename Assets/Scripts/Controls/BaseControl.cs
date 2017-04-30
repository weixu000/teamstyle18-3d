using UnityEngine;

public class BaseControl : DestroyableControl
{
    public GameObject skill1;

    public virtual void Skill1(Position pos)
    {
        Instantiate(skill1, pos.Center(0), Quaternion.identity);
        Debug.Log(name + " skill1 ");
    }
}
