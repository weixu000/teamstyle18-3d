using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWaver : MonoBehaviour {

    float radian = 0; // 弧度  
    public float perRadian = 0.05f; // 每次变化的弧度  
    public float radius = 0.004f; // 半径  
    float phase = 0;
    Vector3 oldPos; // 开始时候的坐标  
    // Use this for initialization  
    void Start()
    {
        oldPos = transform.localPosition; // 将最初的位置保存到oldPos  
        phase = Random.Range(0,180);
    }

    // Update is called once per frame  
    void Update()
    {
        radian += perRadian; // 弧度每次加0.03  
        float dy = Mathf.Cos(radian + phase) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以  
        transform.localPosition = oldPos + new Vector3(0, dy, 0);
        oldPos = transform.localPosition;
    }
}
