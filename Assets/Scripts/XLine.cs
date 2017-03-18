using System.Collections;
using UnityEngine;

public class XLine : AbstractLine {
    public float maxLength;
    public float remainTime;
    public GameObject line, hitPoint;

    public override void Fire(GameObject target)
    {
        base.Fire(target);

        if (line) line.SetActive(true);
        StartCoroutine("ActualFire", target);
        StartCoroutine("WaitToDisable");
    }

    public override void Fire(Position pos, float range = 0)
    {
        base.Fire(pos, range);

        if (line) line.SetActive(true);
        StartCoroutine("ActualFire", GameObject.Find("Terrain"));
        StartCoroutine("WaitToDisable");
    }

    IEnumerator ActualFire(GameObject target)
    {
        var Sc = new Vector3(0.5f, 0, 0.5f);

        do
        {
            var found = false;
            foreach (var hit in Physics.RaycastAll(transform.position, transform.forward))
            {
                if (hit.transform.gameObject == target)
                {
                    Sc.y = hit.distance;
                    if (line != null)
                    {
                        line.SetActive(true);
                        line.transform.localScale = Sc;
                    }

                    hitPoint.transform.position = hit.point;
                    hitPoint.SetActive(true);

                    found = true;
                }
            }

            if (found)
            {
                yield return new WaitForFixedUpdate();
            }
            else
            {
                break;
            }
        } while (true);

        Sc.y = maxLength;
        hitPoint.SetActive(false);
        if (line != null)
        {
            line.SetActive(true);
            line.transform.localScale = Sc;
        }
    }

    IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(remainTime);
        if (line) line.SetActive(false);
        hitPoint.SetActive(false);
        StopCoroutine("ActualFire");
        done = true;
    }
}
