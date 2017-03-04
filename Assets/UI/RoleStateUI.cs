using UnityEngine;

public class RoleStateUI : MonoBehaviour
{
    public string playerName = "Player";
    public float maxHP = 100, currentHP = 0;
    public float currentPeople = 0;
    public float currentMoney = 0;
    public float currentBuildings = 0;
    public float currentScience = 0;

    public UILabel playerNameLabel;
    public UISlider hpSlider;
    public UILabel hpPercentLabel;
    public UILabel PeopleLabel;
    public UILabel MoneyLabel;
    public UILabel BuildingsLabel;
    public UILabel ScienceLabel;

    // Use this for initialization
    void Start()
    {
        playerNameLabel.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {
        float hppercent = currentHP / maxHP;
        hpSlider.value = hppercent;
        hpPercentLabel.text = (hppercent * 100).ToString("F") + "%";

        PeopleLabel.text = currentPeople.ToString();

        MoneyLabel.text = currentMoney.ToString();

        BuildingsLabel.text = currentBuildings.ToString();

        ScienceLabel.text = currentScience.ToString();
    }
}
