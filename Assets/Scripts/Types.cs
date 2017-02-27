using System;
using System.Runtime.InteropServices;
using UnityEngine;

public enum UnitType
{
    BASE,
    INFANTRY,
    VEHICLE,
    AIRCRAFT,
    BUILDING
};

public enum BuffType
{
    ATTACK,      //攻击buff
    DEFENSE,     //防御buff
    HEALTH,      //最大生命值buff
    SHOT_RANGE,  //射程buff
    SPEED		 //速度buff
};

public enum UnitName
{
    __BASE,

    MEAT, HACKER, SUPERMAN,

    BATTLE_TANK, BOLT_TANK, NUKE_TANK,

    UAV, EAGLE,

    HACK_LAB, BID_LAB, CAR_LAB, ELEC_LAB, RADIATION_LAB,
    UAV_LAB, AIRCRAFT_LAB, BUILD_LAB, FINANCE_LAB, MATERIAL_LAB,
    NANO_LAB, TEACH_BUILDING, BANK,
    Type_num
};

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Position
{
    public int x;
    public int y;

    public Position(int xx, int yy)
    {
        x = xx;
        y = yy;
    }

    public Vector3 Random(float t = 1)
    {
        float randx = UnityEngine.Random.Range(0, 5 * t);
        float randy = UnityEngine.Random.Range(0, 5 * t);
        return new Vector3(5 * x + randx, 0, 5 * y + randy);
    }

    public Vector3 Center()
    {
        return new Vector3(5 * x + 2.5f, 0, 5 * y + 2.5f);
    }

    public bool Inside(Vector3 vec)
    {
        if ((int)vec.x / 10 == x && (int)vec.y / 10 == y)
            return true;
        else
            return false;
    }

    public static bool operator ==(Position a, Position b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Position a, Position b)
    {
        return !(a == b);
    }
};

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class UnitState
{
    public UnitName unit_name;
    public UnitType unit_type;
    public int attack_mode;            // 攻击模式，例如可对空，可对坦克，可对步兵之类的
    public float attack_now;                   // 当前攻击
    public float defense_now;              // 当前防御
    public int disable_since;          // 被瘫痪的时间点，用于判断瘫痪时间
    public int flag;                   // 所属阵营
    public int hacked_point;               // 被黑的点数
    public int healing_rate;       // 治疗 / 维修速率	
    public float health_now;                   // 当前生命值		
    public int is_disable;     // 是否被瘫痪
    public float max_health_now;               // 当前HP上限
    public float max_speed_now;                // 当前最大速度
    public Position position;              // 单位位置，目测是一个point之类的东西
    public float shot_range_now;               // 当前射程(现阶段貌似没有提升射程的技能，不过先保留)
    public int skill_last_release_time1;// 上次技能1释放时间
    public int skill_last_release_time2;// 上次技能2释放时间
    public int unit_id;                // 单位id
};

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class PlayerState
{
    public int tech0, money0, remain_people0;
    public int tech1, money1, remain_people1;
};

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class Instr
{
    public int instruction_type;
    public int the_unit_id;
    public int target_id_building_id;
    public Position pos1;
    public Position pos2;
};

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class Buff
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2 * 3 * 6)]
    public float[] data;
};

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class EndState
{
    public int flag;

    public EndState(int _flag)
    {
        flag = _flag;
    }
};
