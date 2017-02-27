using UnityEngine;

public class DestroyableControl : UnitControl
{
    [HideInInspector]
    public float currentHP, maxHP;

    UILabel hpLabel;
    UISlider hpSlider;

    public GameObject boom;

    protected override void Awake()
    {
        base.Awake();
        hpSlider = transform.Find("Blood/Panel/BloodSlider").GetComponent<UISlider>();
        hpLabel = hpSlider.GetComponentInChildren<UILabel>();
    }

    public override void SetState(UnitState state)
    {
        base.SetState(state);
        SetHP(state.health_now, state.max_health_now);
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 1f);
        if(boom != null)
        {
            boom.SetActive(true);
        }
        Debug.Log(name + " is dead");
    }

    public void SetHP(float now,float max)
    {
        currentHP = now;
        maxHP = max;
        hpSlider.value = currentHP / maxHP;
        hpLabel.text = (100 * currentHP / maxHP).ToString("F") + "%";
        if (currentHP == 0)
            Die();
    }
}
