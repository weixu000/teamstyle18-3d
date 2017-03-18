using UnityEngine;
using System.Collections;

public class AbstractLine : MonoBehaviour
{
    [HideInInspector]
    public bool done = false;

    new AudioSource audio;
    ParticleSystem flash;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        flash = GetComponent<ParticleSystem>();
    }

    public virtual void Fire(GameObject target)
    {
        //var hitpos = target.GetComponent<UnitControl>().position.Random(target.transform.position.y, range) - transform.position;
        var hitpos = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(hitpos);

        if (audio) audio.Play();
        if (flash) flash.Play();
        done = false;
    }

    public virtual void Fire(Position pos,float range = 0)
    {
        transform.rotation = Quaternion.LookRotation(pos.Random(0, range) - transform.position);

        if (audio) audio.Play();
        if (flash) flash.Play();
        done = false;
    }
}
