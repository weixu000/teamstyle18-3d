using UnityEngine;

public class RoleStateUI : MonoBehaviour
{
    float maxHP = 100, currentHP = 0;

    public UISlider hpSlider;
    public UILabel hpPercentLabel;
    public UILabel PeopleLabel;
    public UILabel MoneyLabel;
    public UILabel BuildingsLabel;
    public UILabel ScienceLabel;

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

    public int People
    {
        get { return int.Parse(PeopleLabel.text); }
        set
        {
            PeopleLabel.text = value.ToString();
        }
    }

    public int Buildings
    {
        get { return int.Parse(BuildingsLabel.text); }
        set
        {
            BuildingsLabel.text = value.ToString();
        }
    }

    public int Money
    {
        get { return int.Parse(MoneyLabel.text); }
        set
        {
            MoneyLabel.text = value.ToString();
        }
    }

    public int Science
    {
        get { return int.Parse(ScienceLabel.text); }
        set
        {
            ScienceLabel.text = value.ToString();
        }
    }

    void UpdateHP()
    {
        hpSlider.value = currentHP / maxHP;
        hpPercentLabel.text = currentHP.ToString() + "/" + maxHP.ToString();
    }
}
