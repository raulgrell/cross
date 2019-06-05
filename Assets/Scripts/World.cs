using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WaypointInfo : MonoBehaviour
{
    
}

public class World : MonoBehaviour
{
    [SerializeField] private Door[] doors;
    [SerializeField] private WaypointInfo[] waypoints;
    [SerializeField] private SquadController[] squads;

    public static World Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenDoor(string doorName)
    {
        foreach (Door t in doors)
        {
            if (t.name != doorName) continue;
            t.transform.Translate(Vector3.right * 2f);
            t.open = true;
            break;
        }
    }

    public void CloseDoor(string doorName)
    {
        foreach (Door t in doors)
        {
            if (t.name != doorName) continue;
            t.transform.Translate(Vector3.left * 2f);
            t.open = false;
            break;
        }
    }

    public bool DoorIsOpen(string doorName)
    {
        foreach (Door t in doors)
        {
            if (t.name == doorName)
                return t.open;
        }

        return false;
    }

    public Transform GetWaypoint(string name)
    {
        foreach (WaypointInfo wp in waypoints)
        {
            if (wp.name == name) return wp.transform;
        }

        return null;
    }
}