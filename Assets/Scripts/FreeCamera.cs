using UnityEngine;

public class FreeCamera : MonoBehaviour {
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;

    float deltX = 0f;
    float deltY = 0f;

    Vector3 initialPos;
    Quaternion initialRot;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            deltX += Input.GetAxis("Mouse X") * rotationSpeed;
            deltY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            deltX = ClampAngle(deltX, -360, 360);
            deltY = ClampAngle(deltY, -70, 70);
            transform.rotation = Quaternion.Euler(deltY, deltX, 0);
        }

        var moveVector = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed);
        if (Input.GetMouseButton(1))
        {
            transform.Translate(-moveVector, Space.World);
        }
        else
        {
            transform.Translate(moveVector, Space.Self);
        }

        //相机复位远点;
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = initialPos;
            transform.rotation = initialRot;
        }
    }

    //规划角度;
    float ClampAngle(float angle, float minAngle, float maxAgnle)
    {
        if (angle <= -360)
            angle += 360;
        if (angle >= 360)
            angle -= 360;

        return Mathf.Clamp(angle, minAngle, maxAgnle);
    }
}
