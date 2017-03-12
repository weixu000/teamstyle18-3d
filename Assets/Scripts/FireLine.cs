using System;
using UnityEngine;

public class FireLine : MonoBehaviour {
    public GameObject line;
    new AudioSource audio;
    ParticleSystem flash;
    public float range;

    void Awake () {
        audio = GetComponent<AudioSource>();
        flash = GetComponent<ParticleSystem>();
	}

    public void Fire(GameObject target)
    {
        if (target.name != "Terrain")
        {
            var hitpos = target.GetComponent<UnitControl>().position.Random(target.transform.position.y, range) - transform.position;
            transform.rotation = Quaternion.LookRotation(hitpos);
        }
        else
        {
            
        }

        if (audio)
        {
            audio.Play();
        }

        if (flash)
        {
            flash.Play();
        }

        Instantiate(line, transform.position, transform.rotation);
        var bullet = line.GetComponent<BulletHit>();
        if (bullet)
        {
            bullet.GetComponent<BulletHit>().target = target;
        }
    }
}
