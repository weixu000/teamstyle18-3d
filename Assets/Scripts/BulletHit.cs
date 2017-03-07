using UnityEngine;
using System.Collections.Generic;

public class BulletHit : MonoBehaviour
{
    ParticleSystem bullet;
    Rigidbody rb;

    public float velocity=50.0f;
    public GameObject target;
    public GameObject hit;
    public int maxHitPoints = 5;

    void Awake()
    {
        bullet = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = velocity * transform.forward;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other == target||other.name == "Terrain")
        {
            List<ParticleCollisionEvent> collisionEvents=new List<ParticleCollisionEvent>();
            bullet.GetCollisionEvents(other, collisionEvents);

            //foreach (var collision in collisionEvents)
            //{
            //    Instantiate(hit, collision.intersection, Quaternion.identity);
            //}

            for (int i = 0; i < collisionEvents.Count && i < maxHitPoints; i++)
            {
                Instantiate(hit, collisionEvents[i].intersection, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
