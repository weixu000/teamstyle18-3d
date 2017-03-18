using System;
using System.Collections;
using UnityEngine;

public class FireLine : AbstractLine {
    public GameObject line;

    GameObject bullet;

    public override void Fire(GameObject target)
    {
        base.Fire(target);

        bullet = Instantiate(line, transform.position, transform.rotation);
        bullet.GetComponent<BulletHit>().target = target;
        StartCoroutine("WaitForBullet");
    }

    public override void Fire(Position pos)
    {
        base.Fire(pos);

        bullet = Instantiate(line, transform.position, transform.rotation);
        StartCoroutine("WaitForBullet");
    }

    IEnumerator WaitForBullet()
    {
        while (true)
        {
            if (bullet)
            {
                yield return new WaitForFixedUpdate();
            }
            else
            {
                done = true;
                yield break;
            }
        }
    }
}
