using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public Position position;
    public UnitName unit_name;
    public UnitType unit_type;
    public int flag = -1;

    MinimapStore mapStore;
    GameObject mapMark, bmap;

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

        posLabel.text = state.position.ToString();
        idLabel.text = "ID:" + name;

        if (!mapMark || flag != state.flag)
        {
            if (mapMark)
            {
                Destroy(mapMark);
            }

            if (unit_type == UnitType.BUILDING || unit_type==UnitType.BASE)
            {
                mapMark = Instantiate(mapStore.buildings[state.flag + 1], transform, false);
            }
            else
            {
                mapMark = Instantiate(mapStore.units[state.flag], transform, false);
            }
        }

        if (!bmap || flag != state.flag)
        {
            if (bmap)
            {
                Destroy(bmap);
            }

            switch (unit_name)
            {
                case UnitName.AIRCRAFT_LAB:
                case UnitName.BANK:
                case UnitName.BID_LAB:
                case UnitName.BUILD_LAB:
                case UnitName.CAR_LAB:
                case UnitName.ELEC_LAB:
                case UnitName.FINANCE_LAB:
                case UnitName.HACK_LAB:
                case UnitName.MATERIAL_LAB:
                case UnitName.NANO_LAB:
                case UnitName.RADIATION_LAB:
                case UnitName.TEACH_BUILDING:
                case UnitName.UAV_LAB:
                    bmap = Instantiate(mapStore.buildings[state.flag + 6], transform, false);
                    break;
                case UnitName.__BASE:
                    bmap = Instantiate(mapStore.buildings[state.flag + 3], transform, false);
                    break;
                case UnitName.BATTLE_TANK:
                    bmap = Instantiate(mapStore.units[state.flag + 16], transform, false);
                    break;
                case UnitName.UAV:
                    bmap = Instantiate(mapStore.units[state.flag + 14], transform, false);
                    break;
                case UnitName.SUPERMAN:
                    bmap = Instantiate(mapStore.units[state.flag + 12], transform, false);
                    break;
                case UnitName.NUKE_TANK:
                    bmap = Instantiate(mapStore.units[state.flag + 10], transform, false);
                    break;
                case UnitName.MEAT:
                    bmap = Instantiate(mapStore.units[state.flag + 8], transform, false);
                    break;
                case UnitName.HACKER:
                    bmap = Instantiate(mapStore.units[state.flag + 6], transform, false);
                    break;
                case UnitName.EAGLE:
                    bmap = Instantiate(mapStore.units[state.flag + 4], transform, false);
                    break;
                case UnitName.BOLT_TANK:
                    bmap = Instantiate(mapStore.units[state.flag + 2], transform, false);
                    break;

            }
        }

        flag = state.flag;
    }
}
