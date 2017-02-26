using System.Collections;
using UnityEngine;

public class XLine : MonoBehaviour {
    public float maxLength;
    public float remainTime;
    public GameObject line, hitPoint;
    public float range;

    [HideInInspector]
    public GameObject target;

    void OnEnable()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }

        var hitpos = target.GetComponent<UnitControl>().position.Random(range) - transform.position;
        transform.rotation = Quaternion.LookRotation(hitpos);

        StartCoroutine("WaitToDisable");
        Debug.Log("OnEnable called" + gameObject.name);
    }

    void Update()
    {
        var Sc = new Vector3(0.5f, 0, 0.5f);

        foreach (var hit in Physics.RaycastAll(transform.position, transform.forward))
        {
            if (hit.transform.gameObject == target)
            {
                Sc.y = hit.distance;
                if (line != null)
                {
                    line.transform.localScale = Sc;
                }

                hitPoint.transform.position = hit.point;
                hitPoint.SetActive(true);

                return;
            }
        }

        Sc.y = maxLength;
        hitPoint.SetActive(false);
        if (line != null)
        {
            line.transform.localScale = Sc;
        }
    }

    IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(remainTime);
        gameObject.SetActive(false);
    }
}
