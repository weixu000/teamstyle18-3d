using UnityEngine;

public class FreeCamera : MonoBehaviour {
    public float moveSpeed, rotationSpeed, zoomSpeed;
    public float maxY, minY;
    public Transform map;

    float deltX;
    float deltY;

    Vector3 initialPos;
    Quaternion initialRot;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        deltX = transform.root.eulerAngles.y;
        deltY = transform.root.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            deltX += Input.GetAxis("Mouse X") * rotationSpeed;
            deltY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            deltX = ClampAngle(deltX, -360, 360);
            deltY = ClampAngle(deltY, -70, 70);
            transform.rotation = Quaternion.Euler(deltY, deltX, 0);
        }

        var moveVector = transform.right * Input.GetAxis("Horizontal") + new Vector3(transform.forward.x, 0, transform.forward.z) * Input.GetAxis("Vertical");
        transform.Translate(moveVector * moveSpeed * transform.position.y, Space.World);

        transform.Translate(transform.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * transform.position.y, Space.World);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, 0, 500),
            Mathf.Clamp(transform.position.y, minY, maxY),
            Mathf.Clamp(transform.position.z, 0, 500));

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = initialPos;
            transform.rotation = initialRot;
        }
    }

    float ClampAngle(float angle, float minAngle, float maxAgnle)
    {
        if (angle <= -360)
            angle += 360;
        if (angle >= 360)
            angle -= 360;

        return Mathf.Clamp(angle, minAngle, maxAgnle);
    }
}
