using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationCap : MonoBehaviour
{
    SelectionBox select;
    LineRenderer guideLine;
    GameObject map;

	void Awake ()
    {
        select = GetComponent<SelectionBox>();
        guideLine = GetComponent<LineRenderer>();
        map = GameObject.Find("Map");
	}
	
	void Update ()
    {
        RaycastHit hit;
        if (select.selectedUnits.Count> 0 && Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
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
        }
	}

    IEnumerator GuidelineDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        guideLine.enabled = false;
    }
}
