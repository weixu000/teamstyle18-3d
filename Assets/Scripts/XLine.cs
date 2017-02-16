using System.Collections;
using UnityEngine;

public class XLine : MonoBehaviour {
    public float maxLength;

    public float remainTime;

    public GameObject line, hitPoint;

    void OnEnable()
    {
        StartCoroutine("WaitToDisable");
    }

    void Update () {
		RaycastHit hit;
        Vector3 Sc = new Vector3(0.5f, 0, 0.5f);// 变换大小

		//发射射线，通过获取射线碰撞后返回的距离来变换激光模型的y轴上的值
        if (Physics.Raycast(transform.position, transform.forward, out hit)){
            //Debug.DrawLine(transform.position,hit.point);
            Sc.y = hit.distance;

			hitPoint.transform.position=hit.point;//让激光击中物体的粒子效果的空间位置与射线碰撞的点的空间位置保持一致；
			hitPoint.SetActive(true);
		}
		//当激光没有碰撞到物体时，让射线的长度保持为500m，并设置击中效果为不显示
		else{
            Sc.y = maxLength;
		    hitPoint.SetActive(false);
		}
		
        if(line != null)
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
