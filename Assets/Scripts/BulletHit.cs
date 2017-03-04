using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public GameObject hit;
    [HideInInspector]
    public GameObject target;

    new AudioSource audio;
    ParticleSystem bullet;

    void Awake()
    {
        audio = hit.GetComponent<AudioSource>();
        bullet = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other == target)
        {
            List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>();
            bullet.GetCollisionEvents(other, events);
            hit.transform.position = events[0].intersection;
            hit.GetComponent<ParticleSystem>().Play();
            if (audio)
            {
                audio.Play();
            }
        }
    }
}
