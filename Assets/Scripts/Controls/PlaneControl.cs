using UnityEngine;

public class PlaneControl : HackedControl
{
    public GameObject fire1, fire2;

    public override void Skill1(int target_id)
    {
        //fire1.transform.rotation = Quaternion.LookRotation(GameObject.Find(target_id.ToString()).transform.position - fire1.transform.position);
        //fire1.GetComponent<XLine>().target = GameObject.Find(target_id.ToString());
        //fire1.SetActive(true);

        fire1.GetComponent<FireLine>().Fire(GameObject.Find(target_id.ToString()));
        base.Skill1(target_id);
    }

    public override void Skill2(Position pos)
    {
        if (fire2 != null)
        {
            //fire2.transform.rotation = Quaternion.LookRotation(pos.Random() - fire1.transform.position);
            //fire2.GetComponent<XLine>().target = GameObject.Find("Terrain");
            //fire2.SetActive(true);
            //fire2.GetComponent<FireLine>().Fire(GameObject.Find(target_id.ToString()));
        }

        base.Skill2(pos);
    }
}