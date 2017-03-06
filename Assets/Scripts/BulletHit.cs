using UnityEngine;
using System.Collections.Generic;

public class BulletHit : MonoBehaviour
{
    ParticleSystem bullet;
    Rigidbody rb;

    public float velocity=50.0f;
    public GameObject target;
    public GameObject hit;

    void Awake()
    {
        bullet = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity * transform.forward;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other == target)
        {
            List<ParticleCollisionEvent> collisionEvents=new List<ParticleCollisionEvent>();
            bullet.GetCollisionEvents(other, collisionEvents);

            foreach (var collision in collisionEvents)
            {
                Instantiate(hit, collisionEvents[0].intersection, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
