using UnityEngine;
using System.Collections.Generic;

public class BulletHit : MonoBehaviour
{
    public float velocity=50.0f;
    public GameObject hit;
    public int maxHitPoints = 1;

    const float minX = -20, maxX = 570, minY = 0, maxY = 150, minZ = -20, maxZ = 500;

    ParticleSystem bullet;
    Rigidbody rb;

    [HideInInspector]
    public GameObject target;

    void Awake()
    {
        bullet = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = velocity * transform.forward;
        }
    }

    void Update()
    {
        if (transform.position.y < minY || transform.position.y >maxY
            || transform.position.x < minX || transform.position.x > maxX
            || transform.position.z < minZ || transform.position.z > maxZ)
        {
            for (int i = 0; i < maxHitPoints; i++)
            {
                Instantiate(hit, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (target && other == target)
        {
            var collisionEvents=new List<ParticleCollisionEvent>();
            bullet.GetCollisionEvents(other, collisionEvents);

            for (int i = 0; i < collisionEvents.Count && i < maxHitPoints; i++)
            {
                Instantiate(hit, collisionEvents[i].intersection, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
