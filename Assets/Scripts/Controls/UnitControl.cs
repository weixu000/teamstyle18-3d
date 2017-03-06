using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public Position position;
    public UnitName unit_name;
    public UnitType unit_type;
    public int flag = -1;

    UILabel posLabel, idLabel;

    protected virtual void Awake()
    {
        posLabel = transform.Find("Blood/Panel/PositionLabel").GetComponent<UILabel>();
        idLabel = transform.Find("Blood/Panel/IDLabel").GetComponent<UILabel>();
    }

    public virtual void SetState(UnitState state)
    {
        name = state.unit_id.ToString();
        position = state.position;
        unit_name = state.unit_name;
        unit_type = state.unit_type;
        flag = state.flag;

        posLabel.text = "(" + state.position.x + "," + state.position.y + ")";
        idLabel.text = "ID:" + name;
    }
}
