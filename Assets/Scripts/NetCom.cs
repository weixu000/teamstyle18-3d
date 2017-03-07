using UnityEngine;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class NetCom : MonoBehaviour
{
    public static int port;
    public static byte[] addr = { 127, 0, 0, 1 };
    public static string fileName;

    public RoleStateUI player1, player2;
    public GameObject[] unitPrefabs;
    [HideInInspector]
    public int round = 0;
    public bool humanMode = false;

    bool pause = false;
    Thread thread;
    Queue responsesQueue = new Queue();
    HashSet<int> lastUnitRead = new HashSet<int>();
    bool unitUpdateFinished = true, InstrUpdateFinished = true;

    [HideInInspector]
    public LinkedList<Instr> InstrsToSend;

    [HideInInspector]
    public int AI_id;

    void Awake()
    {
        if (humanMode)
        {
            InstrsToSend = new LinkedList<Instr>();
            thread = new Thread(NetCommunicate);
        }
        else
        {
            thread = new Thread(ReadFile);
        }
        thread.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pause = !pause;
        }
    }

    void OnDestroy()
    {
        thread.Abort();
    }

    void FixedUpdate()
    {
        if (!pause && unitUpdateFinished && InstrUpdateFinished && responsesQueue.Count > 0)
        {
            object response;
            lock (responsesQueue)
            {
                response = responsesQueue.Dequeue();
            }
            if (response is PlayerState)
            {
                PlayerStateUpdate((PlayerState)response);
            }
            else if (response is UnitState[])
            {
                var s = StartCoroutine("UnitStateUpdate", response);
            }
            else if (response is Instr[])
            {
                StartCoroutine("InstrsUpdate", response);
            }
            else if (response is EndState)
            {
                EndStateUpdate((EndState)response);
            }
        }
    }

    void PlayerStateUpdate(PlayerState s)
    {
        player1.People = s.remain_people0;
        player1.Money = s.money0;
        player1.Science = s.tech0;
        player2.People = s.remain_people1;
        player2.Money = s.money1;
        player2.Science = s.tech1;

        round++;
    }

    IEnumerator UnitStateUpdate(UnitState[] response)
    {
        while (!InstrUpdateFinished)
        {
            yield return new WaitForEndOfFrame();
        }
        unitUpdateFinished = false;
        foreach (var id in lastUnitRead)
        {
            var unit = GameObject.Find(id.ToString());
            if (unit && unit.GetComponent<InvasiveControl>())
            {
                do
                {
                    yield return new WaitForFixedUpdate();
                }
                while (unit && unit.GetComponent<InvasiveControl>().walking);
            }
        }

        HashSet<int> temp = new HashSet<int>();
        foreach (var s in response)
        {
            temp.Add(s.unit_id);

            var unit = GameObject.Find(s.unit_id.ToString());

            // 计算双方建筑个数
            if (s.unit_type == (UnitType.BASE | UnitType.BUILDING))
            {
                if (unit != null)
                {
                    if (unit.GetComponent<UnitControl>().flag == 0)
                    {
                        player1.Buildings--;
                    }
                    else if (unit.GetComponent<UnitControl>().flag == 1)
                    {
                        player2.Buildings--;
                    }
                }

                if (s.flag == 0)
                {
                    player1.Buildings++;
                }
                else if (s.flag == 1)
                {
                    player2.Buildings++;
                }
            }
            // 更新双方血条
            if (s.unit_name == UnitName.__BASE)
            {
                if (s.flag == 0)
                {
                    player1.CurrentHP = s.health_now;
                    player1.MaxHP = s.max_health_now;
                }
                else
                {
                    player2.CurrentHP = s.health_now;
                    player2.MaxHP = s.max_health_now;
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

        foreach (var id in lastUnitRead)
        {
            if (!temp.Contains(id))
            {
                var unit = GameObject.Find(id.ToString());
                if (unit)
                {
                    unit.GetComponent<DestroyableControl>().CurrentHP = 0;
                }
            }
        }
        lastUnitRead = temp;

        unitUpdateFinished = true;
        yield break;
    }

    IEnumerator InstrsUpdate(Instr[] response)
    {
        InstrUpdateFinished = false;

        foreach (var ins in response)
        {
            switch (ins.instruction_type)
            {
                case 1:
                    {
                        GameObject unit = GameObject.Find(ins.the_unit_id.ToString()), target = GameObject.Find(ins.target_id_building_id.ToString());
                        if (unit && target)
                        {
                            unit.GetComponent<InvasiveControl>().Skill1(ins.target_id_building_id);
                        }
                    }
                    break;
                case 2:
                    {
                        var unit = GameObject.Find(ins.the_unit_id.ToString());
                        if (unit)
                        {
                            unit.GetComponent<InvasiveControl>().Skill2(ins.pos1);
                        }
                    }
                    break;
                case 3:
                    {
                        var unit = GameObject.Find(ins.the_unit_id.ToString());
                        if (unit)
                        {
                            //Debug.Log(unit.name + "produced");
                        }
                    }
                    break;
                case 4:
                    {
                        var unit = GameObject.Find(ins.the_unit_id.ToString());
                        if (unit)
                        {
                            //Debug.Log(unit.name + "moved");
                        }
                    }
                    break;
                case 5:
                    {
                        GameObject unit = GameObject.Find(ins.the_unit_id.ToString()), target = GameObject.Find(ins.target_id_building_id.ToString());
                        if (unit && target)
                        {
                            unit.GetComponent<InvasiveControl>().Skill1(ins.target_id_building_id);
                        }
                    }
                    break;
                default:
                    Debug.LogError("Wrong instr");
                    break;
            }
        }

        InstrUpdateFinished = true;
        yield break;
    }

    void EndStateUpdate(EndState end)
    {
        if (end.flag == 0)
        {
            player2.CurrentHP = 0;
            GameObject.Find(1.ToString()).GetComponent<DestroyableControl>().CurrentHP = 0;
        }
        else
        {
            player1.CurrentHP = 0;
            GameObject.Find(0.ToString()).GetComponent<DestroyableControl>().CurrentHP = 0;
        }
    }

    void NetCommunicate()
    {
        try
        {
            using (var client = new TcpClient(AddressFamily.InterNetwork))
            {
                client.Connect(new IPAddress(addr), port);
                Debug.Log("Net connection established");
                using (var stream = client.GetStream())
                {
                    AI_id = BitConverter.ToInt32(ReadBytes(stream, sizeof(int)), 0);
                    Debug.Log("Got communication No " + AI_id);

                    while (true)
                    {
                        if (stream.DataAvailable)
                        {
                            Read(stream);
                        }

                        if (InstrsToSend.Count > 0)
                        {
                            SendInstr(stream);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        Debug.Log("Net communication stopped.");
    }

    void ReadFile()
    {
        try
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                Debug.Log("File opened");
                while (stream.Length > 0)
                {
                    Read(stream);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        Debug.Log("File reading complete");
    }

    void Read(Stream stream)
    {
        int responseType = BitConverter.ToInt32(ReadBytes(stream, sizeof(int)), 0);
        switch (responseType)
        {
            case 12345:
                lock (responsesQueue)
                {
                    responsesQueue.Enqueue(ReadBundle<UnitState>(stream));
                }
                //Debug.Log("Receive unit states");
                break;
            case 123456:
                lock (responsesQueue)
                {
                    responsesQueue.Enqueue(ReadResponse<Buff>(stream));
                }
                //Debug.Log("Receive buff");
                break;
            case 1234567:
                lock (responsesQueue)
                {
                    responsesQueue.Enqueue(ReadResponse<PlayerState>(stream));
                }
                //Debug.Log("Receive player state");
                break;
            case 12345678:
                lock (responsesQueue)
                {
                    responsesQueue.Enqueue(ReadBundle<Instr>(stream));
                }
                //Debug.Log("Receive instrs");
                break;
            case 300:
                lock (responsesQueue)
                {
                    responsesQueue.Enqueue(new EndState(0));
                }
                Debug.Log("The winner is 0");
                return;
            case 301:
                lock (responsesQueue)
                {
                    responsesQueue.Enqueue(new EndState(1));
                }
                Debug.Log("The winner is 1");
                return;
            default:
                Debug.LogError("Wrong Package " + responseType);
                return;
        }
    }

    ResponseType[] ReadBundle<ResponseType>(Stream stream)
        where ResponseType : new()
    {
        int n = BitConverter.ToInt32(ReadBytes(stream, sizeof(int)), 0);
        var responses = new ResponseType[n];
        for (int i = 0; i < n; i++)
        {
            responses[i] = ReadResponse<ResponseType>(stream);
        }

        return responses;
    }

    ResponseType ReadResponse<ResponseType>(Stream stream)
        where ResponseType : new()
    {
        var responseSize = Marshal.SizeOf(new ResponseType());
        byte[] partResponse = ReadBytes(stream, responseSize);

        var structPtr = Marshal.AllocHGlobal(responseSize);
        Marshal.Copy(partResponse, 0, structPtr, responseSize);
        ResponseType response = new ResponseType();
        Marshal.PtrToStructure(structPtr, response);
        Marshal.FreeHGlobal(structPtr);

        return response;
    }

    byte[] ReadBytes(Stream stream, int size)
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

    void SendInstr(Stream stream)
    {
        Instr instr;
        lock (InstrsToSend)
        {
            instr = InstrsToSend.First.Value;
            InstrsToSend.RemoveFirst();
        }
        var size = Marshal.SizeOf(instr);

        var structPtr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(instr, structPtr, false);

        var bytes = new byte[size];
        Marshal.Copy(structPtr, bytes, 0, size);
        Marshal.FreeHGlobal(structPtr);

        stream.Write(bytes, 0, size);
    }
}