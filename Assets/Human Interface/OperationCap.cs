using System.Collections;
using UnityEngine;

public class OperationCap : MonoBehaviour
{
    SelectionBox select;
    LineRenderer guideLine;

    void Awake()
    {
        select = GetComponent<SelectionBox>();
        guideLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetButton("Ctrl")) return;
        if(select.selectedUnits.Count > 0 && Input.GetMouseButtonDown(0))
        {
            var point=new Vector3();
            if(HitPermitted(ref point))
            {

            }
        }

        if (select.selectedUnits.Count > 0 && Input.GetMouseButtonDown(0))
        {
            var point = new Vector3();
            if (HitPermitted(ref point))
            {

            }
        }
    }

    bool HitPermitted(ref Vector3 point)
    {
        var hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        foreach (var hit in hits)
        {
            if(hit.transform.gameObject.name== "Terrain")
            {
                var positions = new Vector3[2];
                foreach (var unit in select.selectedUnits)
                {
                    positions[0] += unit.transform.position;
                }
                positions[0] /= select.selectedUnits.Count;
                positions[1] = hit.point;

                guideLine.enabled = true;
                guideLine.SetPositions(positions);
                select.Diselect();
                StartCoroutine("GuidelineDisappear");

                point = hit.point;
                return true;
            }
        }

        return false;
    }

    IEnumerator GuidelineDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        guideLine.enabled = false;
    }
}
