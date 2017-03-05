using UnityEngine;
using System.Collections.Generic;

public class SelectionBox : MonoBehaviour
{
    public RectTransform selectBox;
    //[HideInInspector]
    public List<GameObject> units, selectedUnits;
    Vector3 initialPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectBox.gameObject.SetActive(true);
            initialPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            var bottomRight = new Vector3(Mathf.Max(initialPos.x, Input.mousePosition.x), Mathf.Min(initialPos.y, Input.mousePosition.y));
            var upLeft = new Vector3(Mathf.Min(initialPos.x, Input.mousePosition.x), Mathf.Max(initialPos.y, Input.mousePosition.y));

            selectBox.position = (Input.mousePosition + initialPos) / 2;
            selectBox.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (bottomRight - upLeft).x);
            selectBox.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (upLeft - bottomRight).y);

            foreach (var unit in units)
            {
                var location = Camera.main.WorldToScreenPoint(unit.transform.position);
                if (!selectedUnits.Contains(unit) && location.x > upLeft.x && location.x < bottomRight.x && location.y > bottomRight.y && location.y < upLeft.y
                    && location.z > Camera.main.nearClipPlane && location.z < Camera.main.farClipPlane)
                {
                    selectedUnits.Add(unit);
                    unit.transform.Find("Blood/Panel/BloodSlider").GetComponent<UISprite>().enabled = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectBox.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(2))
        {
            Diselect();
        }
    }

    public void Diselect()
    {
        foreach (var unit in selectedUnits)
        {
            unit.transform.Find("Blood/Panel/BloodSlider").GetComponent<UISprite>().enabled = false;
        }

        selectedUnits.Clear();
    }
}