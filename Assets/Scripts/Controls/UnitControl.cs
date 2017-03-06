using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public Position position;
    public UnitName unit_name;
    public UnitType unit_type;
    public int flag = -1;

    MinimapStore mapStore;
    GameObject mapMark;

    UILabel posLabel, idLabel;

    protected virtual void Awake()
    {
        posLabel = transform.Find("Blood/Panel/PositionLabel").GetComponent<UILabel>();
        idLabel = transform.Find("Blood/Panel/IDLabel").GetComponent<UILabel>();

        mapStore = GameObject.Find("MapCamera").GetComponent<MinimapStore>();
    }

    public virtual void SetState(UnitState state)
    {
        name = state.unit_id.ToString();
        position = state.position;
        unit_name = state.unit_name;
        unit_type = state.unit_type;

        posLabel.text = "(" + state.position.x + "," + state.position.y + ")";
        idLabel.text = "ID:" + name;

        if (flag != state.flag)
        {
            if (mapMark)
            {
                Destroy(mapMark);
            }

            if (unit_type == UnitType.BUILDING)
            {
                mapMark = Instantiate(mapStore.buildings[state.flag + 1], transform, false);
            }
            else
            {
                mapMark = Instantiate(mapStore.units[state.flag], transform, false);
            }
        }

        if (!mapMark)
        {
            mapMark = Instantiate(mapStore.buildings[0], transform, false);
        }
        flag = state.flag;
    }
}
