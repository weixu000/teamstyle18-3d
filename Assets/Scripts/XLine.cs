using System.Collections;
using UnityEngine;

public class XLine : MonoBehaviour {
    public float maxLength;
    public float remainTime;
    public GameObject line, hitPoint;

    [HideInInspector]
    public GameObject target;

    void OnEnable()
    {
        StartCoroutine("WaitToDisable");
    }

    void Update()
    {
        Vector3 Sc = new Vector3(0.5f, 0, 0.5f);// 变换大小

        foreach (var hit in Physics.RaycastAll(transform.position, transform.forward))
        {
            if (hit.transform.gameObject == target)
            {
                Sc.y = hit.distance;
                if (line != null)
                {
                    line.transform.localScale = Sc;
                }

                hitPoint.transform.position = hit.point;//让激光击中物体的粒子效果的空间位置与射线碰撞的点的空间位置保持一致；
                hitPoint.SetActive(true);

                break;
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
