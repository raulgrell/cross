using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private DoorInfo[] doors;
    [SerializeField] private WaypointInfo[] waypoints;
    [SerializeField] private SquadController[] squads;
    [SerializeField] private UnitController[] units;

    public void OpenDoor(string doorName)
    {
        foreach (DoorInfo t in doors)
        {
            if (t.name != doorName) continue;
            t.transform.Translate(Vector3.right * 2f);
            t.open = true;
            break;
        }
    }

    public void CloseDoor(string doorName)
    {
        foreach (DoorInfo t in doors)
        {
            if (t.name != doorName) continue;
            t.transform.Translate(Vector3.left * 2f);
            t.open = false;
            break;
        }
    }

    public bool DoorIsOpen(string doorName)
    {
        foreach (DoorInfo t in doors)
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