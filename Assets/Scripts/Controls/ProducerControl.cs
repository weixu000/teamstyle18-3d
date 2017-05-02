using System;
using UnityEngine;

public class ProducerControl : UnitControl {
    public GameObject display1, display2;
    public GameObject selfSkill2, targetSkill2;

    public override void SetState(UnitState state)
    {
        if (state.flag != flag)
        {
            if (display1 != null && state.flag == 0)
            {
                display1.SetActive(true);
                display2.SetActive(false);
                //Debug.Log(name + "captured by 1");
            }
            else if(display2 != null && state.flag == 1)
            {
                display1.SetActive(false);
                display2.SetActive(true);
                //Debug.Log(name + "captured by 2");
            }
        }
        base.SetState(state);
    }

    public virtual void Skill2(int target_id, Position pos)
    {
        try
        {
            switch (unit_name)
            {
                case UnitName.BUILD_LAB:
                    Instantiate(targetSkill2, pos.Center(0), targetSkill2.transform.rotation);
                    break;
                case UnitName.FINANCE_LAB:
                case UnitName.MATERIAL_LAB:
                    Instantiate(targetSkill2, GameObject.Find(target_id.ToString()).transform, false);
                    break;
                default:
                    if (selfSkill2 != null)
                        Instantiate(selfSkill2, transform, false);
                    break;
            }
        }
        catch(Exception)
        {

        }
        Debug.Log(name + " skill2");
    }
}
