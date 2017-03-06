using UnityEngine;

public class DestroyableControl : UnitControl
{
    public GameObject boom;

    float maxHP = 100, currentHP = 0;
    UILabel hpLabel;
    UISlider hpSlider;

    public float MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            UpdateHP();
        }
    }

    public float CurrentHP
    {
        get { return currentHP; }
        set
        {
            currentHP = value;
            UpdateHP();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        hpSlider = transform.Find("Blood/Panel/BloodSlider").GetComponent<UISlider>();
        hpLabel = hpSlider.GetComponentInChildren<UILabel>();
    }

    public override void SetState(UnitState state)
    {
        base.SetState(state);

        if (state.flag == 0)
        {
            hpSlider.transform.FindChild("HP_Foreground").GetComponent<UISprite>().spriteName = "image 52";
        }
        else
        {
            hpSlider.transform.FindChild("HP_Foreground").GetComponent<UISprite>().spriteName = "image 33";
        }

        CurrentHP = state.health_now;
        MaxHP = state.max_health_now;
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

    void UpdateHP()
    {
        hpSlider.value = currentHP / maxHP;
        hpLabel.text = currentHP.ToString() + "/" + maxHP.ToString();
        if (currentHP == 0)
            Die();
    }
}
