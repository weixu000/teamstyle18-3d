using UnityEngine;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using System.Collections;
using System;

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

    public Position(int xx,int yy)
    {
        x = xx;
        y = yy;
    }

    public Vector3 Random()
    {
        float randx = UnityEngine.Random.Range(0, 10);
        float randy = UnityEngine.Random.Range(0, 10);
        return new Vector3(10 * x + randx, 0, 10 * y + randy);
    }

    public Vector3 Center()
    {
        return new Vector3(10 * x + 5f, 0, 10 * y + 5f);
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

public class NetCom : MonoBehaviour
{
    public int port;
    public byte[] addr = { 127, 0, 0, 1 };

    bool pause = false;

    Queue responses = new Queue();

    public RoleStateUI player1, player2;
    public GameObject[] unitPrefabs;

    void Start()
    {
        Thread thread = new Thread(Communicate);
        thread.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pause = !pause;
        }
    }

    void LateUpdate()
    {
        if (pause) return;

        if (responses.Count > 0)
        {
            object response;
            lock (responses)
            {
                response = responses.Dequeue();
            }
            if (response is PlayerState)
            {
                PlayerState s = (PlayerState)response;
                player1.currentPeople = s.remain_people0;
                player1.currentMoney = s.money0;
                player1.currentScience = s.tech0;
                player2.currentPeople = s.remain_people1;
                player2.currentMoney = s.money1;
                player2.currentScience = s.tech1;
            }
            else if (response is UnitState)
            {
                UnitState s = (UnitState)response;
                GameObject unit = GameObject.Find(s.unit_id.ToString());

                // 计算双方建筑个数
                if (s.unit_type == (UnitType.BASE | UnitType.BUILDING))
                {
                    if (unit != null)
                    {
                        if (unit.GetComponent<UnitControl>().flag == 0)
                        {
                            player1.currentBuildings--;
                        }
                        else if (unit.GetComponent<UnitControl>().flag  == 1)
                        {
                            player2.currentBuildings--;
                        }
                    }

                    if (s.flag == 0)
                    {
                        player1.currentBuildings++;
                    }
                    else if (s.flag == 1)
                    {
                        player2.currentBuildings++;
                    }
                }

                // 更新双方血条
                if (s.unit_name == UnitName.__BASE)
                {
                    if (s.flag == 0)
                    {
                        player1.currentHP = s.health_now;
                        player1.maxHP = s.max_health_now;
                    }
                    else
                    {
                        player2.currentHP = s.health_now;
                        player2.maxHP = s.max_health_now;
                    }
                }

                if (unit == null)
                {
                    if (s.unit_type == UnitType.BASE || s.unit_type == UnitType.BUILDING)
                        // 建筑居于格子正中
                        unit = Instantiate(unitPrefabs[(int)s.unit_name], s.position.Center(), unitPrefabs[(int)s.unit_name].transform.rotation);
                    else
                    {
                        // 随机一个方向、位置
                        Vector2 rand = UnityEngine.Random.insideUnitCircle;
                        Vector3 direction = new Vector3(rand.x, 0, rand.y);
                        unit = Instantiate(unitPrefabs[(int)s.unit_name], s.position.Random(), Quaternion.LookRotation(direction));
                    }

                }
                unit.GetComponent<UnitControl>().SetState(s);
            }
            else if(response is Instr)
            {
                Instr ins = (Instr)response;
                GameObject unit = GameObject.Find(ins.the_unit_id.ToString());
                if(unit != null && (ins.instruction_type == 1 || ins.instruction_type == 2))
                {
                    unit.GetComponent<InvasiveControl>().Fire(ins.target_id_building_id);
                }

            }
        }
    }

    void Communicate()
    {
        TcpClient client = new TcpClient(AddressFamily.InterNetwork);
        IPAddress remoteHost = new IPAddress(addr);
        client.Connect(remoteHost, port);
        NetworkStream stream = client.GetStream();
        Debug.Log("Connection established");

        //int No = BitConverter.ToInt32(ReceiveBytes(stream, sizeof(int)), 0);
        //Debug.Log("Got communication No " + No);

        try
        {
            while (true)
            {
                int responseType = BitConverter.ToInt32(ReceiveBytes(stream, sizeof(int)), 0);
                switch (responseType)
                {
                    case 0:
                        ReceiveBundle<UnitState>(stream);
                        Debug.Log("Receive unit states");
                        break;
                    case 1:
                        Receive<Buff>(stream);
                        Debug.Log(string.Format("Receive buff"));
                        break;
                    case 2:
                        Receive<PlayerState>(stream);
                        Debug.Log(string.Format("Receive player states"));
                        break;
                    case 4:
                        Debug.Log(string.Format("Receive {0} instructs", ReceiveBundle<Instr>(stream)));
                        break;
                    case 300:
                        Debug.Log("The winner is 0");
                        break;
                    case 301:
                        Debug.Log("The winner is 1");
                        break;
                    default:
                        Debug.LogError("Wrong Package " + responseType);
                        break;
                }
            }
        }
        finally
        {
            stream.Close();
            client.Close();
            Debug.Log("Communication Stopped");
        }
    }

    int ReceiveBundle<ResponseType>(NetworkStream stream)
        where ResponseType : new()
    {
        int n = BitConverter.ToInt32(ReceiveBytes(stream, sizeof(int)), 0);
        for (int i = 0; i < n; i++)
        {
            Receive<ResponseType>(stream);
        }

        return n;
    }

    void Receive<ResponseType>(NetworkStream stream)
        where ResponseType : new()
    {
        int responseSize = Marshal.SizeOf(new ResponseType());
        byte[] partResponse = ReceiveBytes(stream, responseSize);

        IntPtr structPtr = Marshal.AllocHGlobal(responseSize);
        Marshal.Copy(partResponse, 0, structPtr, responseSize);
        ResponseType response = new ResponseType();
        Marshal.PtrToStructure(structPtr, response);
        Marshal.FreeHGlobal(structPtr);
        lock (responses)
        {
            responses.Enqueue(response);
        }
    }

    byte[] ReceiveBytes(NetworkStream stream, int size)
    {
        byte[] part = new byte[size];
        int partSize = 0;
        while (partSize != size)
        {
            partSize += stream.Read(part, partSize, size - partSize);
        }
        return part;
    }
}