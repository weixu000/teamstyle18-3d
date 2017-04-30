using System.Collections.Generic;
using UnityEngine;

public class OperationCap : MonoBehaviour
{
    public GameObject guideLine;
    public NetCom netcom;

    SelectionBox box;

    class Instruction
    {
        public Instr ins;
        public GameObject line;
    }

    Dictionary<GameObject, Instruction> lines = new Dictionary<GameObject, Instruction>();

    const int shootableLayer = 8, producerLayer = 9;

    void Awake()
    {
        box = GetComponent<SelectionBox>();
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
                        //netcom.InstrsToSend.Enqueue(new Instr(InstrType.CAPTURE, int.Parse(unit.name), int.Parse(target.name)));
                        UpdateInstruction(unit, target.transform.position,
                            new Instr
                            {
                                type = InstrType.CAPTURE,
                                id = int.Parse(unit.name),
                                target_id_building_id = int.Parse(target.name)
                            });
                        Debug.Log(unit.name + "order to capture " + target.name);
                    }
                    else
                    {
                        //netcom.InstrsToSend.Enqueue(new Instr(InstrType.SKILL1, int.Parse(unit.name), int.Parse(target.name)));
                        UpdateInstruction(unit, target.transform.position,
                            new Instr
                            {
                                type = InstrType.SKILL1,
                                id = int.Parse(unit.name),
                                target_id_building_id = int.Parse(target.name)
                            });
                        Debug.Log(unit.name + "order to skill1 " + target.name);
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
                    //netcom.InstrsToSend.Enqueue(new Instr(InstrType.SKILL2, int.Parse(unit.name), 0, target, Position.zero));
                    UpdateInstruction(unit, target.Center(0),
                        new Instr
                        {
                            type = InstrType.SKILL1,
                            id = int.Parse(unit.name),
                            pos1 = target
                        });
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
                if (lines.ContainsKey(producer))
                {
                    lines.Remove(producer);
                }
                //netcom.InstrsToSend.Enqueue(new Instr(InstrType.PRODUCE, int.Parse(producer.name)));
                lines.Add(producer, new Instruction
                {
                    ins =
                    new Instr
                    {
                        type = InstrType.PRODUCE,
                        id = int.Parse(producer.name)
                    }
                });
                Debug.Log(producer.name + "order to produce");
            }
        }
    }

    bool HitTerrain(out Position pos)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, 1 << shootableLayer) && hit.collider.gameObject.name == "Terrain")
        {
            pos = Position.InsideWhere(hit.point);
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
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    void UpdateInstruction(GameObject unit,Vector3 point,Instr ins)
    {
        if (lines.ContainsKey(unit))
        {
            Destroy(lines[unit].line);
            lines.Remove(unit);
        }
        var line = Instantiate(guideLine, gameObject.transform);
        line.GetComponent<LineRenderer>().SetPositions(new[] { unit.transform.position, point });
        lines.Add(unit, new Instruction { ins = ins, line = line });
    }
}
