using System.Collections;
using UnityEngine;

public class OperationCap : MonoBehaviour
{
    SelectionBox box;
    LineRenderer guideLine;
    NetCom netcom;

    const int shootableLayer = 8, producerLayer = 9;

    void Awake()
    {
        box = GetComponent<SelectionBox>();
        guideLine = GetComponent<LineRenderer>();
        netcom = GameObject.Find("GameController").GetComponent<NetCom>();
    }

    void Update()
    {
        if(box.selectedUnits.Count > 0 && !box.ContinueSelecting() && Input.GetMouseButtonDown(0))
        {
            var target = HitUnit(1<<shootableLayer);
            if (target != null)
            {
                foreach (var unit in box.selectedUnits)
                {
                    if (unit.GetComponent<UnitControl>().unit_name == UnitName.MEAT)
                    {
                        netcom.InstrsToSend.Enqueue(new Instr(InstrType.CAPTURE, int.Parse(unit.name), int.Parse(target.name)));
                        Debug.Log(unit.name + "order to capture" + target.name);
                    }
                    else
                    {
                        netcom.InstrsToSend.Enqueue(new Instr(InstrType.SKILL1, int.Parse(unit.name), int.Parse(target.name)));
                        Debug.Log(unit.name + "order to skill1" + target.name);
                    }
                }
            }
            box.Diselect();
        }
        else if (box.selectedUnits.Count > 0 && !box.ContinueSelecting() && Input.GetMouseButtonDown(1))
        {
            Position target;
            if (HitTerrain(out target))
            {
                foreach (var unit in box.selectedUnits)
                {
                    netcom.InstrsToSend.Enqueue(new Instr(InstrType.SKILL2, int.Parse(unit.name), 0, target, Position.zero));
                    Debug.Log(unit.name + string.Format("order to skill2 at ({0},{1})", target.x, target.y));
                }
            }
            box.Diselect();
        }
        else if(!box.ContinueSelecting() && Input.GetMouseButtonDown(0))
        {
            var producer = HitUnit(1 << producerLayer|1<<shootableLayer);
            if (producer != null)
            {
                netcom.InstrsToSend.Enqueue(new Instr(InstrType.PRODUCE, int.Parse(producer.name)));
                Debug.Log(producer.name + "order to produce");
            }
        }
    }

    bool HitTerrain(out Position pos)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, 1 << shootableLayer) && hit.collider.gameObject.name == "Terrain")
        {
            RenderGuideLine(hit.point);
            pos = Position.Inside(hit.point);
            return true;
        }

        pos = Position.zero;
        return false;
    }

    GameObject HitUnit(int layerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, layerMask))
        {
            RenderGuideLine(hit.point);
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    void RenderGuideLine(Vector3 point)
    {
        var positions = new Vector3[2];
        foreach (var unit in box.selectedUnits)
        {
            positions[0] += unit.transform.position;
        }
        positions[0] /= box.selectedUnits.Count;
        positions[1] = point;

        guideLine.enabled = true;
        guideLine.SetPositions(positions);
        StartCoroutine("GuidelineDisappear");
    }

    IEnumerator GuidelineDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        guideLine.enabled = false;
    }
}
