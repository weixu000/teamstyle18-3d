using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoPresenter : MonoBehaviour
{

    Clicker clicker;
    counter counter;
    UnitControl uControl;
    public UILabel infoLabel;

    // Use this for initialization
    void Start()
    {
        clicker = FindObjectOfType<Clicker>();
        counter = FindObjectOfType<counter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clicker.selectObject != null)
        {
            string team, uavlab, hackerlab1, hackerlab2, liblab, carlab, eleclab, nukelab, hanglab, buildlab, financelab1, financelab2, materiallab, nanolab;
            uControl = clicker.selectObject.GetComponent<UnitControl>();

            if (uControl.flag == -1)
            {
                team = "中立";
                uavlab = hackerlab1 = hackerlab2 = liblab = carlab = eleclab = nukelab = hanglab = buildlab = financelab1 = financelab2 = materiallab = nanolab = null;
            }
            else if (uControl.flag == 0)
            {
                team = "Player1";
                hackerlab1 = counter.redNum[(int)UnitName.HACK_LAB] != 0 ? "科技获取量+5 " : "";
                hackerlab2 = counter.blueNum[(int)UnitName.HACK_LAB] != 0 ? "科技获取量-5 " : "";
                uavlab = counter.redNum[(int)UnitName.UAV_LAB] != 0 ? "资源消耗-10% " : "";
                liblab = counter.redNum[(int)UnitName.BID_LAB] != 0 ? "生命+50% 攻击+10% " : "";
                carlab = counter.redNum[(int)UnitName.CAR_LAB] != 0 ? "防御+15% 速度+3 " : "";
                eleclab = counter.redNum[(int)UnitName.ELEC_LAB] != 0 ? "射程+4 " : "";
                nukelab = counter.redNum[(int)UnitName.UAV_LAB] != 0 ? "攻击+20% " : "";
                hanglab = counter.redNum[(int)UnitName.AIRCRAFT_LAB] != 0 ? "速度+3 攻击+15% " : "";
                buildlab = counter.redNum[(int)UnitName.BUILD_LAB] != 0 ? "防御+100% 攻击-50% " : "";
                financelab1 = counter.redNum[(int)UnitName.FINANCE_LAB] != 0 ? "经济获取量+5 " : "";
                financelab2 = counter.blueNum[(int)UnitName.FINANCE_LAB] != 0 ? "经济获取量-5 " : "";
                materiallab = counter.redNum[(int)UnitName.MATERIAL_LAB] != 0 ? "速度+5 " : "";
                nanolab = counter.redNum[(int)UnitName.NANO_LAB] != 0 ? "维修1.5% " : "";
            }
            else
            {
                team = "Player2";
                hackerlab1 = counter.blueNum[(int)UnitName.HACK_LAB] != 0 ? "科技获取量+5 " : "";
                hackerlab2 = counter.redNum[(int)UnitName.HACK_LAB] != 0 ? "科技获取量-5 " : "";
                uavlab = counter.blueNum[(int)UnitName.UAV_LAB] != 0 ? "资源消耗-10% " : "";
                liblab = counter.blueNum[(int)UnitName.BID_LAB] != 0 ? "生命+50% 攻击+10% " : "";
                carlab = counter.blueNum[(int)UnitName.CAR_LAB] != 0 ? "防御+15% 速度+3 " : "";
                eleclab = counter.blueNum[(int)UnitName.ELEC_LAB] != 0 ? "射程+4 " : "";
                nukelab = counter.blueNum[(int)UnitName.UAV_LAB] != 0 ? "攻击+20% " : "";
                hanglab = counter.blueNum[(int)UnitName.AIRCRAFT_LAB] != 0 ? "速度+3 攻击+15% " : "";
                buildlab = counter.blueNum[(int)UnitName.BUILD_LAB] != 0 ? "防御+100% 攻击-50% " : "";
                financelab1 = counter.blueNum[(int)UnitName.FINANCE_LAB] != 0 ? "经济获取量+5 " : "";
                financelab2 = counter.redNum[(int)UnitName.FINANCE_LAB] != 0 ? "经济获取量-5 " : "";
                materiallab = counter.blueNum[(int)UnitName.MATERIAL_LAB] != 0 ? "速度+5 " : "";
                nanolab = counter.blueNum[(int)UnitName.NANO_LAB] != 0 ? "维修1.5% " : "";
            }

            switch (uControl.unit_name)
            {
                case UnitName.__BASE:
                    infoLabel.text = "基地    " + "队伍：" + team +
                        "\n\n    炮塔(CD:1):对中心目标地点造成25点伤害;对与中心目标距离为1的地点造成50%的伤害; 对与中心目标距离为2的地点造成25%的伤害" +
                        "\n    加成：" + buildlab;
                    break;
                case UnitName.AIRCRAFT_LAB:
                    infoLabel.text = "高等飞行器研究院    " + "队伍：" + team +
                        "\n\n    天空领主：高等飞行器研究院的航空科技首屈一指，增加飞机速度3点，攻击力15%";
                    break;
                case UnitName.BANK:
                    infoLabel.text = "银行    " + "队伍：" + team +
                        "\n\n    加成：" + financelab1 + financelab2;
                    break;
                case UnitName.BATTLE_TANK:
                    infoLabel.text = "主战坦克    " + "队伍：" + team +
                        "\n\n    轨道炮(CD:10)：对单个目标造成伤害150点" +
                        "\n    加成：" + carlab + eleclab + nukelab + uavlab + nanolab;
                    break;
                case UnitName.BID_LAB:
                    infoLabel.text = "生化研究院    " + "队伍：" + team +
                        "\n\n    基因工程：通过对人体基因的改造，提升步兵的生命值50%，攻击 10%";
                    break;
                case UnitName.BOLT_TANK:
                    infoLabel.text = "电子对抗坦克    " + "队伍：" + team +
                        "\n\n    电磁脉冲(CD:10)：瘫痪一辆敌方车辆 5 回合或对一架敌方飞机造成一定伤害" +
                        "\n    加成：" + carlab + eleclab + nukelab + uavlab + nanolab;
                    break;
                case UnitName.BUILD_LAB:
                    infoLabel.text = "建筑土木学院    " + "队伍：" + team +
                        "\n\n    攻防提升：对建筑学的研究，提升基地防御力两倍与基地炮塔攻击0.5倍 \n    人造地震(CD:75)：通过对地质结构的研究，对特定位置上的步兵与坦克造成250点伤害（不计护甲）";
                    break;
                case UnitName.CAR_LAB:
                    infoLabel.text = "特种车辆系    " + "队伍：" + team +
                        "\n\n    通过对复合装甲及履带的研究，可提高坦克防御 15%和速度3点";
                    break;
                case UnitName.EAGLE:
                    infoLabel.text = "鹰式攻击机    " + "队伍：" + team +
                        "\n\n    飞弹(CD:20)：对1格内所有目标造成伤害200点 \n    超音速打击(CD:50)：打击 2个坐标内的任何目标,造成 400点伤害,并在10回合内提升速度5点" +
                        "\n    加成：" + uavlab + hanglab + nanolab;
                    break;
                case UnitName.ELEC_LAB:
                    infoLabel.text = "电子对抗学院    " + "队伍：" + team +
                        "\n\n    高级电子科技：电子对抗学院拥有先进的电子科技。提升坦克的射程4点";
                    break;
                case UnitName.FINANCE_LAB:
                    infoLabel.text = "社会金融研究院    " + "队伍：" + team +
                        "\n\n    心理战争(CD:50): 通过对心理学的研究，忽悠敌方一个单位永久加入我方部队(对精英单位及建筑无效)";
                    break;
                case UnitName.HACKER:
                    infoLabel.text = "黑客    " + "队伍：" + team +
                        "\n\n    (CD:1)网络入侵：入侵敌方坦克，每回合增加15点入侵点数，在达到该单位当前血量时击毁该单位，可对己方单位使用来消减被入侵点数" +
                        "\n    加成：" + liblab + materiallab;
                    break;
                case UnitName.HACK_LAB:
                    infoLabel.text = "黑客学院    " + "队伍：" + team +
                        "\n\n    天网：信息安全学院对敌方的科技进行窃取。可提高我方每个教学楼每回合科技获取量5点，同时降低敌方每个教学楼每回合科技获取量5点";
                    break;
                case UnitName.MATERIAL_LAB:
                    infoLabel.text = "特殊材料研究院    " + "队伍：" + team +
                        "\n\n    超轻材料学：超轻材料学的应用，可以减少步兵的装备 \n    究极材料学(CD:50): 通过对材料的终极研究，可以为一个单位或基地提供无敌的护盾，持续10回合";
                    break;
                case UnitName.MEAT:
                    infoLabel.text = "小鲜肉    " + "队伍：" + team +
                        "\n\n    这是一个萌萌哒小鲜肉" +
                        "\n    加成：" + liblab + materiallab;
                    break;
                case UnitName.NANO_LAB:
                    infoLabel.text = "纳米科技研究院    " + "队伍：" + team +
                        "\n\n    纳米维修机器人：通过纳米机器人的使用，对坦克飞机提供每回合1.5%的维修效果";
                    break;
                case UnitName.NUKE_TANK:
                    infoLabel.text = "核子坦克    " + "队伍：" + team +
                        "\n\n    (CD:10)贫铀穿甲弹：对单个目标造成伤害300点 \n    (CD:70)战术核武：对3*3格内的任何单位造成伤害450点" +
                        "\n    加成：" + carlab + eleclab + nukelab + uavlab + nanolab;
                    break;
                case UnitName.RADIATION_LAB:
                    infoLabel.text = "辐射系    " + "队伍：" + team +
                        "\n\n    辐射弹头：通过核弹头改进，提升坦克攻击力20%";
                    break;
                case UnitName.SUPERMAN:
                    infoLabel.text = "改造人战士    " + "队伍：" + team +
                        "\n\n    镭射步枪(CD:1)：对单个目标造成伤害15点\n    战术背包(CD: 50)：在20回合内可以较高速飞行(速度12)，获得对空能力及每回合2%的治疗" +
                        "\n    加成：" + liblab + materiallab;
                    break;
                case UnitName.TEACH_BUILDING:
                    infoLabel.text = "教学楼    " + "队伍：" + team +
                        "\n\n    加成：" + hackerlab1 + hackerlab2;
                    break;
                case UnitName.UAV:
                    infoLabel.text = "无人战机    " + "队伍：" + team +
                        "\n\n    机枪扫射(CD:1)：对单个目标造成伤害5点" +
                        "\n    加成：" + uavlab + hanglab + nanolab;
                    break;
                case UnitName.UAV_LAB:
                    infoLabel.text = "无人机系    " + "队伍：" + team +
                        "\n\n    自动化生产：高度自动化的控制，飞机坦克资源减少 10%";
                    break;
                default:
                    infoLabel.text = "    teamstyle18";
                    break;

            }
        }
    }
}
