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
        var hitpos = target.GetComponent<UnitControl>().position.Random(target.transform.position.y, range) - transform.position;
        transform.rotation = Quaternion.LookRotation(hitpos);

        ActualFire(target);
    }

    public void Fire(Position pos)
    {
        transform.rotation = Quaternion.LookRotation(pos.Random(0, range) - transform.position);
        ActualFire(GameObject.Find("Terrain"));
    }

    void ActualFire(GameObject target)
    {
        if (audio)
        {
            audio.Play();
        }

        if (flash)
        {
            flash.Play();
        }

        var bullet = Instantiate(line, transform.position, transform.rotation);
        bullet.GetComponent<BulletHit>().target = target;
    }
}
