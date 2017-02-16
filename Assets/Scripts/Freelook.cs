using System;
using UnityEngine;

[Serializable]
public struct Boundary
{
    public float left, right, top, bottom;
}


public class Freelook : MonoBehaviour {
    public Boundary movingBound, viewBound;
    public float moveSpeed, zoomSpeed;
    public float upBound, downBound;

    Transform map;

    void Awake()
    {
        map = GameObject.Find("Map").transform;
    }

    void Update () {
        transform.position += new Vector3(0, -zoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * transform.position.y, 0);
        if (transform.position.y < downBound)
            transform.position = new Vector3(transform.position.x, downBound, transform.position.z);
        else if (transform.position.y > upBound)
            transform.position = new Vector3(transform.position.x, upBound, transform.position.z);

        var viewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        var delta = moveSpeed * Time.deltaTime * transform.position.y;
        if(viewport.x < movingBound.left || Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > map.position.x - map.lossyScale.x * 0.5 * (1 - viewBound.left))
                transform.position -= new Vector3(delta, 0, 0);
        }
        else if(viewport.x > 1 - movingBound.right || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < map.position.x + map.lossyScale.x * 0.5 * (1 - viewBound.right))
                transform.position += new Vector3(delta, 0, 0);
        }
        if(viewport.y < movingBound.bottom || Input.GetKey(KeyCode.S))
        {
            if (transform.position.z > map.position.z - map.lossyScale.y * 0.5 * (1 - viewBound.bottom))
                transform.position -= new Vector3(0, 0, delta);
        }
        else if(viewport.y > 1 - movingBound.top || Input.GetKey(KeyCode.W))
        {
            if (transform.position.z < map.position.z + map.lossyScale.y * 0.5 * (1 - viewBound.top))
                transform.position += new Vector3(0, 0, delta);
        }
    }
}
