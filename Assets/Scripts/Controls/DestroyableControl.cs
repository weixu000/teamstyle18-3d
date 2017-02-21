using UnityEngine;

public class DestroyableControl : UnitControl
{
    [HideInInspector]
    public float currentHP, maxHP;

    UILabel posLabel, hpLabel;
    UISlider hpSlider;

    public GameObject boom;

    protected virtual void Awake()
    {
        posLabel = GetComponentInChildren<UILabel>();
        hpSlider = GetComponentInChildren<UISlider>();
        hpLabel = hpSlider.GetComponentInChildren<UILabel>();
    }

    public override void SetState(UnitState state)
    {
        base.SetState(state);
        posLabel.text = "(" + state.position.x + "," + state.position.y + ")";

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
