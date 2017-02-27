using UnityEngine;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using System.Collections;
using System.IO;
using System;

public class NetCom : MonoBehaviour
{
    public static int port;
    public static byte[] addr = { 127, 0, 0, 1 };
    public static string fileName;

    public RoleStateUI player1, player2;
    public GameObject[] unitPrefabs;

    bool pause = false;

    Queue responses = new Queue();

    Thread thread;

    void Start()
    {
        thread = new Thread(Communicate);
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
                    var direction = new Vector3(UnityEngine.Random.value, 0, UnityEngine.Random.value);
                    if (s.unit_type == UnitType.BASE | s.unit_type == UnitType.BUILDING)
                    {
                        unit = Instantiate(unitPrefabs[(int)s.unit_name], s.position.Center(), Quaternion.LookRotation(direction));
                    }
                    else
                    {
                        unit = Instantiate(unitPrefabs[(int)s.unit_name], s.position.Random(), Quaternion.LookRotation(direction));
                    }

                }
                unit.GetComponent<UnitControl>().SetState(s);
            }
            else if(response is Instr)
            {
                var ins = (Instr)response;

                switch (ins.instruction_type)
                {
                    case 1:
                        {
                            GameObject unit = GameObject.Find(ins.the_unit_id.ToString()), target = GameObject.Find(ins.target_id_building_id.ToString());
                            unit.GetComponent<InvasiveControl>().Skill1(ins.target_id_building_id);
                        }
                        break;
                    case 2:
                        {
                            var unit = GameObject.Find(ins.the_unit_id.ToString());
                            unit.GetComponent<InvasiveControl>().Skill2(ins.pos1);
                        }
                        break;
                    case 3:
                        {
                            var unit = GameObject.Find(ins.the_unit_id.ToString());
                            Debug.Log(unit.name + "produced");
                        }
                        break;
                    case 4:
                        {
                            var unit = GameObject.Find(ins.the_unit_id.ToString());
                            Debug.Log(unit.name + "moved");
                        }
                        break;
                    case 5:
                        {
                            GameObject unit = GameObject.Find(ins.the_unit_id.ToString()), target = GameObject.Find(ins.target_id_building_id.ToString());
                            unit.GetComponent<InvasiveControl>().Skill1(ins.target_id_building_id);
                        }
                        break;
                    default:
                        Debug.LogError("Wrong instr");
                        break;
                }
            }
            else if(response is EndState)
            {
                var end = (EndState)response;
                var control = GameObject.Find(end.flag.ToString()).GetComponent<DestroyableControl>();
                control.SetHP(0, control.maxHP);
                if (end.flag == 0)
                {
                    player1.currentHP = 0;
                }
                else
                {
                    player2.currentHP = 0;
                }
            }
        }
    }

    void OnDisable()
    {
        thread.Abort();
    }

    void Communicate()
    {
        Stream stream = StartFormFile();

        try
        {
            while (true)
            {
                int responseType = BitConverter.ToInt32(ReceiveBytes(stream, sizeof(int)), 0);
                switch (responseType)
                {
                    case 12345:
                        Debug.Log(string.Format("Receive {0} unit states", ReceiveBundle<UnitState>(stream)));
                        break;
                    case 123456:
                        Receive<Buff>(stream);
                        Debug.Log("Receive buff");
                        break;
                    case 1234567:
                        Receive<PlayerState>(stream);
                        Debug.Log("Receive player state");
                        break;
                    case 12345678:
                        Debug.Log(string.Format("Receive {0} instrs", ReceiveBundle<Instr>(stream)));
                        break;
                    case 300:
                        lock (responses)
                        {
                            responses.Enqueue(new EndState(0));
                        }
                        Debug.Log("The winner is 0");
                        return;
                    case 301:
                        lock (responses)
                        {
                            responses.Enqueue(new EndState(1));
                        }
                        Debug.Log("The winner is 1");
                        return;
                    default:
                        Debug.LogError("Wrong Package " + responseType);
                        break;
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            stream.Close();
            Debug.Log("Communication Stopped");
        }
    }

    int ReceiveBundle<ResponseType>(Stream stream)
        where ResponseType : new()
    {
        int n = BitConverter.ToInt32(ReceiveBytes(stream, sizeof(int)), 0);
        for (int i = 0; i < n; i++)
        {
            Receive<ResponseType>(stream);
        }

        return n;
    }

    void Receive<ResponseType>(Stream stream)
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

    byte[] ReceiveBytes(Stream stream, int size)
    {
        byte[] part = new byte[size];
        int partSize = 0;
        while (partSize != size)
        {
            partSize += stream.Read(part, partSize, size - partSize);
            if (partSize == 0)
            {
                throw new Exception("Reach the end of stream");
            }
        }
        return part;
    }

    Stream StartFormFile()
    {
        FileStream stream = File.Open(fileName, FileMode.Open);
        Debug.Log("File opened");
        return stream;
    }

    Stream StartFromTCP()
    {
        TcpClient client = new TcpClient(AddressFamily.InterNetwork);
        IPAddress remoteHost = new IPAddress(addr);
        client.Connect(remoteHost, port);
        NetworkStream stream = client.GetStream();
        Debug.Log("Connection established");

        int No = BitConverter.ToInt32(ReceiveBytes(stream, sizeof(int)), 0);
        Debug.Log("Got communication No " + No);

        return stream;
    }
}