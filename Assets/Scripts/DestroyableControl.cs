using UnityEngine;

public class DestroyableControl : UnitControl
{
    [HideInInspector]
    public float currentHP;
    [HideInInspector]
    public float maxHP;

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
        currentHP = state.health_now;
        maxHP = state.max_health_now;
        hpSlider.value = currentHP / maxHP;
        hpLabel.text = (100 * currentHP / maxHP).ToString() + "%";
        if (currentHP == 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 2f);
        if(boom != null)
        {
            boom.SetActive(true);
        }
        Debug.Log(name + " is dead");
    }
}
