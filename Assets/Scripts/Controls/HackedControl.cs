using UnityEngine;
using System.Collections;

public class HackedControl : InvasiveControl
{
    UILabel hackedLabel;

    protected override void Awake()
    {
        base.Awake();
        hackedLabel = transform.Find("Blood/Panel/HackedLabel").GetComponent<UILabel>();
    }

    public override void SetState(UnitState state)
    {
        base.SetState(state);

        hackedLabel.text = "Hack point: " + state.hacked_point.ToString();
    }
}
